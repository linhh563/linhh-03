using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bot : GameActor
{
    private GameDeterminedTree determinedTree;
    private Node currentNode;
    public int turn {get; private set;}

    // private void Awake() {
    //     turn = 2;
    //     InitializeDeterminedTree(Storage.Instance.totalPebble);
    //     currentNode = determinedTree.root;
    // }

    private void Update() {
        TakePebble();
    }

    public void InitializeBot()
    {
        // MODIFY -- change 2 by the parameter.
        turn = 2;
        currentNode = determinedTree.root;
    }

    public void InitializeDeterminedTree(int totalPebble)
    {
        this.determinedTree = new GameDeterminedTree(totalPebble);
        determinedTree.CreateTree(determinedTree.root);
    }

    public void UpdateCurrentNode(int numberPebble)
    {
        if (numberPebble == -1)
        {
            currentNode = currentNode.parent;
            // Debug.Log("current node value: " + currentNode.value);
            return;
        }

        int updatedValue = currentNode.value - numberPebble;

        foreach (var child in currentNode.children)
        {
            if (child.value == updatedValue)
            {
                currentNode = child;
            }
        }

        // Debug.Log("current node value: " + currentNode.value);
    }
    
    private int BestWay()
    {
        int tempDeterminedValue = DefinedValue.Max;
        int tempValue = 1;

        foreach(var child in currentNode.children)
        {
            if (tempDeterminedValue > child.determinedValue)
            {
                tempDeterminedValue = child.determinedValue;
                tempValue = child.value;
            }
        }

        // Debug.Log("current node: " + currentNode.value + ", value: " + tempValue + ", determined value: " + tempDeterminedValue);

        return currentNode.value - tempValue;
    }

    public void TakePebble()
    {
        if (GameController.Instance.playerInTurn != this.turn)
        {
            return;
        }

        // Debug.Log("Player turn: " + GameController.Instance.playerInTurn);

        int numberPebble = BestWay();
        Storage.Instance.ChangePebbleAmount(-numberPebble);
        GameController.Instance.UpdateTurnLog(numberPebble);
        // UpdateCurrentNode(numberPebble);      
        // Debug.Log("bot take pebble");
        GameController.Instance.SwitchTurn();
        
        // Thread.Sleep(2000);

        // Debug.LogWarning("Turn log");
        // foreach (var turn in GameController.Instance.turnLog)
        // {
        //     Debug.Log("Turn: " + turn.player + " - " + turn.pebbleTaken);
        // }
    }
}
