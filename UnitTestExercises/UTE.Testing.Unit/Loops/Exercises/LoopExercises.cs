using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTE.Testing.Unit.Loops.Helper;

namespace UTE.Testing.Unit.Loops.Exercises
{
	[TestClass]
	public class LoopExercises
	{
		[TestMethod]
		public void TestLoop()
		{
			bool[] coll = new bool[10];

			LoopHelper.ProcessCollection(coll);
		}
	}
}
