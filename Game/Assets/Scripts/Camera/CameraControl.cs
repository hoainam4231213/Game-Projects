using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraControl : MonoBehaviour
{

    public Transform target;
    private Transform trans;
    private Vector3 camera_point;
    // Start is called before the first frame update
    private void Awake()
    {
        trans = transform;
        camera_point = trans.position - target.position;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = camera_point + target.position;
        trans.position = Vector3.Lerp(trans.position, pos, Time.deltaTime * 5);
    }
}
