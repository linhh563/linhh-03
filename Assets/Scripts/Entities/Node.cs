using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int value {get; private set;}
    public int determinedValue {get; private set;}
    public List<Node> children {get; private set;}
    // public Node firstChild {get; private set;}
    // public Node secondChild {get; private set;}
    // public Node thirdChild {get; private set;}
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
        }
        else
        {
            child.SetPlayerTurn(1);
        }

        this.determinedValue = (playerTurn == 1) ? DefinedValue.Min : DefinedValue.Max;
        child.SetDeterminedValueForLeaf();
        child.parent = this;
        isLeaf = false;

        children.Add(child);
    }

    // public void AddFirstChild(Node child)
    // {
    //     this.firstChild = child;

    //     if (this.playerTurn == 1)
    //     {
    //         firstChild.SetPlayerTurn(2);
    //     }
    //     else {
    //         firstChild.SetPlayerTurn(1);
    //     }

    //     // assign temporary determine value for alpha-beta.
    //     this.determinedValue = (this.playerTurn == 1) ? DefinedValue.Min : DefinedValue.Max;
    //     firstChild.SetDeterminedValueForLeaf();
    //     this.isLeaf = false;
    // }

    // public void AddSecondChild(Node child)
    // {
    //     this.secondChild = child;
        
    //     if (this.playerTurn == 1)
    //     {
    //         secondChild.SetPlayerTurn(2);
    //     }
    //     else {
    //         secondChild.SetPlayerTurn(1);
    //     }

    //     // assign temporary determine value for alpha-beta.
    //     this.determinedValue = (this.playerTurn == 1) ? DefinedValue.Min : DefinedValue.Max;
    //     secondChild.SetDeterminedValueForLeaf();
    //     this.isLeaf = false;
    // }

    // public void AddThirdChild(Node child)
    // {
    //     this.thirdChild = child;

    //     if (this.playerTurn == 1)
    //     {
    //         thirdChild.SetPlayerTurn(2);
    //     }
    //     else {
    //         thirdChild.SetPlayerTurn(1);
    //     }

    //     // assign temporary determine value for alpha-beta.
    //     this.determinedValue = (this.playerTurn == 1) ? DefinedValue.Min : DefinedValue.Max;
    //     thirdChild.SetDeterminedValueForLeaf();
    //     this.isLeaf = false;
    // }

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
