using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private GamePlayUIController uiController;
    [SerializeField] private Transform gameActorTrans;
    private GameActor gameActor1;
    private GameActor gameActor2;
    public List<Turn> turnLog {get; private set;}
    public int turnPointer {get; private set;}
    public SavedGame savedGame {get; private set;}
    public int playerInTurn {get; private set;}
    private int _doWithBot;
    public int winner {get; private set;}

    private void Awake() {
        CreateSingleton();
        InitializeComponents();
        _doWithBot = 0;
        LoadGame();
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
        InitializeSavedGame();
        winner = 0;
    }

    private void InitializeSavedGame()
    {
        if (!GameManager.Instance.hasSavedGame)
        {
            turnLog = new List<Turn>();
            turnPointer = -1;
        }
    }

    public void LoadGame()
    {
        this.savedGame = GameManager.Instance.savedGame;
        if (savedGame.gameStyle == GameStyle.PvB)
        {
            LoadPvBGame();
        }
        else
        {
            LoadPvPGame();
        }
    }

    public void LoadPvPGame()
    {
        gameActor1 = gameActorTrans.GetChild(0).AddComponent<Player>();
        gameActor2 = gameActorTrans.GetChild(1).AddComponent<Player>();

        Storage.Instance.InitializePebble(savedGame.totalPebble, savedGame.currentPebble, savedGame.numberPebbleTaken);
        SetPlayerInTurn(savedGame.currentTurn);
        uiController.HighLightPlayer(savedGame.currentTurn);
        turnLog = savedGame.turnLog;
        turnPointer = savedGame.turnPointer;

        if (GameManager.Instance.hasSavedGame)
        {
            uiController.UpdateNumberTakenText((playerInTurn == 1) ? 2 : 1, 
                turnLog.ElementAt(turnPointer).pebbleTaken);

            uiController.ShowNumberPebbleTaken();
        }
    }

    public void LoadPvBGame()
    {
        gameActor1 = gameActorTrans.GetChild(0).AddComponent<Player>();
        gameActor2 = gameActorTrans.GetChild(1).AddComponent<Bot>();

        Storage.Instance.InitializePebble(savedGame.totalPebble, savedGame.currentPebble, savedGame.numberPebbleTaken);
        ((Bot)gameActor2).InitializeBot(Storage.Instance.totalPebble);

        SetPlayerInTurn(savedGame.currentTurn);
        uiController.HighLightPlayer(savedGame.currentTurn);

        if (GameManager.Instance.hasSavedGame)
        {
            uiController.ShowNumberPebbleTaken();
            turnLog = savedGame.turnLog;
            turnPointer = savedGame.turnPointer;
        }
        else
        {
            turnLog = new List<Turn>();
            turnPointer = -1;
        }
    }

    public void SwitchTurn()
    {
        if (playerInTurn == 1)
        {
            SetPlayerInTurn(2);
            uiController.HighLightPlayer(2);
            uiController.UpdateNumberTakenText(1, Storage.Instance.numberPebbleTaken);
        }
        else
        {
            SetPlayerInTurn(1);
            uiController.HighLightPlayer(1);
            uiController.UpdateNumberTakenText(2, Storage.Instance.numberPebbleTaken);
        }

        uiController.ShowNumberPebbleTaken();
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
            turnLog.RemoveRange(turnPointer + 1, turnLog.Count - (turnPointer + 1));
        }

        Turn turn = new Turn(playerInTurn, pebbleAmount);
        turnLog.Add(turn);
        turnPointer++;

        if (savedGame.gameStyle == GameStyle.PvB)
        {
            ((Bot)gameActor2).UpdateCurrentNode(pebbleAmount);
        }
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
            SwitchTurn();
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
    }

    public void SaveGame()
    {
        int total = Storage.Instance.totalPebble;
        int current = Storage.Instance.currentPebble;
        int numberPebbleTaken = Storage.Instance.numberPebbleTaken;

        GameManager.Instance.SaveGame(savedGame.gameStyle, total, current, playerInTurn, turnLog, turnPointer, numberPebbleTaken);
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

    public void SetWinner(int player)
    {
        this.winner = player;
    }

    public void SetWinner()
    {
        winner = playerInTurn;
    }

    public void BackToMenu()
    {
        if (winner != 0)
        {
            GameManager.Instance.SetHasSavedGame(false);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
