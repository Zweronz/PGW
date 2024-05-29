using UnityEngine;

namespace DG.Tweening.Core
{
	public class DOTweenSettings : ScriptableObject
	{
		public const string AssetName = "DOTweenSettings";

		public bool useSafeMode = true;

		public bool showUnityEditorReport;

		public LogBehaviour logBehaviour = LogBehaviour.ErrorsOnly;

		public bool defaultRecyclable;

		public AutoPlay defaultAutoPlay = AutoPlay.All;

		public UpdateType defaultUpdateType;

		public bool defaultTimeScaleIndependent;

		public Ease defaultEaseType = Ease.OutQuad;

		public float defaultEaseOvershootOrAmplitude = 1.70158f;

		public float defaultEasePeriod;

		public bool defaultAutoKill = true;

		public LoopType defaultLoopType;
	}
}
