using System;
using System.Timers;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using static UnityEditor.Progress;

public class Fight : MonoBehaviour
{

    private static List<GameObject> UnderAttack = new List<GameObject>();

    private static Timer aTimer;

    private static Timer bTimer;

    private int[] Elements = new int[5];

    public GameObject ParticleSystemObject;

    private ParticleSystem aParticleSystem;

    private bool TimeIsOver = false;

    private bool TimeAttackTic = false;

    // Start is called before the first frame update
    void Start()
    {
        aParticleSystem = ParticleSystemObject.GetComponent<ParticleSystem>();
        aTimer = new Timer(3000);
        aTimer.Elapsed += OnTimer;
        aTimer.AutoReset = false;

        bTimer = new Timer(400);
        bTimer.Elapsed += OnbTimer;
        /*
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", Color.red);*/
    }
    private void OnTimer(object s, ElapsedEventArgs e)
    {
        TimeIsOver = true;
    }

    private void OnbTimer(object s, ElapsedEventArgs e)
    {
        TimeAttackTic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeAttackTic)
        {
            TimeAttackTic = false;
            for (int i = 0; i < UnderAttack.Count; i++)
            {
                UnderAttack[i].gameObject.SetActive(false);
                //print("!");
                //UnderAttack[i].GetComponent<Renderer>().material.SetColor("color", new Color(0.4f, 0.9f, 0.7f, 1.0f));
                //UnderAttack[i].GetComponent<Renderer>().material.SetColor("_Color", new Color(0.4f, 0.9f, 0.7f, 1.0f));
                //UnderAttack[i].GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                //UnderAttack[i].GetComponent<Renderer>().material.SetColor("_Color", new Color(UnderAttack[i].GetComponent<Renderer>().material.GetColor("_Color").r + 0.02f, 0f, 0f));
                //print(UnderAttack[i].GetComponent<Renderer>().material.color.ToString());
                //item.GetComponent<Material>().color = Color.red;
                //item.GetComponent<Material>().color = new Color(item.GetComponent<Material>().color.r + 0.1f, 0f, 0f);
                //print(item.GetComponent<Material>().color.r);

            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            aTimer.Start();
            bTimer.Start();
            aParticleSystem.Play();
        }
        if (TimeIsOver)
        {
            foreach (ref int element in Elements.AsSpan())
                element = 0;
            aParticleSystem.Pause();
            aParticleSystem.Clear();
            TimeIsOver = false;
            bTimer.Stop();
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MortalObject")
            print("Enter");
    }*/
    private void OnTriggerStay(Collider other)
    {
        UnderAttack.Clear();
        if (other.gameObject.tag == "MortalObject")
        {
            UnderAttack.Add(other.gameObject);
        }

    }
    /*private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MortalObject")
            print("leave");
    }*/
}
