using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class StartInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        loadeScene("Menu");
    }

    public async void loadeScene(string scene)
    {
        
        await Task.Delay(1500);
        SceneManager.LoadScene(scene);
        SceneManager.UnloadSceneAsync("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
