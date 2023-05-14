using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ShopLoader : MonoBehaviour {

    /*    public List<Item> items;

        public Item item;*/

    public Items items;

    [ContextMenu("Load")]
    public void Load() {
        items = JsonUtility.FromJson<Items>(File.ReadAllText(Application.streamingAssetsPath + "/Shop_Items.json"));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

[Serializable]
public class Item {

    public int id;
    public string name;
    public int price;
    public string description;

    public Item(int id, string name, int price, string description)
    {
        this.id = id;
        this.name = name;
        this.price = price;
        this.description = description;
    }
}

[Serializable]
public class Items {

    public List<Item> items;
}