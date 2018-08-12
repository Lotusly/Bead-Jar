 using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

	public static Manager Instance;
	private string filePath = "HighScores.dat";
	// TMP serialize
	[SerializeField]private float previousHighScore;

	private bool[] _achievements;

	// TMP serialize
	[SerializeField] private float _remainingTime;
	


	private bool _countTime = false;
	private bool _fixResource = false;
	private bool _gameOver = false;

	public bool CountTime
	{
		set { _countTime = value; }
		get { return _countTime; }
	}
	
	public bool FixResource
	{
		set { _fixResource = value; }
		get { return _fixResource; }
	}
	

	private bool _inGame = true;

	private float _resource = 10;

	private float _score = 0;

	public float RemainingTime
	{
		get { return this._remainingTime; }
		set { this._remainingTime = value; }
	}

	void Awake()
	{
		if (Instance == null) Instance = this;
		previousHighScore = LoadHighScore();
		_achievements = new bool[7];
		for (int i = 0; i < 7; i++)
		{
			_achievements[i] = false;
		}
	}

	void Start()
	{
		CanvasManager.Instance.SetTime(_remainingTime);
		CanvasManager.Instance.SetResource(_resource);
		CanvasManager.Instance.SetScore(_score);
	}

	void Update()
	{
		if (_countTime)
		{
			_remainingTime -= Time.deltaTime;
			UpdateTime();
			if (!_fixResource)
			{
				_resource += Backpack.Instance.GetTotalResourceMul() * Time.deltaTime - Time.deltaTime;
				UpdateResource();
			}
			_score += Backpack.Instance.GetTotalContentmentMul() * Time.deltaTime;
			UpdateScore();
			if (_remainingTime <= 0 || _resource<0)
			{
				EndGame();
			}
		}
	}

	private void SaveHighScore()
	{
		FileStream scoreFile;
		if (!File.Exists(filePath))
		{
			scoreFile = File.Create(filePath);
		}
		else scoreFile = File.OpenWrite(filePath);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(scoreFile, _score);
		scoreFile.Close();
	}

	private float LoadHighScore()
	{
		if (File.Exists(filePath))
		{
			FileStream scoreFile = File.OpenRead(filePath);
			BinaryFormatter bf = new BinaryFormatter();
			float result = (float) bf.Deserialize(scoreFile);
			scoreFile.Close();
			return result;
		}
		else return 0;
	}
	
	public void SetCountTime(bool newCT)
	{
		_countTime = newCT;
	}

	public void SetFixResource(bool newFR)
	{
		_fixResource = newFR;
		if (newFR)
		{
			CanvasManager.Instance.SetResource(-1);
		}
		else
		{
			_resource = Mathf.Min(10,_resource);
			CanvasManager.Instance.SetResource(_resource);
		}
	}

	private void UpdateTime()
	{
		CanvasManager.Instance.SetTime(_remainingTime);
	}

	private void UpdateResource()
	{
		if (!_achievements[0]&&_resource > 1000)
		{
			ItemManager.Instance.PlaceItem(0);
			GetAchievement(0);
		}
		CanvasManager.Instance.SetResource(_resource);
	}
	
	private void UpdateScore()
	{
		CanvasManager.Instance.SetScore(_score);
	}

	public void GetAchievement(int index)
	{
		_achievements[index] = true;
		CanvasManager.Instance.ShowAchievement(index);
	}

	public void EndGame()
	{
		_countTime = false;
		CharacterGroup.Instance.SetControllable(false);
		CanvasManager.Instance.ShowEndGame();
		if (_score > previousHighScore)
		{
			SaveHighScore();
		}
		CanvasManager.Instance.SetSystemMessage("History High Score: " + previousHighScore);
		_gameOver = true;
	}

	public void OnSpaceDown()
	{
		if(_gameOver) SceneManager.LoadScene("Main");
	}
}
