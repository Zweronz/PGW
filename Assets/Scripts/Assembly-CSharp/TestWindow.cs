using System.Collections.Generic;
using engine.unity;

public class TestWindow : BaseGameWindow
{
	private static TestWindow testWindow_0;

	public TestDataSource testDataSource_0;

	public TestDataSource testDataSource_1;

	public static TestWindow TestWindow_0
	{
		get
		{
			return testWindow_0;
		}
	}

	public static void Show(WindowShowParameters windowShowParameters_1 = null)
	{
		if (!(testWindow_0 != null))
		{
			testWindow_0 = BaseWindow.Load("TestWindow") as TestWindow;
			testWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			testWindow_0.Parameters_0.bool_5 = false;
			testWindow_0.Parameters_0.bool_0 = true;
			testWindow_0.Parameters_0.bool_6 = true;
			testWindow_0.InternalShow(windowShowParameters_1);
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		Init();
	}

	public override void OnHide()
	{
		base.OnHide();
		testWindow_0 = null;
	}

	private void Init()
	{
		List<int> list = new List<int>();
		list.Add(1);
		list.Add(2);
		list.Add(3);
		list.Add(4);
		list.Add(5);
		list.Add(6);
		list.Add(7);
		list.Add(8);
		list.Add(9);
		list.Add(10);
		list.Add(11);
		list.Add(12);
		List<int> list_ = list;
		testDataSource_1.Init();
		testDataSource_1.SetData(list_);
	}
}
