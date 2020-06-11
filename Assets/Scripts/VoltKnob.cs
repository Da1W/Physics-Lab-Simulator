using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltKnob : MonoBehaviour
{
    public float Z;
    float prevZ;
    public float dZ;
    float sensetivity = 6;
    public bool IsTake = false;
    public float Value;

    private float[] values;
    //private float[] positions;

    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            IsTake = true;
        }
    }

    public VoltKnob()
    {

    }

    private int Round(float position)
    {
        return (int)(-1*Math.Round((position - 330) / 22.5));
    }

    void Start()
    {
        values = new float[] { 0.002f, 0.005f, 0.01f, 0.02f, 0.05f, 0.1f, 0.2f, 0.5f, 1, 2, 5, 10, 20};
        //positions = new float[] { 330, 307.5f, 285, 262.5f, 240, 217.5f, 195, 172.5f, 150, 127.5f, 105, 82.5f, 60 };

        prevZ = transform.rotation.eulerAngles.z;
        Z = transform.rotation.eulerAngles.z;
        Value = values[Round(Z)];
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && IsTake)
        {
            var newZ = Z + (Input.GetAxis("Mouse Y") * sensetivity);

            if (newZ <= 330 && newZ >= 60)
            {
                var index = Round(newZ);
                Value = values[index];
                //Z = positions[index];
                Z = newZ;
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                    transform.localEulerAngles.y, Z);
            }
        }
        else
        {
            IsTake = false;
            dZ = prevZ - Z;
            prevZ = Z;
        }
    }
}
