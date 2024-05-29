using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class StockDataStorage : BaseStorage<int, ActionData>
{
	private static StockDataStorage _instance;

	public static StockDataStorage Get
	{
		get
		{
			return _instance ?? (_instance = new StockDataStorage());
		}
	}

	private StockDataStorage()
	{
	}

	protected override void OnAddIndexes()
	{
	}
}
