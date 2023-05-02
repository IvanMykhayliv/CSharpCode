using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTE.Testing.Unit.Collections.Helper
{
	public class StackHelper
	{

	}

	public class StackArray
	{
		private int?[] stackA;

		public StackArray(int?[] inputStackA)
		{
			stackA = inputStackA != null ? inputStackA : new int?[1];
		}

		public void Push(int nextI)
		{
			if (stackA[stackA.Length - 1] == null)
			{
				for (int i = 0; i < stackA.Length; i++)
				{
					if (stackA[i] == null)
					{
						stackA[i] = nextI;
						break;
					}
				}
			}
			else
			{
				int?[] nextStack = new int?[stackA.Length + 1];

				for (int i = 0; i < stackA.Length; i++)
				{
					nextStack[i] = stackA[i];
				}

				nextStack[nextStack.Length - 1] = nextI;


				stackA = nextStack;
			}
		}

		public void Pop()
		{
			if(stackA[stackA.Length - 1] == null)
			{
				for (int i = 0; i < stackA.Length - 1; i++)
				{
					if(stackA[i] != null && stackA[i + 1] == null)
					{
						stackA[i] = null;
						break;
					}
				}
			}
			else
			{
				stackA[stackA.Length - 1] = null;
			}
		}
	}

	public class StackList
	{
		private List<int> stackL;

		public StackList(List<int> inputStackL)
		{
			stackL = inputStackL != null ? inputStackL : new List<int>();
		}

		public void Push(int nextI)
		{
			stackL.Add(nextI);
		}

		public void Pop()
		{
			stackL.Remove(stackL.Last());
		}
	}
}
