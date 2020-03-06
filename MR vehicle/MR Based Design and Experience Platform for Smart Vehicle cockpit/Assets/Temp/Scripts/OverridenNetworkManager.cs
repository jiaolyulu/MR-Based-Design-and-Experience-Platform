using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class OverridenNetworkManager : NetworkManager {
    public NetInfoView_Server _NetInfoView_Server;
    public NetInfoView_Client _NetInfoView_Client;
    public Intersight_LoadAssets LoadAssets;
    //Run On Server
    public override void OnStopServer()
    {
        base.OnStopServer();
    }

    public override void OnStartServer()
    {
       
    }

    public override void OnServerReady(NetworkConnection conn)
    {

        base.OnServerReady(conn);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);

        _NetInfoView_Server.AddPlayer(conn.connectionId, conn.playerControllers[0].gameObject);
        LoadAssets.AssetsManager.SaveAssetsInfos();
        LoadAssets.LoadAllModelsOnClient();

    }



    //Run On Client
    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        _NetInfoView_Client.ShowNetworkInformation(NetInfoView_Client.State.start);
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        _NetInfoView_Client.ShowNetworkInformation(NetInfoView_Client.State.stop);
    }
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        _NetInfoView_Client.ShowNetworkInformation(NetInfoView_Client.State.disconnected);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        _NetInfoView_Client.ShowNetworkInformation(NetInfoView_Client.State.connected);
    }
}
