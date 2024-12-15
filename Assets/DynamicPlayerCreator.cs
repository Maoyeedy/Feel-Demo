using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

/// <summary>
/// A test class that will automatically generate a MMF Player, containing as many scale feedbacks as this object has children.
/// To test it :
/// - create a new scene, and a new object in it. Add this component to it.
/// - create a bunch of cubes, position them where you want, then parent them to this object.
/// - press play in the editor, then press the "Create" button on this component's inspector
/// </summary>
public class DynamicPlayerCreator : MonoBehaviour
{
	// an inspector test button to trigger the Create method
	[MMInspectorButton("Create")] public bool CreateBtn;

	private void Create()
	{
		// we create a MMF Player
		MMF_Player newPlayer = this.gameObject.AddComponent<MMF_Player>();
		// we're going to play this MMF Player right after having created it, we'll initialize it manually.
		// we do this to avoid its Initialization method to run on its Start, which would be after we've played it
		newPlayer.InitializationMode = MMFeedbacks.InitializationModes.Script;

		if (transform.childCount == 0)
		{
			Debug.LogWarning("[DynamicCreation test class] to test this class, create a number of cubes, position them randomly, then parent them to this object.");
			return;
		}

		// for each child transform
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform childTransform = transform.GetChild(i);

			// we create a new scale feedback, tweak its target, and add it to the player
			MMF_Scale newScaleFeedback = new MMF_Scale();
			newScaleFeedback.Label = "Scale child " + i;
			newScaleFeedback.AnimateScaleTarget = childTransform;
			newPlayer.AddFeedback(newScaleFeedback);

			// we create a pause feedback, define its pause duration, and add it to the player
			MMF_Pause newPauseFeedback = new MMF_Pause();
			newPauseFeedback.Label = "Pause " + i;
			newPauseFeedback.PauseDuration = 0.2f;
			newPlayer.AddFeedback(newPauseFeedback);
		}

		// we initialize and play our player
		newPlayer.Initialization();
		newPlayer.PlayFeedbacks();
	}
}

