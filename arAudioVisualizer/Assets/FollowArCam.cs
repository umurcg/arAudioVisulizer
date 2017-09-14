using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowArCam : MonoBehaviour {

    public GameObject arCam;
    public bool useOffset;
    Vector3 offset;

	// Use this for initialization
	void Start () {


        if(arCam==null)
            arCam = GameObject.FindGameObjectWithTag("ARCamera");
        
        if(arCam==null)
        {
            Debug.Log("Couldn't find an ARCam");
            enabled = false;
            return;
        }

        offset = transform.position - arCam.transform.position;

	}
	
	// Update is called once per frame
	void Update () {

        transform.position = (useOffset)? arCam.transform.position+offset : arCam.transform.position;

	}
}
