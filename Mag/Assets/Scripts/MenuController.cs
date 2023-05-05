using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private Stack<GameObject> cameFromWindows = new Stack<GameObject>();

    public void deactivateWindow(GameObject self) {
        self.SetActive(false);

        cameFromWindows.Push(self);
    }

    public void activateWindow(GameObject nextWindow) {
        nextWindow.SetActive(true);
    }

    public void goBack(GameObject self) {
        self.SetActive(false);

        cameFromWindows.Pop().SetActive(true);
    }

    public void loadeScene(string scene)
    {
        SceneManager.LoadScene(scene);
        SceneManager.UnloadSceneAsync("Menu");
    }
}
