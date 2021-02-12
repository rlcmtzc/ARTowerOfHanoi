using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vuforia;
using System.Numerics;
using Test;

public class GameHandler : MonoBehaviour
{
    public GameObject btnt1Right;
    public GameObject btnt1Left;
    public GameObject btnt2Left;
    public GameObject btnt2Right;
    public GameObject btnt3Left;
    public GameObject btnt3Right;

    public GameObject disk1;
    public GameObject disk2;
    public GameObject disk3;

    public TextMesh infoText;

    //Start positions: 
    //      Disk1: 0, 0.005, 0.1
    //      Disk2: 0, 0.015, 0.1
    //      Disk3: 0, 0.025, 0.1

    private Tower Tower1 { get; } = new Tower(0.1f);
    private Tower Tower2 { get; } = new Tower(0.0f);
    private Tower Tower3 { get; } = new Tower(-0.1f);

    string planeName;

    // Use this for initialization
    void Start()
    {
        Reset();
        
        btnt1Right = GameObject.Find("Btnt1Right");
        btnt1Right.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleasedt1Right);

        btnt1Left = GameObject.Find("Btnt1Left");
        btnt1Left.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleasedt1Left);

        btnt2Left = GameObject.Find("Btnt2Left");
        btnt2Left.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleasedt2Left);

        btnt2Right = GameObject.Find("Btnt2Right");
        btnt2Right.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleasedt2Right);

        btnt3Left = GameObject.Find("Btnt3Left");
        btnt3Left.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleasedt3Left);

        btnt3Right = GameObject.Find("Btnt3Right");
        btnt3Right.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleasedt3Right);

        infoText.GetComponent<TextMesh>();

        
    }

    private void Reset()
    {
        Tower1.AddDisk(disk1);
        Tower1.AddDisk(disk2);
        Tower1.AddDisk(disk3);
    }
    
    private void MoveDiskFromTower(Tower SourceTower, Tower TargetTower)
    {
        infoText.text = "";
        if (!SourceTower.MoveDisk(TargetTower))
        {
            infoText.text = "Invalid Move";
            infoText.color = Color.red;
        }
    }
    public void OnButtonReleasedt1Right(VirtualButtonBehaviour vb)
    {
        MoveDiskFromTower(Tower1, Tower2);
    }
 
    public void OnButtonReleasedt1Left(VirtualButtonBehaviour vb)
    {
        MoveDiskFromTower(Tower1, Tower3);
        if (Tower3.IsWinFormation())
        {
            infoText.text = "WIN!!!";
            infoText.color = Color.green;
            Tower3.ClearTower();
            Reset();
        }
    }
 
    public void OnButtonReleasedt2Left(VirtualButtonBehaviour vb)
    {
        MoveDiskFromTower(Tower2, Tower1);
    }

    public void OnButtonReleasedt2Right(VirtualButtonBehaviour vb)
    {
        MoveDiskFromTower(Tower2, Tower3);
        if (Tower3.IsWinFormation())
        {
            infoText.text = "WIN!!!";
            infoText.color = Color.green;
            Tower3.ClearTower();
            Reset();
        }
    }

    public void OnButtonReleasedt3Left(VirtualButtonBehaviour vb)
    {
        MoveDiskFromTower(Tower3, Tower2);
    }
 
    public void OnButtonReleasedt3Right(VirtualButtonBehaviour vb)
    {
        MoveDiskFromTower(Tower3, Tower1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit))
            {
                planeName = Hit.transform.name;
                switch(planeName)
                {
                    case "PlaneBtn1Right":
                        MoveDiskFromTower(Tower1, Tower2);
                        break;
                    case "PlaneBtn1Left":
                        MoveDiskFromTower(Tower1, Tower3);
                        if (Tower3.IsWinFormation())
                        {
                            infoText.text = "WIN!!!";
                            infoText.color = Color.green;
                            Tower3.ClearTower();
                            Reset();
                        }
                        break;
                    case "PlaneBtn2Right":
                        MoveDiskFromTower(Tower2, Tower3);
                        if (Tower3.IsWinFormation())
                        {
                            infoText.text = "WIN!!!";
                            infoText.color = Color.green;
                            Tower3.ClearTower();
                            Reset();
                        }
                        break;
                    case "PlaneBtn2Left":
                        MoveDiskFromTower(Tower2, Tower1);
                        break;
                    case "PlaneBtn3Right":
                        MoveDiskFromTower(Tower3, Tower1);
                        break;
                    case "PlaneBtn3Left":
                        MoveDiskFromTower(Tower3, Tower2);
                        break;
                }
            }
            
        }
    }
}
