using System;
using RGV.DesignByContract.Runtime;
using UnityEngine;

public class TestInvariant : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Before invariant");
        Contract.Invariant(DateTime.Now).AtMidnight();
        Debug.Log("After invariant");
    }
}