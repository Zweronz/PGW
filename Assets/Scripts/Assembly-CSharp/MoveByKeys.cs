using Photon;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class MoveByKeys : Photon.MonoBehaviour
{
	public float float_0 = 10f;

	private void Update()
	{
		if (Input.GetKey(KeyCode.A))
		{
			base.transform.position += Vector3.left * (float_0 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D))
		{
			base.transform.position += Vector3.right * (float_0 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.W))
		{
			base.transform.position += Vector3.forward * (float_0 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S))
		{
			base.transform.position += Vector3.back * (float_0 * Time.deltaTime);
		}
	}

	private void Start()
	{
		base.enabled = base.PhotonView_0.Boolean_1;
	}
}
