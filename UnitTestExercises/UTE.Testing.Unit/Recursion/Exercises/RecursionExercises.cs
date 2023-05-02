using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTE.Testing.Unit.Recursion.Helper;

namespace UTE.Testing.Unit.Recursion.Exercises
{
	[TestClass]
	public class RecursionExercises
	{
		[TestMethod]
		public void TestRecursionInteger()
		{
			bool bShouldRecurse = true;

			int counter = 0;
			int counterMax = 5;

			int count = RecursionHelper.BasicRecursionTestInteger(bShouldRecurse, ref counter, counterMax);


			Assert.AreEqual(count, counterMax);
		}

		[TestMethod]
		public void TestRecursion()
		{
			bool bShouldRecurse = true;

			int counter = 0;
			int counterMax = 5;

			RecursionHelper.BasicRecursionTest(bShouldRecurse, ref counter, counterMax);


			Assert.AreEqual(counter, counterMax);
		}
	}
}
