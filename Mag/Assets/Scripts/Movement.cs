using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public Camera MCamera;

    public GameObject ParticleSystemObject;//var

    private ParticleSystem aParticleSystem;//var

    private RaycastHit Hit;

    private NavMeshAgent Agent;

    private string GroundTag = "Ground";

    private Animator animator;

    private bool TimeIsOver = false;

    private int[] Elements = new int[5];

    private static System.Timers.Timer aTimer;
    void Start()
    {
        aParticleSystem = ParticleSystemObject.GetComponent<ParticleSystem>();//var
        Agent = GetComponent<NavMeshAgent>();
        aTimer = new System.Timers.Timer(2000);
        aTimer.Elapsed += OnTimer;
        aTimer.AutoReset = false;
    }

    private void OnTimer(object s, ElapsedEventArgs e)
    {
        foreach (ref int element in Elements.AsSpan())
            element = 0;
        TimeIsOver = true;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogWarningFormat("Play");
        Ray ray = MCamera.ScreenPointToRay(Input.mousePosition);
        var direction = (Hit.point - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
        if (Input.GetKey(KeyCode.W))
            Elements[0] = 1;
        if (Physics.Raycast(ray.origin, ray.direction, out Hit, Mathf.Infinity, 1 << 7))//if (Physics.Raycast(ray, out Hit, Mathf.Infinity))
            if (Elements[0] == 0)
            {
                if (TimeIsOver)
                {
                    aParticleSystem.Pause();
                    aParticleSystem.Clear();
                }
                if (Hit.collider.CompareTag(GroundTag))
                {
                    if (Input.GetMouseButton(0))
                        Agent.SetDestination(Hit.point);



                    if (Agent.hasPath)
                        animator.SetBool("Walk", true);
                    else
                        animator.SetBool("Walk", false);
                }
            }
            else
            {
                if (!aTimer.Enabled)
                {
                    //Debug.LogWarningFormat("Play");
                    //Debug.LogWarningFormat("W = {0}", Input.GetKey(KeyCode.W));
                    //Debug.LogWarningFormat("!Elements[0] = {0}", Elements[0]);
                    aTimer.Start();
                    aParticleSystem.Play();//var
                    animator.SetBool("Walk", false);
                    Agent.ResetPath();
                }
                if (Hit.collider.CompareTag(GroundTag))
                {
                    

                    if (Input.GetMouseButton(0))
                    {
                        Agent.SetDestination(Hit.point);
                        //foreach (int i in Elements)
                        //    Elements[i] = 0;
                        foreach (ref int element in Elements.AsSpan())
                            element = 0;
                    }
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

