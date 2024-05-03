using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int value {get; private set;}
    public int determinedValue {get; private set;}
    public List<Node> children {get; private set;}
    public bool isLeaf {get; private set;}
    public int playerTurn {get; private set;}
    public Node parent {get; private set;}

    public Node(int value)
    {
        this.value = value;
        this.isLeaf = true;
        children = new List<Node>();
    }

    public void AddChild(Node child)
    {
        if (this.playerTurn == 1)
        {
            child.SetPlayerTurn(2);
            SetDeterminedValue(DefinedValue.Min);
        }
        else
        {
            child.SetPlayerTurn(1);
            SetDeterminedValue(DefinedValue.Max);
        }

        child.SetDeterminedValueForLeaf();
        child.parent = this;
        isLeaf = false;

        children.Add(child);
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
}