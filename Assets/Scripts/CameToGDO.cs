using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameToGDO : MonoBehaviour
{
    public bool IsTap;
    public CameToOsc osc;
    public GameObject ToOscButton;
    public Animation CameraToMove;

    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsTap && !osc.IsTap)
        {
            IsTap = true;
            CameraToMove.Play("CameraToGDO");
            ToOscButton.SetActive(true);
        }
    }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && IsTap)
        {
            IsTap = false;
            CameraToMove.Play("GdoToCamera");
            ToOscButton.SetActive(false);
        }
    }
}
