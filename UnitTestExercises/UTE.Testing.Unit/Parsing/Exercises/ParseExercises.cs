using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTE.Testing.Unit.Parsing.Helper;

namespace UTE.Testing.Unit.Parsing.Exercises
{
	[TestClass]
	public class ParseExercises
	{
		[TestMethod]
		public void TestJSONDeserialization()
		{
			DataSheetMeterCombined mtr = ParseHelper.DeserializeJSON<DataSheetMeterCombined>("Data/JSON/DataUIMeterHealth.txt");
		}
	}
}