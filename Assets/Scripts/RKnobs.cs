using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RKnobs : MonoBehaviour
{
    float Z;
    float prevZ;
    public float dZ;
    float sensetivity = 6;
    public bool IsTake = false;
    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            IsTake = true;
        }
    }
    void Start()
    {
        prevZ = transform.rotation.eulerAngles.z;
        Z = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && IsTake)
        {
            var input = Z + (-Input.GetAxis("Mouse Y") * sensetivity);

            if (Mathf.Abs(input) < 121.6)
            {
                Z = input;
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
