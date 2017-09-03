using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    CharacterController cc;
    public float speed = 3f;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        cc.Move(transform.forward * ver * speed + transform.right * hor * speed);

    }
}
