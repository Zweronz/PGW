using UnityEngine;

public class PropertyBinding : MonoBehaviour
{
	public enum UpdateCondition
	{
		OnStart = 0,
		OnUpdate = 1,
		OnLateUpdate = 2,
		OnFixedUpdate = 3
	}

	public enum Direction
	{
		SourceUpdatesTarget = 0,
		TargetUpdatesSource = 1,
		BiDirectional = 2
	}

	public PropertyReference source;

	public PropertyReference target;

	public Direction direction;

	public UpdateCondition update = UpdateCondition.OnUpdate;

	public bool editMode = true;

	private object object_0;

	private void Start()
	{
		UpdateTarget();
		if (update == UpdateCondition.OnStart)
		{
			base.enabled = false;
		}
	}

	private void Update()
	{
		if (update == UpdateCondition.OnUpdate)
		{
			UpdateTarget();
		}
	}

	private void LateUpdate()
	{
		if (update == UpdateCondition.OnLateUpdate)
		{
			UpdateTarget();
		}
	}

	private void FixedUpdate()
	{
		if (update == UpdateCondition.OnFixedUpdate)
		{
			UpdateTarget();
		}
	}

	private void OnValidate()
	{
		if (source != null)
		{
			source.Reset();
		}
		if (target != null)
		{
			target.Reset();
		}
	}

	[ContextMenu("Update Now")]
	public void UpdateTarget()
	{
		if (source == null || target == null || !source.Boolean_0 || !target.Boolean_0)
		{
			return;
		}
		if (direction == Direction.SourceUpdatesTarget)
		{
			target.Set(source.Get());
		}
		else if (direction == Direction.TargetUpdatesSource)
		{
			source.Set(target.Get());
		}
		else
		{
			if (source.GetPropertyType() != target.GetPropertyType())
			{
				return;
			}
			object obj = source.Get();
			if (object_0 != null && object_0.Equals(obj))
			{
				obj = target.Get();
				if (!object_0.Equals(obj))
				{
					object_0 = obj;
					source.Set(obj);
				}
			}
			else
			{
				object_0 = obj;
				target.Set(obj);
			}
		}
	}
}
