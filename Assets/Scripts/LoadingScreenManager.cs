using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
	[SerializeField] private Slider progressBar;
	[SerializeField] private TextMeshProUGUI progressText;
	[SerializeField] private RandomTextSO textList;
	[SerializeField] private TextMeshProUGUI displayTextComponent;

	private const float MIN_LOADING_TIME = 2.0f;

	private void Start()
	{
		StartCoroutine("LoadGameSceneAsync");
		DisplayRandomText();
	}

	IEnumerator LoadGameSceneAsync()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync("Gameplay");
		operation.allowSceneActivation = false;

		float targetProgress = 0f;
		float currentProgress = 0f;
		float duration = 2f;
		float timer = 0f;

		while (!operation.isDone || timer < duration)
		{
			targetProgress = Mathf.Clamp01(operation.progress / 0.9f);
			currentProgress = Mathf.Lerp(currentProgress, targetProgress, Time.deltaTime * (1f / duration));

			if (operation.progress >= 0.9f && timer >= duration * 0.95f)
			{
				currentProgress = 1f;
			}

			progressBar.value = currentProgress;
			progressText.text = Mathf.RoundToInt(currentProgress * 100f) + "%";

			timer += Time.deltaTime;

			if (operation.progress >= 0.9f && timer >= duration)
			{
				operation.allowSceneActivation = true;
			}

			yield return null;
		}

		progressBar.value = 1f;
		progressText.text = "100%";
	}

	private void DisplayRandomText()
	{
		if (textList != null && displayTextComponent != null)
		{
			string randomText = textList.GetRandomText();
			displayTextComponent.text = randomText;
		}
		else
		{
			Debug.LogWarning("RandomTextDisplay: Cannot display text. Check if TextListSO and Display Text Component are assigned.");
		}
	}
}
