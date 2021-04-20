using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    public Slider Slider;
    // Start is called before the first frame update
    public void SetStamina(int stamina)
    {
        Slider.value = stamina;
    }

    public void SetMaxStamina(int stamina)
    {
        Slider.maxValue = stamina;
        Slider.value = stamina;
    }
}
