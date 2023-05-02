using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTE.Testing.Unit.Recursion.Helper
{
	public static class RecursionHelper
	{
		public static int BasicRecursionTestInteger(bool bShouldRecurse, ref int counter, int counterMax)
		{
			if (bShouldRecurse)
			{
				counter++;

				if (counter >= counterMax)
					bShouldRecurse = false;

				return BasicRecursionTestInteger(bShouldRecurse, ref counter, counterMax);
			}

			return counter;
		}

		public static void BasicRecursionTest(bool bShouldRecurse, ref int counter, int counterMax)
		{
			if(bShouldRecurse)
			{
				counter++;

				if(counter >= counterMax)
					bShouldRecurse = false;

				BasicRecursionTest(bShouldRecurse, ref counter, counterMax);

				return;
			}
		}
	}
}
