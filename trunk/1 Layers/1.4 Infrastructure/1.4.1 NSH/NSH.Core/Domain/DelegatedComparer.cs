using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSH.Core.Domain
{
    public class DelegatedComparer<T> : IComparer<T>
    {
        //在构造函数结束后，_compare就没法改变了。因此DelegatedComparer<T>实例的行为也就可以被锁定了。
        private readonly Func<T, T, int> _compare;

        //传入一个委托，表示比较算法。
        public DelegatedComparer(Func<T, T, int> func)
        {
            _compare = func;
        }

        public int Compare(T x, T y)
        {
            //直接调用委托。
            return _compare(x, y);
        }
    }
}
