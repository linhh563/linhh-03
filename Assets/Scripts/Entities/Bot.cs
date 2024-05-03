using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bot : GameActor
{
    private GameDeterminedTree determinedTree;
    private Node currentNode;
    public int turn {get; private set;}

    private void Update() {
        TakePebble();
    }

    public void InitializeBot(int totalPebble)
    {
        this.determinedTree = new GameDeterminedTree(totalPebble);
        determinedTree.CreateTree(determinedTree.root);

        // MODIFY -- change 2 by the parameter.
        turn = 2;
        currentNode = determinedTree.root;
    }

    public void UpdateCurrentNode(int numberPebble)
    {
        if (numberPebble == -1)
        {
            currentNode = currentNode.parent;
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
        GameController.Instance.SwitchTurn();
    }
}