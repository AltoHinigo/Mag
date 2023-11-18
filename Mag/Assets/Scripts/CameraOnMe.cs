using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnMe : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    private Vector3 offset;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = target.transform.position + offset;

        transform.position = Vector3.Lerp(transform.position,target.transform.position + offset,Time.deltaTime * speed);
    }
}
