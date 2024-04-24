using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.WSA;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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
    // private bool instanceExist = false;

    private void Start() {
        CreateInstance();
        hasSavedGame = false;
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    private void CreateInstance()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;

            savedGame = new SavedGame();

            Debug.Log("Create Instance");
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

        Debug.Log("total: " + savedGame.totalPebble + " , current: " + savedGame.currentPebble);

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

    public void QuitGame()
    {
        UnityEngine.Application.Quit();
    }
}
