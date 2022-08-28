using System;
using RGV.DesignByContract.Runtime;
using UnityEngine;

public class TestPostcondition : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Before postcondition");
        Contract.Ensure(DateTime.Now).AtMidnight();
        Debug.Log("After postcondition");
    }
}