using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActor : MonoBehaviour
{
    private bool isInTurn;
    
    public void SetInTurnState(bool state)
    {
        this.isInTurn = state;
    }
}
