using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Texture2D bgrTexture;
    public UnityEngine.UI.RawImage img;
    private void Awake()
    {
        var r = (byte)UnityEngine.Random.Range(0,256);
        var g = (byte)UnityEngine.Random.Range(0,256);
        var b = (byte)UnityEngine.Random.Range(0,256);

        var r1 = (byte)UnityEngine.Random.Range(0,256);
        var g1 = (byte)UnityEngine.Random.Range(0,256);
        var b1 = (byte)UnityEngine.Random.Range(0,256);

        bgrTexture = new Texture2D(1, 2)
        {
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Bilinear
        };
        bgrTexture.SetPixels(new Color[]
            {new Color32(r,g,b,255),new Color32(r1,g1,b1,255)});
        bgrTexture.Apply();
        img.texture = bgrTexture;


        //var bgColor = new Color(r, g, b);
        //Camera.main.GetComponent<Camera>().backgroundColor = new Color32(r,g,b,255);
    }
}
