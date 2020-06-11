using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDOToTop : MonoBehaviour
{
    public bool IsTap;
    public CameToGDO gdo;
    public GameObject ToOscButton;
    public Animation CameraToMove;
    public Animation TopToMove;
    //public Animation TopToMove;
    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsTap && gdo.IsTap)
        {
            IsTap = true;
            gdo.IsTap = false;
            TopToMove.Play("TopOpen");
            CameraToMove.Play("GDOToTop");
            TopToMove.Play();
            ToOscButton.SetActive(false);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) && IsTap)
        {
            IsTap = false;
            CameraToMove.Play("TopToGDO");
            TopToMove.Play("TopClose");
            gdo.IsTap = true;
            ToOscButton.SetActive(true);
        }
    }
}
