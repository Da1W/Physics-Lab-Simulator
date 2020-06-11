using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using AwesomeCharts;

public class Graph : MonoBehaviour
{
    
    private LineDataSet dataSet;
    public LineChart chart;

    public Knobs msPerDelKnob;
    public VoltKnob VPerDelknob;
    public RKnobs R100;
    public RKnobs R10;

    public Text V_del;
    public Text Ms_del;

    public Text R100_txt;
    public Text R10_txt;

    public PowerButton GDOPowerButton;
    public PowerButton OSCPowerButton;

    GDO gdo;

    Oscillograph osc;

    public void Initialize()
    {
        gdo = new GDO();
        osc = new Oscillograph(gdo);
        dataSet = new LineDataSet();

        dataSet.LineThickness = 3;
        dataSet.LineColor = UnityEngine.Color.green;

        chart.YAxis.MaxAxisValue = VPerDelknob.Value * 5;
        chart.YAxis.MinAxisValue = -VPerDelknob.Value * 5;

        V_del.text = Math.Round(VPerDelknob.Value, 2).ToString() + " В/дел";
        Ms_del.text = (chart.XAxis.MaxAxisValue / 10).ToString() + " Мс/дел";
    }

    public void OffOsc()
    {
        dataSet.Clear();
        chart.SetDirty();
        osc.IsOn = false;
    }

    private void BindButtons()
    {
        GDOPowerButton.SwitchOn += () => gdo.IsOn = true;
        GDOPowerButton.SwitchOn += () => DrawFunction(dataSet);
        GDOPowerButton.SwitchOn += () =>
        {
            GDOPowerButton.GetComponent<Animation>().Play("OffPBGDO");
        };

        GDOPowerButton.SwitchOff += () => gdo.IsOn = false;
        GDOPowerButton.SwitchOff += () => DrawFunction(dataSet);
        GDOPowerButton.SwitchOff += () =>
        {
            GDOPowerButton.GetComponent<Animation>().Play("OnPBGDO");
        };

        OSCPowerButton.SwitchOn += () => osc.IsOn = true;
        OSCPowerButton.SwitchOn += () => DrawFunction(dataSet);
        OSCPowerButton.SwitchOn += () =>
        {
            OSCPowerButton.GetComponent<Animation>().Play("OnPBOsc");
        };

        OSCPowerButton.SwitchOff += () => OffOsc();
        OSCPowerButton.SwitchOff += () =>
        {
            OSCPowerButton.GetComponent<Animation>().Play("OffPBOsc");
        };
    }

    void Start()
    {
        Initialize();
        BindButtons();
    }

    private void DrawFunction(LineDataSet dataSet)
    {
        if (!osc.IsOn) return;

        dataSet.Clear();
        var g = osc.Graph;
        for (int i = 0; i < g.Length; i++)
        {
            dataSet.AddEntry(new LineEntry((float)g[i].X, (float)g[i].Y));
        }
        chart.GetChartData().DataSets.Add(dataSet);
        chart.SetDirty();
    }

    void Update()
    {
        //Временная развёрстка
        if (msPerDelKnob.dZ != 0)
        {
            chart.XAxis.MaxAxisValue = msPerDelKnob.Value / 100;

            if (!msPerDelKnob.IsTake)
            {
                chart.SetDirty();
                Ms_del.text = Math.Round((chart.XAxis.MaxAxisValue / 10), 2).ToString() + " Мс/дел";
            }
        }

        //Развёрстка по Y
        if (VPerDelknob.dZ != 0)
        {
            chart.YAxis.MaxAxisValue = VPerDelknob.Value * 5;
            chart.YAxis.MinAxisValue = -VPerDelknob.Value * 5;

            if (!VPerDelknob.IsTake)
            {
                chart.SetDirty();
                V_del.text = Math.Round(VPerDelknob.Value, 2).ToString() + " В/дел";
            }
        }
  
        //Сопротивление
        if (GDOPowerButton.IsOn)
        {
            if (R100.dZ != 0 && !R100.IsTake)
            {
                gdo.R100 += (int)Math.Round(R100.dZ * 0.3703 * 10);
                DrawFunction(dataSet);
                chart.SetDirty();
            }
            R100_txt.text = gdo.R100.ToString();


            if (R10.dZ != 0 && !R10.IsTake)
            {
                gdo.R10 += (int)Math.Round(R10.dZ * 0.3703);
                DrawFunction(dataSet);
                chart.SetDirty();
            }
            R10_txt.text = gdo.R10.ToString();
        }
        else 
        {
            R10_txt.text = "";
            R100_txt.text = "";
        }
    }

    public class Oscillograph
    {
        public double MsPerDel { get; set; }
        public double VPerDel { get; set; }
        public bool IsOn;

        private readonly GDO _gdo;

        private double MaxT => 10 * (MsPerDel / 1000);
        private double Step => MaxT / 2000;
        public Point[] Graph => _gdo.Generate(Step);

        public Oscillograph(GDO gdo)
        {
            VPerDel = 200; // mV
            MsPerDel = 500; //mkS
            _gdo = gdo;
        }
    }

    public class GDO
    {
        private const int ArraySize = 2000;
        public double R0 { get; }
        public double L { get; }
        public double C { get; }

        public int R100 { get; set; }
        public int R10 { get; set; }
        public double Phase0 { get; }

        public bool IsOn;

        public GDO()
        {
            var rnd = new System.Random();
            R0 = rnd.Next(18, 22); // 20 ± 2 om
            L = rnd.Next(9, 11) * 0.001; // 0,01 ± 0,001 Gn
            C = rnd.Next(45, 55) * Math.Pow(10, -9); // 5 ± 0,5 *10^-8 F
            Phase0 = rnd.Next(785, 1133) * -0.001; // –0,959 ± 0,174 rad
        }

        public Point[] Generate(double step)
        {
            if (!IsOn)
            {
                var empty = new Point[ArraySize];
                for (var i = 0; i < ArraySize; i++)
                {
                    var t = step * i;
                    var point = new Point(t, 0);

                    empty[i] = point;
                }
                return empty;
            }
            var r = R100 + R10 + R0;
            var beta = r / (2 * L);
            var omega0 = 1 / Math.Sqrt(L * C);
            var omega = Math.Sqrt(omega0 * omega0 + beta * beta);
            var result = new Point[ArraySize];

            for (var i = 0; i < ArraySize; i++)
            {
                var t = step * i;
                var point = new Point(t, GetY(t, omega, beta));

                result[i] = point;
            }

            return result;

        }

        public double GetY(double t, double omega, double beta)
        {
            return 0.9 * Math.Pow(Math.E, -beta * t / 1000) * Math.Cos(omega * t / 1000 + Phase0);
        }

    }
    public class Point
    {
        public double X { get; }
        public double Y { get; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{{X={X}, Y={Y}}}";
        }
    }
}
