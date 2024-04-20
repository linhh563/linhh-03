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
    // MODIFY -- change to private.
    public List<Turn> turnLog;
    public int turnPointer {get; private set;}
    private SavedGame savedGame;
    public int playerInTurn {get; private set;}
    private int _doWithBot;

    // --------------- TEST ---------------
    public int totalPebble;

    private void Awake() {
        CreateSingleton();
        InitializeComponents();
        _doWithBot = 0;
        // DELETE LATER
        LoadPvBGame();
        savedGame = new SavedGame();
        savedGame.SetGameStyle(GameStyle.PvB);
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
        ((Bot)gameActor2).InitializeDeterminedTree(Storage.Instance.totalPebble);
        ((Bot)gameActor2).InitializeBot();
        // playerTurn = savedGame.playerTurn;

        // gameActor1.SetInTurnState(true);
        //  MODIFY -- change player turn fit to saved game.
        SetPlayerInTurn(1);
        uiController.HighLightPlayer(1);
        // ADD -- Set turn log.
    }

    public void SwitchTurn()
    {
        // var actor1InTurn = gameActor1.isInTurn;
        if (playerInTurn == 1)
        {
            SetPlayerInTurn(2);
            uiController.HighLightPlayer(2);
            uiController.UpdateNumberTakenText(1, Storage.Instance.numberPebbleTaken);
            // Debug.Log("Switch to player 2 turn");
        }
        else
        {
            SetPlayerInTurn(1);
            uiController.HighLightPlayer(1);
            uiController.UpdateNumberTakenText(2, Storage.Instance.numberPebbleTaken);
            // Debug.Log("Switch to player 1 turn");
        }

        uiController.ShowNumberPebbleTaken();

        // gameActor1.SetInTurnState(!actor1InTurn);
        // gameActor2.SetInTurnState(actor1InTurn);
    }

    public void UpdateTurnLog(int pebbleAmount)
    {
        if (turnPointer < turnLog.Count - 1)
        {
            // tranh truong hop khi nguoi choi redo, toi luot bot di va se lam sai turn log.
            if ((savedGame.gameStyle == GameStyle.PvB) && (playerInTurn == ((Bot)gameActor2).turn))
            {
                return;
            }
            // Debug.Log("Remove " + (turnLog.Count - (turnPointer + 1)) + "elements");
            turnLog.RemoveRange(turnPointer + 1, turnLog.Count - (turnPointer + 1));
            // // TEST
            // var test = turnLog.LastOrDefault();
            // Debug.Log("last element: " + test.player + " - " + test.pebbleTaken);
        }

        // int playerTurn = (playerInTurn == 1) ? 2 : 1;
        Turn turn = new Turn(playerInTurn, pebbleAmount);
        turnLog.Add(turn);
        turnPointer++;

        // TEST
        if (savedGame.gameStyle == GameStyle.PvB)
        {
            ((Bot)gameActor2).UpdateCurrentNode(pebbleAmount);
        }

        // Debug.Log("Update: " + playerInTurn + " - " + pebbleAmount);
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
        uiController.ShowNumberPebbleTaken();
        Storage.Instance.SetNumberPebbleTaken(turn.pebbleTaken);

        if (savedGame.gameStyle == GameStyle.PvB)
        {
            // update bot's current node into its parent
            ((Bot)gameActor2).UpdateCurrentNode(-1);
            if (_doWithBot < 1)
            {
                _doWithBot++;
                Undo();
            }
            else
            {
                _doWithBot = 0;
                return;
            }            
        }
    }

    public void TestRedo()
    {
        Debug.Log(turnLog.Count + ", can redo: " + CanRedo().ToString());
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
        uiController.ShowNumberPebbleTaken();
        Storage.Instance.SetNumberPebbleTaken(turn.pebbleTaken);   


        // if (savedGame.gameStyle == GameStyle.PvB)
        // {
        //     ((Bot)gameActor2).TakePebble();
        //     if (_doWithBot < 1)
        //     {

        //     }
        // } 
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

    public void SetPlayerInTurn(int turn)
    {
        this.playerInTurn = turn;
    }
}
