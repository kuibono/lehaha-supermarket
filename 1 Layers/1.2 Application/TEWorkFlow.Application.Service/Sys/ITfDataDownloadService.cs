using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Sys;

namespace TEWorkFlow.Application.Service.Sys
{
    public interface ITfDataDownloadService
    {
        void AddDownload(string table, string id);

        void AddAllArchivesToBranch(string bCode);

        string Create(TfDataDownload entity);

        TfDataDownload GetById(string id);

        IList<TfDataDownload> GetAll();

        void Update(TfDataDownload entity);

        /// <summary>
        /// 删除指定TfDataDownload
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TfDataDownload entity);

        /// <summary>
        /// TfDataDownload
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<TfDataDownload> entitys);


        SearchResult<TfDataDownload> Search(SearchDtoBase<TfDataDownload> c);

        IList<TfDataDownload> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);
    }
}
