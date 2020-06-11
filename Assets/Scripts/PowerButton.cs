using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PowerButton : MonoBehaviour
{
    public bool IsOn;
    public event Action SwitchOn;
    public event Action SwitchOff;
    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsOn)
        {
            IsOn = true;
            SwitchOn.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && IsOn)
        {
            IsOn = false;
            SwitchOff.Invoke();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
