using UnityEngine;

public class RocketSettings : MonoBehaviour
{
	public enum TypeFlyRocket
	{
		Rocket = 0,
		Grenade = 1,
		Bullet = 2,
		MegaBullet = 3
	}

	public DeadType typeDead = DeadType.EXPLOSION;

	public TypeFlyRocket typeFly;

	public Player_move_c.TypeKills typeKilsIconChat = Player_move_c.TypeKills.explosion;
}
