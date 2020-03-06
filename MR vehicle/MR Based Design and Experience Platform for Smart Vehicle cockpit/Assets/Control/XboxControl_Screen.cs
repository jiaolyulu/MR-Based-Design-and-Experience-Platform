using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class XboxControl_Screen : NetworkBehaviour
{
    private Vector3 movementVector;
    private float movementSpeed = 20;
    private bool triggerdBoard = false;
    private bool triggerdPlaceLeft = false;
    private bool triggerdPlaceRight = false;
    private bool triggerdPlaceFront = false;
    private bool triggerdPlaceTop = false;
    private Vector3 DriftBoard;
    Vector3 contantpoint;
     GameObject qianpaiFront;
    public GameObject screenFront;
    public GameObject screenLeft;
    public GameObject screenRight;
    public GameObject screenTop;
	public GameObject parentFront;
	public GameObject parentLeft;
	public GameObject parentRight;
	public GameObject parentTop;

    GameObject target;


    void spawnscreen(string style)
    {
		 GameObject spawnscreen;
        if (style == "triggerdPlaceLeft")
        {

			 spawnscreen = (GameObject)Instantiate(screenLeft, contantpoint, Quaternion.Euler(-20, 90, 0));

			NetworkServer.Spawn(spawnscreen);


        }
        else if (style == "triggerdPlaceRight")
        {

             spawnscreen = (GameObject)Instantiate(screenRight, contantpoint, Quaternion.Euler(-20, 270, 0));

			NetworkServer.Spawn(spawnscreen);

        }
        else if (style == "triggerdPlaceFront")
        {
            spawnscreen = (GameObject)Instantiate(screenFront, contantpoint, Quaternion.Euler(40, 0, 0));
			//spawnscreen.transform.parent = parentFront.transform;
			NetworkServer.Spawn(spawnscreen);


        }
        else if (style == "triggerdPlaceTop")
        {
             spawnscreen = (GameObject)Instantiate(screenTop, contantpoint, Quaternion.Euler(20, 0, 0));

			NetworkServer.Spawn(spawnscreen);


        }



    }
    [ClientRpc]
    void Rpcscale(string enlarge)
    {
        scale(enlarge);
    }
    void scale(string enlarge)
    {
        if (enlarge == "large")
        {
            target.transform.localScale += new Vector3(0.1F, 0.1F, 0);
        }
        else if (enlarge == "small")
        {
            target.transform.localScale -= new Vector3(0.1F, 0.1F, 0);
        }

    }
    [ClientRpc]
    void RpcDrift()
    {

        Drift();


    }
    void Drift()
    {

		DriftBoard = target.transform.position - transform.position;

    }
    [ClientRpc]
    void RpcDrag()
    {

        Drag();

    }
    void Drag()
    {
		//target.transform.position = transform.position  + DriftBoard;
        if (target.tag.Contains("Board_Front"))
        {
			qianpaiFront = parentFront;
			target.transform.position = new Vector3 (transform.position.x  + DriftBoard.x,target.transform.position.y,target.transform.position.z);
			var leftbound = qianpaiFront.transform.position.x - (qianpaiFront.GetComponent<BoxCollider>().size.x / 2 )+ (target.GetComponent<BoxCollider>().size.x / 2);
			var rightbound = qianpaiFront.transform.position.x + (qianpaiFront.GetComponent<BoxCollider>().size.x / 2) - (target.GetComponent<BoxCollider>().size.x / 2);
			//		var topbound = qianpaiFront.transform.position.y + (qianpaiFront.GetComponent<BoxCollider>().size.y / 2) + (target.GetComponent<BoxCollider>().size.y / 2);
			//		var bottombound = qianpaiFront.transform.position.y - (qianpaiFront.GetComponent<BoxCollider>().size.y / 2 )- (target.GetComponent<BoxCollider>().size.y / 2);
			target.transform.position = new Vector3 (Mathf.Clamp (target.transform.position.x, leftbound,rightbound),target.transform.position.y,target.transform.position.z);

				
        }
        else if (target.tag.Contains("Board_Left"))
        {
			qianpaiFront = parentLeft;
			target.transform.position = new Vector3 (target.transform.position.x,target.transform.position.y,transform.position.z + DriftBoard.z);
			var leftbound = qianpaiFront.transform.position.z - (qianpaiFront.GetComponent<BoxCollider>().size.z / 2 )+ (target.GetComponent<BoxCollider>().size.z / 2);
			var rightbound = qianpaiFront.transform.position.z + (qianpaiFront.GetComponent<BoxCollider>().size.z / 2) - (target.GetComponent<BoxCollider>().size.z / 2);
			//		var topbound = qianpaiFront.transform.position.y + (qianpaiFront.GetComponent<BoxCollider>().size.y / 2) + (target.GetComponent<BoxCollider>().size.y / 2);
			//		var bottombound = qianpaiFront.transform.position.y - (qianpaiFront.GetComponent<BoxCollider>().size.y / 2 )- (target.GetComponent<BoxCollider>().size.y / 2);
			target.transform.position = new Vector3 (target.transform.position.x,target.transform.position.y,Mathf.Clamp (target.transform.position.z, leftbound,rightbound));
        }
        if (target.tag.Contains("Board_Right"))
        {
			qianpaiFront = parentRight;
			target.transform.position = new Vector3 (target.transform.position.x,target.transform.position.y,transform.position.z + DriftBoard.z);
			var leftbound = qianpaiFront.transform.position.z - (qianpaiFront.GetComponent<BoxCollider>().size.z / 2 )+ (target.GetComponent<BoxCollider>().size.z / 2);
			var rightbound = qianpaiFront.transform.position.z + (qianpaiFront.GetComponent<BoxCollider>().size.z / 2) - (target.GetComponent<BoxCollider>().size.z / 2);
			//		var topbound = qianpaiFront.transform.position.y + (qianpaiFront.GetComponent<BoxCollider>().size.y / 2) + (target.GetComponent<BoxCollider>().size.y / 2);
			//		var bottombound = qianpaiFront.transform.position.y - (qianpaiFront.GetComponent<BoxCollider>().size.y / 2 )- (target.GetComponent<BoxCollider>().size.y / 2);
			target.transform.position = new Vector3 (target.transform.position.x,target.transform.position.y,Mathf.Clamp (target.transform.position.z, leftbound,rightbound));
        }
        if (target.tag.Contains("Board_Top"))
        {
			qianpaiFront = parentTop;
			target.transform.position = new Vector3 (transform.position.x  + DriftBoard.x,target.transform.position.y,target.transform.position.z);
			var leftbound = qianpaiFront.transform.position.x - (qianpaiFront.GetComponent<BoxCollider>().size.x / 2 )+ (target.GetComponent<BoxCollider>().size.x / 2);
			var rightbound = qianpaiFront.transform.position.x + (qianpaiFront.GetComponent<BoxCollider>().size.x / 2) - (target.GetComponent<BoxCollider>().size.x / 2);
			//		var topbound = qianpaiFront.transform.position.y + (qianpaiFront.GetComponent<BoxCollider>().size.y / 2) + (target.GetComponent<BoxCollider>().size.y / 2);
			//		var bottombound = qianpaiFront.transform.position.y - (qianpaiFront.GetComponent<BoxCollider>().size.y / 2 )- (target.GetComponent<BoxCollider>().size.y / 2);
			target.transform.position = new Vector3 (Mathf.Clamp (target.transform.position.x, leftbound,rightbound),target.transform.position.y,target.transform.position.z);

        }


    }
	[ClientRpc]
	void RpcIndicator(string button)
	{

		Indicator(button);


	}
	void Indicator(string button)
	{

		if (button=="选中") {
			transform.Find ("选中").gameObject.GetComponent<Renderer> ().material.color = Color.green;
		} else {
			transform.Find("选中").gameObject.GetComponent<Renderer>().material.color = Color.white;
		}
		if (button=="移动") {
			transform.Find ("移动").gameObject.GetComponent<Renderer> ().material.color = Color.green;
		} else {
			transform.Find("移动").gameObject.GetComponent<Renderer>().material.color = Color.white;
		}
		if (button=="放大") {
			transform.Find ("放大").gameObject.GetComponent<Renderer> ().material.color = Color.green;
		} else {
			transform.Find("放大").gameObject.GetComponent<Renderer>().material.color = Color.white;
		}

		if (button=="缩小") {
			transform.Find ("缩小").gameObject.GetComponent<Renderer> ().material.color = Color.green;
		} else {
			transform.Find("缩小").gameObject.GetComponent<Renderer>().material.color = Color.white;
		}


	}

    void Update()
    {
//		transform.Find("放大").gameObject.GetComponent<Renderer>().material.color = Color.white;
//		transform.Find("缩小").gameObject.GetComponent<Renderer>().material.color = Color.white;
//		transform.Find("移动").gameObject.GetComponent<Renderer>().material.color = Color.white;
//		transform.Find("选中").gameObject.GetComponent<Renderer>().material.color = Color.white;
//
//
//		if (Input.GetKey ("joystick 1 button 0")) {
//			Indicator ("选中");
//			RpcIndicator("选中");
//
//		if (Input.GetKey ("joystick 1 button 3")) {
//				Indicator ("移动");
//				RpcIndicator("移动");
//		}
//		if (Input.GetKey ("joystick 1 button 1")) {
//				Indicator ("放大");
//				RpcIndicator("放大");
//			
//		}
//
//		if (Input.GetKey ("joystick 1 button 2")) {
//				Indicator ("缩小");
//				RpcIndicator("缩小");
//		}
//

        if (Input.GetKeyDown("joystick 1 button 0"))
        {
			Debug.Log("joystick 1 button 0");

            if (triggerdPlaceLeft)

            {

                spawnscreen("triggerdPlaceLeft");
            }
            else if (triggerdPlaceRight)
            {
                spawnscreen("triggerdPlaceRight");
            }
            else if (triggerdPlaceFront)
            {
                spawnscreen("triggerdPlaceFront");
            }
            else if (triggerdPlaceTop)
            {
                spawnscreen("triggerdPlaceTop");
            }

        }

        if (triggerdBoard)
        {
            if (Input.GetKeyDown("joystick 1 button 3"))
            {
                RpcDrift();
                Drift();
            }
            if (Input.GetKey("joystick 1 button 3"))
            {
                RpcDrag();
                Drag();
            }

            if (Input.GetKeyDown("joystick 1 button 1"))
            {
                Rpcscale("large");
                scale("large");
            }
            if (Input.GetKeyDown("joystick 1 button 2"))
            {
                Rpcscale("small");
                scale("small");
            }

        }

    }

    void OnTriggerEnter(Collider other)

    {

        if (other.tag.Contains("Board"))
        {
            triggerdBoard = true;
            other.GetComponent<Renderer>().material.color = Color.green;
            target = other.gameObject;
        }
        else if (other.tag.Contains("ScreenPlaceLeft"))
        {

            triggerdPlaceLeft = true;

			transform.Find("屏幕").GetComponent<Renderer>().material.color = Color.green;
			contantpoint = transform.position + new Vector3(-0.06f, 0,0);

        }
        else if (other.tag.Contains("ScreenPlaceRight"))
        {
            triggerdPlaceRight = true;

            transform.Find("屏幕").GetComponent<Renderer>().material.color = Color.green;
			contantpoint =transform.position + new Vector3(0.06f,0,0);

        }
        else if (other.tag.Contains("ScreenPlaceFront"))
        {
            triggerdPlaceFront = true;

            transform.Find("屏幕").GetComponent<Renderer>().material.color = Color.green;
			contantpoint =transform.position + new Vector3(0, 0,0.08f);

        }
        else if (other.tag.Contains("ScreenPlaceTop"))
        {
            triggerdPlaceTop = true;

            transform.Find("屏幕").GetComponent<Renderer>().material.color = Color.green;
			contantpoint =transform.position + new Vector3(0, 0,0.08f);

        }

    }
    void OnTriggerStay(Collider other)
    {



    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Board"))
        {
            other.GetComponent<Renderer>().material.color = Color.black;
            triggerdBoard = false;
            target = null;
        }
        else if (other.tag.Contains("ScreenPlaceLeft"))
        {
            triggerdPlaceLeft = false;

            transform.Find("屏幕").GetComponent<Renderer>().material.color = Color.white;

        }
        else if (other.tag.Contains("ScreenPlaceRight"))
        {
            triggerdPlaceRight = false;

            transform.Find("屏幕").GetComponent<Renderer>().material.color = Color.white;
        }
        else if (other.tag.Contains("ScreenPlaceFront"))
        {
            triggerdPlaceFront = false;
            transform.Find("屏幕").GetComponent<Renderer>().material.color = Color.white;
        }
        else if (other.tag.Contains("ScreenPlaceTop"))
        {
            triggerdPlaceTop = false;
            transform.Find("屏幕").GetComponent<Renderer>().material.color = Color.white;
        }


    }
}