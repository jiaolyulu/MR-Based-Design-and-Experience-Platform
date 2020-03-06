using UnityEngine;
using System.Collections;

public class ObjectItem : MonoBehaviour {
    
    public Transform LocalOVRCamera;
    // Use this for initialization
    void OnEnable() {
        LocalOVRCamera = FindObjectOfType<Camera>().transform;
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(LocalOVRCamera.transform, Vector3.up);
	}
}
