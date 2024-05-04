using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUIController : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject gameActor1Border;
    [SerializeField] private GameObject gameActor2Border;
    [SerializeField] private GameObject numberTaken1UI;
    [SerializeField] private GameObject numberTaken2UI;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject pausePanel;

    [Header("Buttons")]
    [SerializeField] private Button undoBtn;
    [SerializeField] private Button redoBtn;
    [SerializeField] private Slider volumeSlider;

    [Header("Texts & Images")]
    [SerializeField] private Image avatar1;
    [SerializeField] private Image avatar2;
    [SerializeField] private Sprite botAvatar;
    [SerializeField] private Text currentPebbleTxt;
    [SerializeField] private Text actor1PebbleTakenTxt;
    [SerializeField] private Text actor2PebbleTakenTxt;
    [SerializeField] private Text endGameMsgTxt;

    private void Awake() {
        SetGameActorAvatar();
        SetAudioSlider();
    }
    
    private void Update() {
        UpdateCurrentPebbleText();
        EnableUndoBtn();
        EnableRedoBtn();
        EnableEndGameMsg();
    }

    public void EnablePauseUI(bool state)
    {

    }

    public void EnableUndoBtn()
    {
        undoBtn.interactable = GameController.Instance.CanUndo();
    }

    public void EnableRedoBtn()
    {
        redoBtn.interactable = GameController.Instance.CanRedo();
    }

    public void HighLightPlayer(int player)
    {
        switch (player)
        {
            case 1:
                gameActor1Border.SetActive(true);
                gameActor2Border.SetActive(false);
                break;
            case 2:
                gameActor1Border.SetActive(false);
                gameActor2Border.SetActive(true);
                break;
        }
    }

    private void UpdateCurrentPebbleText()
    {
        currentPebbleTxt.text = Storage.Instance.currentPebble.ToString();
    }

    public void ShowNumberPebbleTaken()
    {
        var playerTurn = GameController.Instance.playerInTurn;
        switch (playerTurn)
        {
            case 1:
                numberTaken1UI.SetActive(false);
                numberTaken2UI.SetActive(true);
                avatar1.color = new Color(1f, 1f, 1f, 1f);
                avatar2.color = new Color(0.7f, 0.7f, 0.7f, 1f);
                break;
            case 2:
                numberTaken1UI.SetActive(true);
                numberTaken2UI.SetActive(false);
                avatar1.color = new Color(0.7f, 0.7f, 0.7f, 1f);
                avatar2.color = new Color(1f, 1f, 1f, 1f);
                break;
        }
    }

    public void UpdateNumberTakenText(int player, int numberPebble)
    {
        switch (player)
        {
            case 1:
                actor1PebbleTakenTxt.text = numberPebble.ToString();
                break;
            case 2:
                actor2PebbleTakenTxt.text = numberPebble.ToString();
                break;
        }
    }

    public void HidePebbleTakenUI()
    {
        numberTaken1UI.SetActive(false);
        numberTaken2UI.SetActive(false);
    }

    public void SetGameActorAvatar()
    {
        if (GameManager.Instance.savedGame.gameStyle == GameStyle.PvB)
        {
            avatar2.sprite = botAvatar;
        }
    }

    public void EnableEndGameMsg()
    {
        if (GameController.Instance.winner  != 0)
        {
            endGameMsgTxt.text = "Người chơi " + GameController.Instance.winner + " chiến thắng!!!";
            endGamePanel.SetActive(true);
        }
    }

    public void TogglePausePanel(bool state)
    {
        pausePanel.SetActive(state);
        GameManager.Instance.PlayClickBtnSfx();
    }

    public void PlayClickButtonSfx()
    {
        GameManager.Instance.PlayClickBtnSfx();
    }

    private void SetAudioSlider()
    {
        volumeSlider.value = GameManager.Instance.musicVolume;
    }

    public void SetMusicVolume()
    {
        GameManager.Instance.SetMusicVolume(volumeSlider.value);
    }
}