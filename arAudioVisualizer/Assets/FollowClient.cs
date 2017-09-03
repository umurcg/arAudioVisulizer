using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//This script makes clients follow
public class FollowClient : NetworkBehaviour  {

    public int clientID;
    public PlayerPositionBroadcaster ppb;



    GameObject focus;

	// Use this for initialization
	void Start () {
		
	}

    private void Update()
    {
        //if (isServer && ppb.clientPositionDict!=null && ppb.clientPositionDict.ContainsKey(clientID))
        //{
        //    transform.position = ppb.clientPositionDict[clientID];
        //}
    }

    //void findFocusClient()
    //{
    //    focus=ClientScene.FindLocalObject(clientID);
    //}
    

    [ContextMenu("print connnectd clients")]
    public void printClients()
    {
        foreach (NetworkConnection con in NetworkServer.connections)
        {
            Debug.Log(con.connectionId);
        }
    }
}
