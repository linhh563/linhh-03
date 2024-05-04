using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Dropdown languageDropdown;
    [SerializeField] private Slider volumeSlider;

    [Header("UI Panels")]
    [SerializeField] private GameObject continueMsgPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject selectNewGameOptionsPanel;
    [SerializeField] private GameObject optionsErrorMsgPanel;
    [SerializeField] private GameObject loadScreen;

    [Header("Button Borders")]
    [SerializeField] private GameObject pvpBtnBorder;
    [SerializeField] private GameObject pvbBtnBorder;
    [SerializeField] private GameObject _20PebbleBtnBorder;
    [SerializeField] private GameObject _25PebbleBtnBorder;
    [SerializeField] private GameObject _30PebbleBtnBorder;
    [SerializeField] private GameObject turn1BtnBorder;
    [SerializeField] private GameObject turn2BtnBorder;

    void Start()
    {
        InitializeLanguageDropdown();
        SetAudioSlider();
    }

    void Update()
    {
        languageDropdown.onValueChanged.AddListener(delegate {
            GameManager.Instance.PlayClickBtnSfx();
        });
    }

    private void InitializeLanguageDropdown()
    {
        if (languageDropdown == null)
        {
            Debug.LogError("LANGUAGE DROPDOWN IS NOT SET !!!");
            return;
        }

        languageDropdown.options.Clear();
        languageDropdown.options.Add(new Dropdown.OptionData("TIẾNG VIỆT"));
        languageDropdown.options.Add(new Dropdown.OptionData("ENGLISH"));
    }

    public void ToggleSettingPanel(bool state)
    {
        settingPanel.SetActive(state);
        GameManager.Instance.PlayClickBtnSfx();
    }

    public void ToggleIsContinuePanel(bool state)
    {
        if (!GameManager.Instance.hasSavedGame)
        {
            ToggleOptionsPanel(true);
            return;
        }

        Debug.Log("has saved game: " + GameManager.Instance.hasSavedGame);
        continueMsgPanel.SetActive(state);
        GameManager.Instance.PlayClickBtnSfx();
    }
    public void ToggleOptionsPanel(bool state)
    {
        selectNewGameOptionsPanel.SetActive(state);
        GameManager.Instance.PlayClickBtnSfx();
    }

    public void ToggleOptionsErrorMsg(bool state)
    {
        if (state)
        {
            if (GameManager.Instance.savedGame.gameStyle == GameStyle.Null || GameManager.Instance.savedGame.totalPebble == 0 || GameManager.Instance.savedGame.currentTurn == 0)
            {
                optionsErrorMsgPanel.SetActive(state);
                GameManager.Instance.PlayClickBtnSfx();
            }
            return;
        }

        optionsErrorMsgPanel.SetActive(state);
        GameManager.Instance.PlayClickBtnSfx();
    }

    public void SetGameStyle(int value)
    {
        // high light button
        switch (value)
        {
            case 1: 
                pvpBtnBorder.SetActive(false);
                pvbBtnBorder.SetActive(true);
                break;
            case 2:
                pvpBtnBorder.SetActive(true);
                pvbBtnBorder.SetActive(false);
                break;
        }

        GameManager.Instance.PlayClickBtnSfx();
        GameManager.Instance.SetGameStyle(value);
    }

    public void SetPebble(int value)
    {
        // high light button
        switch (value)
        {
            case 20:
                _20PebbleBtnBorder.SetActive(true);
                _25PebbleBtnBorder.SetActive(false);
                _30PebbleBtnBorder.SetActive(false);
                break;
            case 25:
                _20PebbleBtnBorder.SetActive(false);
                _25PebbleBtnBorder.SetActive(true);
                _30PebbleBtnBorder.SetActive(false);
                break;
            case 30:
                _20PebbleBtnBorder.SetActive(false);
                _25PebbleBtnBorder.SetActive(false);
                _30PebbleBtnBorder.SetActive(true);
                break;
        }

        GameManager.Instance.PlayClickBtnSfx();
        GameManager.Instance.SetPebble(value);
    }

    public void SetPlayerTurn(int value)
    {

        // high light button
        switch (value)
        {
            case 1:
                turn1BtnBorder.SetActive(true);
                turn2BtnBorder.SetActive(false);
                break;
            case 2:
                turn1BtnBorder.SetActive(false);
                turn2BtnBorder.SetActive(true);
                break;
        }

        GameManager.Instance.PlayClickBtnSfx();
        GameManager.Instance.SetPlayerTurn(value);

    }

    public void SetMusicVolume()
    {
        GameManager.Instance.SetMusicVolume(volumeSlider.value);
    }

    public void ToggleLoadScreen(bool state)
    {
        loadScreen.SetActive(state);
    }

    private void SetAudioSlider()
    {
        volumeSlider.value = GameManager.Instance.musicVolume;
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}
