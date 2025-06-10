using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTextList", menuName = "Scriptable Objects/Text List")]
public class RandomTextSO : ScriptableObject
{
	[TextArea(3, 10)]
	public List<string> texts = new List<string>();

	public string GetRandomText()
	{
		if (texts == null || texts.Count == 0)
		{
			Debug.LogWarning("RandomTextSO: The text list is empty or null. Returning empty string.");
			return string.Empty;
		}
		int randomIndex = Random.Range(0, texts.Count);
		return texts[randomIndex];
	}
}
