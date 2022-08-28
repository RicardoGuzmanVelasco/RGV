using RGV.DesignByContract.Runtime;
using UnityEngine;

public class TestPrecondition : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Before precondition");
        Contract.Require(1).GreaterThan(23);
        Debug.Log("After precondition");
    }
}