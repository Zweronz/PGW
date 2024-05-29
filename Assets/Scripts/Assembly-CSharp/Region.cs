using System;

public class Region
{
	public CloudRegionCode cloudRegionCode_0;

	public string string_0;

	public int int_0;

	public static CloudRegionCode Parse(string string_1)
	{
		string_1 = string_1.ToLower();
		CloudRegionCode result = CloudRegionCode.none;
		if (Enum.IsDefined(typeof(CloudRegionCode), string_1))
		{
			result = (CloudRegionCode)(int)Enum.Parse(typeof(CloudRegionCode), string_1);
		}
		return result;
	}

	public override string ToString()
	{
		return string.Format("'{0}' \t{1}ms \t{2}", cloudRegionCode_0, int_0, string_0);
	}
}
