using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TEWorkFlow.Application.Service.WorkFlow;
using NSH.Authorization.Domain;
using TEWorkFlow.Web.Client.Resources;
using NSH.Authorization.Service;
using TEWorkFlow.Web.Client.Models;
using TEWorkFlow.Domain.WorkFlow;
using TEWorkFlow.Web.Client.Common;

using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Web.Client.Controllers
{
    [UserAuthorize]
    public class InvoiceController : Controller
    {
        public IInvoicePointService InvoicePointService { get; set; }

        public IInvoiceService InvoiceService { get; set; }

        public ICheckPointService CheckPointService { get; set; }

        public IWorkStepService WorkStepService { get; set; }

        public IUserService UserService { get; set; }

        public ActionResult List()
        {
            var list = CheckPointService.GetAll().Where(p => p.IsKey);
            ViewBag.Cols = list.Select(o => o.ItemName.Replace(" ", ""));
            return View();
        }

        public ActionResult Process()
        {
            var list = CheckPointService.GetAll().Where(p => p.IsKey);
            ViewBag.Cols = list.Select(o => o.ItemName.Replace(" ", ""));
            var cur_user = UserService.GetById(Convert.ToInt32(Session[AuthorizeSettings.SessionUserID]));
            var points = CheckPointService.GetListByUser(cur_user);

            ViewBag.Cols2 = points.Select(o => o.ItemName.Replace(" ", ""));


            return View();
        }

        public ActionResult Query()
        {
            var user = UserService.GetById(Convert.ToInt32(Session[AuthorizeSettings.SessionUserID]));

            int page = Convert.ToInt32(Request["page"]);
            int rows = Convert.ToInt32(Request["rows"]);

            if (page <= 0)
            {
                page = 1;
            }
            if (rows <= 0)
            {
                rows = 10;
            }

            var all = InvoiceService.GetAll();
            var list = all.Skip(rows * (page - 1)).Take(rows);

            if (user.Role.Id != 4)
            {
                list = list.Where(p => p.WorkStep.Users.Contains(user)).ToList();
            }

            List<JObject> jo = new List<JObject>();
            foreach (var l in list)
            {
                var j = new JObject();
                j["id"] = l.Id;
                j["step"] = l.WorkStep.Name;

                foreach (var p in l.Points)
                {
                    j[p.CheckPoint.ItemName.Replace(" ", "")] = p.Text;
                }
                jo.Add(j);
            }
            return new JsonNetResult(new { total = all.Count, rows = jo });
        }

        public ActionResult GetQueryExcel()
        {
            var user = UserService.GetById(Convert.ToInt32(Session[AuthorizeSettings.SessionUserID]));

            var list = InvoiceService.GetAll().ToList();

            if (user.Role.Id != 4)
            {
                list = list.Where(p => p.WorkStep.Users.Contains(user)).ToList();
            }


            HSSFWorkbook hssfworkbook = new HSSFWorkbook();

            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Workflow";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Workflow";
            hssfworkbook.SummaryInformation = si;

            ICellStyle style1 = hssfworkbook.CreateCellStyle();
            style1.WrapText = true;

            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");

            IRow row = sheet1.CreateRow(0);

            var f = list.First();
            //row.CreateCell(0).SetCellValue("id");
            row.CreateCell(0).SetCellValue("step");

            int celIndex = 1;
            foreach (var p in f.Points)
            {
                row.CreateCell(celIndex).SetCellValue(p.CheckPoint.ItemName);
                celIndex++;
            }

            int rowIndex = 1;
            foreach (var l in list)
            {
                row = sheet1.CreateRow(rowIndex);
                //row.CreateCell(0).SetCellValue(l.Id);
                row.CreateCell(0).SetCellValue(l.WorkStep.Name);

                celIndex = 1;
                foreach (var p in l.Points)
                {
                    row.CreateCell(celIndex).SetCellValue(p.Text);
                    celIndex++;
                }

                rowIndex++;
            }

            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);

            string filename = string.Format("Datas.xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();

            return File(file.GetBuffer(), "application/vnd.ms-excel");
        }

        public ActionResult ReportList()
        {
            var list = CheckPointService.GetAll();
            ViewBag.Cols = list.Select(o => o.ItemName.Replace(" ", ""));
            ViewBag.Fields = list.Select(o => new JsonNameValue(o.ItemName, o.ItemName, o.CheckPointType.ToString())).ToList();
            ViewBag.Fields.Insert(0, new JsonNameValue(Resources.Global.PleaseSelect, "", ""));
            return View();
        }

        public ActionResult ReportQuery(InvoiceSearchDto searchDto)
        {
            searchDto.field = searchDto.field == Resources.Global.PleaseSelect ? "" : searchDto.field;

            DataGridPage dgPage = new DataGridPage();
            var user = UserService.GetById(Convert.ToInt32(Session[AuthorizeSettings.SessionUserID]));
            var list = InvoiceService.GetListByDto(searchDto, dgPage, dgPage);

            List<JObject> jo = new List<JObject>();
            foreach (var l in list)
            {
                var j = new JObject();
                j["id"] = l.Id;
                j["step"] = l.WorkStep.Name;

                foreach (var p in l.Points)
                {
                    j[p.CheckPoint.ItemName.Replace(" ", "")] = p.Text;
                }
                jo.Add(j);
            }
            return new JsonNetResult(new { total = dgPage.RecordCount, rows = jo });
        }

        public ActionResult ReportListExport(InvoiceSearchDto searchDto)
        {
            var invoices = InvoiceService.GetListByDto(searchDto);

            string fileEndName = Session[AuthorizeSettings.SessionUserName].ToString();
            if (String.IsNullOrEmpty(fileEndName)) { fileEndName = new Random().Next(10000, 99999).ToString(); }

            MemoryStream file = WriteInvoicesFile(fileEndName, invoices);

            return File(file.GetBuffer(), "application/vnd.ms-excel");
        }

        public ActionResult Edit(int id)
        {
            return View(id);
        }

        public ActionResult EditInvoice(NameValueCollection nv)
        {
            NameValueCollection items = new NameValueCollection();

            int id = Convert.ToInt32(Request.Form["id"]);

            try
            {
                for (int i = 0; i < Request.Form.AllKeys.Count(); i++)
                {
                    if (Request.Form.AllKeys[i] == "id")
                    {
                        continue;
                    }
                    var invoicePoint = InvoicePointService.GetByInvoiceIdAndItemName(id, Request.Form.AllKeys[i]);
                    invoicePoint.Text = Request.Form[i];

                    InvoicePointService.Update(invoicePoint);
                }
                return Json(new { Success = true, Message = Global.DeleteSuccess }, "text/html", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(string ids)
        {
            foreach (string id in ids.Split(','))
            {
                int i_id = Convert.ToInt32(id);
                try
                {
                    InvoiceService.Delete(InvoiceService.GetById(i_id));
                }
                catch
                {
                }
            }

            return Json(new { Success = true, Message = Global.DeleteSuccess }, "text/html", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInvoicePoints(int id)
        {
            var user = UserService.GetById(Convert.ToInt32(Session[AuthorizeSettings.SessionUserID]));
            var list = InvoicePointService.GetAll().ToList();
            list = list.Where(p => p.Invoice.Id == id).ToList();
            if (user.Role.Id != 4)
            {
                list = list.Where(p => p.CheckPoint.CheckUser.Id == user.Id).ToList();
            }

            var result = list.Select(p => new
            {
                id = p.Id,
                itemName = p.CheckPoint.ItemName,
                text = p.Text,
                ischecked = p.IsChecked,
                remark = p.Remark
            });
            return Json(result, "text/plain", JsonRequestBehavior.AllowGet);
            //list.FirstOrDefault().CheckPoint.ItemName
        }

        public ActionResult SaveInvoiceCheck(InvoicePoint entity)
        {
            var ic = InvoicePointService.GetById(entity.Id);
            ic.IsChecked = entity.IsChecked;
            ic.Remark = entity.Remark;
            InvoicePointService.Update(ic);

            InvoiceService.CheckFinished(ic.Invoice.Id);

            return Json(new { Success = true, Message = Global.SaveSuccess }, "text/html", JsonRequestBehavior.AllowGet);
        }



        #region 从上传的Excel中读取数据并且保存结果
        /// <summary>
        /// 从上传的Excel中读取数据并且保存结果
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ActionResult ImportInvoiceResult(HttpPostedFileBase file)
        {
            if (file == null)
            {
                file = Request.Files[0];
            }
            if (file == null || file.ContentLength == 0)
            {
                return Json(new { Success = true, Message = Global.SaveSuccess }, "text/html", JsonRequestBehavior.AllowGet);
            }

            DataTable dt = ExcelToDataTable(file);

            var user = UserService.GetById(Convert.ToInt32(Request.Form["uid"]));

            var step = WorkStepService.GetAll().Where(p => p.Users.Contains(user)).FirstOrDefault();
            var points = CheckPointService.GetAll().Where(p => p.Step == step);
            var invoices = InvoiceService.GetAll().Where(p => p.WorkStep == step).ToList();

            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["Number"]);
                var invoice = invoices.Where(p => p.Id == id).FirstOrDefault();
                var ps = invoice.Points.Where(p => points.Contains(p.CheckPoint));

                if (row["Check"].ToString().ToLower() == "y")
                {
                    foreach (var p in ps)
                    {
                        p.IsChecked = true;
                        InvoicePointService.Update(p);
                    }

                    InvoiceService.CheckFinished(id);
                }
            }

            return Json(new { Success = true, Message = Global.SaveSuccess }, "text/html", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportInvoice(HttpPostedFileBase file)
        {
            JsonMessage jMsg = new JsonMessage();

            if (file == null)
            {
                file = Request.Files[0];
            }
            if (file == null || file.ContentLength == 0)
            {
                return Json(jMsg.AsSuccess(Global.SaveSuccess), JsonMessage.ContentType);
            }

            DataTable dt = ExcelToDataTable(file);

            var pubpoints = CheckPointService.GetAll().Where(p => p.IsKey == true);
            var cur_user = UserService.GetById(Convert.ToInt32(Session[AuthorizeSettings.SessionUserID]));
            var points = WorkStepService.GetAll().Where(p => p.Users.Contains(cur_user)).FirstOrDefault().CheckPoints;
            var allowCheckPoints = CheckPointService.GetListByUser(cur_user);

            foreach (var pp in pubpoints)
            {
                if (!allowCheckPoints.Any(o => o.Id == pp.Id))
                {
                    allowCheckPoints.Add(pp);
                }
            }
            //System.Predicate<string> t = new Predicate<string>(true "xxx" );

            //check column
            List<string> noExistColumns = new List<string>();
            foreach (var cp in allowCheckPoints)
            {
                if (!dt.Columns.Contains(cp.ItemName))
                {
                    noExistColumns.Add(cp.ItemName);
                }
            }

            if (noExistColumns.Count > 0)
            {
                return Json(
                    jMsg.AsFail(
                    String.Format("The file format wrong,item \"{0}\" does not exist. ", String.Join(",", noExistColumns.ToArray()))
                    )
                    , JsonMessage.ContentType);
            }

            //create or update invoice
            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, string> keyValues = new Dictionary<string, string>();

                foreach (var pub in pubpoints)
                {
                    keyValues.Add(pub.ItemName, row[pub.ItemName].ToString());
                }
                var invoices = InvoiceService.GetListByKeyword(keyValues);
                Invoice iv = new Invoice();
                if (invoices.Count == 0)
                {
                    iv = new Invoice();
                    iv.Points = new List<InvoicePoint>();
                    iv.WorkStep = WorkStepService.GetAll().FirstOrDefault();

                    iv.Id = InvoiceService.Create(iv);
                }
                else
                {
                    iv = invoices.FirstOrDefault();
                }

                foreach (var point in points)
                {
                    InvoicePoint ip = iv.Points.FirstOrDefault(o => o.CheckPoint.ItemName == point.ItemName);
                    if (ip == null)
                    {
                        ip = new InvoicePoint();
                        iv.Points.Add(ip);
                    }
                    ip.CheckPoint = point;
                    ip.Invoice = iv;
                    //ip.IsChecked = row["Check"].ToString().ToLower() == "y";
                    ip.Remark = "";
                    if (ip.CheckPoint.CheckPointType == CheckPointType.Date)
                    {
                        try
                        {
                            ip.Text = Convert.ToDateTime(row[point.ItemName]).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            ip.Text = "";
                        }
                    }
                    else
                    {
                        ip.Text = row[point.ItemName].ToString();
                    }
                    ip.UpdateUser = cur_user;

                }
                InvoiceService.Update(iv);
                InvoiceService.CheckFinished(iv.Id);

            }
            return Json(jMsg.AsSuccess(Global.SaveSuccess), JsonMessage.ContentType);
        }

        public ActionResult ImportCompleteInvoices(HttpPostedFileBase file)
        {
            if (file == null)
            {
                file = Request.Files[0];
            }
            if (file == null || file.ContentLength == 0)
            {
                return Json(new { Success = true, Message = Global.SaveSuccess }, "text/html", JsonRequestBehavior.AllowGet);
            }

            DataTable dt = ExcelToDataTable(file);

            var all_points = CheckPointService.GetAll();

            foreach (DataRow row in dt.Rows)
            {
                Invoice iv = new Invoice();
                iv.WorkStep = new WorkStep { Id = 1 };
                iv.Points = new List<InvoicePoint>();

                foreach (var point in all_points)
                {
                    InvoicePoint p = new InvoicePoint();
                    p.CheckPoint = point;
                    p.Invoice = iv;
                    p.IsChecked = false;
                    p.Remark = "";
                    p.Text = Convert.ToString(row[point.ItemName]);

                    iv.Points.Add(p);
                }
                InvoiceService.Create(iv);

            }
            return Json(new { Success = true, Message = Global.SaveSuccess }, "text/html", JsonRequestBehavior.AllowGet);
        }

 

        private DataTable ExcelToDataTable(HttpPostedFileBase file)
        {
            string ExtName = System.IO.Path.GetExtension(file.FileName).Replace(".", "").ToLower();
            string fileName = Server.MapPath(string.Format("~/tmp/{0}", file.FileName));


            file.SaveAs(fileName);

            DataTable dt = new DataTable();
            switch (ExtName)
            {
                case "xls":
                    dt = ReadFromXls(fileName);
                    break;
                case "xlsx":
                    dt = ReadFromXlsx(fileName);
                    break;
            }
            return dt;
        }
        #endregion

        #region 从Excel2003读取数据
        public DataTable ReadFromXls(string filePath)
        {
            Stream s = new FileStream(filePath, FileMode.Open);
            HSSFWorkbook workbook = new HSSFWorkbook(s);
            ISheet sheet = workbook.GetSheet("Sheet1");

            System.Collections.IEnumerator rows;
            try
            {
                rows = sheet.GetRowEnumerator();
            }
            catch
            {
                throw new Exception("Can not get sheet names“sheet1”");
            }

            DataTable dt = new DataTable();
            rows.MoveNext();
            IRow firstrow = (HSSFRow)rows.Current;
            for (int j = 0; j < firstrow.Cells.Count; j++)
            {
                //dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
                dt.Columns.Add(firstrow.Cells[j].ToString());
            }
            //rows.Reset();

            while (rows.MoveNext())
            {
                IRow row = (HSSFRow)rows.Current;
                DataRow dr = dt.NewRow();

                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);


                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        try
                        {
                            dr[i] = cell.RichStringCellValue.ToString();
                        }
                        catch
                        {
                            dr[i] = cell.ToString();
                        }
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
        #endregion

        #region 从Excel2007/2010读取数据
        public DataTable ReadFromXlsx(string filePath)
        {
            Stream s = new FileStream(filePath, FileMode.Open);
            IWorkbook workbook = new XSSFWorkbook(s);
            ISheet sheet = workbook.GetSheet("Sheet1");


            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            DataTable dt = new DataTable();
            rows.MoveNext();
            IRow firstrow = (XSSFRow)rows.Current;

            for (int j = 0; j < firstrow.Cells.Count; j++)
            {
                //dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
                dt.Columns.Add(firstrow.Cells[j].ToString());
            }

            //rows.Reset();
            while (rows.MoveNext())
            {
                IRow row = (XSSFRow)rows.Current;
                DataRow dr = dt.NewRow();

                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);


                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
        #endregion

        #region 输出当前用户的Excel
        /// <summary>
        /// 输出当前用户的Excel
        /// </summary>
        /// <returns></returns>
        public ActionResult GetExcelResult()
        {
            var user = UserService.GetById(Convert.ToInt32(Session[AuthorizeSettings.SessionUserID]));
            var invoices = InvoiceService.GetListByUser(user, onlyUsersPoints: true);

            MemoryStream file = //WriteInvoicesFile(user.UserName, invoices);
                WriteCheckTemplateFile(user.UserName);

            return File(file.GetBuffer(), "application/vnd.ms-excel");

        }


        private MemoryStream WriteCheckTemplateFile(string fileEndName, bool headerOnly = false)
        {
            var user = UserService.GetById(Convert.ToInt32(Session[AuthorizeSettings.SessionUserID]));
            var points = WorkStepService.GetAll().Where(p => p.Users.Contains(user)).FirstOrDefault().CheckPoints;
            var pubpoints = CheckPointService.GetAll().Where(p => p.IsKey == true);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook();

            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Workflow";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Workflow";
            hssfworkbook.SummaryInformation = si;

            ICellStyle style1 = hssfworkbook.CreateCellStyle();
            style1.WrapText = true;

            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
            int rowNumber = 0;
            IRow row = sheet1.CreateRow(0);

            int i = 0;
            foreach (var pub in pubpoints)
            {

                row.CreateCell(i).SetCellValue(pub.ItemName);
                i++;
            }
            foreach (var point in points)
            {
                if (pubpoints.Where(p => p.Id == point.Id).Count() > 0)
                {
                    continue;
                }
                row.CreateCell(i).SetCellValue(point.ItemName);
                i++;
            }
            //row.CreateCell(i).SetCellValue("Check");
            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);


            string filename = string.Format("InvoiceCheck_{0}.xls", fileEndName);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();
            return file;
        }
        private MemoryStream WriteInvoicesFile(string fileEndName, IList<Invoice> invoices, bool headerOnly = false)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();

            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Workflow";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Workflow";
            hssfworkbook.SummaryInformation = si;

            ICellStyle style1 = hssfworkbook.CreateCellStyle();
            style1.WrapText = true;

            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
            int rowNumber = 0;
            IRow row;
            for (int i = 0; i < invoices.Count; i++)
            {
                if (i == 0)
                {
                    int colNum = 0;
                    row = sheet1.CreateRow(rowNumber++);
                    row.CreateCell(colNum++).SetCellValue("Number");

                    for (int j = 0; j < invoices[i].Points.Count(); j++)
                    {
                        var header = row.CreateCell(colNum++);
                        header.SetCellValue(invoices[i].Points[j].CheckPoint.ItemName);
                        header.CellStyle = style1;

                    }
                    row.CreateCell(colNum++).SetCellValue("Check");
                }
                if (!headerOnly)
                {
                    row = sheet1.CreateRow(rowNumber++);
                    row.CreateCell(0).SetCellValue(invoices[i].Id);

                    for (int j = 0; j < invoices[i].Points.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(invoices[i].Points[j].Text);
                    }
                }
            }

            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);


            string filename = string.Format("InvoiceCheck_{0}.xls", fileEndName);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();
            return file;
        }
        #endregion

        public ActionResult GetInvoiceTemplate()
        {
            var points = CheckPointService.GetAll();

            HSSFWorkbook hssfworkbook = new HSSFWorkbook();

            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;

            ICellStyle style1 = hssfworkbook.CreateCellStyle();
            style1.WrapText = true;

            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");

            IRow row = sheet1.CreateRow(0);
            for (int i = 0; i < points.Count; i++)
            {
                row.CreateCell(i).SetCellValue(points[i].ItemName);
            }

            string filename = string.Format("InvoiceTemplate.xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();

            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);


            return File(file.GetBuffer(), "application/vnd.ms-excel");
        }

        public ActionResult Add()
        {
            var chks = CheckPointService.GetAll();
            ViewBag.Chks = chks;
            return View();
        }
    }
}
