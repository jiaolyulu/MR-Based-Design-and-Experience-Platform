using UnityEngine;
using System.Collections;

public class Intersight_ModelAsset : MonoBehaviour {
    public Texture2D[] OptionalTextures;
    public Renderer[] SubMeshRenderers;
    public int index;
   
	// Use this for initialization
	void OnEnable () {
        SubMeshRenderers = GetComponentsInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

   
}
