using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUIController : MonoBehaviour
{
    private GameObject pausePanel;
    [SerializeField] private GameObject gameActor1Border;
    [SerializeField] private GameObject gameActor2Border;
    [SerializeField] Image avatar1;
    [SerializeField] Image avatar2;
    [SerializeField] private Text currentPebbleTxt;
    [SerializeField] private GameObject numberTaken1UI;
    [SerializeField] private GameObject numberTaken2UI;
    [SerializeField] private Button undoBtn;
    [SerializeField] private Button redoBtn;
    [SerializeField] private Text actor1PebbleTakenTxt;
    [SerializeField] private Text actor2PebbleTakenTxt;

    private void Update() {
        UpdateCurrentPebbleText();
        EnableUndoBtn();
        EnableRedoBtn();
    }

    public void EnablePauseUI(bool state)
    {

    }

    public void EnableUndoBtn()
    {
        if (GameController.Instance.CanUndo())
        {
            undoBtn.interactable = true;
        }
        else
        {
            undoBtn.interactable = false;
        }
    }

    public void EnableRedoBtn()
    {
        if (GameController.Instance.CanRedo())
        {
            redoBtn.interactable = true;
        }
        else
        {
            redoBtn.interactable = false;
        }
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
}
