using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class XboxControl_Switch : NetworkBehaviour
{

    private bool triggerd = false;

    LineRenderer laserLine;
    public GameObject BackRow1;
    public GameObject BackRow2;
    public GameObject BackRow3;
    public GameObject BackRow4;
    public GameObject BackRow5;
    public GameObject BackRow6;
    public GameObject BackRow7;
    public GameObject buju1;
    public GameObject buju2;
    //public GameObject buju3;
    //public GameObject buju4;
    
    GameObject hitobject;


    Camera fpsCam;
    RaycastHit hit;
    // Use this for initialization
    [ClientRpc]
    void RpcSwtichBig()
    {
        if (buju1.activeInHierarchy)
        {
            buju1.SetActive(false);
            buju2.SetActive(true);
        }
        else if (buju2.activeInHierarchy)
        {
            buju1.SetActive(true);
            buju2.SetActive(false);
        }
    }
    [ClientRpc]
    void RpcSwtichBackrow()
    {
        if (BackRow1.activeInHierarchy)
        {
            BackRow1.SetActive(false);
            BackRow2.SetActive(true);
            BackRow3.SetActive(false);
            BackRow4.SetActive(false);
            BackRow5.SetActive(false);
            BackRow6.SetActive(false);
            BackRow7.SetActive(false);
        }
        else if (BackRow2.activeInHierarchy)
        {
            BackRow1.SetActive(false);
            BackRow2.SetActive(false);
            BackRow3.SetActive(true);
            BackRow4.SetActive(false);
            BackRow5.SetActive(false);
            BackRow6.SetActive(false);
            BackRow7.SetActive(false);
        }
        else if (BackRow3.activeInHierarchy)
        {
            BackRow1.SetActive(false);
            BackRow2.SetActive(false);
            BackRow3.SetActive(false);
            BackRow4.SetActive(true);
            BackRow5.SetActive(false);
            BackRow6.SetActive(false);
            BackRow7.SetActive(false);
        }
        else if (BackRow4.activeInHierarchy)
        {
            BackRow1.SetActive(false);
            BackRow2.SetActive(false);
            BackRow3.SetActive(false);
            BackRow4.SetActive(false);
            BackRow5.SetActive(true);
            BackRow6.SetActive(false);
            BackRow7.SetActive(false);
        }
        else if (BackRow5.activeInHierarchy)
        {
            BackRow1.SetActive(false);
            BackRow2.SetActive(false);
            BackRow3.SetActive(false);
            BackRow4.SetActive(false);
            BackRow5.SetActive(false);
            BackRow6.SetActive(true);
            BackRow7.SetActive(false);
        }
        else if (BackRow6.activeInHierarchy)
        {
            BackRow1.SetActive(false);
            BackRow2.SetActive(false);
            BackRow3.SetActive(false);
            BackRow4.SetActive(false);
            BackRow5.SetActive(false);
            BackRow6.SetActive(false);
            BackRow7.SetActive(true);
        }
        else if (BackRow7.activeInHierarchy)
        {
            BackRow1.SetActive(true);
            BackRow2.SetActive(false);
            BackRow3.SetActive(false);
            BackRow4.SetActive(false);
            BackRow5.SetActive(false);
            BackRow6.SetActive(false);
            BackRow7.SetActive(false);
        }


    }
	void RpcIndicator(string button)
	{

		Indicator(button);


	}
	void Indicator(string button)
	{

		if (button=="选中") {
			transform.Find("选中").GetComponent<Renderer>().material.color = Color.green;
		} else {
			transform.Find("选中").GetComponent<Renderer>().material.color = Color.white;
		}



	}
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();

        fpsCam = GetComponent<Camera>();
    }

    
    void Update()
	{
//		transform.Find("选中").GetComponent<Renderer>().material.color = Color.white;
//        if (Input.GetKey("joystick 3 button 0"))
//        {
//			Indicator ("选中");
//			RpcIndicator ("选中");
//        }
      
        if (Input.GetKeyDown("joystick 3 button 3"))
        {
            RpcSwtichBig();
            if (buju1.activeInHierarchy)
            {
                buju1.SetActive(false);
                buju2.SetActive(true);
            }
            else if (buju2.activeInHierarchy)
            {
                buju1.SetActive(true);
                buju2.SetActive(false);
            }
        }
        if (Input.GetKeyDown("joystick 3 button 0"))
        {
			Debug.Log("joystick 3 button 0");
        }
            bool objecthit = false;
        

        laserLine.SetPosition(0, transform.position);
        
        if (Physics.Raycast(transform.position, fpsCam.transform.forward, out hit))
        {
            
			if (hit.collider.tag.Contains ("SwtichBackrow"))
            {
                laserLine.SetPosition(1, hit.point);
                hitobject = hit.collider.gameObject;
                objecthit = true;
                //hitobject.GetComponent<Renderer>().material.color = Color.green;
                if (Input.GetKeyDown("joystick 3 button 0") )
                {
					Debug.Log ("joystick 3");
                    RpcSwtichBackrow();
                    if (BackRow1.activeInHierarchy)
                    {
                        BackRow1.SetActive(false);
                        BackRow2.SetActive(true);
                        BackRow3.SetActive(false);
                        BackRow4.SetActive(false);
                        BackRow5.SetActive(false);
                        BackRow6.SetActive(false);
                        BackRow7.SetActive(false);
                    }
                    else if (BackRow2.activeInHierarchy)
                    {
                        BackRow1.SetActive(false);
                        BackRow2.SetActive(false);
                        BackRow3.SetActive(true);
                        BackRow4.SetActive(false);
                        BackRow5.SetActive(false);
                        BackRow6.SetActive(false);
                        BackRow7.SetActive(false);
                    }
                    else if (BackRow3.activeInHierarchy)
                    {
                        BackRow1.SetActive(false);
                        BackRow2.SetActive(false);
                        BackRow3.SetActive(false);
                        BackRow4.SetActive(true);
                        BackRow5.SetActive(false);
                        BackRow6.SetActive(false);
                        BackRow7.SetActive(false);
                    }
                    else if (BackRow4.activeInHierarchy)
                    {
                        BackRow1.SetActive(false);
                        BackRow2.SetActive(false);
                        BackRow3.SetActive(false);
                        BackRow4.SetActive(false);
                        BackRow5.SetActive(true);
                        BackRow6.SetActive(false);
                        BackRow7.SetActive(false);
                    }
                    else if (BackRow5.activeInHierarchy)
                    {
                        BackRow1.SetActive(false);
                        BackRow2.SetActive(false);
                        BackRow3.SetActive(false);
                        BackRow4.SetActive(false);
                        BackRow5.SetActive(false);
                        BackRow6.SetActive(true);
                        BackRow7.SetActive(false);
                    }
                    else if (BackRow6.activeInHierarchy)
                    {
                        BackRow1.SetActive(false);
                        BackRow2.SetActive(false);
                        BackRow3.SetActive(false);
                        BackRow4.SetActive(false);
                        BackRow5.SetActive(false);
                        BackRow6.SetActive(false);
                        BackRow7.SetActive(true);
                    }
                    else if (BackRow7.activeInHierarchy)
                    {
                        BackRow1.SetActive(true);
                        BackRow2.SetActive(false);
                        BackRow3.SetActive(false);
                        BackRow4.SetActive(false);
                        BackRow5.SetActive(false);
                        BackRow6.SetActive(false);
                        BackRow7.SetActive(false);
                    }


                }
            }
        }
        else
        {
            
            laserLine.SetPosition(1, transform.position + fpsCam.transform.forward * 10);
        }
        if (objecthit)
        {
            hitobject.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GameObject.FindGameObjectWithTag("SwtichBackrow_Color").GetComponent<Renderer>().material.color = Color.white;
        }

    }
}
