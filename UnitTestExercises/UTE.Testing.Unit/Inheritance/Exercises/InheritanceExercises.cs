using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTE.Testing.Unit.Inheritance.Helper;

namespace UTE.Testing.Unit.Inheritance.Exercises
{
	[TestClass]
	public class InheritanceExercises
	{

		[TestMethod]
		public void TestInheritanceCasting()
		{
			List<ParentObj> objs = new List<ParentObj>();

			ChildObj childObj = new ChildObj();

			InheritanceHelper.AddToCollection(objs, childObj);

			ChildObj tempNextObj = (ChildObj) objs[0];

			tempNextObj.ChildFunc();

			Assert.AreEqual(tempNextObj.Field, "Something Else");
			Assert.AreEqual(objs[0].InternalID, 1);
			Assert.AreEqual(objs[0].Counter, 5);
		}
	}
}
