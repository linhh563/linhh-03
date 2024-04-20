using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedGame
{
    public GameStyle gameStyle {get; private set;}
    public int totalPebble {get; private set;}
    public int currentPebble {get; private set;}
    // this refer to which player is in current turn
    public int currentTurn {get; private set;}
    public List<Turn> turnLog {get; private set;}

    public SavedGame()
    {
        turnLog = new List<Turn>();
    }

    public SavedGame(GameStyle gameStyle, int totalPebble, int currentPebble, int currentTurn, List<Turn> turnLog)
    {
        this.gameStyle = gameStyle;
        this.totalPebble = totalPebble;
        this.currentPebble = currentPebble;
        this.currentTurn = currentTurn;
        this.turnLog = turnLog;
    }

    public void SetTotalPebble(int totalPebble)
    {
        this.totalPebble = totalPebble;
    }

    public void SetGameStyle(GameStyle gameStyle)
    {
        this.gameStyle = gameStyle;
    }

    public void SetCurrentPebble(int currentPebble)
    {
        this.currentPebble = currentPebble;
    } 

    public void SetCurrentTurn(int currentTurn)
    {
        this.currentTurn = currentTurn;
    }
}
