using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSH.VSTO
{
    public static class CharNumberConvertor
    {
        /// <summary>
        /// Excel列头的字母转换为下标序号
        /// </summary>
        /// <param name="Column">列头字母，如“A”，“H”</param>
        /// <returns></returns>
        public static int ToColumnIndex(this string Column)
        {
            string[] arr = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ" };

            for (int i=0;i<arr.Length;i++)
            {
                if(arr[i]==Column)
                {
                    return i;
                }
            }
            return int.MinValue;
        }

        public static string ToColumnName(this int index)
        {
            string[] arr = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ" };
            return arr[index];
        }

        public static int[] ToIntArray(this string[] Columns)
        {
            int[] result=new int[Columns.Length];
            for(int i=0;i<Columns.Length;i++)
            {
                result[i] = Columns[i].ToInt32();
            }
            return result;
        }
    }
}
