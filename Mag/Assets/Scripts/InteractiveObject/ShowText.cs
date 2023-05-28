using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowText : InteractiveObject
{
    public GameObject TextBox;
    public string Text;
    private GameObject _TextBox;
    private double radius;
    private void Start()
    {
        _TextBox = Instantiate(TextBox);
        _TextBox.SetActive(false);
        _TextBox.GetComponent<TextMeshPro>().text = Text;
        _TextBox.transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        //_TextBox.transform.parent = gameObject.transform;
        /*radius = GetComponent<SphereCollider>().radius;
        radius = radius * 0.9f;*/

        OnEnter += ShowTextBox;
        OnLeave += HideTextBox;
    }

    private void ShowTextBox(GameObject Self)
    {
        _TextBox.SetActive(true);
    }

    private void HideTextBox(GameObject Self)
    {
        _TextBox.SetActive(false);
    }

    private void Update()
    {
        _TextBox.transform.LookAt(new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z));
        _TextBox.transform.Rotate(0, 180, 0);
    }
}
