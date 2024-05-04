using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SavedSetting savedSetting = new SavedSetting();

    [Header("Audio")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioMixer audioMixer;
    public AudioClip backgroundMusic;
    public AudioClip clickBtnSfx;
    public float musicVolume {get; private set;}

    // [Header("Game")]
    public bool hasSavedGame {get; private set;}
    public SavedGame savedGame {get; private set;}

    private void Start() {
        CreateInstance();
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    private void CreateInstance()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;

            LoadFromJson();
        }
    }

    public void PlayClickBtnSfx()
    {
        sfxSource.PlayOneShot(clickBtnSfx);
    }

    public void SetGameStyle(int value)
    {
        if (value == 1)
        {
            savedGame.SetGameStyle(GameStyle.PvB);
        }
        else
        {
            savedGame.SetGameStyle(GameStyle.PvP);
        }
    }

    public void SetPebble(int pebble)
    {
        savedGame.SetTotalPebble(pebble);
        savedGame.SetCurrentPebble(pebble);
    }

    public void SetPlayerTurn(int turn)
    {
        savedGame.SetCurrentTurn(turn);
    }

    public void StartGame()
    {
        PlayClickBtnSfx();

        if (savedGame.gameStyle == GameStyle.Null || savedGame.totalPebble == 0 || savedGame.currentTurn == 0)
        {
            return;
        }

        PlayClickBtnSfx();
        SceneManager.LoadScene("Gameplay");
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        audioMixer.SetFloat("music", Mathf.Log10(musicVolume) * 20);
    }

    public void SaveGame(GameStyle style, int total, int currentPebble, int currentTurn, List<Turn> log, int turnPointer, int numberPebbleTaken)
    {
        this.savedGame.SetGameStyle(style);
        this.savedGame.SetTotalPebble(total);
        this.savedGame.SetCurrentPebble(currentPebble);
        this.savedGame.SetCurrentTurn(currentTurn);
        this.savedGame.SetTurnLog(log);
        this.savedGame.SetTurnPointer(turnPointer);
        this.savedGame.SetNumberPebbleTaken(numberPebbleTaken);

        hasSavedGame = true;
    }

    public void SetHasSavedGame(bool state)
    {
        hasSavedGame = state;
    }

    private void SaveData()
    {
        savedSetting.musicVolume = this.musicVolume;
        savedSetting.hasSavedGame = this.hasSavedGame;

        if (!this.hasSavedGame)
        {
            savedSetting.gameStyle = GameStyle.Null;
            savedSetting.totalPebble = 0;
            savedSetting.currentPebble = 0;
            savedSetting.currentTurn = 0;
            savedSetting.turnPointer = -1;
            savedSetting.numberPebbleTaken = 0;
        }
        else
        {
            savedSetting.gameStyle = this.savedGame.gameStyle;
            savedSetting.totalPebble = this.savedGame.totalPebble;
            savedSetting.currentPebble = this.savedGame.currentPebble;
            savedSetting.currentTurn = this.savedGame.currentTurn;
            savedSetting.turnLog = this.savedGame.turnLog;
            savedSetting.turnPointer = this.savedGame.turnPointer;
            savedSetting.numberPebbleTaken = this.savedGame.numberPebbleTaken;
        }
    }

    private void SaveToJson()
    {
        string settingData = JsonUtility.ToJson(savedSetting);
        string filePath = UnityEngine.Application.persistentDataPath + "/SettingData.json";
        // Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, settingData);
    }

    private void LoadData()
    {
        this.musicVolume = savedSetting.musicVolume;
        SetHasSavedGame(savedSetting.hasSavedGame);

        savedGame = new SavedGame();
        if (hasSavedGame)
        {
            savedGame.SetGameStyle(savedSetting.gameStyle);
            savedGame.SetTotalPebble(savedSetting.totalPebble);
            savedGame.SetCurrentPebble(savedSetting.currentPebble);
            savedGame.SetCurrentTurn(savedSetting.currentTurn);
            savedGame.SetTurnLog(savedSetting.turnLog);
            savedGame.SetTurnPointer(savedSetting.turnPointer);
            savedGame.SetNumberPebbleTaken(savedSetting.numberPebbleTaken);
        }
    }

    private void LoadFromJson()
    {
        string filePath = UnityEngine.Application.persistentDataPath + "/SettingData.json";
        string settingData = System.IO.File.ReadAllText(filePath);

        savedSetting = JsonUtility.FromJson<SavedSetting>(settingData);
        LoadData();
    }

    public void QuitGame()
    {
        SaveData();
        SaveToJson();
        UnityEngine.Application.Quit();
    }
}


[System.Serializable]
public class SavedSetting {
    public float musicVolume;
    public bool hasSavedGame;
    public GameStyle gameStyle;
    public int totalPebble;
    public int currentPebble;
    public int currentTurn;
    public List<Turn> turnLog = new List<Turn>();
    public int turnPointer;
    public int numberPebbleTaken;
}