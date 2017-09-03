using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Linq;
using System.Collections.Generic;
/// <summary>
/// Gets spectrum of frequencies at every fixed update and share this through network
/// </summary>

public class AudioPeerNetwork : NetworkBehaviour {

    
    AudioSource audioSource;
    
    //Samples should be get at every update. After assigning samples every frequency visulize must be updated in network
    //public SyncListFloat samples =new SyncListFloat();
        
    //Frequency visulizer of scene
    //public FrequencyVisulizer[] visulizers;
    List<FrequencyVisulizer> visulizerList;


    public int spectrumLength = 512;

    NetworkClient myClient;


    private void Awake()
    {
        if (visulizerList == null)  visulizerList = new List<FrequencyVisulizer>();
        //foreach(FrequencyVisulizer fv in visulizerList)
        //{
        //    visulizerList.Add(fv);
        //}
    }

    [ContextMenu("Print Visulize listt")]
    void printVisulizerList()
    {
        Debug.Log(visulizerList.Count);
    }

    // Use this for initialization
    void Start () {

        
               
        audioSource = GetComponent<AudioSource>();
        //If not server than there shouldn't be audio source
        if (!isServer)
        {
            Debug.Log("Destroying source");
            Destroy(audioSource);
        }
  

 
	}

    public void registerVisulizer(FrequencyVisulizer fv)
    {
        if (visulizerList == null) visulizerList = new List<FrequencyVisulizer>();

        visulizerList.Add(fv);
    }

    //public void OnConnected(NetworkConnection conn, NetworkReader reader)
    //{
    //   conn.SetChannelOption(Channels.DefaultReliable, ChannelOption.MaxPendingBuffers, 64);
    //    Debug.Log("New client is connected");
    //}


    //[ContextMenu("Change pending buffer rate")]
    //public void setChannelBufferLimit()
    //{
    //    connectionToClient.SetChannelOption(Channels.DefaultReliable, ChannelOption.MaxPendingBuffers, 64);
    //}

	// Update is called once per frame
	void FixedUpdate () {

        //If server than get spectrum from audio
        if (isServer) GetSpectrumAudioSource();
	}

    void GetSpectrumAudioSource()
    {
        //If no audio clip then return
        if (audioSource.clip == null) return;

        //create float array for spectrum
        float[] samplesArray=new float[spectrumLength];
        
        //get spectrum data
        audioSource.GetSpectrumData(samplesArray, 0, FFTWindow.Blackman);

        ////Fill sample sync array 
        //for(int i = 0; i < spectrumLength; i++)
        //{
        //    samples[i] = samplesArray[i];
        //}

        //Update all visulizer in network
        RpcUpdateVisulizer(samplesArray);
    }

    [ClientRpc]
    void RpcUpdateVisulizer(float[] samples)
    {
        foreach(FrequencyVisulizer fv in visulizerList)
            fv.updateVisulizer(samples);
    }



}
