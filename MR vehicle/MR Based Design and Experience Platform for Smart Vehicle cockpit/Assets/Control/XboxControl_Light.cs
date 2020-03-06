using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class XboxControl_Light : NetworkBehaviour
{
    private Vector3 movementVector;
    private float movementSpeed = 20;
    private bool triggerd = false;
    //private bool triggerdDash = false;
    private Vector3 Drift;
    // private Vector3 DriftDash;
    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;
    public GameObject Light4;
    public GameObject Light5;
    bool triggerdLight1 = false;
    bool triggerdLight2 = false;
    bool triggerdLight3 = false;
    bool triggerdLight4 = false;
    bool triggerdLight5 = false;
    GameObject target;

    //private CharacterController characterController;
    // Use this for initialization
    void Start()
    {
        //characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame

    //void spawnlight(string style)
    //{
    //    if (style == "triggerdLight1")
    //    {
    //       Light1.SetActive(true);
    //    }
    //    else if (style == "triggerdLight2")
    //    {
    //        Light2.SetActive(true);
    //    }
    //    else if(style == "triggerdLight3")
    //    {
    //        Light3.SetActive(true);
    //    }
    //    else if (style == "triggerdLight4")
    //    {
    //        Light4.SetActive(true);
    //    }
    //    else if (style == "triggerdLight5")
    //    {
    //        Light5.SetActive(true);
    //    }

    //}
	[ClientRpc]
	void RpcLight(){
		Light ();



	}
	void Light(){
		if (triggerdLight1)

		{
			Light1.SetActive(true);
		}
		else if (triggerdLight2)

		{
			Light2.SetActive(true);
		}
		else if (triggerdLight3)

		{
			Light3.SetActive(true);
		}
		else if (triggerdLight4)

		{
			Light4.SetActive(true);
		}
		else if (triggerdLight5)

		{
			Light5.SetActive(true);
		}




	}
	void RpcIndicator(string button)
	{

		Indicator(button);


	}
	void Indicator(string button)
	{

		if (button=="选中") {
		transform.Find("选中_021").gameObject.GetComponent<Renderer>().material.color = Color.green;		} else {
			transform.Find("选中_021").gameObject.GetComponent<Renderer>().material.color = Color.white;		}



	}
    void Update()
    {
//		transform.Find("选中_021").gameObject.GetComponent<Renderer>().material.color = Color.white;
//        if (Input.GetKey("joystick 5 button 0"))
//        {
//			Indicator ("选中");
//			RpcIndicator ("选中");        }
       



        if (Input.GetKeyDown("joystick 5 button 0"))
        {
            Debug.Log("joystick 5");
			Light ();
			RpcLight ();

        }



    }
    void OnTriggerEnter(Collider other)

    {


        if (other.tag.Contains("Light1"))
        {
            triggerdLight1 = true;
            transform.Find("灯光_020").GetComponent<Renderer>().material.color = Color.green;
            // other.GetComponent<Renderer>().material.color = Color.green;
            target = other.gameObject;
        }
        else if (other.tag.Contains("Light2"))
        {
            triggerdLight2 = true;
            transform.Find("灯光_020").GetComponent<Renderer>().material.color = Color.green;
            // other.GetComponent<Renderer>().material.color = Color.green;
            target = other.gameObject;
        }
        else if (other.tag.Contains("Light3"))
        {
            triggerdLight3 = true;
            transform.Find("灯光_020").GetComponent<Renderer>().material.color = Color.green;
            // other.GetComponent<Renderer>().material.color = Color.green;
            target = other.gameObject;
        }
        else if (other.tag.Contains("Light4"))
        {
            triggerdLight4 = true;
            transform.Find("灯光_020").GetComponent<Renderer>().material.color = Color.green;
            // other.GetComponent<Renderer>().material.color = Color.green;
            target = other.gameObject;

        }
        else if (other.tag.Contains("Light5"))
        {
            triggerdLight5 = true;
            transform.Find("灯光_020").GetComponent<Renderer>().material.color = Color.green;
            // other.GetComponent<Renderer>().material.color = Color.green;
            target = other.gameObject;

        }
    }
    void OnTriggerStay(Collider other)
    {



    }
    void OnTriggerExit(Collider other)
    {
       if (other.tag.Contains("Light1"))
        {
            triggerdLight1 = false;
            //other.GetComponent<Renderer>().material.color = Color.white;
            transform.Find("灯光_020").GetComponent<Renderer>().material.color = Color.white;
            target = null;
        }
        else if (other.tag.Contains("Light2"))
        {
            triggerdLight2 = false;
            //other.GetComponent<Renderer>().material.color = Color.white;
            transform.Find("灯光_020").GetComponent<Renderer>().material.color = Color.white;
            target = null;
        }
        else if (other.tag.Contains("Light3"))
        {
            triggerdLight3 = false;
            //other.GetComponent<Renderer>().material.color = Color.white;
            transform.Find("灯光_020").GetComponent<Renderer>().material.color = Color.white;
            target = null;
        }
        else if (other.tag.Contains("Light4"))
        {
            triggerdLight4 = false;
            //other.GetComponent<Renderer>().material.color = Color.white;
            transform.Find("灯光_020").GetComponent<Renderer>().material.color = Color.white;
            target = null;
        }
        else if (other.tag.Contains("Light5"))
        {
            triggerdLight5 = false;
            //other.GetComponent<Renderer>().material.color = Color.white;
            transform.Find("灯光_020").GetComponent<Renderer>().material.color = Color.white;
            target = null;
        }


    }

}




