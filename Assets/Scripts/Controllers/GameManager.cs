using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Editor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    public AudioClip backgroundMusic;
    public AudioClip clickBtnSfx;
    public bool hasSavedGame {get; private set;}
    public SavedGame savedGame {get; private set;}

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
}
