using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class StatBar : MonoBehaviour
{
    float consumingCooldownBeginning;
    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if(slider.value >= 1f)
            return;

        if(Time.time - consumingCooldownBeginning > .5f)
            slider.value += Time.deltaTime;
    }

    public void ConsumeStat(float quantity)
    {
        slider.value -= quantity;
        consumingCooldownBeginning = Time.time;
    }
}