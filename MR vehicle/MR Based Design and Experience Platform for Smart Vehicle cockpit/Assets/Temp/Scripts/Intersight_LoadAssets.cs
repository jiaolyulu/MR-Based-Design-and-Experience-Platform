using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class Intersight_LoadAssets : NetworkBehaviour {
    public Intersight_AssetsManager AssetsManager;

    public override void OnStartServer()
    {
        base.OnStartServer();
        InitAssetsOnServer();
    }
    
    private string[] AssetBundleNames;
    [Server]
    public void InitAssetsOnServer()
    {
        LoadAssetBundleNames();
        LoadAllFromServer(NetInfoView_Server.ProjectName, AssetBundleNames);
        AssetsManager.LoadAssetInfos();
        AssetsManager.AllocateModelsOnServer();
    }

    [Server]
    public void LoadAllModelsOnClient()
    {

        AssetsManager.AllocateModelsOnClient();
        RpcLoadAllFromServer(NetInfoView_Server.ProjectName, AssetBundleNames);
    }

    [ClientRpc]
    public void RpcLoadAllFromServer(string projectName, string[] assetBundleNames)
    {
        LoadAllFromServer(projectName, assetBundleNames);
    }

    void LoadAllFromServer(string projectName, string[] assetBundleNames)
    {
        if (isLoaded)
            return;
        isLoaded = true;
        loadCounter = assetBundleNames.Length;
        if (loadCounter == 0)
            return;
        for (int i = 0; i < assetBundleNames.Length - 1; i++)
        {
            StartCoroutine(DownLoad(projectName, assetBundleNames[i]));
        }
        StartCoroutine(DownLoad(projectName, assetBundleNames[assetBundleNames.Length-1]));
    }

    public bool isLoaded = false;
    public static bool LoadComplete = false;
    public int loadCounter = -1;
    IEnumerator DownLoad(string projectName, string assetBundleName)
    {
        string url = "http://192.168.1.2/Collab_design_Projects/"
            + projectName + "/Assets/INterSIGHT_Tool/AssetBundles/"
            + assetBundleName;
        using (WWW www = new WWW(url))
        {
            yield return www;
            if (www.error != null)
                throw new Exception("WWW download had an error:" + www.error);
            AssetBundle bundle = www.assetBundle;

            Texture2D[] Textures;
            GameObject[] Models;

            Models = bundle.LoadAllAssets<GameObject>();
            Textures = bundle.LoadAllAssets<Texture2D>();

            for (int i = 0; i < Models.Length; i++)
            {
                int currIndex = AssetsManager.Assets.Count;
                
                AssetsManager.Assets.Add(Models[i].transform.name, Instantiate(Models[i]));
                Intersight_ModelAsset Curr = AssetsManager.Assets[Models[i].transform.name].AddComponent<Intersight_ModelAsset>();
                Curr.OptionalTextures = Textures;
                Curr.gameObject.SetActive(false);
                Curr.name = Models[i].name;
            }

            loadCounter--;
            if (loadCounter == 0)
                LoadComplete = true;
        }
    }

    [Server]
    void LoadAssetBundleNames()
    {
        if (File.Exists("D:/xampp/htdocs/Collab_design_Projects/" + NetInfoView_Server.ProjectName + "/Assets/INterSIGHT_Tool/Informations/AssetBundleNames.xml"))//Application.dataPath+ "/INterSIGHT_Tool/Informations/AssetBundleNames.xml"))
        {
            XmlSerializer PieceInfo = new XmlSerializer(typeof(string[]));
            using (FileStream fs = File.OpenRead("D:/xampp/htdocs/Collab_design_Projects/" + NetInfoView_Server.ProjectName + "/Assets/INterSIGHT_Tool/Informations/AssetBundleNames.xml"))
            {
                AssetBundleNames = (string[])PieceInfo.Deserialize(fs);
                fs.Close();
            }
        }
    }

}
