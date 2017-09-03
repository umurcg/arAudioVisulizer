using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SpawnObject : NetworkBehaviour {

    //[SerializeField] GameObject objectToSpawn;
    //[SerializeField] GameObject ownerPlayer;

    [SerializeField] List<GameObject> SpawnObjects;
    [SerializeField] GameObject[] players;
    [SerializeField] NetworkManager manager;
    //[SerializeField] Transform buttonPrefab;

    [SerializeField] Dropdown objectDD, playerDD;

    float time = 5;
    float timer = 5;

	// Use this for initialization
	void Start () {

        SpawnObjects = manager.spawnPrefabs;

        fillObjectDD();
        updatePlayerDropdown();

	}
	
	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            updatePlayerDropdown();
            timer = time;
        }
   
	}

     void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("New player is connected");
        updatePlayerDropdown();

    }


    public void spawnObject(/*GameObject objectToSpawn, GameObject player*/)
    {

        GameObject objectToSpawn = SpawnObjects[objectDD.value];
        GameObject player = players[playerDD.value];

        GameObject spawnedObject = Instantiate(objectToSpawn) as GameObject;

        spawnedObject.transform.parent = player.transform;
        spawnedObject.transform.localPosition = Vector3.zero;


        NetworkServer.Spawn(spawnedObject);
    }

    void fillObjectDD()
    {

        List<string> options = new List<string>();

        for(int i = 0; i < SpawnObjects.Count; i++)
        {
            options.Add(SpawnObjects[i].name);   
        }

        objectDD.AddOptions(options);
    }

    [ContextMenu("Update Player DD")]
    void updatePlayerDropdown()
    {
        playerDD.ClearOptions();

        players = GameObject.FindGameObjectsWithTag("Player");
        List<string> options = new List<string>();

        for (int i = 0; i < players.Length; i++)
        {
            NetworkIdentity id = players[i].GetComponent<NetworkIdentity>();
           
            options.Add("Id: "+id.netId.ToString());
        }

        playerDD.AddOptions(options);


    }


    //public void createObjectButtons()
    //{

    //    foreach(GameObject obj in SpawnObjects)
    //    {
    //        Transform spawnedButton=Instantiate(buttonPrefab, transform);
    //        Button but=spawnedButton.GetComponent<Button>();
    //        but.GetComponentInChildren<Text>().text = obj.name;
    //        but.onClick

    //    }

    //}








}
