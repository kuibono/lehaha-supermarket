using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop.Excel;
using Excel2010 = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


namespace NSH.VSTO
{
    /// <summary>
    /// Summary description for MyExcel
    /// </summary>
    public class MyExcel2010 : IDisposable
    {
        //TlbImp Excel9.olb Excel.dll

        private Excel2010.Application excelApplication = null;
        private Excel2010.Workbook excelWorkbook = null;
        private Excel2010.Range excelRange = null;
        private Excel2010.Worksheet excelWorksheet = null;
        private Excel2010.Chart excelChart = null;

        private string filename = "";

        /// <summary>
        /// saved file name
        /// </summary>
        public string FileName
        {
            set { this.filename = value; }
            get { return this.filename; }
        }

        public MyExcel2010()
        {

        }

        public int SheetCount
        {
            get
            {
                return this.excelWorkbook.Sheets.Count;
            }
        }
        public object GetValue(string cell1, string cell2)
        {
            this.SetRange(cell1, cell2);
            return this.excelRange.Value2;
        }

        public object GetValue(string column, int row)
        {
            this.SetRange(column, row);
            return this.excelRange.Value2;
        }

        public void OpenExcel(string fileFullName, params string[] sheetName)
        {
            OpenExcelFromApplication(new Excel2010.ApplicationClass(), fileFullName, sheetName);
        }

        /// <summary>
        /// init Excel object
        /// </summary>
        /// <param name="sheetName">sheet names</param>
        private void OpenExcelFromApplication(Excel2010.Application excelApplication, string fileFullName, params string[] sheetName)
        {
            this.excelApplication = excelApplication;

            if (this.excelApplication == null)
            {
                throw new Exception("EXCEL couldn't be started!");
            }

            this.excelApplication.Visible = false;
            this.excelApplication.DisplayAlerts = false;
            this.excelApplication.AskToUpdateLinks = false;
            this.excelApplication.AlertBeforeOverwriting = false;

            this.excelWorkbook = this.excelApplication.Workbooks.Open(fileFullName);

            //this.excelWorkbook.Author = "";


            if (sheetName != null && sheetName.Length > 0)
            {
                int i = 1;
                for (i = 1; i <= sheetName.Length; i++)
                {
                    if (i <= this.excelWorkbook.Worksheets.Count)
                    {
                        ((Excel2010.Worksheet)(this.excelWorkbook.Worksheets[i])).Name = sheetName[i - 1];
                    }
                    else
                    {
                        this.AddWorksheet(sheetName[i - 1]);
                    }
                }
            }
            this.excelWorksheet = (Excel2010.Worksheet)this.excelWorkbook.Sheets.get_Item(1);
        }

        public void SetPrintPage(int pageSize)
        {
            this.excelWorksheet.PageSetup.PaperSize = (Excel2010.XlPaperSize)Enum.Parse(typeof(Excel2010.XlPaperSize), pageSize.ToString());
        }

        public void SetPrintOrientation(int Orientation)
        {
            this.excelWorksheet.PageSetup.Orientation = (Excel2010.XlPageOrientation)Enum.Parse(typeof(Excel2010.XlPageOrientation), Orientation.ToString());
        }
        public void setPrintFitToPagesWide(int wide)
        {
            this.excelWorksheet.PageSetup.FitToPagesWide = wide;

        }
        public void setPrintFitToPagesTall(int tall)
        {
            this.excelWorksheet.PageSetup.FitToPagesTall = tall;

        }

        public void SetPrintMarginPoint(int leftMargin, int rightMargin, int topMargin, int bottomMargin, int headerMargin, int footerMargin)
        {
            this.excelWorksheet.PageSetup.LeftMargin = leftMargin;
            this.excelWorksheet.PageSetup.RightMargin = rightMargin;
            this.excelWorksheet.PageSetup.TopMargin = topMargin;
            this.excelWorksheet.PageSetup.BottomMargin = bottomMargin;
            this.excelWorksheet.PageSetup.HeaderMargin = headerMargin;
            this.excelWorksheet.PageSetup.FooterMargin = footerMargin;

        }


        public void SetZoom(object zoom)
        {
            this.excelWorksheet.PageSetup.Zoom = zoom;
        }

        /// <summary>
        /// $A$1:$CD$58
        /// </summary>
        /// <param name="cellSelect"></param>
        public void SetPrintArea(string cellSelect)
        {
            this.excelWorksheet.PageSetup.PrintArea = cellSelect;
        }

        public void SetVisible(bool visible)
        {
            this.excelApplication.Visible = visible;
        }

        public void InsertRow(int RowNumber)
        {
            this.SetRange(RowNumber, 1, RowNumber, 1);
            this.excelRange.EntireRow.Insert(Excel2010.XlInsertShiftDirection.xlShiftDown, Type.Missing);
        }

        public void InsertRow(int rowNumber, int rowCount, int? templateRowNumber = null)
        {
            if (rowCount <= 0) { return; }

            string rowIndex = String.Format("{0}:{1}", rowNumber + 1, rowNumber + rowCount);
            Excel2010.Range newRange = this.excelWorksheet.Rows[rowIndex] as Excel2010.Range;
            newRange.Insert(Excel2010.XlInsertShiftDirection.xlShiftDown);
            if (templateRowNumber.HasValue)
            {
                Excel2010.Range templateRange = this.excelWorksheet.Rows[templateRowNumber.Value] as Excel2010.Range;
                templateRange.Copy();
                newRange = this.excelWorksheet.Rows[rowIndex] as Excel2010.Range;
                newRange.PasteSpecial(
                    Paste: XlPasteType.xlPasteFormats,
                    Operation: XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                    SkipBlanks:false, Transpose:false);
                this.excelApplication.CutCopyMode = XlCutCopyMode.xlCopy;
                newRange.FillDown();
            }
        }

        public void CopyRange()
        {
            this.excelRange.Copy();
        }

        public void InsertCopyRange()
        {
            this.excelRange.Insert(Shift: Excel2010.XlInsertShiftDirection.xlShiftDown);
        }

        public void ClearCopy()
        {
            this.excelApplication.CutCopyMode = XlCutCopyMode.xlCopy;
            this.excelRange.FillDown();
        }

        public void DeleteRow(int RowNumber)
        {
            this.SetRange(RowNumber, 1, RowNumber, 1);
            this.excelRange.EntireRow.Delete(Excel2010.XlDeleteShiftDirection.xlShiftUp);
        }

        public void DeleteRange()
        {
            this.excelRange.Delete(XlDeleteShiftDirection.xlShiftUp);
        }

        /// <summary>
        /// set the current active sheet 
        /// </summary>
        /// <param name="worksheetIndex">sheet index ,from 1 start</param>
        public void SetActiveWorksheet(int worksheetIndex)
        {
            this.excelWorksheet = (Excel2010.Worksheet)this.excelWorkbook.Sheets.get_Item(worksheetIndex);
            this.excelWorksheet.Select();
        }

        /// <summary>
        /// add a net sheet with name
        /// </summary>
        /// <param name="strName">sheet name</param>
        public void AddWorksheet(string strName)
        {
            if (this.excelWorkbook.Sheets.Count == 0)
            {
                ((Excel2010.Worksheet)this.excelWorkbook.Sheets.Add()).Name = strName;
            }
            else
            {
                ((Excel2010.Worksheet)this.excelWorkbook.Sheets.Add(Type.Missing, this.excelWorkbook.Sheets[this.excelWorkbook.Sheets.Count], Type.Missing, Type.Missing)).Name = strName;
            }
        }

        ///   <summary>
        ///   复制工作表
        ///   </summary>
        ///   <param   name= "src_sheet "> </param>
        ///   <param   name= "obj_book "> </param>
        ///   <param   name= "obj_pos "> 目标位置,   最小为1,   最大为obj_book工作薄数+1 </param>
        ///   <param   name= "new_sheet_name "> </param>
        public void CopyWorksheet(string srcFile, int srcSheetIndex, int newSheetIndex, string newSheetName)
        {
            MyExcel2010 srcExcel = new MyExcel2010();
            srcExcel.OpenExcelFromApplication(this.excelApplication, srcFile);
            srcExcel.SetActiveWorksheet(srcSheetIndex);
            Excel2010.Worksheet srcSheet = srcExcel.excelWorksheet;

            if (newSheetIndex >= 0 && newSheetIndex <= this.excelWorkbook.Worksheets.Count)
            {
                srcSheet.Copy(this.excelWorkbook.Worksheets[newSheetIndex], Missing.Value);
            }
            else
            {
                newSheetIndex = this.excelWorkbook.Worksheets.Count;
                srcSheet.Copy(Missing.Value, this.excelWorkbook.Worksheets[newSheetIndex]);
                newSheetIndex += 1;
            }

            ArrayList arr = new ArrayList();
            for (int i = 1; i <= this.excelWorkbook.Worksheets.Count; i++)
            {
                arr.Add((this.excelWorkbook.Worksheets[i] as Excel2010.Worksheet).Name.ToUpper());
            }

            if (newSheetName != " " && !arr.Contains(newSheetName.ToUpper()))
            {
                ((Excel2010.Worksheet)this.excelWorkbook.Worksheets[newSheetIndex]).Name = newSheetName;
            }
        }



        /// <summary>
        /// set range
        /// </summary>
        /// <param name="cell1">cell1 EG. A3</param>
        /// <param name="cell2">cell2 EG. b3</param>
        public void SetRange(string cell1, string cell2)
        {
            this.excelRange = this.excelWorksheet.get_Range(cell1, cell2);

        }

        public void SetRange(string cellName)
        {
            this.excelRange = this.excelWorksheet.get_Range(cellName);
        }

        public void SetRange(string column, int Row)
        {
            string cellRange = string.Format("{0}{1}", column, Row);
            this.excelRange = this.excelWorksheet.get_Range(cellRange, cellRange);
        }

        public void SetRange(int startrow, int startcolumn, int endrow, int endcolumn)
        {
            this.excelRange = this.excelWorksheet.get_Range(this.excelWorksheet.Cells[startrow, startcolumn], this.excelWorksheet.Cells[endrow, endcolumn]);
        }

        public void SetRangeValue(string column, int Row, object value)
        {
            this.excelWorksheet.Range[String.Format("{0}{1}", column, Row)].Value = value;
        }

        public void SetSelection()
        {
            this.excelRange.Select();
        }

        public int GetRangeRowIndex()
        {
            return this.excelRange.EntireRow.Row;
        }

        public int GetRangeColumnIndex()
        {
            return this.excelRange.Column;
        }

        public void Print()
        {
            this.excelWorksheet.PrintOut();
        }

        /// <summary>
        /// set formula of R1C1 format
        /// </summary>
        /// <param name="formula">EG.=R[-3]C+R[-2]C+R[-1]C</param>
        public void SetR1C1(string formula)
        {
            this.excelRange.FormulaR1C1 = formula;
        }
        /// <summary>
        /// set range value 
        /// </summary>
        /// <param name="strArray">string array</param>
        public void SetValue2(string[] strArray)
        {
            this.excelRange.Value2 = strArray;
        }
        public void SetValue2(string[,] strArray)
        {
            this.excelRange.Value2 = strArray;
        }
        public void SetValue2(int[,] strArray)
        {
            this.excelRange.Value2 = strArray;
        }
        public void SetValue2(object[,] strArray)
        {
            this.excelRange.Value2 = strArray;
        }

        /// <summary>
        /// set range value
        /// </summary>
        /// <param name="strArray"></param>
        public void SetValue2(decimal[] strArray)
        {
            this.excelRange.Value2 = strArray;
        }
        public void SetValue2(object[] strArray)
        {
            this.excelRange.Value2 = strArray;
        }

        /// <summary>
        /// set range value
        /// </summary>
        /// <param name="strArray"></param>
        public void SetValue2(int[] strArray)
        {
            this.excelRange.Value2 = strArray;
        }

        public void CopyRow(int row)
        {
            ((Excel2010.Range)this.excelWorksheet.Rows[string.Format("{0}:{0}", row), Type.Missing]).Copy(Type.Missing);
        }

        public void Paste(int row)
        {
            ((Excel2010.Range)this.excelWorksheet.Rows[string.Format("{0}:{0}", row), Type.Missing]).Select();

            this.excelWorksheet.Paste(Type.Missing, Type.Missing);
        }

        //public void InsertPicture(int sheetIndex, string fileName, float left, float top, float width, float height)
        //{
        //    this.SetActiveWorksheet(sheetIndex);
        //    this.excelWorksheet.Shapes.AddPicture(fileName, MsoTriState.msoFalse,
        //        Microsoft.Office.Core.MsoTriState.msoTrue, left, top, width, height);
        //}

        /// <summary>
        /// set range value
        /// </summary>
        /// <param name="strArray"></param>
        public void SetValue2(DateTime[] strArray)
        {
            this.excelRange.Value2 = strArray;
        }

        /// <summary>
        /// set range width
        /// </summary>
        /// <param name="width">inch</param>
        public void SetWidth(decimal width)
        {
            this.excelRange.Columns.ColumnWidth = width;
        }

        /// <summary>
        /// set range hieght
        /// </summary>
        /// <param name="height"> inch</param>
        public void SetHeight(decimal height)
        {
            this.excelRange.Rows.RowHeight = height;
        }

        /// <summary>
        /// set single cell value
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(string value)
        {
            this.excelRange.Value2 = value;
        }
        /// <summary>
        /// set single cell value
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(decimal value)
        {
            this.excelRange.Value2 = value;
        }
        public void SetValue(object value)
        {
            this.excelRange.Value2 = value;
        }

        /// <summary>
        /// set range formula
        /// </summary>
        /// <param name="formula"></param>
        public void SetFormula(string formula)
        {
            this.excelRange.Formula = formula;
        }

        public void SetBackColor(int colorIndex)
        {
            this.excelRange.Interior.ColorIndex = colorIndex;
        }


        /// <summary>
        /// set number display format
        /// </summary>
        /// <param name="numberFormat">EG. 0.00%</param>
        public void SetNumberFormat(string numberFormat)
        {
            this.excelRange.NumberFormatLocal = numberFormat;
        }

        /// <summary>
        /// merge the cell
        /// </summary>
        public void Merge()
        {
            this.excelRange.MergeCells = true;
        }

        public void Merge2()
        {
            this.excelRange.Merge();
        }
        public void SetWrapText(bool bolWrapText)
        {
            this.excelRange.WrapText = bolWrapText;
        }

        /// <summary>
        /// set font style
        /// </summary>
        /// <param name="bolBold">Bold</param>
        /// <param name="bolItalic">Italic</param>
        /// <param name="colorIndex">colorIndex</param>
        /// <param name="intFontSize">intFontSize</param>
        public void SetFont(bool bolBold, bool bolItalic, int colorIndex, int intFontSize)
        {
            this.excelRange.Font.Bold = bolBold;
            this.excelRange.Font.Italic = bolItalic;
            this.excelRange.Font.ColorIndex = colorIndex;
            this.excelRange.Font.Size = intFontSize;
        }

        /// <summary>
        /// save the document
        /// </summary>
        public void SaveAs()
        {
            try
            {
                this.excelWorkbook.SaveAs(this.filename);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SetCenterFooter(string content)
        {
            this.excelWorksheet.PageSetup.CenterFooter = content;
        }

        public void Save()
        {
            try
            {
                this.excelWorkbook.Save();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        /// <summary>
        /// split window
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public void SetSplit(int column, int row)
        {
            this.excelApplication.ActiveWindow.SplitColumn = column;
            this.excelApplication.ActiveWindow.SplitRow = row;
            this.excelApplication.ActiveWindow.FreezePanes = true;
        }

        public void SetAlign(int alignIndex)
        {
            this.excelRange.VerticalAlignment = Excel2010.XlVAlign.xlVAlignCenter;
            if (alignIndex == 0)
            {
                this.excelRange.HorizontalAlignment = Excel2010.XlHAlign.xlHAlignLeft;
            }
            else if (alignIndex == 1)
            {
                this.excelRange.HorizontalAlignment = Excel2010.XlHAlign.xlHAlignCenter;
            }
            else if (alignIndex == 2)
            {
                this.excelRange.HorizontalAlignment = Excel2010.XlHAlign.xlHAlignRight;
            }
            else
            {
                this.excelRange.HorizontalAlignment = Excel2010.XlHAlign.xlHAlignCenter;
            }
        }

        /// <summary>
        /// delete object
        /// </summary>
        public void Destroy()
        {
            if (this.excelWorkbook != null)
            {
                //this.excelWorkbook.Close( false , Type.Missing , Type.Missing );
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);

            }
            if (this.excelApplication != null)
            {

                // this.excelApplication.Workbooks.Close( );
                //this.excelApplication.Quit( );
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApplication);
            }
            if (this.excelRange != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelRange);
            }
            if (this.excelWorksheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorksheet);
            }
            if (this.excelChart != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelChart);
            }

            this.excelApplication = null;
            this.excelWorkbook = null;
            this.excelRange = null;
            this.excelWorksheet = null;
            this.excelChart = null;

            GC.Collect();
            GC.Collect();
        }

        /// <summary>
        /// 判断文件是否已经打开
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool AlreadyOpen(string filename)
        {
            if (!File.Exists(filename))
            {
                return false;
            }
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Truncate))
                {
                }
                return false;
            }
            catch (IOException)
            {
                return true;
                //throw;
            }
        }

        /// <summary>
        /// 用于excel表格中列号字母转成列索引，从1对应A开始
        /// </summary>
        /// <param name="column">列号</param>
        /// <returns>列索引</returns>
        private static int ColumnToIndex(string column)
        {
            if (!Regex.IsMatch(column.ToUpper(), @"[A-Z]+"))
            {
                throw new Exception("Invalid parameter");
            }
            int index = 0;
            char[] chars = column.ToUpper().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                index += ((int)chars[i] - (int)'A' + 1) * (int)Math.Pow(26, chars.Length - i - 1);
            }
            return index;
        }

        /// <summary>
        /// 用于将excel表格中列索引转成列号字母，从A对应1开始
        /// </summary>
        /// <param name="index">列索引</param>
        /// <returns>列号</returns>
        private static string IndexToColumn(int index)
        {
            if (index <= 0)
            {
                throw new Exception("Invalid parameter");
            }
            index--;
            string column = string.Empty;
            do
            {
                if (column.Length > 0)
                {
                    index--;
                }
                column = ((char)(index % 26 + (int)'A')).ToString() + column;
                index = (int)((index - index % 26) / 26);
            } while (index > 0);

            return column;
        }

        #region KillApplication
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        /// <summary>
        /// 使用Process关闭Excel进程
        /// </summary>
        /// <param name="excelApplication"></param>
        public void Kill()
        {
            try
            {
                if (excelApplication != null)
                {
                    //得到这个句柄，具体作用是得到这块内存入口
                    IntPtr t = new IntPtr(excelApplication.Hwnd);
                    int k = 0;
                    //得到本进程唯一标志k
                    GetWindowThreadProcessId(t, out k);
                    //得到对进程k的引用
                    System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);
                    //关闭进程k
                    p.Kill();
                }
            }
            catch
            {
            }
        }
        #endregion

        #region IDisposable Members

        public string ActiveSheetName()
        {
            return excelWorksheet.Name;
        }

        public void Dispose()
        {
            if (this.excelWorkbook != null)
            {
                this.excelWorkbook.Close(false, Type.Missing, Type.Missing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);

            }
            if (this.excelApplication != null)
            {

                this.excelApplication.Workbooks.Close();
                this.excelApplication.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApplication);
            }
            if (this.excelRange != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelRange);
            }
            if (this.excelWorksheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorksheet);
            }
            if (this.excelChart != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelChart);
            }

            this.excelApplication = null;
            this.excelWorkbook = null;
            this.excelRange = null;
            this.excelWorksheet = null;
            this.excelChart = null;

            GC.Collect();
            GC.Collect();
        }

        #endregion
    }
}
