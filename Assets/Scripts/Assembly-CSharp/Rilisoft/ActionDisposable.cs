using System;

namespace Rilisoft
{
	internal sealed class ActionDisposable : IDisposable
	{
		private readonly Action action_0;

		private bool bool_0;

		public ActionDisposable(Action action_1)
		{
			action_0 = action_1;
		}

		public void Dispose()
		{
			if (!bool_0)
			{
				if (action_0 != null)
				{
					action_0();
				}
				bool_0 = true;
			}
		}
	}
}
