using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private int value;
    private int determinedValue;
    public List<Node> children {get; private set;}
    private bool isLeaf;
    private int playerTurn;
    private Node parent;

    public Node(int value)
    {
        this.value = value;
    }

    public void AddFirstChild(Node child)
    {

    }

    public void AddSecondChild(Node child)
    {

    }

    public void AddThirdChild(Node child)
    {

    }

    public void SetPlayerTurn(int turn)
    {

    }

    public void SetDeterminedValueForLeaf(int value)
    {

    }

    public void SetDeterminedValue(int value)
    {

    }

    // public int FindBestWay()
    // {

    // }

    // public int FindWay()
    // {

    // }
}
