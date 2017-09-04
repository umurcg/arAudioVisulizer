using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent (typeof(NetworkManager))]
public class CustomNetworkManagerHUD : MonoBehaviour {

    public string localIP = "192.168.1.77";

    public Button hostARScene;
    public Button connectToARScene;
    public Button disconnectButton;

    //Number of try to connect server as a client. If try number reaches this then it exits connection.
    public int numberOfTryToConnect = 1000;

    NetworkManager networkMan;

    private void Awake()
    {
        networkMan = GetComponent<NetworkManager>();
        networkMan.networkAddress = localIP;
    }

    // Use this for initialization
    void Start () {

        disconnectButton.gameObject.SetActive(false);

        //If device is mobile then disable host functionality
//#if UNITY_IOS
//        hostARScene.gameObject.SetActive(false);
//#elif UNITY_ANDROID
//        hostARScene.gameObject.SetActive(false);
//#endif

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void host()
    {
        
        networkMan.StartHost();
        disconnectButton.gameObject.SetActive(true);
        disconnectButton.GetComponentInChildren<Text>().text = "Kill Host";
        disconnectButton.onClick.AddListener(killHost);

        hostARScene.gameObject.SetActive(false);
        connectToARScene.gameObject.SetActive(false);
    }

    public void connectTohost()
    {
        StartCoroutine(_connectTohost());
    }

    IEnumerator _connectTohost()
    {
        networkMan.StartClient();


        int numberOfTry = 0;

        while(networkMan.client.isConnected == false)
        {
            numberOfTry++;
            Debug.Log("Waiting to connect");

            if (numberOfTry > numberOfTryToConnect) yield break;

            yield return null;

        }

        Debug.Log("Cleint connected to server successfully");
        

        disconnectButton.gameObject.SetActive(true);
        disconnectButton.GetComponentInChildren<Text>().text = "Disconnect Host";
        disconnectButton.onClick.AddListener(disconnectFromHost);

        connectToARScene.gameObject.SetActive(false);

    }

    public void killHost()
    {
        networkMan.StopHost();
        disconnectButton.gameObject.SetActive(false);

        hostARScene.gameObject.SetActive(true);
        connectToARScene.gameObject.SetActive(true);

        
    }

    public void disconnectFromHost()
    {
        networkMan.StopClient();
        disconnectButton.gameObject.SetActive(false);

        connectToARScene.gameObject.SetActive(true);

        networkMan.client.Disconnect();
    }

    

}
