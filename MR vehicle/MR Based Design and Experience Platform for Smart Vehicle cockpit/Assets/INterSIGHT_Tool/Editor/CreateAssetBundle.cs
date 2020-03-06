using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml.Serialization;

public class CreateAssetBundle
{
    public static string AssetsPath = "Assets/Resources";
    public static string BundlesPath = "Assets/INterSIGHT_Tool/AssetBundles";

    [MenuItem("Assets/CreateAssetBundles")]
    public static void BuildAllAssetBundles()
    {
        Texture2D[] Textures;
        GameObject[] Models;
        string[] paths = AssetDatabase.GetSubFolders(AssetsPath);
        string[] AssetBundleNames = new string[paths.Length];

        AssetBundleBuild[] buildMap = new AssetBundleBuild[paths.Length];

        string[] Assets;

        for (int i = 0; i< paths.Length; i++)
        {
            string currPath = paths[i].Substring(AssetsPath.Length +1, paths[i].Length -AssetsPath.Length -1);
            AssetBundleNames[i] = currPath;
            buildMap[i].assetBundleName = currPath;

            Textures = Resources.LoadAll<Texture2D>(currPath);
            Models = Resources.LoadAll<GameObject>(currPath);
            Assets = new string[Textures.Length + Models.Length];

            for (int k = 0; k < Models.Length; k++)
            {
                Assets[k] = AssetDatabase.GetAssetPath((Object)Models[k]);
            }
            for (int k = Models.Length; k< Assets.Length; k++)
            {
                Assets[k] = AssetDatabase.GetAssetPath((Object)Textures[k - Models.Length]);
            }

            buildMap[i].assetNames = Assets;
        }

        SaveAssetBundleNames(AssetBundleNames);

       
        BuildPipeline.BuildAssetBundles(BundlesPath/*Application.dataPath + "/ImportAssetBundles/AssetBundles"*/, buildMap, 
                                        BuildAssetBundleOptions.None, 
                                        BuildTarget.StandaloneWindows);
    }
    //[MenuItem("Assets/Load")]
    //public void LoadSetting()
    //{   
    //    if (File.Exists(Application.dataPath + "/AssetBundleNames.xml"))
    //    {

    //        XmlSerializer PieceInfo = new XmlSerializer(typeof(Vector3[]));
    //        using (FileStream fs = File.OpenRead(Application.dataPath + "/AssetBundleNames.xml"))
    //        {
    //            AllPiece = (Vector3[])PieceInfo.Deserialize(fs);
    //            fs.Close();
    //        }
    //        for (int i = 0; i < AllPiece.Length; i++)
    //        {
    //            Debug.Log(AllPiece[i]);
    //        }
    //    }
    //}

    public static void SaveAssetBundleNames(string[] paths)
    {
        XmlSerializer AssetBundleNames = new XmlSerializer(typeof(string[]));
        using (FileStream fs = File.OpenWrite( Application.dataPath + "/INterSIGHT_Tool/Informations/AssetBundleNames.xml"))
        {
            fs.SetLength(0);
            AssetBundleNames.Serialize(fs, paths);
            fs.Close();
        }
    }
}
