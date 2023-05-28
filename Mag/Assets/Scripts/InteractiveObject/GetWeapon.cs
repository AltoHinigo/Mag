using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GetWeapon : InteractiveObject
{
    public int MagicStaff = 1;
    private bool Flag = false;
    private void Start()
    {
        //OnInside += Script;
        OnEnter += Enter;
        OnLeave += Leave;
    }

    private void Enter(GameObject Self)
    {
        Flag = true;
        GlobalEventManager.InteractiveObjects.Add(this);
    }

    private void Leave(GameObject Self)
    {
        Flag = false;
        GlobalEventManager.InteractiveObjects.Remove(this);
    }

    /*public void Script(GameObject Self)
    {
        //Debug.Log(Vector3.Distance(transform.position, Self.transform.position).ToString() + "    " + Radius);
        Dist = Vector3.Distance(transform.position, Self.transform.position);
        if (Dist < Radius)
        {
            if (_switch)
            {
                //Debug.Log("+= ChangWeapon");
                GlobalEventManager.InteractiveObjects.Add(this);
                _objct = Self;
                _switch = false;
            }
        }
        else
            if (!_switch)
        {
            //Debug.Log("-= ChangWeapon");
            GlobalEventManager.InteractiveObjects.Remove(this);
            _switch = true;
        }
    }*/

    public override void Use(GameObject Self)
    {
        ChangWeapon(Self);
    }

    public void ChangWeapon(GameObject _objct)
    {
        if (_objct.TryGetComponent(out Stats stats))
        {
            stats.ChangeMagicStaff(MagicStaff);
        }
    }

    /*
    public override void Script(GameObject Self)
    {
        //Debug.Log(Vector3.Distance(transform.position, Self.transform.position).ToString() + "    " + Radius);
        Dist = Vector3.Distance(transform.position, Self.transform.position);
        if (Dist < Radius)
        {
            if (_switch)
            {
                //Debug.Log("+= ChangWeapon");
                GlobalEventManager.InteractiveObjects.Add(this);
                _objct = Self;
                _switch = false;
            }
        }
        else
            if (!_switch)
            {
            //Debug.Log("-= ChangWeapon");
            GlobalEventManager.InteractiveObjects.Remove(this);
            _switch = true;
            }
    }

    public override void Use(GameObject Self)
    {
        ChangWeapon();
    }

    public void ChangWeapon()
    {
        if (_objct.TryGetComponent(out Stats stats))
        {
            stats.ChangeMagicStaff(MagicStaff);
        }
    }
*/
    private void OnDestroy()
    {
        if (Flag)
            GlobalEventManager.InteractiveObjects.Remove(this);
    }
}
