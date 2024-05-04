using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class Turn
{
    public int player;
    public int pebbleTaken;

    public Turn(int player, int pebbleTaken)
    {
        this.player = player;
        this.pebbleTaken = pebbleTaken;
    }
}
