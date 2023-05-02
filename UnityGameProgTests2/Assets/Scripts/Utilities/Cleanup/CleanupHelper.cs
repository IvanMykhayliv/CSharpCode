using System;

public static class CleanupHelper
{
	public static void CollectGarbage()
	{
		GC.Collect();
	}

	public static void EmptyObj(object targetObj)
	{
		targetObj = null;
	}
}