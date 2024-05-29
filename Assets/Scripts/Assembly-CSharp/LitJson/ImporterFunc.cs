namespace LitJson
{
	internal delegate object ImporterFunc(object object_0);
	public delegate TValue ImporterFunc<TJson, TValue>(TJson gparam_0);
}
