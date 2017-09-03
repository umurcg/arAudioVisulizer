using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnObjectOnClient : NetworkBehaviour {


    public short clientID;
    public GameObject objectToSpawn;
    public GameObject parent;

	// Use this for initialization
	void Start () {
		
	}

    [ContextMenu("Spawn")]
    void spawn()
    {
        RpcSpawnOnClient(clientID);
    }

    [ClientRpc]
    void RpcSpawnOnClient(short id)
    {
        //if (id == GetComponent<NetworkIdentity>().id)
        //{
            GameObject spawnedObject = Instantiate(objectToSpawn) as GameObject;
            spawnedObject.transform.parent = parent.transform;
            spawnedObject.transform.localPosition = Vector3.zero;

            NetworkServer.Spawn(spawnedObject);
        //}
    }

    void printIDs()
    {
        Debug.Log("My id is " + playerControllerId);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
