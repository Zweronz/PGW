using UnityEngine;

[RequireComponent(typeof(BotMovement))]
public class BotAI : MonoBehaviour
{
	private bool bool_0;

	private bool bool_1;

	public Transform Target;

	private Transform transform_0;

	private BotMovement botMovement_0;

	private BotHealth botHealth_0;

	private BotTrigger botTrigger_0;

	public Transform homePoint;

	private void Awake()
	{
		if (Defs.bool_4)
		{
			base.enabled = false;
		}
	}

	private void Start()
	{
		Target = null;
		botMovement_0 = GetComponent<BotMovement>();
		botHealth_0 = GetComponent<BotHealth>();
		transform_0 = base.transform;
		botTrigger_0 = GetComponent<BotTrigger>();
	}

	private void Update()
	{
		if (!botHealth_0.getIsLife() && !bool_1)
		{
			SendMessage("Death");
			botTrigger_0.shouldDetectPlayer = false;
			bool_1 = true;
		}
	}

	public void SetTarget(Transform transform_1, bool bool_2)
	{
		bool_0 = bool_2;
		Target = transform_1;
		botMovement_0.SetTarget(Target, bool_2);
	}
}
