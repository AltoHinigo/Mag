using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerKB : MonoBehaviour
{
    [Header("Move speed")]
    public float speed = 7f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //transform.localPosition += transform.forward * speed * Time.deltaTime;
    }

    private void Move()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.localPosition += transform.forward * speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += transform.right * speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition -= transform.forward * speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition -= transform.right * speed * Time.deltaTime;

        }
    }
}
