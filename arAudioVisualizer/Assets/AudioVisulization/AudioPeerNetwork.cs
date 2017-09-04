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

    public float[] samples;
        
    public int spectrumLength = 512;

    NetworkClient myClient;
    
    private void Awake()
    {

        samples = new float[spectrumLength];

    }



    // Use this for initialization
    void Start () {        
               
        audioSource = GetComponent<AudioSource>();
        VoiceRecorder recorder = GetComponent<VoiceRecorder>();

        //If not server than there shouldn't be audio source
        if (!isServer)
        {
            Debug.Log("Destroying source");
            Destroy(audioSource);
            Destroy(recorder);
        }
  

 
	}



	// Update is called once per frame
	void FixedUpdate () {

        if (!isServer) return;

         //If server than get spectrum from audio
        GetSpectrumAudioSource();
        //MakeFrequencyBands(samples);
        //GetAmplitude(out Amplitude, out AmplitudeBuffer);
    }

    public float[] GetSpectrumAudioSource()
    {
        //If no audio clip then return
        if (audioSource.clip == null) return null;

        //create float array for spectrum
        float[] samplesArray=new float[spectrumLength];

        //Kepp track samples, so other scripts can get without calling this function
        samples = samplesArray;
        
        //get spectrum data
        audioSource.GetSpectrumData(samplesArray, 0, FFTWindow.Blackman);

        //Update all visulizer in network
        RpcUpdateVisulizer(samplesArray);

        return samplesArray;
    }

   


    [ClientRpc]
    void RpcUpdateVisulizer(float[] samples)
    {
        this.samples = samples;

        Visulizer.updateAllVisulizers(samples);
    }





}
