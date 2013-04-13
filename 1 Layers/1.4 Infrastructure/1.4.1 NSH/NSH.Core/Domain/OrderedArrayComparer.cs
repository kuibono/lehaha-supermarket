using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSH.Core.Domain
{
    /// <summary>
    /// 根据给定的已排序的数组比较（数组中在前的为大，不存在为小）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OrderedArrayComparer<T> : IComparer<T>
    {
        #region IComparer<T> Members
        protected T[] _orderedArray;

        public OrderedArrayComparer(T[] orderedArray)
        {
            _orderedArray = orderedArray;
        }

        public int Compare(T x, T y)
        {
            int xIndex = Array.IndexOf(_orderedArray, x);
            int yIndex = Array.IndexOf(_orderedArray, y);
            if (xIndex == -1) { xIndex = _orderedArray.Length; }
            if (yIndex == -1) { yIndex = _orderedArray.Length; }

            if (xIndex < yIndex)
            {
                return 1;
            }
            else if (xIndex == yIndex)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }
}
