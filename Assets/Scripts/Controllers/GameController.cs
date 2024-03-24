using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private GamePlayUIController uiController;
    private int playerTurn;
    private Transform gameActorTrans;
    private GameActor gameActor1;
    private GameActor gameActor2;
    // private List<Turn> turnLog;
    private int turnPointer;

    public void LoadGame()
    {

    }

    public void LoadPvPGame(SavedGame savedGame)
    {

    }

    public void LoadPvBGame(SavedGame savedGame)
    {

    }

    public void SwitchTurn()
    {

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
}
