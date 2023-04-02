using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    [SerializeField] private GameObject[] _Object;
    private List<GameObject> gameObjects = new List<GameObject>();
    private bool flag = true;

    public void DoSomthing()
    {
        int i = 0;
        while (i < gameObjects.Count)
        {
            if (!gameObjects[i].activeSelf)
            {
                Debug.Log("break");
                break;
            }
            i++;
        }
        Debug.Log("i " + i);
        //Debug.Log("gameObjects" + gameObjects.Count);
        if (!(i < gameObjects.Count))
            flag = true;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        for (i = 0; i < _Object.Length; i++)
        {
            var tmp = Instantiate(_Object[i]);
            tmp.SetActive(true);
            gameObjects.Add(tmp);
        }
        for (i = 0; i < gameObjects.Count; i++)
        {
            if (!gameObjects[i].activeSelf)
                gameObjects.RemoveAt(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!flag) return;
        flag = false;
        int i = 0;
        for (i = 0; i < gameObjects.Count; i++)
        {
                Destroy(gameObjects[i]);
                gameObjects.RemoveAt(i);
        }
        for (i = 0; i < _Object.Length; i++)
        {
            var tmp = Instantiate(_Object[i]);
            tmp.SetActive(true);
            gameObjects.Add(tmp);
        }
    }
}
