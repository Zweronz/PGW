public interface IBasePartyController
{
	void SendRequestToUser(int type, int userId, string from_user_nick = "", int mode_id = 0, bool is_ranked = false);

	void GetResponseFromUser(int type, int userId, string from_user_nick = "", int mode_id = 0, bool is_ranked = false);

	bool SendInviteToFight(string fightId, string roomName, int type);

	void GetResponseToFight(string fightId, string roomName, int type, int from_user_id);
}
