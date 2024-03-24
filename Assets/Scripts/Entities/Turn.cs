using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public int player {get; private set;}
    public int pebbleTaken {get; private set;}

    public Turn(int player, int pebbleTaken)
    {
        this.player = player;
        this.pebbleTaken = pebbleTaken;
    }
}
