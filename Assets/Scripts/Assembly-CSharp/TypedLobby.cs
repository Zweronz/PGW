public class TypedLobby
{
	public string string_0;

	public LobbyType lobbyType_0;

	public static readonly TypedLobby typedLobby_0 = new TypedLobby();

	public bool Boolean_0
	{
		get
		{
			return lobbyType_0 == LobbyType.Default && string.IsNullOrEmpty(string_0);
		}
	}

	public TypedLobby()
	{
		string_0 = string.Empty;
		lobbyType_0 = LobbyType.Default;
	}

	public TypedLobby(string string_1, LobbyType lobbyType_1)
	{
		string_0 = string_1;
		lobbyType_0 = lobbyType_1;
	}

	public override string ToString()
	{
		return string.Format("Lobby '{0}'[{1}]", string_0, lobbyType_0);
	}
}
