using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTE.Testing.Unit.States.Model;

namespace UTE.Testing.Unit.States.Helper
{
	public static class StateHelper
	{
		//Doesn't change inputState, inputState needs to be either out or ref
		public static void ChangeStateInner(MASTERSTATE inputState, MASTERSTATE outputState)
		{
			inputState = outputState;
		}

		public static MASTERSTATE ChangeStateSendBackInput(MASTERSTATE inputState, MASTERSTATE outputState)
		{
			inputState = outputState;

			return inputState;
		}

		public static MASTERSTATE ChangeStateSendDesiredOutput(MASTERSTATE outputState)
		{
			return outputState;
		}

		public static MASTERSTATE ChangeStateCreateAndSendDesiredOutput(MASTERSTATE outputState)
		{
			MASTERSTATE testState;

			testState = outputState;


			return testState;
		}

		public static string ProcessStates(MASTERSTATE inputState, MASTERSTATE testState, int style)
		{
			string outputStr = String.Empty;

			if (style == 1)
			{
				switch (inputState)
				{
					case MASTERSTATE.VAL1:
						outputStr = "VAL1";
						break;
					case MASTERSTATE.VAL2:
						outputStr = "VAL2";
						break;
					default:
						outputStr = "Default State";
						break;
				}
			}
			else if (style == 2)
			{
				if ((inputState & MASTERSTATE.VAL1) == MASTERSTATE.VAL1)
					outputStr += "VAL1";
				if ((inputState & MASTERSTATE.VAL2) == MASTERSTATE.VAL2)
					outputStr += "VAL2";
			}
			else if (style == 3)
			{
				switch (inputState & testState)
				{
					case MASTERSTATE.VAL1:
						outputStr = "VAL1";
						break;
					case MASTERSTATE.VAL2:
						outputStr = "VAL2";
						break;
					default:
						outputStr = "Default State";
						break;
				}

				float num1 = -30;
				float num2 = 2;

				float num3 = num1 + num2;

				int result = 5 % 4;

				bool a = true;
				bool b = true;

				switch(a || b)
				{
					case true:
						break;
				}

				switch(num1 + num2)
				{
					case float value when value < 0 && value > -2:
						break;
					case float value when value < 4 && value > 0:
						break;
					case float value when value < 6 && value > 4:
						break;
					default:
						outputStr = "Default State";
						break;
				}
			}

			return outputStr;
		}
	}
}
