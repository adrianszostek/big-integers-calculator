using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BICViewNamespace
{
    internal static class BICView
    {
        public static void DisplayResult<T>(T[] resultArr)
        {
            Console.WriteLine("Result:");
            foreach (T num in resultArr)
            {
                Console.Write(num);
            }
        }
        public static void DisplayResultFloatingPoint(int[] resultArr, int numbersSizeDifference)
        {
            Console.WriteLine("Result:");
            int shift = resultArr[0] - 99 > 0 ? 1 : 0;
            int floatingPointIndex = numbersSizeDifference + shift;
            string resultStr = "";
            string resultStrWithDot = "";

            for (int i = floatingPointIndex; i <= 0; i++)
                resultStr += "0";

            foreach (int num in resultArr)
            {
                if (num < 10)
                    resultStr += "0";
                resultStr += num;
            }

            if (floatingPointIndex <= 0)
                floatingPointIndex = 1;

            for (int i = 0; i < resultStr.Length; i++)
            {
                if (i == floatingPointIndex)
                    resultStrWithDot += ".";

                resultStrWithDot += resultStr[i];
            }
            Console.WriteLine(resultStrWithDot);
        }
    }
}
