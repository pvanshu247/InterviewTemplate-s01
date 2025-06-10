using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
	public void LoadNextScene()
	{
		SceneManager.LoadScene("LoadingScene");
	}
}
