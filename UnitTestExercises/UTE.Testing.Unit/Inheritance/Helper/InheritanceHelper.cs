using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTE.Testing.Unit.Inheritance.Helper
{
	public static class InheritanceHelper
	{
		public static void AddToCollection(List<ParentObj> coll, ParentObj obj)
		{
			coll.Add(obj);
		}
	}

	public abstract class ParentObj
	{
		private int ID;

		public int Counter { get; set; }

		public ParentObj()
		{
			Counter = 50;
		}

		public virtual void DefaultFunc()
		{
			Counter++;
		}

		public int InternalID
		{
			get { return ID; }
			set { ID = value; }
		}

		public abstract void OutlineFunc();
	}

	public class ChildObj : ParentObj
	{
		public string Field { get; set; }

		public ChildObj()
		{
			Counter = 0;
			Field = "Nothing";
		}

		public override void DefaultFunc()
		{
			Counter += 2;
		}

		public override void OutlineFunc()
		{
			Field = "Something";
		}

		public void ChildFunc()
		{
			Field = "Something Else";
			InternalID++;
			Counter = 5;
		}
	}
}
