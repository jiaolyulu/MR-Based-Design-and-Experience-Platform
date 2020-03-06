using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class XboxControl_Color : NetworkBehaviour
{

    private bool triggerd = false;
    public ColorManager cm;
    LineRenderer laserLine;
    GameObject target;
    Camera fpsCam;
    RaycastHit hit;
    Vector3 hitpoint;
    private bool coloron = false;
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();

        fpsCam = GetComponent<Camera>();

    }


    [ClientRpc]
    void RpcColoron()
    {
        Coloron();
    }
    void Coloron()
    {
        coloron = !coloron;
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
    void Update()
    {
//		transform.Find("选中").gameObject.GetComponent<Renderer>().material.color = Color.white;
//		if (Input.GetKey("joystick 4 button 1"))
//		{
//			Indicator ("选中");
//			RpcIndicator ("选中");
//		}
        laserLine.SetPosition(0, transform.position);

        laserLine.material.color = cm.color;
        

        if (Input.GetKeyDown("joystick 4 button 1"))
        {
            Debug.Log("joystick 4");
            Coloron();
			RpcColoron ();

        }
        if (coloron)
        {
            laserLine.SetPosition(1, transform.position + fpsCam.transform.forward * 5);
            if (Physics.Raycast(transform.position, fpsCam.transform.forward, out hit))
            {

                if (hit.collider.tag.Contains("Color"))
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = cm.color; ;
                    laserLine.SetPosition(1, hit.point);

                }
            }
        }
        else
        {
            laserLine.SetPosition(1, transform.position);
        }








    }


}
