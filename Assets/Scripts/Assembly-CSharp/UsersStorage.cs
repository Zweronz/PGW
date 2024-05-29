using System;
using System.Runtime.CompilerServices;
using engine.data;

public sealed class UsersStorage : UserStorage<int, UserData>
{
	public enum Indexes
	{
		Uid = 0
	}

	[CompilerGenerated]
	private static Func<UserData, HashUniqueIndex<int, UserData>, int> func_0;

	public UsersStorage()
	{
		AddIndex(new HashUniqueCallbackIndex<int, UserData>((UserData userData_0, HashUniqueIndex<int, UserData> hashUniqueIndex_0) => userData_0.user_0.int_0));
	}
}
