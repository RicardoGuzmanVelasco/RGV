using System;
using RGV.DesignByContract.Runtime;
using UnityEngine;

public class tesst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Before");
        Contract.Ensure(DateTime.Now).AtMidnight();
        Debug.Log("After");
    }

    // Update is called once per frame
    void Update() { }
}