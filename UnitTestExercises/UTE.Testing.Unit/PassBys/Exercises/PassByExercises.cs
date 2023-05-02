using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTE.Testing.Unit.PassBys.Helper;
using UTE.Testing.Unit.States.Model;

namespace UTE.Testing.Unit.PassBys.Exercises
{
	[TestClass]
	public class PassByExercises
	{
		[TestMethod]
		public void TestCollPassByNumIndexes()
		{
			ObjTyped[] objs = new ObjTyped[5];

			for(int i = 0; i < objs.Length; i++)
			{
				objs[i] = new ObjTyped();
			}

			int curIndex = 2;
			int lastActiveIndex = 3;

			PassByProcessor.ResortByActivate(objs, curIndex, ref lastActiveIndex, objs[2]);

			Assert.AreEqual(2, lastActiveIndex);
		}

		//Even temporary objects made to reference indices in a collection are pass-by reference, even if the function
		//changing them is a void and there's no parameter modifier like ref.
		[TestMethod]
		public void TestFilteredLstPassByGeneratedTempObj()
		{
			List<ObjTyped> masterObjs = PassByProcessor.GenerateObjTypedList(10);

			int objIDToChange = 5;
			PassByProcessor.PassByModifyTempObj(masterObjs, objIDToChange);

			ObjTyped tempObj = masterObjs.Where(a => a.ObjId == objIDToChange).FirstOrDefault();


			Assert.AreEqual(tempObj.Field, "Something");
		}

		//Even though the list is filtered and generated from a new list entirely, changes made to the old list are still
		//reflected in the new one.
		[TestMethod]
		public void TestFilteredLstPassByGenerated()
		{
			List<ObjTyped> masterObjs = PassByProcessor.GenerateObjTypedList(10);
			List<ObjTyped> subObjs = PassByProcessor.GenerateObjTypedSubList(MASTERSTATE.VAL2, masterObjs);

			int objIDToChange = 5;

			subObjs[0] = null;

			subObjs[0] = masterObjs[5];

			subObjs.Where(a => a.ObjId == objIDToChange).FirstOrDefault().Field = "Something";

			string masterField = masterObjs.Where(a => a.ObjId == objIDToChange).Select(a => a.Field).FirstOrDefault();
			string subField = subObjs.Where(a => a.ObjId == objIDToChange).Select(a => a.Field).FirstOrDefault();


			Assert.AreEqual(subField, masterField);
		}

		//Even though the list is filtered, changes made to the old list are still reflected in the new one.
		[TestMethod]
		public void TestFilteredLstPassBy()
		{
			TestObj tObj1 = new TestObj()
			{
				Field1 = "Nothing"
			};
			TestObj tObj2 = new TestObj()
			{
				Field1 = "Something"
			};
			TestObj tObj3 = new TestObj()
			{
				Field1 = "Something"
			};

			List<TestObj> oldObjs = new List<TestObj>();
			oldObjs.Add(tObj1);
			oldObjs.Add(tObj2);
			oldObjs.Add(tObj3);

			List<TestObj> nextObjs = oldObjs.Where(a => a.Field1 == "Something").ToList();

			oldObjs[1].Field2 = "Something New";

			Assert.AreEqual(nextObjs[0].Field1, oldObjs[1].Field1);
			Assert.AreEqual(nextObjs[0].Field2, oldObjs[1].Field2);
		}
		[TestMethod]
		public void TestStructPassByRef()
		{
			TestObj tObj = new TestObj()
			{
				Field1 = "Nothing"
			};

			TestSubStruct tSS = new TestSubStruct()
			{
				FieldVar = "Nothing",
				FieldProp = "Nothing"
			};

			TestStruct str = new TestStruct()
			{
				FieldVar = "Nothing",
				FieldProp = "Nothing",
				obj = tObj,
				subStructVar = tSS,
				subStructProp = tSS
			};

			TestStruct str2 = new TestStruct("Nothing", "Nothing", tObj, ref tSS, ref tSS, false, false);

			PassByProcessor.PassByRefStructFields(ref str);
			PassByProcessor.PassByRefStructObjFields(ref str);
			PassByProcessor.PassByRefStructSubStructFields(ref str);

			PassByProcessor.PassByRefStructFields(ref str2);
			PassByProcessor.PassByRefStructObjFields(ref str2);
			PassByProcessor.PassByRefStructSubStructFields(ref str2);


			Assert.AreEqual("Something", str.FieldVar);
			Assert.AreEqual("Something", str.FieldProp);
			Assert.AreEqual("Something", str.obj.Field1);
			Assert.AreEqual("Something", str.obj.Field2);
			Assert.AreEqual("Something", str.subStructVar.FieldVar);
			Assert.AreEqual("Something", str.subStructVar.FieldProp);
			Assert.AreEqual("Nothing", str.subStructProp.FieldVar);
			Assert.AreEqual("Nothing", str.subStructProp.FieldProp);
			Assert.IsTrue(str.bTestVar);
			Assert.IsTrue(str.bTestProp);

			Assert.AreEqual("Something", str2.FieldVar);
			Assert.AreEqual("Something", str2.FieldProp);
			Assert.AreEqual("Something", str2.obj.Field1);
			Assert.AreEqual("Something", str2.obj.Field2);
			Assert.AreEqual("Something", str2.subStructVar.FieldVar);
			Assert.AreEqual("Something", str2.subStructVar.FieldProp);
			Assert.AreEqual("Nothing", str2.subStructProp.FieldVar);
			Assert.AreEqual("Nothing", str2.subStructProp.FieldProp);
			Assert.IsTrue(str2.bTestVar);
			Assert.IsTrue(str2.bTestProp);
		}

		[TestMethod]
		public void TestStructPassBy()
		{
			TestObj tObj = new TestObj()
			{
				Field1 = "Nothing"
			};

			TestSubStruct tSS = new TestSubStruct()
			{
				FieldVar = "Nothing",
				FieldProp = "Nothing"
			};

			TestStruct str = new TestStruct()
			{
				FieldVar = "Nothing",
				FieldProp = "Nothing",
				obj = tObj,
				subStructVar = tSS,
				subStructProp = tSS
			};

			TestStruct str2 = new TestStruct("Nothing", "Nothing", tObj, ref tSS, ref tSS, false, false);

			PassByProcessor.PassByStructFields(str);
			PassByProcessor.PassByStructObjFields(str);
			PassByProcessor.PassByStructSubStructFields(str);

			PassByProcessor.PassByStructFields(str2);
			PassByProcessor.PassByStructObjFields(str2);
			PassByProcessor.PassByStructSubStructFields(str2);


			Assert.AreEqual("Nothing", str.FieldVar);
			Assert.AreEqual("Nothing", str.FieldProp);
			Assert.AreEqual("Something", str.obj.Field1);
			Assert.AreEqual("Something", str.obj.Field2);
			Assert.AreEqual("Nothing", str.subStructVar.FieldVar);
			Assert.AreEqual("Nothing", str.subStructVar.FieldProp);
			Assert.AreEqual("Nothing", str.subStructProp.FieldVar);
			Assert.AreEqual("Nothing", str.subStructProp.FieldProp);
			Assert.IsTrue(!str.bTestVar);
			Assert.IsTrue(!str.bTestProp);


			Assert.AreEqual("Nothing", str2.FieldVar);
			Assert.AreEqual("Nothing", str2.FieldProp);
			Assert.AreEqual("Something", str2.obj.Field1);
			Assert.AreEqual("Something", str2.obj.Field2);
			Assert.AreEqual("Nothing", str2.subStructVar.FieldVar);
			Assert.AreEqual("Nothing", str2.subStructVar.FieldProp);
			Assert.AreEqual("Nothing", str2.subStructProp.FieldVar);
			Assert.AreEqual("Nothing", str2.subStructProp.FieldProp);
			Assert.IsTrue(!str2.bTestVar);
			Assert.IsTrue(!str2.bTestProp);
		}

		[TestMethod]
		public void TestObjPassByInstantiate()
		{
			TestObj obj = new TestObj();

			PassByProcessor.PassByObjInstantiate(obj);


			Assert.AreEqual("Instantiated", obj.Field2);
		}

		[TestMethod]
		public void TestObjNumPassBy()
		{
			ObjTyped obj = new ObjTyped()
			{
				ObjId = 0
			};

			PassByProcessor.PassByNumObj(obj);


			Assert.AreEqual(1, obj.ObjId);
		}

		[TestMethod]
		public void TestNumPassByStatic()
		{
			int num = 0;

			PassByProcessor.PassByNum(ref num);


			Assert.AreEqual(1, num);
		}

		[TestMethod]
		public void TestObjPassByStatic()
		{
			TestObj obj = new TestObj()
			{
				Field2 = "TestName"
			};

			PassByProcessor.PassByObj(ref obj);


			Assert.AreEqual("Something", obj.Field2);
		}

		[TestMethod]
		public void TestObjRef()
		{
			TestObj obj = new TestObj()
			{
				Field2 = "TestName"
			};

			PassByProcessor.PassByRefObj(ref obj.Field2);


			Assert.AreEqual("Something", obj.Field2);
		}

		[TestMethod]
		public void TestPrimitivePassBy()
		{
			int x = 25;
			int y = x;

			x = 26;


			Assert.AreNotEqual(x, y);
		}
		/*Tests to see if parameters with the in modifier can be modified - their primitives, collections, and sub-objects
		can, but the parameter object next to the in modifier cannot be instantiated, re-instantiated, nulled, or set to a
		pre-instantiated object, even if it's a primitive.*/
		[TestMethod]
		public void TestIn()
		{
			SubObj sObj = new SubObj()
			{
				CurId = 1
			};

			TestObj obj = new TestObj()
			{
				Field1 = "TestName",
				Flag1 = false,
				CurObj = sObj
			};

			PassByProcessor.PassByRefPrimitiveCollection(in sObj, 5);

			PassByProcessor.PassByRefInFlagToggle(in obj);


			Assert.IsTrue(obj.Flag1);
			Assert.AreEqual(sObj.PastIds.Count, 1);
			Assert.AreEqual(obj.CurObj.PastIds.Count, 1);

			sObj = new SubObj();

			Assert.AreEqual(sObj.PastIds.Count, 0);
			Assert.AreEqual(obj.CurObj.PastIds.Count, 1);


			sObj = new SubObj();

			PassByProcessor.PassByRefPrimitiveCollection(in sObj, 10);

			Assert.AreEqual(sObj.PastIds.Count, 1);
			Assert.AreEqual(sObj.PastIds[0], 10);
			Assert.AreEqual(obj.CurObj.PastIds.Count, 1);
			Assert.AreEqual(obj.CurObj.PastIds[0], 5);
		}
	}
}
