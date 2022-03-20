using UnityEngine;

[RequireComponent(typeof(StatBar))]
public class StatBarFakeInput : MonoBehaviour
{
    [SerializeField] KeyCode key = KeyCode.A;
    StatBar statBar;

    void Start()
    {
        statBar = GetComponent<StatBar>();
    }

    void Update()
    {
        if(Input.GetKeyDown(key))
            statBar.ConsumeStat(.25f);
    }
}