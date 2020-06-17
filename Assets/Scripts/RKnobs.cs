using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RKnobs : MonoBehaviour
{
    float Z;
    float prevZ;
    public float dZ;
    //float sensetivity = 6;
    public bool IsTake = false;

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
    void OnMouseEnter()
    {
        IsEnter = true;
    }

    void OnMouseExit()
    {
        IsEnter = false;
    }

    void Start()
    {
        positions = new float[] { 121.5f, 94.5f, 67.5f, 40.5f, 13.5f, -13.5f, -40.5f, -67.5f, -94.5f, -121.5f };

        prevZ = transform.rotation.eulerAngles.z;
        Z = transform.rotation.eulerAngles.z;
    }
    private int Round(float position)
    {
        return (int)(Math.Round((121.5 - position) / 27));
    }

    void Update()
    {
        if (IsEnter && Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            var index = Round(Z);

            if (index < 9)
            {
                Z = positions[index + 1];
                dZ = -27;

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
                dZ = 27;
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                        transform.localEulerAngles.y, Z);
            }
        }

        //if (Input.GetMouseButton(0) && IsTake)
        //{
        //    var input = Z + (-Input.GetAxis("Mouse X") * sensetivity);

        //    if (Mathf.Abs(input) < 121.6)
        //    {
        //        Z = input;
        //        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
        //            transform.localEulerAngles.y, Z);
        //    }
        //}
        //else
        //{
        //    IsTake = false;
        //    dZ = prevZ - Z;
        //    prevZ = Z;
        //}
    }
}
