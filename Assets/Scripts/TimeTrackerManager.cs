using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeTrackerManager : MonoBehaviour
{
	public static TimeTrackerManager Instance;
	public GameObject timerHUD;

	private GameObject _currentUI;

	private float _sceneTime = 0f;
	private float _todayTime = 0f;
	private float _lifetimeTime = 0f;
	private DateTime _sessionStart;

	private string _lastScene;

	private const string LIFETIME_KEY = "LifetimeTime";
	private const string TODAY_KEY = "TodayTime";
	private const string LAST_PLAYED_DATE_KEY = "LastPlayedDate";

	private float _updateTimer = 0f;
	private float _saveTimer = 0f;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		_sessionStart = DateTime.Now;
		_todayTime = PlayerPrefs.GetFloat(TODAY_KEY, 0f);
		_lifetimeTime = PlayerPrefs.GetFloat(LIFETIME_KEY, 0f);

		string savedDate = PlayerPrefs.GetString(LAST_PLAYED_DATE_KEY, "");
		if (!string.IsNullOrEmpty(savedDate) && DateTime.TryParse(savedDate, out DateTime parsedDate))
		{
			if (parsedDate.Date != DateTime.Today)
				_todayTime = 0f;
		}

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		_lastScene = scene.name;
		_sceneTime = 0f;

		if (scene.name == "LoadingScene") // Skip UI in loading scene
		{
			if (_currentUI != null)
				Destroy(_currentUI);
			return;
		}

		InjectUI();
	}

	void InjectUI()
	{
		if (_currentUI != null)
			Destroy(_currentUI);

		GameObject canvas = GameObject.Find("Canvas");
		if (canvas == null)
		{
			canvas = new GameObject("Canvas", typeof(Canvas));
			canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
		}

		_currentUI = Instantiate(timerHUD, canvas.transform);
	}

	void Update()
	{
		float delta = Time.unscaledDeltaTime;
		_sceneTime += delta;
		_todayTime += delta;
		_lifetimeTime += delta;

		_updateTimer += delta;
		_saveTimer += delta;

		if (_updateTimer >= 1f)
		{
			UpdateText();
			_updateTimer = 0f;
		}

		if (_saveTimer >= 1f)
		{
			PlayerPrefs.SetFloat(TODAY_KEY, _todayTime);
			PlayerPrefs.SetFloat(LIFETIME_KEY, _lifetimeTime);
			PlayerPrefs.SetString(LAST_PLAYED_DATE_KEY, DateTime.Today.ToString());
			PlayerPrefs.Save();
			_saveTimer = 0f;
		}
	}

	void UpdateText()
	{
		if (_currentUI == null) return;

		float sessionTime = Mathf.Ceil((float)(DateTime.Now - _sessionStart).TotalSeconds);

		_currentUI.transform.Find("Text_Scene").GetComponent<TextMeshProUGUI>().text = "Scene Time = " + FormatTime(_sceneTime);
		_currentUI.transform.Find("Text_Session").GetComponent<TextMeshProUGUI>().text = "Session Time = " + FormatTime(sessionTime);
		_currentUI.transform.Find("Text_Today").GetComponent<TextMeshProUGUI>().text = "Today Time = " + FormatTime(_todayTime);
		_currentUI.transform.Find("Text_Lifetime").GetComponent<TextMeshProUGUI>().text = "Lifetime Time = " + FormatTime(_lifetimeTime);
	}

	string FormatTime(float seconds)
	{
		TimeSpan time = TimeSpan.FromSeconds(seconds);
		if (time.TotalDays >= 1)
			return $"{(int)time.TotalDays}d {time.Hours}h {time.Minutes}m {time.Seconds}s";
		else
			return $"{(int)time.TotalHours}h {time.Minutes}m {time.Seconds}s";
	}
}