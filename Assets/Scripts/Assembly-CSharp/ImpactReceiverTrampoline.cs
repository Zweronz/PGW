using UnityEngine;

public class ImpactReceiverTrampoline : MonoBehaviour
{
	private float float_0 = 1f;

	private Vector3 vector3_0 = Vector3.zero;

	private CharacterController characterController_0;

	private void Start()
	{
		characterController_0 = GetComponent<CharacterController>();
	}

	private void Update()
	{
		if (vector3_0.magnitude > 0.2f)
		{
			characterController_0.Move(vector3_0 * Time.deltaTime);
		}
		else
		{
			Object.Destroy(this);
		}
		vector3_0 = Vector3.Lerp(vector3_0, Vector3.zero, 1f * Time.deltaTime);
	}

	public void AddImpact(Vector3 vector3_1, float float_1)
	{
		vector3_1.Normalize();
		if (vector3_1.y < 0f)
		{
			vector3_1.y = 0f - vector3_1.y;
		}
		vector3_0 += vector3_1.normalized * float_1 / float_0;
	}
}
