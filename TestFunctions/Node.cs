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
        firstChild = node;
        isLeaf = false;
    }

    public void SetSecondChild(Node node)
    {
        secondChild = node;
        isLeaf = false;
    }

    public void SetThirdChild(Node node)
    {
        thirdChild = node;
        isLeaf = false;
    }
}