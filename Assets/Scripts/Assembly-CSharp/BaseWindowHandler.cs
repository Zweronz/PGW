using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class BaseWindowHandler
{
	[CompilerGenerated]
	private Vector2 vector2_0;

	public Vector2 Vector2_0
	{
		[CompilerGenerated]
		get
		{
			return vector2_0;
		}
		[CompilerGenerated]
		protected set
		{
			vector2_0 = value;
		}
	}

	public abstract void SetBorderlessStyle();

	public abstract void SetWindowPosition(Vector2 vector2_1);

	public abstract void MinimizeWindwow();

	public abstract void MaximizeWindwow();
}
