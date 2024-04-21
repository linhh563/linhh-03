using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    public AudioClip backgroundMusic;
    public AudioClip clickBtnSfx;
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
