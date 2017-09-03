using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnVisulizer : MonoBehaviour {

    public GameObject objectToSpawn;
    public PlayerPositionBroadcaster ppb;
    public int spawnObjectID;

	// Use this for initialization
	void Start () {
	
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        GameObject spawnedObject = Instantiate(objectToSpawn) as GameObject;
        FollowClient fc = spawnedObject.GetComponent<FollowClient>();
        fc.ppb = ppb;
        fc.clientID = spawnObjectID;

        NetworkServer.Spawn(spawnedObject);
        
        
        //Destroy(Samsu)

    }

    
    

    [ContextMenu("print connnectd clients")]
    public void printClients()
    {
        foreach (NetworkConnection con in NetworkServer.connections)
        {
            Debug.Log(con.connectionId);
        }
    }

}
