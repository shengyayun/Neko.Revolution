using System;

namespace Library.Common
{
    public class LevenshteinDistance
    {
        private static LevenshteinDistance _instance = null;
        public static LevenshteinDistance Instance
        {
            get
            {
                return _instance ?? new LevenshteinDistance();
            }
        }


        /// <summary>
        /// 取最小的一位数
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <returns></returns>
        public int LowerOfThree(int first, int second, int third)
        {
            int min = first;
            if (second < min)
                min = second;
            if (third < min)
                min = third;
            return min;
        }

        public int Levenshtein_Distance(string str1, string str2)
        {
            var n = str1.Length;
            var m = str2.Length;

            int i;
            int j;
            if (n == 0)
            {
                return m;
            }
            if (m == 0)

                return n;
            var matrix = new int[n + 1, m + 1];

            for (i = 0; i <= n; i++)
            {
                //初始化第一列
                matrix[i, 0] = i;
            }

            for (j = 0; j <= m; j++)
            {
                //初始化第一行
                matrix[0, j] = j;
            }

            for (i = 1; i <= n; i++)
            {
                var ch1 = str1[i - 1];
                for (j = 1; j <= m; j++)
                {
                    var ch2 = str2[j - 1];
                    var temp = ch1.Equals(ch2) ? 0 : 1;
                    matrix[i, j] = LowerOfThree(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1, matrix[i - 1, j - 1] + temp);
                }
            }

            for (i = 0; i <= n; i++)
            {
                for (j = 0; j <= m; j++)
                {
                    Console.Write(" {0} ", matrix[i, j]);
                }
                Console.WriteLine("");
            }
            return matrix[n, m];

        }

        /// <summary>
        /// 计算字符串相似度
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public decimal LevenshteinDistancePercent(string str1, string str2)
        {
            int maxLenth = str1.Length > str2.Length ? str1.Length : str2.Length;
            int val = Levenshtein_Distance(str1, str2);
            return 1 - (decimal)val / maxLenth;
        }
    }
}
