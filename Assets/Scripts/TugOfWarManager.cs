using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TugOfWarManager : MonoBehaviour
{
	[Header("Game Settings")]
	[SerializeField] private float matchDuration = 30f;      // Total match time
	[SerializeField] private float tapPower = 0.1f;          // Amount of movement per tap

	[Header("References")]
	[SerializeField] private Transform ropeTransform;        // The center transform of the rope
	[SerializeField] private Transform ropeMarker;
	[SerializeField] private Transform topLine;              // Opponent win line
	[SerializeField] private Transform bottomLine;           // Your win line

	[SerializeField] private RectTransform topTapArea;       // Opponent tap area (top half of screen)
	[SerializeField] private RectTransform bottomTapArea;    // Your tap area (bottom half of screen)

	[Header("UI (Optional)")]
	[SerializeField] private TextMeshProUGUI timerText;
	[SerializeField] private TextMeshProUGUI resultText;
	[SerializeField] private Button restartButton;

	private float _ropePositionY;
	private float _timer;
	private bool _gameOver = false;

	void Start()
	{
		_timer = matchDuration;
		_ropePositionY = ropeTransform.position.y;
		resultText.text = "";
	}

	void Update()
	{
		if (_gameOver) return;

		_timer -= Time.deltaTime;
		if (_timer <= 0f)
		{
			_timer = 0f;
			EndGame("Timeout!");
		}

		UpdateInput();
		CheckWinCondition();
		UpdateTimerUI();
	}

	void UpdateInput()
	{
#if UNITY_EDITOR || UNITY_STANDALONE
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePos = Input.mousePosition;
			if (RectTransformUtility.RectangleContainsScreenPoint(topTapArea, mousePos))
			{
				MoveRope(tapPower); // Opponent taps
			}
			else if (RectTransformUtility.RectangleContainsScreenPoint(bottomTapArea, mousePos))
			{
				MoveRope(-tapPower); // You tap
			}
		}
#else
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				Vector2 touchPos = touch.position;
				if (RectTransformUtility.RectangleContainsScreenPoint(topTapArea, touchPos))
				{
					MoveRope(tapPower); // Opponent taps
				}
				else if (RectTransformUtility.RectangleContainsScreenPoint(bottomTapArea, touchPos))
				{
					MoveRope(-tapPower); // You tap
				}
			}
		}
#endif
	}

	void MoveRope(float deltaY)
	{
		_ropePositionY += deltaY;

		// Move the rope
		ropeTransform.position = new Vector3(
			ropeTransform.position.x,
			_ropePositionY,
			ropeTransform.position.z
		);
	}

	void CheckWinCondition()
	{
		float markerY = ropeMarker.position.y;

		if (markerY >= topLine.position.y)
		{
			EndGame("You Lose!");
		}
		else if (markerY <= bottomLine.position.y)
		{
			EndGame("You Win!");
		}
	}

	void EndGame(string result)
	{
		_gameOver = true;
		resultText.text = result;
		restartButton.gameObject.SetActive(true);
		Debug.Log(result);
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void UpdateTimerUI()
	{
		if (timerText != null)
		{
			timerText.text = _timer.ToString("F1") + "s";
		}
	}
}
