using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUIController : MonoBehaviour
{
    private GameObject pausePanel;
    [SerializeField] private GameObject gameActor1Border;
    [SerializeField] private GameObject gameActor2Border;
    [SerializeField] private Text currentPebbleTxt;
    [SerializeField] private GameObject numberTaken1UI;
    [SerializeField] private GameObject numberTaken2UI;
    [SerializeField] private Text actor1PebbleTakenTxt;
    [SerializeField] private Text actor2PebbleTakenTxt;
    private int btnIsHighLighted;
    private float timeHighLighted;

    private void Update() {
        UpdateCurrentPebbleText();
    }

    public void EnablePauseUI(bool state)
    {

    }

    public void EnableUndoBtn(bool state)
    {

    }

    public void EnableRedoBtn(bool state)
    {
        
    }

    public void HighLightTakePebbleBtn()
    {

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

    public void SetGameActorBorderColor(int gameActor)
    {

    }

    public void SetTakePebbleBtnBorderColor(int btnOrder, float r, float g, float b)
    {

    }

    private void UpdateCurrentPebbleText()
    {
        currentPebbleTxt.text = Storage.Instance.currentPebble.ToString();
    }

    public void ShowNumberPebbleAreTaken()
    {
        var playerTurn = GameController.Instance.playerTurn;
        switch (playerTurn)
        {
            case 1:
                numberTaken1UI.SetActive(false);
                numberTaken2UI.SetActive(true);
                break;
            case 2:
                numberTaken1UI.SetActive(true);
                numberTaken2UI.SetActive(false);
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
}
