using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage Instance;
    public int totalPebble {get; private set;}
    public int currentPebble {get; private set;}
    public int numberPebbleTaken {get; private set;}

    private void Awake() {
        CreateSingleton();
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void InitializePebble(int totalPebble, int currentPebble)
    {
        this.totalPebble = totalPebble;
        this.currentPebble = currentPebble;
    }

    public void ChangePebbleAmount(int value)
    {
        this.currentPebble += value;

        if (currentPebble <= 0)
        {
            GameController.Instance.SetWinner();
        }

        numberPebbleTaken = Math.Abs(value);
    }

    public void SetNumberPebbleTaken(int value)
    {
        numberPebbleTaken = value;
    }
}
