using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class CameraInfo : MonoBehaviour
{
	public DepthTextureMode currentDepthMode;

	public RenderingPath currentRenderPath;

	public int recognizedPostFxCount;

	public virtual void Main()
	{
	}
}
