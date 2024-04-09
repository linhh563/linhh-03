using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : GameActor
{
    private GameDeterminedTree determinedTree;
    private Node currentNode;
    private int turn;

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

    private void UpdateCurrentNode(int numberPebble)
    {
        int updatedValue = currentNode.value - numberPebble;

        foreach (var child in currentNode.children)
        {
            if (child.value == updatedValue)
            {
                currentNode = child;
            }
        }
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

        return currentNode.value - tempValue;
    }

    public void TakePebble()
    {
        if (GameController.Instance.playerInTurn != this.turn)
        {
            return;
        }

        int numberPebble = BestWay();
        Storage.Instance.ChangePebbleAmount(-numberPebble);
        GameController.Instance.UpdateTurnLog(numberPebble);
        UpdateCurrentNode(numberPebble);
        GameController.Instance.SwitchTurn();

        Debug.LogWarning("Turn log");
        foreach (var turn in GameController.Instance.turnLog)
        {
            Debug.Log("Turn: " + turn.player + " - " + turn.pebbleTaken);
        }
    }
}
