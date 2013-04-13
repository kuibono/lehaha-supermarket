using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSH.Core.Domain
{
    public struct OperationCount<T>
    {
        public int AddCount { get; set; }

        public int UpdateCount { get; set; }

        public int DeleteCount { get; set; }

        private List<OperationEntity<T>> _list;

        public List<OperationEntity<T>> List { get { 
            return _list ?? (_list = new List<OperationEntity<T>>()); } }

        public void Reset()
        {
            AddCount = 0;
            UpdateCount = 0;
            DeleteCount = 0;
        }
    }
}
