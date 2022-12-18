using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public static Balance instance;
    public BalanceTracker[] balanceTrackers;
    private void Awake()
    {
        instance = this;
    }

    public void TurnBalanceTrackerOn(int i)
    {
        balanceTrackers[i].gameObject.SetActive(true);
    }
}
