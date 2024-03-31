using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private GamePlayUIController uiController;
    // private int playerTurn;
    [SerializeField] private Transform gameActorTrans;
    private GameActor gameActor1;
    private GameActor gameActor2;
    private List<Turn> turnLog;
    public int turnPointer {get; private set;}
    private SavedGame savedGame;
    public int playerInTurn {get; private set;}

    // --------------- TEST ---------------
    public int totalPebble;

    private void Awake() {
        CreateSingleton();
        InitializeComponents();
        LoadPvBGame();
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void GetComponents()
    {
        
    }

    private void InitializeComponents()
    {
        turnLog = new List<Turn>();
        turnPointer = -1;
    }

    public void LoadGame()
    {

    }

    public void LoadPvPGame(SavedGame savedGame)
    {

    }

    public void LoadPvBGame(/* SavedGame savedGame */)
    {
        gameActor1 = gameActorTrans.GetChild(0).AddComponent<Player>();
        gameActor2 = gameActorTrans.GetChild(1).AddComponent<Bot>();

        Storage.Instance.InitializePebble(totalPebble, totalPebble);
        ((Bot)gameActor2).SetBotLevel(3);
        ((Bot)gameActor2).InitializeDeterminedTree(Storage.Instance.totalPebble);
        // playerTurn = savedGame.playerTurn;

        // ------------- TEST -------------
        gameActor1.SetInTurnState(true);
        uiController.HighLightPlayer(1);
        playerInTurn = 1;
    }

    public void SwitchTurn()
    {
        // var actor1InTurn = gameActor1.isInTurn;
        if (playerInTurn == 1)
        {
            uiController.HighLightPlayer(2);
            playerInTurn = 2;
            uiController.UpdateNumberTakenText(1, Storage.Instance.numberPebbleTaken);
        }
        else
        {
            uiController.HighLightPlayer(1);
            playerInTurn = 1;
            uiController.UpdateNumberTakenText(2, Storage.Instance.numberPebbleTaken);
        }

        // gameActor1.SetInTurnState(!actor1InTurn);
        // gameActor2.SetInTurnState(actor1InTurn);
    }

    public void UpdateTurnLog(int pebbleAmount)
    {
        if (turnPointer < turnLog.Count - 1)
        {
            // Debug.Log("Remove " + (turnLog.Count - (turnPointer + 1)) + "elements");
            turnLog.RemoveRange(turnPointer + 1, turnLog.Count - (turnPointer + 1));
            // // TEST
            // var test = turnLog.LastOrDefault();
            // Debug.Log("last element: " + test.player + " - " + test.pebbleTaken);
        }

        int playerTurn = (playerInTurn == 1) ? 2 : 1;
        Turn turn = new Turn(playerTurn, pebbleAmount);
        turnLog.Add(turn);
        turnPointer++;
    }

    public bool CanUndo()
    {
        return (turnPointer >= 0) ? true : false;
    }

    public bool CanRedo()
    {
        return (turnPointer < turnLog.Count - 1) ? true : false;
    }

    public void Undo()
    {
        turnPointer--;

        if (turnPointer == -1)
        {
            Storage.Instance.ChangePebbleAmount(Storage.Instance.numberPebbleTaken);
            uiController.HidePebbleTakenUI();
            return;
        }

        var turn = turnLog.ElementAt(turnPointer);
        playerInTurn = (turn.player == 1) ? 2 : 1;
        uiController.HighLightPlayer(playerInTurn);
        Storage.Instance.ChangePebbleAmount(Storage.Instance.numberPebbleTaken);

        uiController.UpdateNumberTakenText(turn.player, turn.pebbleTaken);
        uiController.ShowNumberPebbleAreTaken();
        Storage.Instance.SetNumberTaken(turn.pebbleTaken);
    }

    public void Redo()
    {
        turnPointer++;

        if (turnPointer == turnLog.Count - 1)
        {
            return;
        }

        var turn = turnLog.ElementAt(turnPointer);
        playerInTurn = (turn.player == 1) ? 2 : 1;
        uiController.HighLightPlayer(playerInTurn);
        Storage.Instance.ChangePebbleAmount(-turn.pebbleTaken);

        uiController.UpdateNumberTakenText(turn.player, turn.pebbleTaken);
        uiController.ShowNumberPebbleAreTaken();
        Storage.Instance.SetNumberTaken(turn.pebbleTaken);    
    }

    public void SaveGame()
    {

    }

    public void PauseGame()
    {

    }

    public void ResumeGame()
    {
        
    }

    public void SetPlayerInTurn(int player)
    {
        this.playerInTurn = player;
    }
}
