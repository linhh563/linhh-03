using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage Instance;
    private int totalPebble;
    public int currentPebble {get; private set;}

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

    }

    public void ChangePebbleAmount(int value)
    {

    }
}
