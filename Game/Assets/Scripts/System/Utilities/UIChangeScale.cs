using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeScale : MonoBehaviour
{
    public CanvasScaler[] canvasScalers;
    public float orignal_scale;
    // Start is called before the first frame update
    private void Awake()
    {
        float scale = (float)Screen.width / (float)Screen.height;
        scale = scale * 0.5f / orignal_scale;
        foreach(CanvasScaler e in canvasScalers)
        {
            e.matchWidthOrHeight = scale;
        }
    }
}
