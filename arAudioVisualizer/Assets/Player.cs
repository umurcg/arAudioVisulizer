using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

[System.Serializable] 
public class ToggleEvent : UnityEvent<bool>{}

public class Player : NetworkBehaviour {

    [SerializeField] ToggleEvent onToggleShared;
    [SerializeField] ToggleEvent onToggleLocal;
    [SerializeField] ToggleEvent onToggleRemote;

    private void Start()
    {
        enablePlayer();
    }

    void disablePlayer()
    {
        onToggleShared.Invoke(false);


        if (isLocalPlayer)
            onToggleLocal.Invoke(false);
        else
            onToggleRemote.Invoke(false);
    }

    void enablePlayer()
    {
        //Debug.Log("Enabling player");
        onToggleShared.Invoke(true);


        if (isLocalPlayer)
            onToggleLocal.Invoke(true);
        else
            onToggleRemote.Invoke(true);

    }

}
