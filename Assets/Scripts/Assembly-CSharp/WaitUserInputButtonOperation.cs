using System.Collections.Generic;
using engine.events;
using engine.operations;
using engine.unity;

public class WaitUserInputButtonOperation : Operation
{
	public enum ButtonState
	{
		Up = 0,
		Down = 1,
		Any = 2
	}

	private ButtonState buttonState_0 = ButtonState.Any;

	private List<string> list_1 = new List<string>();

	public WaitUserInputButtonOperation(ButtonState buttonState_1, params string[] string_1)
	{
		buttonState_0 = buttonState_1;
		if (string_1 != null)
		{
			list_1.AddRange(string_1);
		}
	}

	protected override void Execute()
	{
		if (list_1.Count == 0)
		{
			WaitComplete();
		}
		else if (!DependSceneEvent<MainUpdate>.Contains(Update))
		{
			DependSceneEvent<MainUpdate>.GlobalSubscribe(Update, true);
		}
	}

	private void Update()
	{
		bool flag = false;
		int num = 0;
		while (true)
		{
			if (num < list_1.Count)
			{
				switch (buttonState_0)
				{
				default:
					flag = InputManager.GetButtonAnyState(list_1[num]);
					break;
				case ButtonState.Up:
					flag = InputManager.GetButtonUp(list_1[num]);
					break;
				case ButtonState.Down:
					flag = InputManager.GetButtonDown(list_1[num]);
					break;
				case ButtonState.Any:
					flag = InputManager.GetButtonAnyState(list_1[num]);
					break;
				}
				if (flag)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		WaitComplete();
	}

	private void WaitComplete()
	{
		if (DependSceneEvent<MainUpdate>.Contains(Update))
		{
			DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
		}
		Complete();
	}
}
