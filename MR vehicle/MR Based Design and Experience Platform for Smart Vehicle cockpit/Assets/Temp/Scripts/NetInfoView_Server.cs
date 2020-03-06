using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetInfoView_Server : MonoBehaviour {
	static public string ProjectName = "graduation_project_khs_test_newcontroller";
    public bool ProjectNameExist;
    string Title;
    string IpInfo;
    string StateInfo;
    string OperateInfo;
    
    public Intersight_Player[] Players = new Intersight_Player[10];

    public void AddPlayer(int index, GameObject player)
    {
        Players[index - 1] = player.GetComponent<Intersight_Player>();
    }

    void Update()
    {
        ListenIsSomeOneOperating();
    }

    void ListenIsSomeOneOperating()
    {
        bool tag = false;
        for(int i = 0; i< Players.Length; i++)
        {
            if(Players[i] != null && Players[i].IsOperating)
            {
                tag = true;
                break;
            }
        }

        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i] != null)
            {
                Players[i].OthersOperating = tag;
            }
        }
    }
    
    string ServerState = "START SERVER";
    int counter;
    void OnGUI()
    {
        ProjectName = GUI.TextField(new Rect(10, 10, 150, 30), ProjectName);
        if (!ProjectNameExist && GUI.Button(new Rect(170, 10, 80, 30), "COMFIRM"))
        {
            ProjectNameExist = true;
        }

        if (!ProjectNameExist)
            return;

        if(ServerState == "STOP SERVER")
            Title = "Server: 192.168.1.2\nPort:7777\n\nConnections:("+ counter +")";
        IpInfo = "";
        StateInfo = "";
        OperateInfo = "";
        counter = 0;
        
        for(int i = 0; i< Players.Length; i++)
        {
            if(Players[i] != null)
            {
                counter++;
                //IpInfo += "\n" + Players[i].Ip;
                StateInfo += "\n" + Players[i].LoadState;
                OperateInfo += Players[i].IsOperating ? "\n(Locked)" : "\n";
            }
        }

        if (GUI.Button(new Rect(10, 50, 150, 30), ServerState))
        {
            if (ServerState == "START SERVER")
            {
                NetworkManager.singleton.StartServer();
                Intersight_ServerController._ServerState = Intersight_ServerController.ServerState.ServerStart;
                ServerState = "STOP SERVER";
            }
            else
            {
                NetworkManager.singleton.StopServer();

                //Application.Quit();
            }
        }
        
        GUI.Label(new Rect(30, 130, 140, 100), Title);
        GUI.Label(new Rect(30, 180, 140, 400), IpInfo);
        GUI.Label(new Rect(120, 180, 140, 400), StateInfo);
        GUI.Label(new Rect(180, 180, 140, 400), OperateInfo);
    }
}
