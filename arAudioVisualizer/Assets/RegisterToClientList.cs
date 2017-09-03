using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RegisterToClientList : NetworkBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    [ContextMenu("print connnectd clients")]
    public void printClients()
    {
        foreach(NetworkConnection con in NetworkServer.connections)
        {
            Debug.Log(con.connectionId);
        }
    }


}
