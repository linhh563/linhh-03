using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActor : MonoBehaviour
{
    public bool isInTurn {get; private set;}
    
    public void SetInTurnState(bool state)
    {
        this.isInTurn = state;
    }
}
