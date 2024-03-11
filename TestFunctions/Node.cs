using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Node {
    public string label;
    public int value {get; private set;}
    public Node firstChild {get; private set;}
    public Node secondChild {get; private set;}
    public Node thirdChild {get; private set;}
    public bool isLeaf {get; private set;}
    public int playerTurn {get; private set;}
    public int determinedValue {get; private set;}

    public Node(int value)
    {
        this.value = value;
        firstChild = null;
        secondChild = null;
        thirdChild = null;
        isLeaf = true;
    }

    public void SetFirstChild(Node node)
    {
        this.firstChild = node;
        
        if (this.playerTurn == 1)
        {
            firstChild.SetPlayerTurn(2);
        }
        else {
            firstChild.SetPlayerTurn(1);
        }

        this.determinedValue = DefinedValue.UndefinedDeterminedValue;
        firstChild.SetDeterminedValue();
        this.isLeaf = false;
    }

    public void SetSecondChild(Node node)
    {
        this.secondChild = node;
        
        if (this.playerTurn == 1)
        {
            secondChild.SetPlayerTurn(2);
        }
        else {
            secondChild.SetPlayerTurn(1);
        }

        this.determinedValue = DefinedValue.UndefinedDeterminedValue;
        secondChild.SetDeterminedValue();
        this.isLeaf = false;
    }

    public void SetThirdChild(Node node)
    {
        this.thirdChild = node;

        if (this.playerTurn == 1)
        {
            thirdChild.SetPlayerTurn(2);
        }
        else {
            thirdChild.SetPlayerTurn(1);
        }

        this.determinedValue = DefinedValue.UndefinedDeterminedValue;
        thirdChild.SetDeterminedValue();
        this.isLeaf = false;
    }

    public void SetPlayerTurn(int value)
    {
        playerTurn = value;
    }

    private void SetDeterminedValue()
    {
        this.determinedValue = (playerTurn == 1) ? 1 : 0;
    }
}