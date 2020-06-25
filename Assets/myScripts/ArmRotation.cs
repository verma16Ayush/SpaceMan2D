using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public float rotOffset = 0f;    

    // Update is called once per frame
    void Update()
    {
        Vector3 cursorToHandVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float rotZ = (float)(Math.Atan2(cursorToHandVector.y, cursorToHandVector.x) * 180 / Math.PI);
        transform.rotation = Quaternion.Euler(0,0,rotZ + rotOffset);
    }    
}
