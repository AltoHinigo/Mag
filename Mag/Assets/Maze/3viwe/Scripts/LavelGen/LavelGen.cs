using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static LavelGen.Block;

public class LavelGen : MonoBehaviour
{
    [SerializeField] private float blockSize;
    [SerializeField] private int Width;
    [SerializeField] private int Height;
    [SerializeField] private Object LType;
    [SerializeField] private Object IType;
    [SerializeField] private Object TType;
    [SerializeField] private Object XType;
    [SerializeField] private List<Object> pool;
    [SerializeField] private Object roomType;
    private Block[,] map;
    // Start is called before the first frame update
    void Start()
    {
        map = new Block[Width, Height];
        Generator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generator()
    {
        int w, h;
        w = 0;
        h = 0;
        var past = map[0, 0];
        do
        {
            if (Random.Range(0, 1) == 1)
                if (Random.Range(0, 1) == 1)
                    w += 1;
                else
                    w += -1;
            else
                if (Random.Range(0, 1) == 1)
                    h += 1;
                else
                    h += -1;

            if (map[w, h].blockType != null)
                continue;


            map[w, h].Set((BlockType)Random.Range(0, Block.BlockTypeMaxVal), Quaternion.Euler(0, 0, 0));
            switch (map[w, h].blockType)
            {
                case BlockType.L:
                    map[w, h].rotation = Quaternion.Euler(0, 90 + 90 * Random.Range(0, 1) + past.rotation.y, 0);
                    w += 1;//куда может дальше поставить чанк
                    //Instantiate(LType, new Vector3(blockSize * i, 0, blockSize * j), Quaternion.Euler(0, 90 * Random.Range(0, 3), 0));

                    break;
                case BlockType.I:
                    map[w, h].rotation = Quaternion.Euler(0, past.rotation.y, 0);
                    break;
                case BlockType.X:
                    map[w, h].rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case BlockType.T:
                    map[w, h].rotation = Quaternion.Euler(0, 90 + 90 * Random.Range(-1, 1) + past.rotation.y, 0);
                    break;
            }
            past = map[w, h];
        }
        while (w != Width && h != Height);



        for (int i = 0; i < Width; i++)
            for (int j = 0; j < Height; j++)
            {
                if (map[i, j].blockType == null)
                    continue;
                map[i, j].Set((BlockType)Random.Range(0, Block.BlockTypeMaxVal), Quaternion.Euler(0,0,0));
                
            }

        for (int i = 0; i < Width; i++)
            for (int j = 0; j < Height; j++)
                switch (map[i, j].blockType)
                {
                    case BlockType.L:
                        Instantiate(LType, new Vector3(blockSize * i, 0, blockSize * j), Quaternion.Euler(0, 90 * Random.Range(0, 3), 0));
                        break;
                    case BlockType.I:
                        Instantiate(IType, new Vector3(blockSize * i, 0, blockSize * j), Quaternion.Euler(0, 90 * Random.Range(0, 3), 0));
                        break;
                    case BlockType.X:
                        Instantiate(XType, new Vector3(blockSize * i, 0, blockSize * j), Quaternion.Euler(0, 90 * Random.Range(0, 3), 0));
                        break;
                    case BlockType.T:
                        Instantiate(TType, new Vector3(blockSize * i, 0, blockSize * j), Quaternion.Euler(0, 90 * Random.Range(0, 3), 0));
                        break;
                }
        /*        var key = 4;
        switch(key)
        {
            case 0:
                break;
            case 1:
                break;
        }*/
    }
    public struct Block
    {
        public BlockType blockType;
        public Quaternion rotation;

        public void Set(BlockType blockType, Quaternion rotation)
        {
            this.blockType = blockType;
            this.rotation = rotation;
        }
        public const int BlockTypeMaxVal = 3;
        public enum BlockType
        {
            L, I, T, X
        }
    }
}
