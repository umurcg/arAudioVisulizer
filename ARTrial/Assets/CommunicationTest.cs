using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CommunicationTest : NetworkBehaviour{

    public Slider slider;
    public Text number;

    [SyncVar]
    public float numberValue;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer) Destroy(slider);

        slider.value = numberValue;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setNumber(float num)
    {
        numberValue = num;
        updateAllClients();
    }

    
    public void updateAllClients()
    {
        if (NetworkServer.active)
        {
            RpcUpdateNumberText();
        }
    }

    [ClientRpc]
    public void RpcUpdateNumberText()
    {
        number.text = numberValue.ToString();
        slider.value = numberValue;
    }

    //[ContextMenu("SendMessage")]
    //void sendMessageToClients()
    //{
    //    if (isServer)
    //    {

    //    }
    //}

    //[RPC]
    //public void recievedMessage()
    //{
    //    Debug.Log("Message is recieved " + gameObject.name);
    //}


}
