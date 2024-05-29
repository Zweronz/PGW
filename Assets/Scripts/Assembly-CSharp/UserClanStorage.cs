using System;
using System.Runtime.CompilerServices;
using engine.data;

public sealed class UserClanStorage : UserStorage<string, UserClanData>
{
	[CompilerGenerated]
	private static Func<UserClanData, HashUniqueIndex<string, UserClanData>, string> func_0;

	public UserClanStorage()
	{
		AddIndex(new HashUniqueCallbackIndex<string, UserClanData>((UserClanData userClanData_0, HashUniqueIndex<string, UserClanData> hashUniqueIndex_0) => userClanData_0.string_0));
	}
}
