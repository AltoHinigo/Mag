using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowFPS : MonoBehaviour {
 
    public static float fps;
    public TextMeshPro TMP;
 
    void OnGUI()
    {
        fps = 1.0f / Time.deltaTime;
        GUILayout.Label("FPS: " + (int)fps);
        TMP.text = "FPS: " + fps.ToString();
    }

    
}
