using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationManager : MonoBehaviour 
{
    public Animation CameraToMove;
    public CameToGDO gdo;
    public CameToOsc osc;
    public bool IsTapGraph;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) && IsTapGraph)
        {
            IsTapGraph = false;
            CameraToMove.Play("GraphToOsc");
            osc.IsTap = true;
            osc.ToGDOButton.SetActive(true);
            osc.ToGraphButton.SetActive(true);
        }
    }

    public void ClickToGdo()
    {
        CameraToMove.Play("OscToGTDO");
        gdo.IsTap = true;
        osc.IsTap = false;
        osc.ToGraphButton.SetActive(false);
        osc.ToGDOButton.SetActive(false);
        gdo.ToOscButton.SetActive(true);
    }

    public void ClickToOsc()
    {
        CameraToMove.Play("GDOToOsc");
        osc.IsTap = true;
        gdo.IsTap = false;
        osc.ToGraphButton.SetActive(true);
        osc.ToGDOButton.SetActive(true);
        gdo.ToOscButton.SetActive(false);
    }

    public void ClickToGraph()
    {
        if (gdo.IsTap == false && osc.IsTap == true)
        {
            CameraToMove.Play("OscToGraph");
            IsTapGraph = true;
            osc.IsTap = false;
            osc.ToGDOButton.SetActive(false);
            osc.ToGraphButton.SetActive(false);
        }
    }
}
