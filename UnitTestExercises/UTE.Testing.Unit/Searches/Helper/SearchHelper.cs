using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTE.Testing.Unit.Searches.Helper
{
	public static class SearchHelper
	{
		public static string BinarySearch(int[] sortedArray, int desiredVal)
		{
			int floorVal = 0;
			int ceilingVal = sortedArray.Length - 1;
			int midVal = (floorVal + ceilingVal) / 2;

			string outputText;

			do
			{
				if (desiredVal == sortedArray[midVal])
				{
					outputText = sortedArray[midVal].ToString();
					break;
				}
				else if (desiredVal > sortedArray[midVal] &&
				midVal < sortedArray.Length - 1 &&
				floorVal < midVal && ceilingVal > midVal)
				{
					floorVal = midVal + 1;
					midVal = (floorVal + ceilingVal) / 2;
					outputText = midVal.ToString();
				}
				else if (desiredVal < sortedArray[midVal] &&
				midVal > 0 &&
				floorVal < midVal && ceilingVal > midVal)
				{
					ceilingVal = midVal - 1;
					midVal = (floorVal + ceilingVal) / 2;
					outputText = midVal.ToString();
				}
				else
				{
					outputText = "Value Not Found";
					break;
				}
			} while (midVal != desiredVal);


			return outputText;
		}

		public static int FindMissingNumber(int[] array)
		{
			int arrayElementSum = 0;
			int largestArrayElement = array[array.Length - 1];

			for (int i = 0; i < array.Length; i++)
			{
				arrayElementSum += array[i];
			}

			int arraySumFormula = ((largestArrayElement + 1) * largestArrayElement) / 2;

			int missingNumber = arraySumFormula - arrayElementSum;


			return missingNumber;
		}
	}
}
