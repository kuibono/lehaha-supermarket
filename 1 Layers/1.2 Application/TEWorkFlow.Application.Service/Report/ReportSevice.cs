using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEWorkFlow.Domain;
using NSH.Core.Domain;
using TEWorkFlow.Dto;
using NHibernate;
using TEWorkFlow.Domain.Archives;
using NHibernate.Transform;

namespace TEWorkFlow.Application.Service.Report
{
    public class ReportSevice : IReportSevice
    {
        public IRepositoryGUID<RetailPurchaseCompare> EntityRepository { get; set; }

        public SearchResult<RetailPurchaseCompare> GetRetailPurchaseCompareList(DateTime dateS,DateTime dateE,SearchDtoBase<RetailPurchaseCompare> c)
        {
            string sql = @"

select 
	row_number() over(order by m.b_code) as '{RetailPurchaseCompare.Id}',
	m.b_code as '{RetailPurchaseCompare.bCode}',
	m.b_name as '{RetailPurchaseCompare.bName}',
	m.sup_code AS '{RetailPurchaseCompare.SupCode}',
	m.sup_name AS '{RetailPurchaseCompare.SupName}',
	m.goods_code as '{RetailPurchaseCompare.GoodsCode}',
	m.goods_bar_code as '{RetailPurchaseCompare.BarCode}',
	m.goods_name as '{RetailPurchaseCompare.GoodsName}',
	isnull(p.amount,0)  as '{RetailPurchaseCompare.PurchaseMoney}',
	isnull(r.amount,0)  as '{RetailPurchaseCompare.RetailMoney}',
	isnull(p.c,0) as '{RetailPurchaseCompare.PurchaseCount}',
	isnull(r.c,0) as '{RetailPurchaseCompare.RetailCount}',
	isnull(isnull(p.c,0)-isnull(r.c,0),0) as '{RetailPurchaseCompare.CountCompare}',
	isnull(isnull(p.amount,0)-isnull(r.amount,0),0) as '{RetailPurchaseCompare.MoneyCompare}'
from (
	select 
		b.b_code,
		b.b_name,
		g.goods_code,
		g.goods_bar_code,
		g.goods_name,
		g.sup_code,
		s.sup_name
	from dbo.bs_branch_archives b
	left join dbo.fb_goods_archives g
	on 1=1
	left join dbo.fb_supplier_archives s
	on g.sup_code=s.sup_code
) m

left join (
	----查询分店下，某段时间内，商品金额数据
	select goods_code,b_code,sum(amount) as amount,sum(c) as c from (
		select pd.goods_code,pd.pc_number,pd.amount,pm.b_code,pd.c from 
		(select goods_code,pc_number,isnull(sum(purchase_money),0) as amount,count(0) as c from dbo.pc_purchase_detail_history group by goods_code,pc_number) pd
		left join pc_purchase_manage_history pm
		on pm.pc_number=pd.pc_number
		where pm.purchase_date between '@dates' and '@datee'
	) tmp group by goods_code,b_code
) p
on m.goods_code=p.goods_code and m.b_code=p.b_code
left join (
	----查询分店下，某段时间内，商品的总金额
	select goods_code,b_code,sum(amount) as amount,sum(c) as c from (
		select rd.goods_code,rd.rt_number,rd.amount,rm.b_code,rd.c from 
		(select goods_code,rt_number,isnull(sum(sale_money),0) as amount,count(0) as c from dbo.rt_retail_detail group by goods_code,rt_number) rd
		left join dbo.rt_retail_manage rm
		on rd.rt_number=rm.rt_number
		where rm.rt_date between '@dates' and '@datee'
	) tmp group by goods_code,b_code
) r
on m.goods_code=r.goods_code and m.b_code=r.b_code
where (r.c>0 or p.c>0)

";
            sql = sql.Replace("@dates", dateS.ToString("yyyy-MM-dd"));
            sql = sql.Replace("@datee", dateE.ToString("yyyy-MM-dd"));

            if (c.entity != null && string.IsNullOrEmpty(c.entity.bCode) == false)
            {
                sql += string.Format(" and m.b_code='{0}' ", c.entity.bCode); ;
            }
            if (c.entity != null && string.IsNullOrEmpty(c.entity.GoodsCode) == false)
            {
                sql += string.Format(" and m.goods_code='{0}' ", c.entity.GoodsCode); ;
            }
            if (c.entity != null && string.IsNullOrEmpty(c.entity.SupCode) == false)
            {
                sql += string.Format(" and m.sup_code='{0}' ", c.entity.SupCode); ;
            }
            int count = EntityRepository.Session.CreateSQLQuery(sql)
                .AddEntity("RetailPurchaseCompare", typeof(RetailPurchaseCompare))
                .List<RetailPurchaseCompare>()
                .Count;
            var items = EntityRepository.Session.CreateSQLQuery(sql)
                .AddEntity("RetailPurchaseCompare", typeof(RetailPurchaseCompare))
                .List<RetailPurchaseCompare>();

            var aa = items
                .Skip(c.pageSize*(c.pageIndex-1)).
                Take(c.pageSize)
                .ToList().ToSearchResult<RetailPurchaseCompare>(count);
            return aa;

        }
    }
}
