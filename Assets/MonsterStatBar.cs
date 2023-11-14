using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStatBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI hpText;


    public void SetUI(int hp, int maxHp)
    {
        float ratio = 0;
        if (hp < 0)
            hp = 0;
        if(hp != 0)
            ratio = (float)hp / (float)maxHp;
        SetSlider(ratio);
        SetHpText(hp, maxHp);
    }

    private void SetSlider(float hpRatio)
    {
        slider.value = hpRatio;
    }
    private void SetHpText(int hp, int maxHp)
    {
        hpText.text = $"{hp}/{maxHp}";
    }
}
