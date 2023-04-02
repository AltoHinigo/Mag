using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private Image _Bar;
    [SerializeField] private Image _BarFiller;

    void Start()
    {
        //_Bar = GetComponent<Image>();
        //_BarFiller = GetComponent<Image>();
        //_BarFiller.fillAmount = 0.5f;
    }

    public void ChangeColor(Color BarColor, Color BarColorFiller)
    {
        _Bar.color = BarColor;
        _BarFiller.color = BarColorFiller;
    }

    public void ChangeColor(Color BarColor)
    {
        _Bar.color = BarColor;
    }

    public void ChangeColorFiller(Color BarColorFiller)
    {
        _BarFiller.color = BarColorFiller;
    }

    public void ChangeFill(float Fill)
    {
        if (Fill <= 1 && Fill >= 0)
            _BarFiller.fillAmount = Fill;
    }

}
