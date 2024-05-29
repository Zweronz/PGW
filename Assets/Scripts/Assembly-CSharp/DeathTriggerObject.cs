using engine.helpers;

public class DeathTriggerObject : TriggerObject
{
	public override void OnEnter(FirstPersonPlayerController firstPersonPlayerController_0)
	{
		Log.AddLine("DeathTriggerObject::OnEnter > name: " + base.gameObject.name);
		firstPersonPlayerController_0.Player_move_c_0.PlayerParametersController_0.ForceKillPlayer();
		firstPersonPlayerController_0.Player_move_c_0.PlayerScoreController_0.SelfKill();
		firstPersonPlayerController_0.Player_move_c_0.sendImDeath("my_nick", 0);
	}
}
