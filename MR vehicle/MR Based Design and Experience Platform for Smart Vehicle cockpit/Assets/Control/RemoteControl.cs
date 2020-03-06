using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteControl : MonoBehaviour
{
    public GameObject remote_light;
    public GameObject remote_screen;
    public GameObject remote_eraser;
    public GameObject remote_color;
    public GameObject remote_switch;
    public GameObject remote_window;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetAxis("D-Pad X Axis") == 1)
        if (Input.GetAxis("Right Stick X Axis") > 0)
        {

            remote_light.GetComponent<XboxControl_Light>().enabled = true;
            remote_light.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.green;
            remote_screen.GetComponent<XboxControl_Screen>().enabled = false;
            remote_screen.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;
            remote_eraser.GetComponent<XboxControl_Eraser>().enabled = false;
            remote_eraser.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;
            remote_color.GetComponent<XboxControl_Color>().enabled = false;
            remote_color.transform.Find("Obj_000013").GetComponent<Renderer>().material.color = Color.white;
            remote_switch.GetComponent<XboxControl_Switch>().enabled = false;
            remote_switch.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;



        }
        //else if (Input.GetAxis("D-Pad X Axis") == -1)
        else if (Input.GetAxis("Right Stick X Axis") < 0)
        {
            remote_light.GetComponent<XboxControl_Light>().enabled = false;
            remote_light.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;
            remote_screen.GetComponent<XboxControl_Screen>().enabled = true;
            remote_screen.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.green;
            remote_eraser.GetComponent<XboxControl_Eraser>().enabled = false;
            remote_eraser.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;
            remote_color.GetComponent<XboxControl_Color>().enabled = false;
            remote_color.transform.Find("Obj_000013").GetComponent<Renderer>().material.color = Color.white;
            remote_switch.GetComponent<XboxControl_Switch>().enabled = false;
            remote_switch.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;

        }
        //else if (Input.GetAxis("D-Pad Y Axis") == 1)
        else if (Input.GetAxis("Right Stick Y Axis") > 0)
        {
            remote_light.GetComponent<XboxControl_Light>().enabled = false;
            remote_light.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;
            remote_screen.GetComponent<XboxControl_Screen>().enabled = false;
            remote_screen.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;
            remote_eraser.GetComponent<XboxControl_Eraser>().enabled = true;
            remote_eraser.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.green;
            remote_color.GetComponent<XboxControl_Color>().enabled = false;
            remote_color.transform.Find("Obj_000013").GetComponent<Renderer>().material.color = Color.white;
            remote_switch.GetComponent<XboxControl_Switch>().enabled = false;
            remote_switch.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;
        }
        //else if (Input.GetAxis("D-Pad Y Axis") == -1)
        else if (Input.GetAxis("Right Stick Y Axis") < 0)
        {
            remote_light.GetComponent<XboxControl_Light>().enabled = false;
            remote_light.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;
            remote_screen.GetComponent<XboxControl_Screen>().enabled = false;
            remote_screen.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;
            remote_eraser.GetComponent<XboxControl_Eraser>().enabled = false;
            remote_eraser.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;
            remote_color.GetComponent<XboxControl_Color>().enabled = true;
            remote_color.transform.Find("Obj_000013").GetComponent<Renderer>().material.color = Color.green;
            remote_switch.GetComponent<XboxControl_Switch>().enabled = false;
            remote_switch.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;


        }
        else if (Input.GetButtonDown("Right Stick Click")) {
            remote_light.GetComponent<XboxControl_Light>().enabled = false;
            remote_light.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;
            remote_screen.GetComponent<XboxControl_Screen>().enabled = false;
            remote_screen.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;
            remote_eraser.GetComponent<XboxControl_Eraser>().enabled = false;
            remote_eraser.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.white;
            remote_color.GetComponent<XboxControl_Color>().enabled = false;
            remote_color.transform.Find("Obj_000013").GetComponent<Renderer>().material.color = Color.white;
            remote_switch.GetComponent<XboxControl_Switch>().enabled = true;
            remote_switch.transform.Find("Obj_000001").GetComponent<Renderer>().material.color = Color.green;



        }
    }
}
