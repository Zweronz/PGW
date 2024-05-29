using System;
using System.Runtime.CompilerServices;
using engine.data;

public sealed class UserArtikulStorage : UserStorage<string, UserArtikul>
{
	public enum Indexes
	{
		ArtikulId = 0,
		SlotType = 1
	}

	[CompilerGenerated]
	private static Func<UserArtikul, HashUniqueIndex<int, UserArtikul>, int> func_0;

	[CompilerGenerated]
	private static Func<UserArtikul, HashIndex<SlotType, UserArtikul>, SlotType> func_1;

	public UserArtikulStorage()
	{
		AddIndex(new HashUniqueCallbackIndex<int, UserArtikul>((UserArtikul userArtikul_0, HashUniqueIndex<int, UserArtikul> hashUniqueIndex_0) => userArtikul_0.int_0));
		AddIndex(new HashCallbackIndex<SlotType, UserArtikul>(delegate(UserArtikul userArtikul_0, HashIndex<SlotType, UserArtikul> hashIndex_0)
		{
			ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(userArtikul_0.int_0);
			return artikul.SlotType_0;
		}));
	}
}
