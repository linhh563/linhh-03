using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

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
    public SavedGame savedGame {get; private set;} = new SavedGame();

    private void Awake() {
        CreateInstance();
        DontDestroyOnLoad(this.gameObject);

        hasSavedGame = false;
    }

    private void Start() {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    private void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
