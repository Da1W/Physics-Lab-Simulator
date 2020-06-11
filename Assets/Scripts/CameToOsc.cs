using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameToOsc : MonoBehaviour
{
    public bool IsTap;
    public CameToGDO gdo;
    public GameObject ToGDOButton;
    public GameObject ToGraphButton;
    public Animation CameraToMove;

    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsTap && !gdo.IsTap)
        {
            IsTap = true;
            CameraToMove.Play("CameraToOsc");
            ToGDOButton.SetActive(true);
            ToGraphButton.SetActive(true);
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
            CameraToMove.Play("OscToCamera");
            ToGDOButton.SetActive(false);
            ToGraphButton.SetActive(false);
        }
    }
}
