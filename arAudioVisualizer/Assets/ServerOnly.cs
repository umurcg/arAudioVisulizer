using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//This script disables owner object if it is not in server
public class ServerOnly : NetworkBehaviour {

	// Use this for initialization
	void Start () {

        if (isClient) gameObject.SetActive(false);
        else gameObject.SetActive(true);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
