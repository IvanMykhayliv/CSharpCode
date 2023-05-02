using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTE.Testing.Unit.States.Helper;
using UTE.Testing.Unit.States.Model;

namespace UTE.Testing.Unit.States.Exercises
{
	[TestClass]
	public class StateExercises
	{
		[TestMethod]
		public void StateProcess()
		{
			MASTERSTATE testState = MASTERSTATE.VAL1 | MASTERSTATE.VAL2;
			MASTERSTATE inputState = MASTERSTATE.VAL1;

			string lastStr = StateHelper.ProcessStates(testState, inputState, 3);


			Assert.AreEqual(lastStr, "VAL1VAL2");
		}

		[TestMethod]
		public void StateProcessByValRef()
		{
			MASTERSTATE inputState = MASTERSTATE.VAL1;
			MASTERSTATE outputState = MASTERSTATE.VAL2;

			//Doesn't change inputState
			StateHelper.ChangeStateInner(inputState, outputState);

			inputState = StateHelper.ChangeStateSendBackInput(inputState, outputState);

			Assert.AreEqual(inputState, outputState);
		}
	}
}
