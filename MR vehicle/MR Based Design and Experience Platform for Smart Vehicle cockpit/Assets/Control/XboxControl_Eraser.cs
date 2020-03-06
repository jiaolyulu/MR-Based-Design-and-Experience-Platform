using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class XboxControl_Eraser : NetworkBehaviour
{

    private bool triggerd = false;
    GameObject target;
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
    void Start () {
		
	}

    
    void destroy(GameObject target)
    {
       
            NetworkServer.Destroy(target);
        


    }
	[ClientRpc]
	void RpcPutout(){
		Putout ();



	}
	void Putout(){
		if (triggerdLight1)

		{
			Light1.SetActive(false);
		}
		else if (triggerdLight2)

		{
			Light2.SetActive(false);
		}
		else if (triggerdLight3)

		{
			Light3.SetActive(false);
		}
		else if (triggerdLight4)

		{
			Light4.SetActive(false);
		}
		else if (triggerdLight5)

		{
			Light5.SetActive(false);
		}



	}
	void RpcIndicator(string button)
	{

		Indicator(button);


	}
	void Indicator(string button)
	{

		if (button=="选中") {
			transform.Find("选中_009").GetComponent<Renderer>().material.color = Color.green;
		} else {
			transform.Find("选中_009").GetComponent<Renderer>().material.color = Color.white;
		}



	}
    void Update () {

//		transform.Find("选中_009").GetComponent<Renderer>().material.color = Color.white;
//        if (Input.GetKey("joystick 2 button 0"))
//        {
//			Indicator ("选中");
//			RpcIndicator ("选中");
//        }
       
        if (Input.GetKeyDown("joystick 2 button 0"))
        {
            Debug.Log("joystick 2 button 0");
            if (triggerd)
            {
              
                    destroy(target);
                

            }
			Putout ();
			RpcPutout ();


        }



    }
    void OnTriggerEnter(Collider other)
    {

        if (other.tag.Contains("Light1"))
        {
            triggerdLight1 = true;
            transform.Find("删除").GetComponent<Renderer>().material.color = Color.green;
            target = other.gameObject;
        }
        else if (other.tag.Contains("Light2"))
        {
            triggerdLight2 = true;
            
            transform.Find("删除").GetComponent<Renderer>().material.color = Color.green;
            target = other.gameObject;
        }
        else if (other.tag.Contains("Light3"))
        {
            triggerdLight3 = true;
            
            transform.Find("删除").GetComponent<Renderer>().material.color = Color.green;
            target = other.gameObject;
        }
        else if (other.tag.Contains("Light4"))
        {
            triggerdLight4 = true;
            
            transform.Find("删除").GetComponent<Renderer>().material.color = Color.green;
            target = other.gameObject;

        }
        else if (other.tag.Contains("Light5"))
        {
            triggerdLight5 = true;
            
            transform.Find("删除").GetComponent<Renderer>().material.color = Color.green;
            target = other.gameObject;

        }
		if (other.tag.Contains( "Board"))
        {
            triggerd = true;
            transform.Find("删除").GetComponent<Renderer>().material.color = Color.green;
            
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
            transform.Find("删除").GetComponent<Renderer>().material.color = Color.white;
            
            target = null;
        }
        else if (other.tag.Contains("Light2"))
        {
            triggerdLight2 = false;
            transform.Find("删除").GetComponent<Renderer>().material.color = Color.white;
            
            target = null;
        }
        else if (other.tag.Contains("Light3"))
        {
            triggerdLight3 = false;
            transform.Find("删除").GetComponent<Renderer>().material.color = Color.white;
            
            target = null;
        }
        else if (other.tag.Contains("Light4"))
        {
            triggerdLight4 = false;
            transform.Find("删除").GetComponent<Renderer>().material.color = Color.white;
            
            target = null;
        }
        else if (other.tag.Contains("Light5"))
        {
            triggerdLight5 = false;
            transform.Find("删除").GetComponent<Renderer>().material.color = Color.white;
            
            target = null;
        }
		if (other.tag.Contains( "Board"))
        {
            transform.Find("删除").GetComponent<Renderer>().material.color = Color.white;
            
            triggerd = false;
            target = null;
        }

    }
}
