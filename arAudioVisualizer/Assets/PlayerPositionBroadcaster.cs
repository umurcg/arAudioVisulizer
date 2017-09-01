using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerPositionBroadcaster : NetworkBehaviour
{

    GameObject placeHolder;

	// Use this for initialization
	void Start () {

        placeHolder = GameObject.CreatePrimitive(PrimitiveType.Cube);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //[ContextMenu("Broad cast positions")]
    //public void broadCastPositions()
    //{
    //    RpcUpdatePlaceHolderPosition();
    //}


    //[ClientRpc]
    //void RpcUpdatePlaceHolderPosition()
    //{
    //    Debug.Log("I am " + netId + " and my position is " + transform.position);
    //}

}
