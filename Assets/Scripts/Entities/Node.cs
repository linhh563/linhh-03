using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int value {get; private set;}
    public int determinedValue {get; private set;}
    public List<Node> children {get; private set;}
    public Node firstChild {get; private set;}
    public Node secondChild {get; private set;}
    public Node thirdChild {get; private set;}
    public bool isLeaf {get; private set;}
    private int playerTurn;
    private Node parent;

    public Node(int value)
    {
        this.value = value;
        this.isLeaf = true;
        children = new List<Node>();
    }

    public void AddFirstChild(Node child)
    {
        this.firstChild = child;

        if (this.playerTurn == 1)
        {
            firstChild.SetPlayerTurn(2);
        }
        else {
            firstChild.SetPlayerTurn(1);
        }

        this.determinedValue = DefinedValue.UndefinedDeterminedValue;
        firstChild.SetDeterminedValueForLeaf();
        this.isLeaf = false;
    }

    public void AddSecondChild(Node child)
    {
        this.secondChild = child;
        
        if (this.playerTurn == 1)
        {
            secondChild.SetPlayerTurn(2);
        }
        else {
            secondChild.SetPlayerTurn(1);
        }

        this.determinedValue = DefinedValue.UndefinedDeterminedValue;
        secondChild.SetDeterminedValueForLeaf();
        this.isLeaf = false;
    }

    public void AddThirdChild(Node child)
    {
        this.thirdChild = child;

        if (this.playerTurn == 1)
        {
            thirdChild.SetPlayerTurn(2);
        }
        else {
            thirdChild.SetPlayerTurn(1);
        }

        this.determinedValue = DefinedValue.UndefinedDeterminedValue;
        thirdChild.SetDeterminedValueForLeaf();
        this.isLeaf = false;
    }

    public void SetPlayerTurn(int turn)
    {
        playerTurn = turn;
    }

    private void SetDeterminedValueForLeaf()
    {
        this.determinedValue = (playerTurn == 1) ? 1 : 0;
    }

    public void SetDeterminedValue(int value)
    {
        determinedValue = value;
    }

    // public int FindBestWay()
    // {

    // }

    // public int FindWay()
    // {

    // }
}
