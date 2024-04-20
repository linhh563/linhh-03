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

    [Header("Button Borders")]
    [SerializeField] private GameObject pvpBtnBorder;
    [SerializeField] private GameObject pvbBtnBorder;
    [SerializeField] private GameObject _20PebbleBtnBorder;
    [SerializeField] private GameObject _30PebbleBtnBorder;
    [SerializeField] private GameObject _40PebbleBtnBorder;
    [SerializeField] private GameObject turn1BtnBorder;
    [SerializeField] private GameObject turn2BtnBorder;

    void Start()
    {
        InitializeLanguageDropdown();
    }

    void Update()
    {
        
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
        optionsErrorMsgPanel.SetActive(state);
        GameManager.Instance.PlayClickBtnSfx();
    }

    public void HighLightGameStyleBtn(int value)
    {
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
    }

    public void HighLightPebbleBtn(int value)
    {
        switch (value)
        {
            case 1:
                _20PebbleBtnBorder.SetActive(true);
                _30PebbleBtnBorder.SetActive(false);
                _40PebbleBtnBorder.SetActive(false);
                break;
            case 2:
                _20PebbleBtnBorder.SetActive(false);
                _30PebbleBtnBorder.SetActive(true);
                _40PebbleBtnBorder.SetActive(false);
                break;
            case 3:
                _20PebbleBtnBorder.SetActive(false);
                _30PebbleBtnBorder.SetActive(false);
                _40PebbleBtnBorder.SetActive(true);
                break;
        }

        GameManager.Instance.PlayClickBtnSfx();
    }

    public void HighLightTurnBtn(int value)
    {
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
    }
}
