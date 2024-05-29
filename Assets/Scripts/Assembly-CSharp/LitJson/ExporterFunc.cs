namespace LitJson
{
	internal delegate void ExporterFunc(object object_0, JsonWriter jsonWriter_0);
	public delegate void ExporterFunc<T>(T gparam_0, JsonWriter jsonWriter_0);
}
