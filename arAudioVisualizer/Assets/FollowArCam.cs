using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowArCam : MonoBehaviour {

    public GameObject arCam;

	// Use this for initialization
	void Start () {

        arCam = GameObject.FindGameObjectWithTag("ARCamera");
        
        if(arCam==null)
        {
            Debug.Log("Couldn't find an ARCam");
            enabled = false;
            return;
        }

	}
	
	// Update is called once per frame
	void Update () {

        transform.position = arCam.transform.position;

	}
}
