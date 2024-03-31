using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : GameActor
{
    private int level;
    private GameDeterminedTree determinedTree;
    private Node currentNode;
    private int startTurn;

    public void InitializeDeterminedTree(int totalPebble)
    {
        this.determinedTree = new GameDeterminedTree(totalPebble);
        determinedTree.CreateTree(determinedTree.root);
    }

    public void UpdateCurrentNode(int currentPebble)
    {

    }

    // public int FindBestWay()
    // {

    // }

    // public int FindWay()
    // {

    // }

    public void SetBotLevel(int level)
    {
        this.level = level;
    }
}
