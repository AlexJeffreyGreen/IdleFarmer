using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    public Text StaminaAmount;
    public Slider Slider;
    // Start is called before the first frame update
    public void SetStamina(int stamina)
    {
        if (stamina > Slider.maxValue)
            Slider.value = Slider.maxValue;
        else if (stamina < Slider.minValue)
            Slider.value = Slider.minValue;
        else
            Slider.value = stamina;
        this.SetText();
    }
    

    public void SetMaxStamina(int stamina)
    {
        Slider.maxValue = stamina;
        Slider.value = stamina;
        this.SetText();
    }

    private void SetText()
    {
        this.StaminaAmount.text = Slider.value.ToString() + "/" + Slider.maxValue.ToString();
        // this.StaminaAmount.text = $"{Slider.value.ToString("D3")}/{Slider.maxValue}";
    }
}
