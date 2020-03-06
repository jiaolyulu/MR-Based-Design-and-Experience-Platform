using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[NetworkSettings(channel = 0, sendInterval = 0f)]
public class Intersight_Player : NetworkBehaviour {
    public Transform OVRCamera;
    public Transform OVREyeCenter;
    public Transform OVRHMD;

    public UI_ItemManager SelectAssetsWindow;
    [SyncVar]
    public bool OthersOperating = false;
    [SyncVar]
    public bool IsOperating = false;
    [SyncVar]
    public string LoadState;
    public OptitrackStreamingClient StreamingClient;

    public Intersight_AssetsManager AssetsManager;

    public void OnEnable()
    {
        AssetsManager = FindObjectOfType<Intersight_AssetsManager>();
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Destroy(Camera.main.gameObject);
        OVRCamera.gameObject.SetActive(true);
        OVRCamera.eulerAngles = new Vector3(0.0f, -OVREyeCenter.eulerAngles.y, 0.0f);
        SelectAssetsWindow.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update () {

        SetOperatingTag(IsOperating);
       
        if (isLocalPlayer)
        {
			OVRHMD.GetComponent<OptitrackRigidBody> ().RigidBodyId = NetInfoView_Client.ClientID;
            RaycastDetect();
            ModelControll();
            if(OVRInput.GetDown(OVRInput.Button.Back))
            {
                SetOperating();
            }
			OVRCamera.position = OVRHMD.position - OVREyeCenter.localPosition - OVREyeCenter.forward * 0.05f - OVREyeCenter.up * 0.04f;

        }
    }

    void FixedUpdate()
    {

        if (!isLocalPlayer)
            return;
        if (isLocalPlayer)
        {
            if (Intersight_LoadAssets.LoadComplete)
            {
                CmdSetLoadState("Loaded");
                CmdSetOVRHMDRot(OVREyeCenter.eulerAngles);
				CmdSetOVRHMDPos (OVRHMD.position);
                CmdSetController(OVREyeCenter.eulerAngles, transform.position);
            }
            else
            {
                 //CmdSetLoadState("Loading");
            }
            
        }
    }

    void ModelControll()
    {
       
        if (OVRInput.Get(OVRInput.Button.Start))
        {
                
            if (OVRInput.Get(OVRInput.Button.Left))
            {
                CmdRotateObject(CurrModelIndex, AssetsManager.ModelsInScene[CurrModelIndex].transform.eulerAngles - new Vector3(0.0f, Time.deltaTime * 40.0f, 0.0f));
            }
            if (OVRInput.Get(OVRInput.Button.Up))
            {
                CmdRotateObject(CurrModelIndex, AssetsManager.ModelsInScene[CurrModelIndex].transform.eulerAngles + new Vector3(0.0f, Time.deltaTime * 40.0f, 0.0f));
            }
        }
        else
        {
            if (OVRInput.Get(OVRInput.Button.Left))
            {
                CmdMoveObject(CurrModelIndex, AssetsManager.ModelsInScene[CurrModelIndex].transform.position + Time.deltaTime * Vector3.left);
            }
            if (OVRInput.Get(OVRInput.Button.Up))
            {
                CmdMoveObject(CurrModelIndex, AssetsManager.ModelsInScene[CurrModelIndex].transform.position + Time.deltaTime * Vector3.forward);
            }
            if (OVRInput.Get(OVRInput.Button.Right))
            {
                CmdMoveObject(CurrModelIndex, AssetsManager.ModelsInScene[CurrModelIndex].transform.position + Time.deltaTime * Vector3.right);
            }


            if (OVRInput.Get(OVRInput.Button.Down))
            {
                CmdMoveObject(CurrModelIndex, AssetsManager.ModelsInScene[CurrModelIndex].transform.position + Time.deltaTime * Vector3.back);
            }

        }

    }


    void RaycastDetect()
    {
        Ray ray = new Ray(OVRHMD.position, OVRHMD.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            switch (hit.transform.tag)
            {
                case "AssetItem":
                    SelectAssetToCreate(hit.transform.gameObject);
                    break;
                case "ModelInScene":
                    SelectModelToMove(hit.transform.gameObject);
                    break;
            }
        }
    }

    public int CurrModelIndex = 0;



    GameObject currAssetItem;
    void SelectAssetToCreate(GameObject selectAsset)
    {
        if (currAssetItem != null)
        {
            currAssetItem.transform.localScale = Vector3.one * 0.001f;
        }
        currAssetItem = selectAsset;
        currAssetItem.transform.localScale = Vector3.one * 0.0012f;

        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            CmdInstantiateNew(selectAsset.name, Vector3.zero, Quaternion.identity, Vector3.one);
        }
    }

    void SelectModelToMove(GameObject model)
    {
        
        CurrModelIndex = model.GetComponent<Intersight_ModelAsset>().index;

        if(OVRInput.GetDown(OVRInput.Button.Back))
        {
            CmdRemoveCurrModel(model.GetComponent<Intersight_ModelAsset>().index);
        }
    }

    
    [Command]
    void CmdMoveObject(int index, Vector3 position)
    {
        AssetsManager.MoveCurrModel(index, position);
    }
    [Command]
    void CmdRotateObject(int index, Vector3 eulerAngles)
    {
        AssetsManager.RotateCurrModel(index, eulerAngles);
    }
    [Command]
    void CmdInstantiateNew(string name, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        AssetsManager.InstantiateNew(name, pos, rot, scale);
    }
    [Command]
    void CmdRemoveCurrModel(int index)
    {
        AssetsManager.RemoveCurrModel(index);
    }


    void SetOperating()
    {
        if (IsOperating)
        {
            CmdSetIsOperating(false);
            SelectAssetsWindow.GetComponent<Animator>().SetBool("CloseWindow", true);
            SelectAssetsWindow.RemoveAllItems();
        }
        else if (!OthersOperating)
        {
            CmdSetIsOperating(true);
            Vector3 WindowForward = Vector3.ProjectOnPlane(OVRHMD.forward, Vector3.up).normalized;
            SelectAssetsWindow.transform.position = OVRHMD.transform.position + WindowForward;
            SelectAssetsWindow.transform.forward = WindowForward;
            foreach (string key in AssetsManager.Assets.Keys)
            {
                SelectAssetsWindow.AddAssetItems(key);
            }
            SelectAssetsWindow.GetComponent<Animator>().SetBool("CloseWindow", false);
        }
    }

   
    [Command]
    void CmdSetController(Vector3 euler, Vector3 pos)
    {
        SetController(euler, pos);
        RpcSetController(euler, pos);
    }

    [ClientRpc]
    void RpcSetController(Vector3 euler, Vector3 pos)
    {
        SetController(euler, pos);
    }

    void SetController(Vector3 euler, Vector3 pos)
    {
        //Controller.Controller.eulerAngles = euler;
        //Controller.Controller.position = pos;
    }

    //........................................................................
    [Command]
    void CmdSetOVRHMDRot(Vector3 rot)
    {
        SetOVRHMDRot(rot);
        RpcSetOVRHMDRot(rot);
    }

    [ClientRpc]
    void RpcSetOVRHMDRot(Vector3 rot)
    {
        SetOVRHMDRot(rot);
    }

    void SetOVRHMDRot(Vector3 rot)
    {
        OVRHMD.eulerAngles = rot;
    }

	[Command]
	void CmdSetOVRHMDPos(Vector3 pos)
	{
		SetOVRHMDPos (pos);
		RpcSetOVRHMDPos (pos);
	}
    [ClientRpc]
    void RpcSetOVRHMDPos( Vector3 pos)
    {
		if (isLocalPlayer)
			return;
        SetOVRHMDPos(pos);
    }

    void SetOVRHMDPos( Vector3 pos)
    {
		OVRHMD.position = pos;
		OVRCamera.position = pos - OVREyeCenter.localPosition - OVREyeCenter.forward * 0.05f - OVREyeCenter.up * 0.04f;
    }

    [Command]
    void CmdSetLoadState(string state)
    {
        LoadState = state;
    }
    [Command]
    void CmdSetIsOperating(bool isOperating)
    {
        IsOperating = isOperating;
    }

    void SetOperatingTag(bool isOperating)
    {
        if (isOperating)
            OVRHMD.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        else
            OVRHMD.GetComponent<Renderer>().material.SetColor("_Color", Color.white);

    }

}
