using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class Intersight_AssetsManager : NetworkBehaviour {
    public Dictionary<string, GameObject> Assets = new Dictionary<string, GameObject>();
    public List<Intersight_ModelAsset> ModelsInScene = new List<Intersight_ModelAsset>();
    public List<string> ModelNames = new List<string>();
    public List<Vector3> TransformInfo = new List<Vector3>();
    

    public bool AllocateComplleted = false;

    void Update()
    {
        
    }

    //Allocate Models Only On Server.
    [Server]
    public void AllocateModelsOnServer()
    {
        StartCoroutine(StartAllocateModels());
    }

    //Allocate Models On Client.
    [Server]
    public void AllocateModelsOnClient()
    {   
        RpcAllocateModels(ModelNames.ToArray(), TransformInfo.ToArray());
    }

    [ClientRpc]
    void RpcAllocateModels(string[] modelNames, Vector3[] transformInfos)
    {

        for (int i = 0; i < modelNames.Length; i++)
        {
            ModelNames.Add(modelNames[i]);
        }

        for (int i = 0; i < transformInfos.Length; i++)
        {
            TransformInfo.Add(transformInfos[i]);
        }
        StartCoroutine(StartAllocateModels());
    }

    IEnumerator StartAllocateModels()
    {
        while(!Intersight_LoadAssets.LoadComplete)
        {
            yield return null;
        }
        AllocateAllModels();
    }

    void AllocateAllModels()
    {
        if (AllocateComplleted)
            return;
        AllocateComplleted = true;
        for(int i = 0; i< ModelNames.Count; i++)
        {
            InstantiateModel(ModelNames[i], TransformInfo[3* i], Quaternion.Euler(TransformInfo[3 * i + 1]), TransformInfo[3* i +2]);
        }
    }
    
    //.......................................................................
    //Create New Model: Function Start.
    [Server]
    public void InstantiateNew(string name, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        InstantiateModel(name, pos, rot, scale);
        RpcInstantiateNew(name, pos, rot, scale);
    }

    [ClientRpc]
    void RpcInstantiateNew(string name, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        InstantiateModel(name, pos, rot, scale);
    }

    void InstantiateModel(string name, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        if (Assets[name] != null)
        {
            GameObject Curr = (GameObject)Instantiate(Assets[name], pos, rot);
            Curr.name = name;
            Curr.tag = "ModelInScene";
            ModelsInScene.Add(Curr.GetComponent<Intersight_ModelAsset>());
            ModelsInScene[ModelsInScene.Count - 1].index = ModelsInScene.Count - 1;
            Curr.SetActive(true);
        }
    }
    //Create New Model: Function End.
    //.....................................................................
    //Remove Model From Scene : Function Start.
    [Server]
    public void RemoveCurrModel(int index)
    {
        RemoveModel(index);
        RpcRemoveCurrModel(index);
    }


    [ClientRpc]
    void RpcRemoveCurrModel(int index)
    {
        RemoveModel(index);
    }

    void RemoveModel(int index)
    {
        if(index < ModelsInScene.Count)
        {
            Destroy(ModelsInScene[index].gameObject);
            ModelsInScene.RemoveAt(index);
            for(int i = index; i< ModelsInScene.Count; i++)
            {
                ModelsInScene[i].index = i;
            }
        }
    }
    //Remove Model From Scene : Function End.
    //...............................................................................
    //Move Model: Function Start.
    [Server]
    public void MoveCurrModel(int index, Vector3 pos)
    {
        MoveModel(index, pos);
        RpcMoveModel(index, pos);
    }

    [ClientRpc]
    void RpcMoveModel(int index, Vector3 pos)
    {
        MoveModel(index, pos);
    }

    void MoveModel(int index, Vector3 pos)
    {
        if (index < ModelsInScene.Count)
        {
            ModelsInScene[index].transform.position = pos;
        }
    }
    //Move Model: Fuction End.
    //..............................................................................
    //Rotate Model: Function Start.
    [Server]
    public void RotateCurrModel(int index, Vector3 euler)
    {
        RotateModel(index, euler);
        RpcRotateModel(index, euler);
    }

    [ClientRpc]
    void RpcRotateModel(int index, Vector3 euler)
    {
        RotateModel(index, euler);
    }

    void RotateModel(int index, Vector3 euler)
    {
        if (index < ModelsInScene.Count)
        {
            ModelsInScene[index].transform.eulerAngles = euler;
        }
    }
    //Move Model: Fuction End.

    //............................................................................
    [Server]
    public void LoadAssetInfos()
    {
        string path = "D:/xampp/htdocs/Collab_design_Projects/" + NetInfoView_Server.ProjectName + "/Assets/INterSIGHT_Tool/Informations/AssetsNames.xml";
        if (File.Exists(path))
        {
            XmlSerializer XmlS = new XmlSerializer(typeof(List<string>));
            using (FileStream fs = File.OpenRead(path))
            {
                ModelNames = (List<string>)XmlS.Deserialize(fs);
                fs.Close();
            }
        }

        path = "D:/xampp/htdocs/Collab_design_Projects/" + NetInfoView_Server.ProjectName + "/Assets/INterSIGHT_Tool/Informations/AssetsTransforms.xml";
        if (File.Exists(path))
        {
            XmlSerializer XmlS = new XmlSerializer(typeof(List<Vector3>));
            using (FileStream fs = File.OpenRead(path))
            {
                TransformInfo = (List<Vector3>)XmlS.Deserialize(fs);
                fs.Close();
            }
        }
    }

    [Server]
    public void SaveAssetsInfos()
    {
        ModelNames.Clear();
        TransformInfo.Clear();
        for (int i = 0; i < ModelsInScene.Count; i++)
        {
            ModelNames.Add(ModelsInScene[i].transform.name);
            TransformInfo.Add(ModelsInScene[i].transform.position);
            TransformInfo.Add(ModelsInScene[i].transform.eulerAngles);
            TransformInfo.Add(ModelsInScene[i].transform.localScale);
        }

        string path = "D:/xampp/htdocs/Collab_design_Projects/" + NetInfoView_Server.ProjectName + "/Assets/INterSIGHT_Tool/Informations/AssetsNames.xml";
        XmlSerializer xmlS = new XmlSerializer(typeof(List<string>));
        using (FileStream fs = File.OpenWrite(path))
        {
            fs.SetLength(0);
            xmlS.Serialize(fs, ModelNames);
            fs.Close();
        }

        path = "D:/xampp/htdocs/Collab_design_Projects/" + NetInfoView_Server.ProjectName + "/Assets/INterSIGHT_Tool/Informations/AssetsTransforms.xml";
        xmlS = new XmlSerializer(typeof(List<Vector3>));
        using (FileStream fs = File.OpenWrite(path))
        {
            fs.SetLength(0);
            xmlS.Serialize(fs, TransformInfo);
            fs.Close();
        }
    }

    void OnGUI()
    {
        if (!isServer)
            return;
        if (GUI.Button(new Rect(10, 90, 150, 30), "SAVE PROJECT"))
        {
            SaveAssetsInfos();
        }
    }
}
