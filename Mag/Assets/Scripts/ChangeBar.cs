using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBar : MonoBehaviour
{
    [SerializeField] private Image _Bar;

    [SerializeField] private Color _BarColor;

    [SerializeField] private Image _BarFiller;

    [SerializeField] private Color _BarColorFiller;

    [SerializeField] private float _BarFill;

    public void Change(float Fill)
    {
        if (Fill <= 1 && Fill >= 0)
            _BarFiller.fillAmount = Fill;
    }

    private void Awake()
    {
        _Bar.color = _BarColor;
        _BarFiller.color = _BarColorFiller;
        _BarFiller.fillAmount = _BarFill;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
