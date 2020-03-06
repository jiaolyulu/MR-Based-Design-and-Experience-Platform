using UnityEngine;
using System.Collections;

public class Intersight_ServerController : MonoBehaviour {
    public enum ServerState
    {
        WaitingStart,
        ServerStart,
        WaitingOthers,
        AllLoaded
    }

    public static ServerState _ServerState = ServerState.WaitingStart;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
