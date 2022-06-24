using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BICOperationsNamespace
{
	internal static class BICOperations
	{
		// Input string to sbyte array conversion
		private static sbyte[] StrToArr(string str, int totalLength)
		{
			int strLength = str.Length;
			int emptyLength = totalLength - strLength;

			sbyte[] outputArr = new sbyte[totalLength];

			for (int i = 0; i < emptyLength; i++)
				outputArr[i] = 0;

			for (int i = emptyLength; i < totalLength; i++)
				outputArr[i] = (sbyte)(str[i - emptyLength] - '0');
	
			return outputArr;
		}
		private static T[] RemoveLeadingZeros<T>(T[] arr)
		{
			int leadingZerosLength = 0;
			while (arr[leadingZerosLength].Equals(0))
				leadingZerosLength++;

			return arr[leadingZerosLength..];
		}

		public static sbyte[] Addition(string firstNumber, string secondNumber)
		{
			int firstLength = firstNumber.Length;
			int secondLength = secondNumber.Length;
			int resultLength = Math.Max(firstLength, secondLength);

			sbyte[] firstArr = StrToArr(firstNumber, resultLength);
			sbyte[] secondArr = StrToArr(secondNumber, resultLength);
			sbyte[] resultArr = new sbyte[resultLength];

			sbyte reminder = 0;

			for (int i = resultLength - 1; i >= 0; i--)
			{
				resultArr[i] = (sbyte)(firstArr[i] + secondArr[i] + reminder);
				if (i != 0)
				{
					if (resultArr[i] > 9)
					{
						resultArr[i] -= 10;
						reminder = 1;
					}
					else
					{
						reminder = 0;
					}
				}
			}
			return resultArr;
		}
		public static sbyte[] Subtraction(string firstNumber, string secondNumber, bool reversedMode = false)
		{
			int firstLength = firstNumber.Length;
			int secondLength = secondNumber.Length;
			int resultLength = Math.Max(firstLength, secondLength);

			sbyte[] firstArr = StrToArr(firstNumber, resultLength);
			sbyte[] secondArr = StrToArr(secondNumber, resultLength);
			sbyte[] resultArr = new sbyte[resultLength];

			sbyte reminder = 0;

			for (int i = resultLength - 1; i >= 0; i--)
			{
				resultArr[i] = (sbyte)(firstArr[i] - secondArr[i] - reminder);
				if (resultArr[i] < 0)
				{
					resultArr[i] += 10;
					reminder = 1;
				}
				else
				{
					reminder = 0;
				}
			}
			resultArr = RemoveLeadingZeros(resultArr);
			if (reversedMode)
				resultArr[0] *= -1;

			return resultArr;
		}
		public static int[] Multiplication(string firstNumber, string secondNumber)
		{
			int firstLength = firstNumber.Length;
			int secondLength = secondNumber.Length;
			int longerNumLength = Math.Max(firstLength, secondLength);
			int resultLength = firstLength+secondLength-1;

			sbyte[] firstArr = StrToArr(firstNumber, firstLength);
			sbyte[] secondArr = StrToArr(secondNumber, secondLength);
			int[] resultArr = new int[resultLength];
			for (int i = 0; i < resultLength; i++)
				resultArr[i] = 0;


			for (int i = firstLength - 1; i >= 0; i--)
				for (int j = secondLength - 1; j >= 0; j--)
					resultArr[i+j] += firstArr[i] * secondArr[j];

			sbyte reminder = 0;
			int currentOverload;

			for (int i = resultLength - 1; i >= 0; i--)
			{
				currentOverload = resultArr[i];
				if (currentOverload >= 10 && i != 0)
				{
					reminder = (sbyte)(currentOverload / 10);
					resultArr[i] %= 10;
					resultArr[i - 1] += reminder;
				}
			}
			resultArr = RemoveLeadingZeros<int>(resultArr);
			return resultArr;
		}
		// Implementation of the division based on the Fourier division method
		// https://en.wikipedia.org/wiki/Fourier_division
		public static int[] Division(string firstNumber, string secondNumber, byte significantDigits)
		{
			int firstLength = firstNumber.Length;
			int secondLength = secondNumber.Length;
			int resultLength = (significantDigits+1)/2;

			int[] a = new int[resultLength+1];
			int[] b = new int[resultLength];
			int[] c = new int[resultLength+1];

			for (int i = 0; i < resultLength+1; i += 2)
			{
				try
				{
					c[i / 2] = (firstNumber[i] - '0') * 10;
				}
				catch (IndexOutOfRangeException)
				{
					c[i / 2] = 0;
				}
				try
				{
					c[i / 2] += firstNumber[i+1] - '0';
				}
				catch (IndexOutOfRangeException) { }


				try
				{
					a[i / 2] = (secondNumber[i] - '0') * 10;
				}
				catch (IndexOutOfRangeException)
				{
					a[i / 2] = 0;
				}
				try
				{
					a[i / 2] += secondNumber[i+1] - '0';
				}
				catch (IndexOutOfRangeException) { }
			}

			int bCoeff;
			int bDivider;
			int r;

			b[0] = ((c[0] * 100 + c[1]) / a[0]);
			r = ((c[0] * 100 + c[1]) % a[0]);

			for (int i = 1; i < resultLength; i++)
			{
				bCoeff = 0;
				for (int j = 1; j <= i; j++)
					bCoeff += (b[i - j] * a[j]);

				bDivider = (r * 100 + c[i + 1] - bCoeff);
				b[i] = bDivider >= 0 ? (bDivider / a[0]) : (bDivider / a[0] - 1);
				r = bDivider >= 0 ? (bDivider % a[0]) : a[0] + (bDivider % a[0]);
			}
			int hundreds;
			for (int i = resultLength-1; i > 0; i--)
			{
				if (b[i] > 100)
				{
					hundreds = b[i] / 100;
					b[i] -= 100*hundreds;
					b[i - 1] += hundreds;
				}
				else if (b[i] < 0)
				{
					hundreds = b[i] / -100 + 1;
					b[i] += 100*hundreds;
					b[i - 1] -= hundreds;
				}
			}
			return b;
		}
	}
}