using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Intersight_Controller : NetworkBehaviour {
    public Transform Controller;
    Transform CurrModel;
    Transform OriginParent;
	// Update is called once per frame
	void Update () {
        
    }

    public override void OnStartLocalPlayer()
    {
        
    }
    
    void OnTriggerStay(Collider other)
    {
        other.transform.SetParent(transform);
        OriginParent = other.transform.parent;
        CurrModel = other.transform;
    }

    void OnTriggerExist(Collider other)
    {
        other.transform.SetParent(OriginParent);
        CurrModel = null;
        OriginParent = null;
    }

}
