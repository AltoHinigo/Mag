using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public Camera MCamera;

    private RaycastHit Hit;

    private NavMeshAgent Agent;

    private string GroundTag = "Graund";

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, (target.transform.position - transform.position), radian_angle, 0.0F);
        //transform.rotation = Quaternion.LookRotation(newDir);
        Ray ray = MCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray.origin, ray.direction, out Hit))//if (Physics.Raycast(ray, out Hit, Mathf.Infinity))
            if (Hit.collider.CompareTag(GroundTag))
            {
                var direction = (Hit.point - transform.position).normalized;
                direction.y = 0f;
                transform.rotation = Quaternion.LookRotation(direction);
                if (Input.GetMouseButton(0))
                    Agent.SetDestination(Hit.point);


                
                if(Agent.hasPath)
                {
                    animator.SetFloat("Speed", 0.5f);
                }
                else
                {
                    animator.SetFloat("Speed", 0f);
                }
            }
    }
    /*
    void LookAtXZ(this Transform transform, Vector3 point)
    {
        var direction = (point - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void LookAtXZ(this Transform transform, Vector3 point, float speed)
    {
        var direction = (point - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), speed);
    }*/
}

