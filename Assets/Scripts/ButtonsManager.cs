using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class ButtonsManager : MonoBehaviour
{
    public GameObject manual;
    public GameObject report;
    public Scrollbar manualSB;
    public Scrollbar reportSB;
    public List<GameObject> hints;
    public List<GameObject> OscHints;
    public CameToOsc osc;
    //public CameToGDO gdo;
    //public GDOToTop top;

    private bool help;
    private bool IsAllHintsOn = true;
    void Start()
    {
        
    }

    void Update()
    {
        if (IsAllHintsOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                foreach (var e in hints) e.SetActive(false);
                //foreach (var e in OscHints) e.SetActive(false);
                //if (osc.IsTap)
                //{
                //    help = true;
                //}

            }
            if (Input.GetMouseButtonDown(1))
            {
                report.SetActive(false);
                manual.SetActive(false);
            }
            //if (osc.IsTap && !help)
            //{
            //    foreach (var e in hints) e.SetActive(false);
            //    foreach (var e in OscHints) e.SetActive(true);
            //}
            //else if (!osc.IsTap)
            //{
            //    foreach (var e in hints) e.SetActive(false);
            //    foreach (var e in OscHints) e.SetActive(false);
            //    help = false;
            //}
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ManualButton()
    {
        if (!manual.activeInHierarchy)
        {
            manual.SetActive(true);
            report.SetActive(false);
        }
        else
        {
            manual.SetActive(false);
        }
    }

    public void ReportButton()
    {
        if (!report.activeInHierarchy)
        {
            report.SetActive(true);
            manual.SetActive(false);
        }
        else
        {
            report.SetActive(false);
        }
    }

    public void ControlButton()
    {
        IsAllHintsOn = !IsAllHintsOn;
    }

}
