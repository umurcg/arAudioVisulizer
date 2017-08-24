using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Linq;

public class AudioPeer : NetworkBehaviour {


    AudioSource audioSource;
    public SyncListFloat samples =new SyncListFloat();
    public FrequencyVisulizer visulizer;

    
    // Use this for initialization
    void Start () {
               
        audioSource = GetComponent<AudioSource>();
        if (!isServer)
        {
            Debug.Log("Destroying source");
            Destroy(audioSource);
        }
        else
        {
            for(int i = 0; i < 512; i++)
            {
                samples.Add(0);
            }
        }

 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(isServer)
            GetSpectrumAudioSource();
	}

    void GetSpectrumAudioSource()
    {
        if (audioSource.clip == null) return;

        float[] samplesArray=new float[512];
        audioSource.GetSpectrumData(samplesArray, 0, FFTWindow.Blackman);


        for(int i = 0; i < 512; i++)
        {
            samples[i] = samplesArray[i];
        }

        RpcUpdateVisulizer();
    }

    [ClientRpc]
    void RpcUpdateVisulizer()
    {
        visulizer.updateVisulizer();
    }



}
