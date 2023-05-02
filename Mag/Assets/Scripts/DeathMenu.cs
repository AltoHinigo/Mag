using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.Timers;
using UnityEngine.UI;
using System.Threading.Tasks;

public class DeathMenu : MonoBehaviour {

    [SerializeField] private GameObject _interface; 

    [SerializeField] private Image _deathScreenImage;
    
    [SerializeField] private GameObject _deathScreenButtons; 

    // private Timer deathScreenFader;
    Color newColor;

    // Start is called before the first frame update
    void Awake() {


        // deathScreenFader = new Timer(300);
        // deathScreenFader.Elapsed += fadeScreen;
        // deathScreenFader.AutoReset = true;

        // deathScreenFader.Start();
    }

    // Update is called once per frame
    void Update() {

    }

    // void fadeScreen(object s, ElapsedEventArgs e) {
    //     newColor.a = newColor.a + 0.05f;

    //     _deathScreenImage.color = newColor;

    //     if(newColor.a == 1f) {
    //         deathScreenFader.Stop();
    //     }        
    // }

    public async void fade() {
        newColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        _deathScreenImage.color = newColor;

        _interface.SetActive(false);

        while (newColor.a < 0.9f) {
            newColor.a = newColor.a + 0.05f;

            await Task.Delay(100);

            _deathScreenImage.color = newColor;
        }

        _deathScreenButtons.SetActive(true);
    }

    public void showInterface() {
        _deathScreenImage.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        _deathScreenButtons.SetActive(false);
        _interface.SetActive(true);
    } 
}
