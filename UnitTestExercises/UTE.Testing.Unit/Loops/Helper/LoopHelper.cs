using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTE.Testing.Unit.Loops.Helper
{
	public static class LoopHelper
	{
		public static void ProcessCollection(bool[] inputColl)
		{
			int lastRelevantIndex = (int) (inputColl.Length * 0.5);

			for(int i = 0; i <= lastRelevantIndex; i++)
			{
				inputColl[i] = true;

				lastRelevantIndex--;
			}
		}
	}
}
