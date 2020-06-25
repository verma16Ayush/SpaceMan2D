using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgroundsPos;
    public Transform cam;
    public float[] parallaxScale;
    private Vector3 previousCamPos;
    public float smoothing = 1f;
    
    
    private void Awake()
    {
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;
        
        parallaxScale = new float[backgroundsPos.Length];
        for (int i = 0; i < parallaxScale.Length; i++)
        {
            parallaxScale[i] = backgroundsPos[i].position.z * 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgroundsPos.Length; i++)
        {
            // calculating the x parallax disp of each background
            float parallax = (cam.position.x - previousCamPos.x) * parallaxScale[i];
            // calculating y parallax displacement of each background
            float parallay = (cam.position.y - previousCamPos.y) * parallaxScale[i];
            // target y coordinate of the ith background
            float backgroundTargetY = backgroundsPos[i].position.y + parallay;
            // target x coordinate of the ith background
            float backgroundTagetX = backgroundsPos[i].position.x + parallax;
            //target vector3 of at ith background 
            Vector3 backgroundTarget = new Vector3(backgroundTagetX, backgroundTargetY, backgroundsPos[i].position.z);
            // linearly interpolate between these two points according to the smoothing parameter
           backgroundsPos[i].position =  Vector3.Lerp(backgroundsPos[i].position, backgroundTarget, smoothing * Time.deltaTime);
        }
        
        previousCamPos = cam.position;
    }
}
