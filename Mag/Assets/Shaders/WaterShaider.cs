using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using System.Threading.Tasks;



public class WaterShaider : MonoBehaviour
{
    public CustomRenderTexture Water;
    public Material WaterMat;

    public bool _DrawFlag = true;

    private bool _ExDrawFlag;

    private bool ShaderRun = true;

    private Texture2D tex;

    private Vector2 hitTextureCord;

    private static readonly int DrawPosition = Shader.PropertyToID("_DrawPosition");
    private static readonly int DrawFlag = Shader.PropertyToID("_Draw");
    private static readonly Vector2 Vec2 = new Vector2(-1, -1);

    // Start is called before the first frame update
    void Start()
    {
        _ExDrawFlag = _DrawFlag;
        Water.Initialize();
        WaterOnIce();
    }
Texture2D toTexture2D(RenderTexture rTex)
{
    Texture2D tex = new Texture2D(Water.width, Water.height, TextureFormat.Alpha8, false);
    // ReadPixels looks at the active RenderTexture.
    RenderTexture.active = rTex;
    tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
    tex.Apply();
    return tex;
}

    // Update is called once per frame
    public void _AddICE()//Update()
    {
        if (_DrawFlag != _ExDrawFlag)
        {
            if (_DrawFlag)
                WaterMat.SetFloat(DrawFlag, 1);
            else
                WaterMat.SetFloat(DrawFlag, 0);
            _ExDrawFlag = _DrawFlag;
        }
        if (Input.GetKey(KeyCode.Q) || true)
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Vector3(0, transform.position.y - 4, 0), Color.green);
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Vector3(0, transform.position.y - 4, 0), out RaycastHit hit, 4f, 1<<4))
            {
                hitTextureCord = hit.textureCoord;
                WaterMat.SetFloat(DrawFlag, 1);
                WaterMat.SetVector(DrawPosition, hitTextureCord);
                Water.Update();
                if(tex != null)
                Debug.Log(tex.GetPixel((int)(hitTextureCord.x * Water.width), (int)(hitTextureCord.y * Water.height)));
            }
        }
        else
            if(hitTextureCord != Vec2)
                hitTextureCord = Vec2;
    }

    public void AddICE(Vector2 hitTextureCord)
    {
        WaterMat.SetFloat(DrawFlag, 1);
        WaterMat.SetVector(DrawPosition, hitTextureCord);
        Water.Update();
        if (tex != null)
            Debug.Log(tex.GetPixel((int)(hitTextureCord.x * Water.width), (int)(hitTextureCord.y * Water.height)));
    }

    public float OnICE(Vector2 hitTextureCord)
    {
        return tex.GetPixel((int)(hitTextureCord.x * Water.width), (int)(hitTextureCord.y * Water.height)).a;
    }

    public async void WaterOnIce()
    {
        while (ShaderRun)
        {
            await Task.Delay(200);
            if (_DrawFlag)
            {
                tex = toTexture2D(Water);
            }
            WaterMat.SetFloat(DrawFlag, 0);
            WaterMat.SetVector(DrawPosition, hitTextureCord);
            Water.Update();
        }
    }

    private void OnDestroy()
    {
        ShaderRun = false;
    }
}
