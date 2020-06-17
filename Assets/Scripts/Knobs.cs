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
    private float[] positions;

    private bool IsEnter;
    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            IsTake = true;
        }
    }
    private int Round(float position)
    {
        return (int)(Math.Round((330 - position) / 33.75));
    }
    void Start()
    {
        values = new float[] { 1, 2, 5, 10, 20, 50, 100, 200, 500 };
        positions = new float[] { 330, 296.25f, 262.5f, 228.75f, 195, 161.25f, 127.5f, 93.75f, 60 };

        prevZ = transform.rotation.eulerAngles.z;
        Z = transform.rotation.eulerAngles.z;
    }
    void OnMouseEnter()
    {
        IsEnter = true;
    }

    void OnMouseExit()
    {
        IsEnter = false;
    }

    void Update()
    {
        if (IsEnter && Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            var index = Round(Z);

            if (index < 8)
            {
                Z = positions[index + 1];
                Value = values[index + 1];

                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                        transform.localEulerAngles.y, Z);
            }
        }
        else if (IsEnter && Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            var index = Round(Z);

            if (index > 0)
            {
                Z = positions[index - 1];
                Value = values[index - 1];

                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                        transform.localEulerAngles.y, Z);
            }
        }

        if (Input.GetMouseButton(0) && IsTake)
        {
            var newZ = Z + (Input.GetAxis("Mouse X") * sensetivity);

            if (newZ <=330 && newZ >= 60)
            {
                var index = Round(newZ);
                Z = positions[index];
                Value = values[index];

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
