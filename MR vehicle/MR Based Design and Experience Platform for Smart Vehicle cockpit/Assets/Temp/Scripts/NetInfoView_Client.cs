using UnityEngine;
using UnityEngine.Networking;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
public class NetInfoView_Client : MonoBehaviour {
    
    public enum State
    {
        start,
        stop,
        connected,
        disconnected
    }
    
    string Title ;
    string IpInfo = null;
    static public string userIp = null;

    void Start()
    {
        NetworkManager.singleton.networkAddress = "192.168.1.2";
        //NetworkManager.singleton.StartClient();
    }

    public void ShowNetworkInformation(State state)
    {
        switch(state)
        {
            case State.start:
                Title = "Connecting To Server...";
                break;
            case State.stop:
                Title = "";
                break;
            case State.connected:
                Title = "Client Connected\n\nServer: 192.168.1.2\nPort: 7777";
                break;
            case State.disconnected:
                ClientState = "START CLIENT";
                Title = "Server Shut Down";
                break;
        }
    }

    static string ClientState = "START CLIENT";
    static public int  ClientID;
    string id = "0";
    string idText;
    void OnGUI()
    {
        
        id = GUI.TextField(new Rect(170, 10, 30, 30), id);
        id = Regex.Replace(id, "[^0-9]", "0");
        int.TryParse(id, out ClientID);
        
        if (userIp != null)
            IpInfo = "Client: " + userIp;
        GUI.Label(new Rect(30, 50, 140, 400), Title);
        GUI.Label(new Rect(30, 120, 140, 400), IpInfo);

        if (GUI.Button(new Rect(10, 10, 150, 30), ClientState))
        {
            if (ClientState == "START CLIENT")
            {
                NetworkManager.singleton.StartClient();
                ClientState = "STOP CLIENT";
            }
            else
            {
                NetworkManager.singleton.StopClient();
                ClientState = "START CLIENT";
            }
        }
    }
}
