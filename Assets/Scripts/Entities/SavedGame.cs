using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedGame
{
    public GameStyle gameStyle;
    public int totalPebble;
    public int currentPebble;
    public int botLevel;
    // this refer to which player is in current turn
    public int currentTurn;
    public List<Turn> turnLog;

    public SavedGame()
    {
        turnLog = new List<Turn>();
    }

    public SavedGame(GameStyle gameStyle, int totalPebble, int currentPebble, int botLevel, int currentTurn, List<Turn> turnLog)
    {
        this.gameStyle = gameStyle;
        this.totalPebble = totalPebble;
        this.currentPebble = currentPebble;
        this.botLevel = botLevel;
        this.currentTurn = currentTurn;
        this.turnLog = turnLog;
    }
}
