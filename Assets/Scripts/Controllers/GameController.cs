using System.Collections;
using System.Collections.Generic;
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
    // private List<Turn> turnLog;
    private int turnPointer;
    private SavedGame savedGame;
    public int playerTurn {get; private set;}

    // --------------- TEST ---------------
    public int totalPebble;

    private void Awake() {
        CreateSingleton();
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

        ((Bot)gameActor2).SetBotLevel(3);
        Storage.Instance.InitializePebble(totalPebble, totalPebble);
        // playerTurn = savedGame.playerTurn;

        // ------------- TEST -------------
        gameActor1.SetInTurnState(true);
        uiController.HighLightPlayer(1);
        playerTurn = 1;
    }

    public void SwitchTurn()
    {
        var actor1InTurn = gameActor1.isInTurn;
        if (actor1InTurn)
        {
            uiController.HighLightPlayer(2);
            playerTurn = 2;
            uiController.UpdateNumberTakenText(1, Storage.Instance.numberPebbleTaken);
        }
        else
        {
            uiController.HighLightPlayer(1);
            playerTurn = 1;
            uiController.UpdateNumberTakenText(2, Storage.Instance.numberPebbleTaken);
        }

        gameActor1.SetInTurnState(!actor1InTurn);
        gameActor2.SetInTurnState(actor1InTurn);
    }

    public void UpdateTurnLog(int pebbleAmount)
    {

    }

    // public bool CanUndo()
    // {

    // }

    // public bool CanRedo()
    // {

    // }

    public void Undo()
    {

    }

    public void Redo()
    {

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

    public void SetPlayerInTurn(int value)
    {
        this.playerTurn = value;
    }
}
