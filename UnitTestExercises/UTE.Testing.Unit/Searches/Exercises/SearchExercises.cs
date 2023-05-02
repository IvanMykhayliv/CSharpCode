using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTE.Testing.Unit.Searches.Helper;

namespace UTE.Testing.Unit
{
	[TestClass]
	public class SearchExercises
	{
		[TestMethod]
		public void SearchBinary()
		{
			int[] sArray1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			int[] sArray2 = new int[] { 0, 1, 2, 3, 3, 5, 6, 7, 8, 9 };
			int[] sArray3 = new int[] { 1, 4, 5, 6, 8, 9 };

			int desiredVal1 = 4;
			int desiredVal2 = 8;
			int desiredVal3 = 3;
			int desiredVal4 = 9;
			int desiredVal5 = 0;
			int desiredVal6 = 70;

			string result1 = SearchHelper.BinarySearch(sArray1, desiredVal1);
			string result2 = SearchHelper.BinarySearch(sArray1, desiredVal2);
			string result3 = SearchHelper.BinarySearch(sArray2, desiredVal3);
			string result4 = SearchHelper.BinarySearch(sArray3, desiredVal4);
			string result5 = SearchHelper.BinarySearch(sArray1, desiredVal5);
			string result6 = SearchHelper.BinarySearch(sArray3, desiredVal6);


			Assert.AreEqual("4", result1);
			Assert.AreEqual("8", result2);
			Assert.AreEqual("3", result3);
			Assert.AreEqual("9", result4);
			Assert.AreEqual("0", result5);
			Assert.AreEqual("Value Not Found", result6);
		}

		[TestMethod]
		public void FindMissingNumber()
		{
			int[] array = new int[] { 1, 2, 3, 5 };
			int intendedMissingNumber = 4;

			int outputMissingNumber = SearchHelper.FindMissingNumber(array);


			Assert.AreEqual(intendedMissingNumber, outputMissingNumber);
		}
	}
}
