using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System;

public class ButtonsManager : MonoBehaviour
{
    public GameObject manual;
    public GameObject report;

    public Scrollbar manualSB;
    public Scrollbar reportSB;

    public GameObject MainHints;
    public GameObject OscHints;
    public GameObject GDOHints;
    public GameObject TopHints;

    public AnimationManager am;
    public CameToOsc osc;
    public CameToGDO gdo;
    public GDOToTop top;

    private List<GameObject> AllHints = new List<GameObject>();
    private bool IsAllHintsOn = true;

    void Start()
    {
        AllHints.Add(MainHints);
        AllHints.Add(OscHints);
        AllHints.Add(GDOHints);
        AllHints.Add(TopHints);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            report.SetActive(false);
            manual.SetActive(false);
        }

        if (IsAllHintsOn)
        {
            if (osc.IsTap && !OscHints.activeInHierarchy)
            {
                OffAllHints();
                OscHints.SetActive(true);
            }
            else if (am.IsTapGraph && !osc.IsTap && OscHints.activeInHierarchy)
            {
                OffAllHints();
            }
            else if (gdo.IsTap && !GDOHints.activeInHierarchy)
            {
                OffAllHints();
                GDOHints.SetActive(true);
            }
            else if (top.IsTap && !gdo.IsTap && !TopHints.activeInHierarchy)
            {
                OffAllHints();
                TopHints.SetActive(true);
            }
            else if(!MainHints.activeInHierarchy && !osc.IsTap && 
                !gdo.IsTap && !top.IsTap && !am.IsTapGraph)
            {
                OffAllHints();
                MainHints.SetActive(true);
            }
        }
    }

    private void OffAllHints()
    {
        foreach (var e in AllHints) e.SetActive(false);
    }
    public void ExitButton()
    {
        Application.Quit();
    }

    public void ManualButton()
    {
        if (!manual.activeInHierarchy)
        {
            OffAllHints();
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
            OffAllHints();
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
        OffAllHints();
    }

}
