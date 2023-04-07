using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingHelpButton : MonoBehaviour
{

    [SerializeField] private GameObject HelpBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick()
    {
        if(HelpBox.activeSelf)
            HelpBox.SetActive(false);
        else
            HelpBox.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
