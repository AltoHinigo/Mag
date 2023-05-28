using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsSpawn : InteractiveObject
{
    [SerializeField] private GameObject[] _Object;
    private List<GameObject> gameObjects = new List<GameObject>();
    /*public override void Script(GameObject Self)
    {
        ObjectsRestart();
    }*/
    private void Start()
    {
        Run += ObjectsRestart;
    }
    private void ObjectsRestart()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            Destroy(gameObjects[i]);
        }
        gameObjects.Clear();
        for (int i = 0; i < _Object.Length; i++)
        {
            GameObject tmp = Instantiate(_Object[i]);
            tmp.SetActive(true);
            gameObjects.Add(tmp);
        }
    }
}
