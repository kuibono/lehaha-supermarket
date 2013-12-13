using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Category;
using TEWorkFlow.Application.Service.Sys;
using NSH.VSTO;
using TEWorkFlow.Domain.Sys;
using System.Data;
using System.Configuration;
using TeWorkFlow.Application.DataEntity;

namespace TEWorkFlow.Application.Service.Archives
{
    public class GoodsArchivesService : IGoodsArchivesService
    {

        public IRepositoryGUID<GoodsArchives> EntityRepository { get; set; }
        public IRepositoryGUID<FbPaGoodsGb> GbRepository { get; set; }
        public IRepositoryGUID<FbPaGoodsGm> GmRepository { get; set; }
        public IRepositoryGUID<FbPaGoodsGs> GsRepository { get; set; }
        public IRepositoryGUID<FbPaGoodsGl> GlRepository { get; set; }
        public IRepositoryGUID<FbSupplierArchives> SupplierRepository { get; set; }
        public IFbPaBaseSetService FbPaBaseSetService { get; set; }
        public IRepositoryGUID<FbGoodsArchivesBar> GoodsArchivesBarRepository { get; set; }
        public IRepositoryGUID<FbGoodsArchivesSupplier> GoodsArchivesSupplierRepository { get; set; }
        //public IRepositoryGUID<TfDataDownload> DataDownloadRepository { get; set; }
        public ITfDataDownloadService TfDataDownloadService { get; set; }

        public IFbGoodsArchivesBarService FbGoodsArchivesBarService { get; set; }

        public IFbGoodsArchivesSupplierService FbGoodsArchivesSupplierService { get; set; }

        [Transaction]
        private string GetSupName(string[] supCodes)
        {
            var allArchives = SupplierRepository.LinqQuery.AsCache("_AllSuppliers").ToList();
            StringBuilder result = new StringBuilder();
            foreach (string supCode in supCodes)
            {
                result.Append(allArchives.Where(p => p.Id == supCode).FirstOrDefault().SupName);
                result.Append(",");
            }
            return result.ToString().TrimEnd(',');
        }


        private fb_goods_archives ConvertToDataBaseEntity(GoodsArchives entity)
        {
            fb_goods_archives e = new fb_goods_archives();
            e.article_number = entity.ArticleNumber;
            e.assessor = entity.Assessor;
            e.avg_cost = entity.AvgCost;
            e.backup_code = entity.BackupCode;
            e.check_mode = entity.CheckMode;
            e.check_unit_code = entity.CheckUnitCode;
            e.create_date = entity.CreateDate;
            e.examine_date = entity.ExamineDate;
            e.gb_code = entity.GbCode;
            e.gl_code = entity.GlCode;
            e.gm_code = entity.GmCode;
            e.goods_bar_code = entity.GoodsBarCode;
            e.goods_code = entity.Id;
            e.goods_name = entity.GoodsName;
            e.goods_state = entity.GoodsState;
            e.goods_sub_code = entity.GoodsSubCode;
            e.goods_sub_name = entity.GoodsSubName;
            e.goods_type = entity.GoodsType;
            e.gross_rate = entity.GrossRate;
            e.gs_code = entity.GsCode;
            e.if_examine = entity.IfExamine;
            e.if_new = entity.IfNew;
            e.input_tax = entity.InputTax;
            e.nontax_avg_cost = entity.NontaxAvgCost;
            e.nontax_purchase_price = entity.NontaxPurchasePrice;
            e.offer_min = entity.OfferMin;
            e.offer_mode = entity.OfferMode;
            e.op_code = entity.OpCode;
            e.@operator = entity.Operator;
            e.operator_date = entity.OperatorDate;
            e.output_tax = entity.OutputTax;
            e.pack_coef = entity.PackCoef;
            e.pack_unit_code = entity.PackUnitCode;
            e.pool_rate = entity.PoolRate;
            e.price_history = entity.PriceHistory;
            e.producing_area = entity.ProducingArea;
            e.propose_price = entity.ProposePrice;
            e.purchase_price = entity.PurchasePrice;
            e.push_rate = entity.PushRate;
            e.py_code = entity.PyCode;
            e.remark = entity.Remark;
            e.sale_price = entity.SalePrice;
            e.shelf_life = entity.ShelfLife;
            e.specification = entity.Specification;
            e.stock_lower_limit = entity.StockLowerLimit;
            e.stock_upper_limit = entity.StockUpperLimit;
            e.sup_code = entity.SupCode;
            e.trade_price = entity.TradePrice;
            e.under_counter_code = entity.UnderCounterCode;
            e.under_floor_code = entity.UnderFloorCode;
            e.vip_price = entity.VipPrice;
            e.qty_type = entity.QtyType;
            return e;
        }

        [Transaction]
        public Result Create(GoodsArchives entity)
        {
            Result r = new Result();
            r.IsSuccess = true;
            SuperMarketEntities ent = new SuperMarketEntities();


            fb_goods_archives e = ConvertToDataBaseEntity(entity);
            e.operator_date = DateTime.Now;
            //e.goods_sub_name = GetSupName(e.sup_code.Split(','));

            if (string.IsNullOrEmpty(e.goods_bar_code))
            {
                e.goods_bar_code = GenerateBarCode();
            }
            else
            {
                //验证条码是否否存在
                if (ent.fb_goods_archives.Any(p => p.goods_bar_code == e.goods_bar_code))
                {
                    r.IsSuccess = false;
                    r.Message = "输入的条形码不唯一，请重新输入";
                    return r;
                }
            }
            ent.AddTofb_goods_archives(e);
            ent.SaveChanges();

            string id = e.goods_code;
            if (e.if_examine == "1")
            {
                var allBranchs = ent.bs_branch_archives.ToList();
                foreach (var br in allBranchs)
                {
                    tf_data_download d = new tf_data_download();
                    d.download_id = Guid.NewGuid().ToString();
                    d.download_branchcode = br.b_code;
                    d.download_keyvalue = e.goods_code;
                    d.download_order = 0;
                    d.download_tablename = "fb_goods_archives";
                    ent.AddTotf_data_download(d);
                }

            }
            fb_goods_archives_bar b = new fb_goods_archives_bar();
            b.goods_bar_code = e.goods_bar_code;
            b.goods_bar_name = e.goods_sub_name;
            b.goods_code = e.goods_code;
            b.if_examine = "1";
            b.if_main_bar = "1";
            b.sale_price = e.propose_price;

            b.qty_type = e.qty_type;
            b.pack_coef = 1;
            b.price_type = "1";
            b.push_rate = 0;
            b.trade_price =0;
            b.vip_price =0;
            b.pack_unit_code = "01";

            if (ent.fb_goods_archives_bar.Any(p => p.goods_bar_code == e.goods_bar_code) == false)
            {
                ent.AddTofb_goods_archives_bar(b);
            }

            //处理传过来的多个SupCode
            SaveGoodsSuppliers(e, e.sup_code.Split(','), ent);

            SaveGoodsBarCode(e, ent);
            e.if_examine = "0";
            ent.SaveChanges();
            if(entity.IfExamine=="1")
            {
                e.if_examine = "1";
                ent.SaveChanges();
            }
           
            r.Str = id;
            return r;
        }

        protected void SaveGoodsBarCode(fb_goods_archives e,SuperMarketEntities ent)
        {
            var codes = from l in ent.fb_goods_bar_code where l.goods_code == e.goods_code select l;
            foreach (fb_goods_bar_code code in codes)
            {
                ent.DeleteObject(code);
            }
            //ent.SaveChanges();

            fb_goods_bar_code b=new fb_goods_bar_code();
            //b.sale_price = e.sale_price;
            b.gb_code = e.gb_code;
            b.gl_code = e.gl_code;
            b.gm_code = e.gm_code;
            b.goods_bar_code = e.goods_bar_code;
            b.goods_code = e.goods_code;
            b.goods_name = e.goods_name;
            b.goods_state = e.goods_state;
            b.goods_sub_code = e.goods_sub_code;
            b.goods_sub_name = e.goods_sub_name;
            b.gs_code = e.gs_code;
            b.modify_date = DateTime.Now;
            b.op_code = e.op_code;
            b.owner_type = "1";
            b.pack_coef = e.pack_coef;
            b.pack_unit_code = e.pack_unit_code;
            b.pool_rate = e.pool_rate;
            b.price_type = "1";
            b.py_code = e.py_code;
            b.qty_type = e.qty_type;
            b.sale_price = e.sale_price;
            b.sup_code = e.sup_code;
            b.trade_price = e.trade_price;
            b.vip_price = e.vip_price;

            ent.AddTofb_goods_bar_code(b);
            
        }

        [Transaction]
        public GoodsArchives GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<GoodsArchives> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public Result Update(GoodsArchives entity)
        {


            //Cache.Clear(entity.SupCode);
            Result r = new Result();
            r.IsSuccess = true;
            SuperMarketEntities ent = new SuperMarketEntities();



            fb_goods_archives e = ConvertToDataBaseEntity(entity);
            

            var oldEntity = ent.fb_goods_archives.First(p => p.goods_code == e.goods_code);
            e.EntityKey = oldEntity.EntityKey;
           
            e.operator_date = DateTime.Now;
            //e.goo = GetSupName(e.sup_code.Split(','));

            if (string.IsNullOrEmpty(e.goods_bar_code))
            {
                e.goods_bar_code = GenerateBarCode();
            }
            else
            {
                if (e.goods_bar_code != oldEntity.goods_bar_code)
                {
                    //验证条码是否否存在
                    if (ent.fb_goods_archives.Any(p => p.goods_bar_code == e.goods_bar_code && p.goods_code != e.goods_code))
                    {
                        r.IsSuccess = false;
                        r.Message = "输入的条形码不唯一，请重新输入";
                        return r;
                    }
                }
            }
            //ent.AddTofb_goods_archives(e);
            //ent.SaveChanges();

            //string id = e.goods_code;
            if (e.if_examine == "1")
            {
                var allBranchs = ent.bs_branch_archives.ToList();
                foreach (var br in allBranchs)
                {
                    tf_data_download d = new tf_data_download();
                    d.download_id = Guid.NewGuid().ToString();
                    d.download_branchcode = br.b_code;
                    d.download_keyvalue = e.goods_code;
                    d.download_order = 0;
                    d.download_tablename = "fb_goods_archives";
                    ent.AddTotf_data_download(d);
                }

            }
            
            #region 条码发生变化后，更新条码表
            if (oldEntity.goods_bar_code != e.goods_bar_code)
            {
                var oldBarCodes = ent.fb_goods_archives_bar.Where(p => p.goods_bar_code == oldEntity.goods_bar_code); //GoodsArchivesBarRepository.Get(oldEntity.GoodsBarCode);
                if (oldBarCodes.Count()>0)
                {
                    foreach (var fbGoodsArchivesBar in oldBarCodes)
                    {
                        ent.DeleteObject(fbGoodsArchivesBar);
                    }
                    
                }
                var newBarCodes= ent.fb_goods_archives_bar.Where(p => p.goods_bar_code == e.goods_bar_code);
                if (newBarCodes.Count()>0)
                {
                    foreach (var fbGoodsArchivesBar in newBarCodes)
                    {
                        ent.DeleteObject(fbGoodsArchivesBar);
                    }
                    
                }

                fb_goods_archives_bar bar = new fb_goods_archives_bar();
                bar.goods_bar_code = e.goods_bar_code;
                bar.goods_code = e.goods_code;
                bar.goods_bar_name = e.goods_sub_name;
                bar.if_examine = "1";
                bar.if_main_bar = "1";
                bar.sale_price = e.propose_price;

                bar.qty_type = e.qty_type;
                bar.pack_coef = 1;
                bar.price_type = "1";
                bar.push_rate = 0;
                bar.trade_price = 0;
                bar.vip_price = 0;
                bar.pack_unit_code = "01";

                ent.AddTofb_goods_archives_bar(bar);
            }
            #endregion

            if (e.purchase_price != oldEntity.purchase_price)
            {
                if (e.price_history == null)
                    e.price_history = "";
                if (string.IsNullOrEmpty(e.price_history) == false)
                {
                    e.price_history += ",";
                }
                e.price_history += string.Format("{0}|{1}", oldEntity.price_history, oldEntity.operator_date);
            }

            //处理传过来的多个SupCode
            
            SaveGoodsSuppliers(e, e.sup_code.Split(','), ent);

            SaveGoodsBarCode(e, ent);
            
            //oldEntity = e;
            oldEntity.article_number = e.article_number;
            oldEntity.assessor = e.assessor;
            oldEntity.avg_cost = e.avg_cost;
            oldEntity.backup_code = e.backup_code;
            oldEntity.check_mode = e.check_mode;
            oldEntity.check_unit_code = e.check_unit_code;
            oldEntity.create_date = e.create_date;
            oldEntity.examine_date = e.examine_date;
            oldEntity.gb_code = e.gb_code;
            oldEntity.gl_code = e.gl_code;
            oldEntity.gm_code = e.gm_code;
            oldEntity.goods_bar_code = e.goods_bar_code;
            //oldEntity.goods_code = e.goods_code;
            oldEntity.goods_name = e.goods_name;
            oldEntity.goods_state = e.goods_state  ;
            oldEntity.goods_sub_code = e.goods_sub_code;
            oldEntity.goods_sub_name = e.goods_sub_name;
            oldEntity.goods_type = e.goods_type;
            oldEntity.gross_rate = e.gross_rate;
            oldEntity.gs_code = e.gs_code;
            oldEntity.if_examine = e.if_examine;
            oldEntity.if_new = e.if_new;
            oldEntity.input_tax = e.input_tax;
            oldEntity.nontax_avg_cost = e.nontax_avg_cost;
            oldEntity.nontax_purchase_price = e.nontax_purchase_price;
            oldEntity.offer_min = e.offer_min;
            oldEntity.offer_mode = e.offer_mode;
            oldEntity.op_code = e.op_code;
            oldEntity.@operator = e.@operator;
            oldEntity.operator_date = e.operator_date;
            oldEntity.output_tax = e.output_tax;
            oldEntity.pack_coef = e.pack_coef;
            oldEntity.pack_unit_code = e.pack_unit_code;
            oldEntity.pool_rate = e.pool_rate;
            oldEntity.price_history = e.price_history;
            oldEntity.producing_area = e.producing_area;
            oldEntity.propose_price = e.propose_price;
            oldEntity.purchase_price = e.purchase_price;
            oldEntity.push_rate = e.push_rate;
            oldEntity.py_code = e.py_code;
            oldEntity.remark = e.remark;
            oldEntity.sale_price = e.sale_price;
            oldEntity.shelf_life = e.shelf_life;
            oldEntity.specification = e.specification;
            oldEntity.stock_lower_limit = e.stock_lower_limit;
            oldEntity.stock_upper_limit = e.stock_upper_limit;
            oldEntity.sup_code = e.sup_code;
            oldEntity.trade_price = e.trade_price;
            oldEntity.under_counter_code = e.under_counter_code;
            oldEntity.under_floor_code = e.under_floor_code;
            oldEntity.vip_price = e.vip_price;

            ent.SaveChanges();
            r.Str = e.goods_code;
            return r;



            //entity.SupName = GetSupName(entity.SupCode.Split(','));

            //Cache.Clear(entity.SupCode);
            //entity.OperatorDate = DateTime.Now;
            //Result r = new Result();
            //r.IsSuccess = true;
            ////验证条码是否否存在
            //if (EntityRepository.LinqQuery.Any(p => p.GoodsBarCode == entity.GoodsBarCode && p.Id != entity.Id))
            //{
            //    r.IsSuccess = false;
            //    r.Message = "输入的条形码不唯一，请重新输入";
            //    return r;
            //}
            //var oldEntity = EntityRepository.Get(entity.Id);

            //if (entity.GbCode.IsNullOrEmpty())
            //{
            //    entity.GbCode = "";
            //}
            //if (entity.GmCode.IsNullOrEmpty())
            //{
            //    entity.GmCode = "";
            //}
            //if (entity.GsCode.IsNullOrEmpty())
            //{
            //    entity.GsCode = "";
            //}
            //if (entity.GlCode.IsNullOrEmpty())
            //{
            //    entity.GlCode = "";
            //}

            //#region 条码发生变化后，更新条码表
            //if (oldEntity.GoodsBarCode != entity.GoodsBarCode)
            //{
            //    var oldBarCode = GoodsArchivesBarRepository.Get(oldEntity.GoodsBarCode);
            //    if (oldBarCode != null)
            //    {
            //        GoodsArchivesBarRepository.Delete(oldBarCode);
            //    }
            //    FbGoodsArchivesBar bar = new FbGoodsArchivesBar();
            //    bar.Id = entity.GoodsBarCode;
            //    bar.GoodsCode = entity.Id;
            //    bar.GoodsBarName = entity.GoodsSubName;
            //    bar.IfMainBar = "1";
            //    bar.IfExamine = "1";
            //    bar.SalePrice = entity.SalePrice;
            //    //GoodsArchivesBarRepository.Save(bar);
            //    FbGoodsArchivesBarService.Update(bar);
            //}
            //#endregion

            ////处理传过来的多个SupCode
            //SaveGoodsSuppliers(entity, entity.SupCode.Split(','));

            ////#region
            ////if (oldEntity.SupCode != entity.SupCode)
            ////{
            ////    FbGoodsArchivesSupplier sup = new FbGoodsArchivesSupplier();
            ////    sup.GoodsCode = entity.Id;
            ////    sup.Id = Guid.NewGuid().ToString();
            ////    sup.IfExamine = "1";
            ////    sup.IfMainSupplier = "1";
            ////    sup.PyCode = entity.PyCode;
            ////    sup.SupCode = entity.SupCode;
            ////    sup.SupName = SupplierRepository.Get(entity.SupCode).SupName;
            ////    //GoodsArchivesSupplierRepository.Save(sup);
            ////    FbGoodsArchivesSupplierService.Update(sup);
            ////}
            ////#endregion

            ////if (entity.ProposePrice != oldEntity.ProposePrice)
            ////{
            //entity.SalePrice = entity.ProposePrice;
            ////}

            //if (entity.PurchasePrice != oldEntity.PurchasePrice)
            //{
            //    if (entity.PriceHistory == null)
            //        entity.PriceHistory = "";
            //    if (string.IsNullOrEmpty(entity.PriceHistory) == false)
            //    {
            //        entity.PriceHistory += ",";
            //    }
            //    entity.PriceHistory += string.Format("{0}|{1}", oldEntity.PurchasePrice, oldEntity.OperatorDate);
            //}
            //entity.OperatorDate = DateTime.Now;
            //EntityRepository.Update(entity);
            //if (entity.IfExamine == "1")
            //{
            //    TfDataDownloadService.AddDownload("fb_goods_archives", entity.Id);
            //}
            //return r;
            //DataDownloadRepository.Save(new TfDataDownload() { Id = Guid.NewGuid().ToString(), DownloadKeyvalue = entity.Id, DownloadTablename = "fb_goods_archives" });
        }

        private void SaveGoodsSuppliers(fb_goods_archives good, string[] supCodes,SuperMarketEntities ent)
        {
            List<fb_goods_archives_supplier> sups = ent.fb_goods_archives_supplier.Where(p => p.goods_code == good.goods_code).ToList();

            var allSupplier = ent.fb_supplier_archives.Select(p => new StringKeyValuePair { Key = p.sup_code, Value = p.sup_name }).AsCache("_AllSupplierNameValuePair").ToList();

            foreach (var sup in sups)
            {
                if (supCodes.Any(p => p == sup.sup_code) == false)
                {
                    foreach (var deleteItem in sups.Where(p => supCodes.Contains(p.sup_code)==false).ToList())
                    {
                        deleteItem.goods_code = "0";
                        //ent.DeleteObject(deleteItem);
                    }
                }
            }
            foreach (string supCode in supCodes)
            {
                if (sups.Any(p=>p.sup_code==supCode) == false)//数据库中不存在传过来的供货商编码
                {
                    //增加
                    fb_goods_archives_supplier p = new fb_goods_archives_supplier();
                    p.goods_code = good.goods_code;
                    p.if_examine = "1";
                    p.if_main_supplier = "1";
                    p.input_tax = good.input_tax;
                    p.nontax_purchase_price = good.nontax_purchase_price;
                    p.offer_min = good.offer_min;
                    p.offer_mode = good.offer_mode;
                    p.op_code = good.op_code;
                    p.pool_rate = good.pool_rate;
                    p.purchase_price = good.purchase_price;
                    p.py_code = good.py_code;
                    p.sup_code = supCode;
                    p.sup_name = allSupplier.First(prp => prp.Key == supCode).Value; ;
                    p.sys_guid = _string.GenerateStringID();
                    ent.AddTofb_goods_archives_supplier(p);
                }

                //if (sups.Any(p => p.sup_code == supCode) == false)//数据库中存在没有传过来的供货商
                //{
                //    //删除
                //    var needDeletedItems = sups.Where(p => p.sup_code == supCode).ToList();
                //    foreach (var needDeleteItem in needDeletedItems)
                //    {
                //        ent.DeleteObject(needDeleteItem);
                //    }
                //}
            }
        }

        [Transaction]
        public void Delete(GoodsArchives entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<GoodsArchives> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }

        private void FillClassName(IList<GoodsArchives> entitys)
        {
            var gbs = GbRepository.LinqQuery.ToList();
            var gms = GmRepository.LinqQuery.ToList();
            var gss = GsRepository.LinqQuery.ToList();
            var gls = GlRepository.LinqQuery.ToList();

            foreach (var entity in entitys)
            {
                var gb = from l in gbs where l.Id == entity.GbCode select l;
                if (gb.Count() > 0)
                {
                    entity.GbName = gb.First().GbName;
                }
                var gm = from l in gms where l.Id == entity.GmCode select l;
                if (gm.Count() > 0)
                {
                    entity.GmName = gm.First().GmName;
                }
                var gs = from l in gss where l.Id == entity.GsCode select l;
                if (gs.Count() > 0)
                {
                    entity.GsName = gs.First().GsName;
                }

                var gl = from l in gls where l.Id == entity.GlCode select l;
                if (gl.Count() > 0)
                {
                    entity.GlName = gl.First().GlName;
                }

            }
        }

        private void FillSupInfo(IList<GoodsArchives> goods)
        {
            var sups = SupplierRepository.LinqQuery.ToList();
            for (int i = 0; i < goods.Count; i++)
            {
                var sup = sups.Where(p => p.Id == goods[i].SupCode);
                if (sup.Count() == 0)
                {
                    goods[i].SupName = "";
                    goods[i].SupTel = "";
                }
                else
                {
                    goods[i].SupName = sup.First().SupName;
                    goods[i].SupTel = sup.First().ContactPhone;
                }
            }

            foreach (var good in goods)
            {
                good.SupName = GetSupName(good.SupCode.Split(','));
            }
        }

        [Transaction]
        public SearchResult<GoodsArchives> Search(SearchDtoBase<GoodsArchives> c)
        {
            var q = EntityRepository.LinqQuery;

            if (string.IsNullOrEmpty(c.key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(c.key)
                    || l.GoodsBarCode.Contains(c.key)
                    || l.GoodsSubCode.Contains(c.key)
                    || l.GbCode.Contains(c.key)
                    || l.GmCode.Contains(c.key)
                    || l.GsCode.Contains(c.key)
                    || l.GlCode.Contains(c.key)
                    || l.GoodsType.Contains(c.key)
                    || l.CheckMode.Contains(c.key)
                    || l.SupCode.Contains(c.key)
                    || l.OpCode.Contains(c.key)
                    || l.GoodsName.Contains(c.key)
                    || l.GoodsSubName.Contains(c.key)
                    || l.PyCode.Contains(c.key)
                    || l.ProducingArea.Contains(c.key)
                    || l.ArticleNumber.Contains(c.key)
                    || l.Specification.Contains(c.key)
                    || l.ShelfLife.Contains(c.key)
                    || l.PackUnitCode.Contains(c.key)
                    || l.OfferMode.Contains(c.key)
                    || l.UnderFloorCode.Contains(c.key)
                    || l.UnderCounterCode.Contains(c.key)
                    || l.Operator.Contains(c.key)
                    || l.Assessor.Contains(c.key)
                    select l;
            }
            if (c.entity != null)
            {
                if (string.IsNullOrEmpty(c.entity.GoodsBarCode) == false)
                {
                    q = q.Where(p => p.GoodsBarCode.Contains(c.entity.GoodsBarCode));
                }
                if (string.IsNullOrEmpty(c.entity.GoodsSubCode) == false)
                {
                    q = q.Where(p => p.GoodsSubCode.Contains(c.entity.GoodsSubCode));
                }
                if (string.IsNullOrEmpty(c.entity.GbCode) == false)
                {
                    q = q.Where(p => p.GbCode.Contains(c.entity.GbCode));
                }
                if (string.IsNullOrEmpty(c.entity.GmCode) == false)
                {
                    q = q.Where(p => p.GmCode.Contains(c.entity.GmCode));
                }
                if (string.IsNullOrEmpty(c.entity.GsCode) == false)
                {
                    q = q.Where(p => p.GsCode.Contains(c.entity.GsCode));
                }
                if (string.IsNullOrEmpty(c.entity.GlCode) == false)
                {
                    q = q.Where(p => p.GlCode.Contains(c.entity.GlCode));
                }
                //if (string.IsNullOrEmpty(c.entity.GoodsType) == false)
                //{
                //    q = q.Where(p => p.GoodsType.Contains(c.entity.GoodsType));
                //}
                //if (string.IsNullOrEmpty(c.entity.CheckMode) == false)
                //{
                //    q = q.Where(p => p.CheckMode.Contains(c.entity.CheckMode));
                //}
                if (string.IsNullOrEmpty(c.entity.SupCode) == false)
                {
                    var goodsSupplierArvhices = FbGoodsArchivesSupplierService.GetBySupCode(c.entity.SupCode).Select(p => p.GoodsCode).ToList(); ;
                    q = q.Where(p => goodsSupplierArvhices.Contains(p.Id));
                }
                //if (string.IsNullOrEmpty(c.entity.OpCode) == false)
                //{
                //    q = q.Where(p => p.OpCode.Contains(c.entity.OpCode));
                //}
                if (c.entity.PoolRate > 0)
                {
                    q = q.Where(p => p.PoolRate == c.entity.PoolRate);
                }

                if (string.IsNullOrEmpty(c.entity.GoodsName) == false)
                {
                    q = q.Where(p => p.GoodsName.Contains(c.entity.GoodsName));
                }
                if (string.IsNullOrEmpty(c.entity.GoodsSubName) == false)
                {
                    q = q.Where(p => p.GoodsSubName.Contains(c.entity.GoodsSubName));
                }
                if (string.IsNullOrEmpty(c.entity.PyCode) == false)
                {
                    q = q.Where(p => p.PyCode.Contains(c.entity.PyCode));
                }
                //if (string.IsNullOrEmpty(c.entity.GoodsState) == false)
                //{
                //    q = q.Where(p => p.GoodsState.Contains(c.entity.GoodsState));
                //}
                if (string.IsNullOrEmpty(c.entity.ProducingArea) == false)
                {
                    q = q.Where(p => p.ProducingArea.Contains(c.entity.ProducingArea));
                }
                //if (string.IsNullOrEmpty(c.entity.ArticleNumber) == false)
                //{
                //    q = q.Where(p => p.ArticleNumber.Contains(c.entity.ArticleNumber));
                //}
                if (string.IsNullOrEmpty(c.entity.Specification) == false)
                {
                    q = q.Where(p => p.Specification.Contains(c.entity.Specification));
                }
                //if (string.IsNullOrEmpty(c.entity.ShelfLife) == false)
                //{
                //    q = q.Where(p => p.ShelfLife.Contains(c.entity.ShelfLife));
                //}
                //if (string.IsNullOrEmpty(c.entity.PackUnitCode) == false)
                //{
                //    q = q.Where(p => p.PackUnitCode.Contains(c.entity.PackUnitCode));
                //}
                //if (string.IsNullOrEmpty(c.entity.OfferMode) == false)
                //{
                //    q = q.Where(p => p.OfferMode.Contains(c.entity.OfferMode));
                //}
                //if (c.entity.PackCoef > 0)
                //{
                //    q = q.Where(p => p.PackCoef == c.entity.PackCoef);
                //}

                //if (c.entity.OfferMin > 0)
                //{
                //    q = q.Where(p => p.OfferMin == c.entity.OfferMin);
                //}

                //if (c.entity.InputTax > 0)
                //{
                //    q = q.Where(p => p.InputTax == c.entity.InputTax);
                //}

                //if (c.entity.OutputTax > 0)
                //{
                //    q = q.Where(p => p.OutputTax == c.entity.OutputTax);
                //}

                //if (c.entity.StockUpperLimit > 0)
                //{
                //    q = q.Where(p => p.StockUpperLimit == c.entity.StockUpperLimit);
                //}

                //if (c.entity.StockLowerLimit > 0)
                //{
                //    q = q.Where(p => p.StockLowerLimit == c.entity.StockLowerLimit);
                //}

                //if (string.IsNullOrEmpty(c.entity.UnderFloorCode) == false)
                //{
                //    q = q.Where(p => p.UnderFloorCode.Contains(c.entity.UnderFloorCode));
                //}
                //if (string.IsNullOrEmpty(c.entity.UnderCounterCode) == false)
                //{
                //    q = q.Where(p => p.UnderCounterCode.Contains(c.entity.UnderCounterCode));
                //}
                //if (string.IsNullOrEmpty(c.entity.CheckUnitCode) == false)
                //{
                //    q = q.Where(p => p.CheckUnitCode.Contains(c.entity.CheckUnitCode));
                //}
                //if (c.entity.PurchasePrice > 0)
                //{
                //    q = q.Where(p => p.PurchasePrice == c.entity.PurchasePrice);
                //}

                //if (c.entity.NontaxPurchasePrice > 0)
                //{
                //    q = q.Where(p => p.NontaxPurchasePrice == c.entity.NontaxPurchasePrice);
                //}

                //if (c.entity.AvgCost > 0)
                //{
                //    q = q.Where(p => p.AvgCost == c.entity.AvgCost);
                //}

                //if (c.entity.NontaxAvgCost > 0)
                //{
                //    q = q.Where(p => p.NontaxAvgCost == c.entity.NontaxAvgCost);
                //}

                //if (c.entity.GrossRate > 0)
                //{
                //    q = q.Where(p => p.GrossRate == c.entity.GrossRate);
                //}

                //if (c.entity.SalePrice > 0)
                //{
                //    q = q.Where(p => p.SalePrice == c.entity.SalePrice);
                //}

                //if (c.entity.VipPrice > 0)
                //{
                //    q = q.Where(p => p.VipPrice == c.entity.VipPrice);
                //}

                //if (c.entity.TradePrice > 0)
                //{
                //    q = q.Where(p => p.TradePrice == c.entity.TradePrice);
                //}

                //if (c.entity.PushRate > 0)
                //{
                //    q = q.Where(p => p.PushRate == c.entity.PushRate);
                //}

                if (string.IsNullOrEmpty(c.entity.Operator) == false)
                {
                    q = q.Where(p => p.Operator.Contains(c.entity.Operator));
                }
                //if (string.IsNullOrEmpty(c.entity.Assessor) == false)
                //{
                //    q = q.Where(p => p.Assessor.Contains(c.entity.Assessor));
                //}
                if (string.IsNullOrEmpty(c.entity.IfExamine) == false)
                {
                    q = q.Where(p => p.IfExamine == c.entity.IfExamine);
                }

            }
            int count = q.Count();

            q = q.OrderByDescending(p => p.OperatorDate).Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            FillClassName(result);
            FillSupInfo(result);
            result.ForEach(p =>
            {
                if (p.SupTel == null)
                {
                    p.SupTel = "";
                }
            });
            return result.ToSearchResult(count);
        }

        public IList<GoodsArchives> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.GoodsBarCode.Contains(key)
                    || l.GoodsSubCode.Contains(key)
                    || l.GbCode.Contains(key)
                    || l.GmCode.Contains(key)
                    || l.GsCode.Contains(key)
                    || l.GlCode.Contains(key)
                    || l.GoodsType.Contains(key)
                    || l.CheckMode.Contains(key)
                    || l.SupCode.Contains(key)
                    || l.OpCode.Contains(key)
                    || l.GoodsName.Contains(key)
                    || l.GoodsSubName.Contains(key)
                    || l.PyCode.Contains(key)
                        //|| l.GoodsState.Contains(key)
                    || l.ProducingArea.Contains(key)
                    || l.ArticleNumber.Contains(key)
                    || l.Specification.Contains(key)
                    || l.ShelfLife.Contains(key)
                    || l.PackUnitCode.Contains(key)
                    || l.OfferMode.Contains(key)
                    || l.UnderFloorCode.Contains(key)
                    || l.UnderCounterCode.Contains(key)
                        //|| l.CheckUnitCode.Contains(key)
                    || l.Operator.Contains(key)
                    || l.Assessor.Contains(key)
                    || l.IfExamine.Contains(key)
                    select l;


            }
            q = q.OrderByDescending(p => p.OperatorDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var result = q.ToList();
            FillSupInfo(result);
            return result.ToList();
        }

        [Transaction]
        public void Delete(IList<string> ids)
        {
            var q = EntityRepository.LinqQuery.Where(p => ids.Contains(p.Id));
            foreach (var each in q)
            {
                Delete(each);
            }
        }

        [Transaction]
        public string GenarateGbCode()
        {
            var setting = FbPaBaseSetService.Get();
            var q = from l in EntityRepository.LinqQuery orderby l.GoodsSubCode descending select l;
            int maxId = 0;
            if (q.Count() > 0)
            {

                var item = q.First();
                maxId = item.GoodsSubCode.ToInt32(0);
            }
            string result = (maxId + 1).ToString().FillByStrings('0', setting.GoodsLen.ToInt32());
            while (EntityRepository.LinqQuery.Any(p => p.GoodsSubCode == result))
            {
                result = (result.ToInt32() + 1).ToString().FillByStrings('0', setting.GoodsLen.ToInt32());
            }
            return (maxId + 1).ToString().FillByStrings('0', setting.GoodsLen.ToInt32());
        }

        [Transaction]
        public string GenarateId(GoodsArchives entity)
        {
            var allId = EntityRepository.LinqQuery.Select(p => p.Id).ToList();
            string clsCode = "";
            switch (FbPaBaseSetService.Get().GoodsClassLevel.ToInt32())
            {
                case 1:
                    clsCode = entity.GbCode;
                    break;
                case 2:
                    clsCode = entity.GmCode;
                    break;
                case 3:
                    clsCode = entity.GsCode;
                    break;
                case 4:
                    clsCode = entity.GlCode;
                    break;
                default:
                    clsCode = "";
                    break;
            }

            string result = clsCode + entity.GoodsSubCode;
            while (allId.Any(p => p == result))
            {
                result = (result.ToInt32() + 1).ToString().FillByStrings('0', 8);
            }
            return result;
        }

        public string GenerateBarCode()
        {
            string b = Genarate12BarCode();
            int c1 = (iint(1, b) + iint(3, b) + iint(5, b) + iint(7, b) + iint(9, b) + iint(11, b)) * 3;
            int c2 = iint(0, b) + iint(2, b) + iint(4, b) + iint(6, b) + iint(8, b) + iint(10, b);
            int c3 = 10 - (c1 + c2) % 10;
            return b + c3.ToString();
        }

        private int iint(int index, string str)
        {
            return Convert.ToInt32(str[index]);
        }
        protected string Genarate12BarCode()
        {
            var barcode = "200" + "0000" + _string.GetRandomNumber(10000, 99999);
            if (EntityRepository.LinqQuery.Any(p => p.BackupCode == barcode))
            {
                return Genarate12BarCode();
            }
            else
            {
                return barcode;
            }
        }
    }
}
