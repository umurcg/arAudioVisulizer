using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerPositionBroadcaster : NetworkBehaviour
{

    static List<GameObject> players;

    public float updateTolerance = 0.001f;
    Vector3 previousPos;

	// Use this for initialization
	void Awake () {

        if (!isClient)
        {
            players = new List<GameObject>();
            Debug.Log("Creating new players list");
        }


        //clientPositionDict = new Dictionary<int, Vector3>();

	}

    public override void OnStartClient() {
        Debug.Log("on start client");
        base.OnStartClient();


        if (!isServer)
            CmdRegisterPlayer(/*gameObject*/);

    }

    //void OnConnectedToServer()
    //{
    //    CmdRegisterPlayer(/*gameObject*/);
    //    Debug.Log("Conntected to server");
    //}

    [Command]
    void CmdRegisterPlayer(/*GameObject player*/)
    {
        Debug.Log("Registering player");
        //if (!isClient) players.Add(player);
    }

    [ContextMenu ("Print players")]
    void printPlayers()
    {
        Debug.Log("Player count is " + players.Count);
        //foreach(PlayerPositionBroadcaster ppb in players)
        //{
        
        //}
    }

    private void Start()
    {
        //updatePosition();

        //previousPos = transform.position;
    }

    private void updatePosition()
    {
        //if (connectionToServer!=null)
        //{
        //    RpcRegisterPosition(connectionToServer.connectionId, transform.position);
        //}
        //else
        //{
        //    Debug.Log("Connection to server is null");
        //}
    }

    // Update is called once per frame
    void Update () {

        //if (Vector3.Distance(transform.position, previousPos) > updateTolerance)
        //{
        //    updatePosition();
        //}
	    	

       

	}



    //[ClientRpc]
    //void RpcRegisterPosition(int clientID,Vector3 pos)
    //{
    //    if (clientPositionDict == null)
    //    {
            
    //        Debug.Log("Client Position Dict is null");
    //        return;
    //    }

    //    clientPositionDict[clientID] = pos;

    //    printDict();
    //}

    //[ContextMenu("Print Dictionary")]
    //void printDict()
    //{
    //    if (clientPositionDict != null)
    //    {
    //        foreach(KeyValuePair<int,Vector3>keyvalue in clientPositionDict)
    //        {
    //            Debug.Log(keyvalue.Key + "th client is at " + keyvalue.Value);
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("No dictionary");
    //    }
    //}


}
