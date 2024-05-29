using System.Collections;
using UnityEngine;

public class JKeepCharOnPlatform : MonoBehaviour
{
	public struct Data
	{
		public CharacterController characterController_0;

		public Transform transform_0;

		public float float_0;

		public Data(CharacterController characterController_1, Transform transform_1, float float_1)
		{
			characterController_0 = characterController_1;
			transform_0 = transform_1;
			float_0 = float_1;
		}
	}

	public float verticalOffset = 0.5f;

	private Hashtable hashtable_0 = new Hashtable();

	private Vector3 vector3_0;

	private void OnTriggerEnter(Collider collider_0)
	{
		CharacterController characterController = collider_0.GetComponent(typeof(CharacterController)) as CharacterController;
		if (!(characterController == null))
		{
			Transform transform_ = collider_0.transform;
			float float_ = characterController.height / 2f - characterController.center.y + verticalOffset;
			Data data = new Data(characterController, transform_, float_);
			hashtable_0.Add(collider_0.transform, data);
		}
	}

	private void OnTriggerExit(Collider collider_0)
	{
		hashtable_0.Remove(collider_0.transform);
	}

	private void Start()
	{
		vector3_0 = base.transform.position;
	}

	private void Update()
	{
		Vector3 position = base.transform.position;
		float y = position.y;
		Vector3 vector = position - vector3_0;
		float y2 = vector.y;
		vector.y = 0f;
		vector3_0 = position;
		foreach (DictionaryEntry item in hashtable_0)
		{
			Data data = (Data)item.Value;
			float y3 = data.characterController_0.velocity.y;
			if (y3 <= 0f || y3 <= y2)
			{
				Vector3 position2 = data.transform_0.position;
				position2.y = y + data.float_0;
				position2 += vector;
				data.transform_0.position = position2;
			}
		}
	}
}
