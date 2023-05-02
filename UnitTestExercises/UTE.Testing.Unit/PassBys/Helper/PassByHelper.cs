using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTE.Testing.Unit.States.Model;

namespace UTE.Testing.Unit.PassBys.Helper
{
	public class PassByHelper
	{

	}

	public struct TestStruct
	{
		public TestStruct(string FieldProp, string FieldVar, TestObj obj, ref TestSubStruct subStructVar, ref TestSubStruct subStructProp, bool bTestVar, bool bTestProp)
		{
			this.FieldProp = FieldProp;
			this.FieldVar = FieldVar;
			this.obj = obj;
			this.subStructVar = subStructVar;
			this.subStructProp = subStructProp;
			this.bTestVar = bTestVar;
			this.bTestProp = bTestProp;
		}

		public string FieldProp { get; set; }

		public string FieldVar;

		public TestObj obj { get; set; }

		public TestSubStruct subStructVar;

		public TestSubStruct subStructProp { get; set; }

		public bool bTestVar;
		public bool bTestProp { get; set; }
	}

	public struct TestSubStruct
	{
		public string FieldVar;
		public string FieldProp { get; set; }
	}

	public class TestObj
	{
		public string Field2;

		public TestObj()
		{
			if (CurObj == null)
				CurObj = new SubObj();

			if (PastObjs == null)
				PastObjs = new List<SubObj>();

			if (PastFields == null)
				PastFields = new List<string>();

			Field2 = String.Empty;
		}

		public string Field1 { get; set; }
		public bool Flag1 { get; set; }

		public SubObj CurObj { get; set; }
		public List<SubObj> PastObjs { get; set; }
		public List<string> PastFields { get; set; }
	}

	public class SubObj
	{
		public SubObj()
		{
			if (PastIds == null)
				PastIds = new List<int>();
		}

		public int CurId { get; set; }
		public List<int> PastIds { get; set; }
	}

	public class ObjTyped
	{
		public int ObjId { get; set; }
		public MASTERSTATE ObjType { get; set; }
		public string Field { get; set; }
	}

	public static class PassByProcessor
	{
		/*Invalid, will yield a build-error stating num is a read-only variable, as the in modifier prevents its parameter
		object from being changed, even if it's a primitive.*/

		/*public void PassByRefInPrimitive(in int num)
		{
			int numNext = 5;

			num = numNext;
		}*/

		public static void PassByModifyTempObj(List<ObjTyped> objList, int objIndex)
		{
			ObjTyped tempObj = objList.Where(a => a.ObjId == objIndex).FirstOrDefault();

			tempObj.Field = "Something";


			//return tempObj;
		}

		public static List<ObjTyped> GenerateObjTypedList(int intendedSize)
		{
			List<ObjTyped> objList = new List<ObjTyped>();
			MASTERSTATE intendedObjType;

			for (int i = 0; i < intendedSize; i++)
			{
				intendedObjType = (i >= (intendedSize * 0.5)) ? MASTERSTATE.VAL2 : MASTERSTATE.VAL1;

				objList.Add
					(
						new ObjTyped()
						{
							ObjId = i,
							ObjType = intendedObjType,
							Field = "Nothing"
						}
					);
			}

			return objList;
		}

		public static List<ObjTyped> GenerateObjTypedSubList(MASTERSTATE intendedType, List<ObjTyped> inputList)
		{
			List<ObjTyped> subList = new List<ObjTyped>();

			foreach(ObjTyped obj in inputList)
			{
				if (obj.ObjType == intendedType)
					subList.Add(obj);
			}

			return subList;
		}

		public static void PassByRefStructSubStructFields(ref TestStruct str)
		{
			str.subStructVar.FieldVar = "Something";
			str.subStructVar.FieldProp = "Something";

			//Won't work because get set struct properties are not considered variables
			//str.subStructProp.FieldVar = "Something";
			//str.subStructProp.FieldProp = "Something";
		}

		public static void PassByRefStructObjFields(ref TestStruct str)
		{
			str.obj.Field1 = "Something";
			str.obj.Field2 = "Something";
		}

		public static void PassByRefStructFields(ref TestStruct str)
		{
			str.bTestVar = true;
			str.bTestProp = true;
			str.FieldVar = "Something";
			str.FieldProp = "Something";
		}

		public static void PassByStructSubStructFields(TestStruct str)
		{
			str.subStructVar.FieldVar = "Something";
			str.subStructVar.FieldProp = "Something";

			//Won't work because get set struct properties are not considered variables
			//str.subStructProp.FieldVar = "Something";
			//str.subStructProp.FieldProp = "Something";
		}

		public static void PassByStructObjFields(TestStruct str)
		{
			str.obj.Field1 = "Something";
			str.obj.Field2 = "Something";
		}

		public static void PassByStructFields(TestStruct str)
		{
			str.bTestVar = true;
			str.bTestProp = true;
			str.FieldVar = "Something";
			str.FieldProp = "Something";
		}

		//All valid statements, no read-only variable build errors, even for primitive collections.
		public static void PassByRefPrimitiveCollection(in SubObj obj, int valToAdd)
		{
			obj.PastIds.Add(valToAdd);

			//List<int> nums = new List<int>();
			//obj.PastIds = nums;
		}

		public static void PassByRefPrimitive(ref int num, int valToBe)
		{
			num = valToBe;
		}

		public static void PassByRefObj(ref string field)
		{
			field = "Something";

			string tempField = field;
		}

		public static void PassByNumObj(ObjTyped obj)
		{
			obj.ObjId++;
		}

		public static void ResortByActivate(ObjTyped[] inputObjs, int curIndex, ref int lastActiveIndex,
			ObjTyped curLiveObj)
		{
			if (curIndex == lastActiveIndex + 1)
			{
				lastActiveIndex += 1;
				return;
			}

			inputObjs[curIndex] = inputObjs[lastActiveIndex + 1];
			inputObjs[lastActiveIndex + 1] = curLiveObj;
			lastActiveIndex += 1;
		}

		public static void PassByNum(ref int num)
		{
			int tempNum = num;
			num++;
		}

		public static void PassByObjInstantiate(TestObj obj)
		{
			obj = new TestObj()
			{
				Field2 = "Instantiated"
			};
		}

		public static void PassByObj(ref TestObj obj)
		{
			obj.Field2 = "Something";
		}

		public static void PassByRefInFlagToggle(in TestObj obj)
		{
			obj.Flag1 = !obj.Flag1;

			/*Instantiating new objects or collections withing the parameter object, or even setting them to
			pre-instantiated ones, is acceptable with the in modifier - all in prevents is the instantiation of the
			parameter object, setting it to a new, pre-instantiated one, or nulling it.*/

			//All valid, no read-only variable build errors.
			/*SubObj tempSubObj = new SubObj();
			List<SubObj> tempPastObjs = new List<SubObj>();
			List<string> tempFields = new List<string>();*/

			//TestObj tempObj = new TestObj();

			//Invalid, will lead to a read-only variable build error.
			//obj = null;
		}
	}
}
