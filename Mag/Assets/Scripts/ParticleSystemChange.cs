using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemChange : MonoBehaviour
{
    [SerializeField] private ParticleSystem _CoreFire;
    [SerializeField] private ParticleSystem _Fire;
    [SerializeField] private ParticleSystem _FireDark;
    [SerializeField] private ParticleSystem _FireSmoke;
    [SerializeField] private ParticleSystem _Sparks;

    public void SetPrefab(Color color, Color color_FireDark)
    {
        _CoreFire.startColor = color;
        _Fire.startColor = color;
        _FireDark.startColor = color_FireDark;

    }

    public void Play()
    {
        _CoreFire.Play();
        _Fire.Play();
        _FireDark.Play();
        _FireSmoke.Play();
        _Sparks.Play();

    }

    public void Pause()
    {
        _CoreFire.Pause();
        _Fire.Pause();
        _FireDark.Pause();
        _FireSmoke.Pause();
        _Sparks.Pause();
    }

    public void Clear()
    {
        _CoreFire.Clear();
        _Fire.Clear();
        _FireDark.Clear();
        _FireSmoke.Clear();
        _Sparks.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
