using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour { 
    private const float min = -50.0f;
    private const float max = 0.0f;

    public Transform lookAt;
    public float distance;
    float gap;
    public bool mouseOn;
    public bool speed;
    public float x;
    public float z;
    public  float currentX = 190f;
    public  float currentY = -15.0f;
    public Transform body;
    public float height;

    private void Start(){
        speed = Input.GetButton("Fire2");
    }

    private void Update() { 
        bool aim = Input.GetButton("Aim");
        gap = distance;
        if (mouseOn){
            currentX += Input.GetAxis("Mouse X");
            currentY += Input.GetAxis("Mouse Y");
        }
        currentY = Mathf.Clamp(currentY, min, max);
    }

    private void LateUpdate(){
        Vector3 dir = new Vector3(x, z, -gap);
        Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0);

        transform.position = lookAt.position + rotation * dir;
        transform.LookAt(lookAt.position);
        transform.position = new Vector3(transform.position.x +x, transform.position.y+1f, transform.position.z+z);

        Quaternion torque = Quaternion.Euler(0, currentX, 0);
        body.rotation = torque;

    }
}
