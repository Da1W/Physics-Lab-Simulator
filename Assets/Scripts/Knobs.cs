using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knobs : MonoBehaviour
{
    float Z;
    float prevZ;
    public float dZ;
    float sensetivity = 6;
    public bool IsTake = false;
    public float Value = 500;
    private float[] values;
    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            IsTake = true;
        }
    }
    private int Round(float position)
    {
        return (int)Math.Round((position - 60) / 38.57);
    }
    void Start()
    {
        values = new float[] { 500, 200, 100, 50, 20, 10, 5, 2, 1 };
        prevZ = transform.rotation.eulerAngles.z;
        Z = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && IsTake)
        {
            var newZ = Z + (Input.GetAxis("Mouse Y") * sensetivity);

            if (newZ <=330 && newZ >= 60)
            {
                var index = Round(newZ);
                Value = values[index];
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
