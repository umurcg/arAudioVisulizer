using UnityEngine;
using System.Collections;
using System.Linq;
/// <summary>
/// Gets spectrum of frequencies at every fixed update and share this through network
/// </summary>

public class AudioPeer : MonoBehaviour
{


    AudioSource audioSource;

    //Samples should be get at every update. After assigning samples every frequency visulize must be updated in network
    //public SyncListFloat samples =new SyncListFloat();

    //Frequency visulizer of scene
    public FrequencyVisulizer visulizer;

    public int spectrumLength = 512;




    // Use this for initialization
    void Start()
    {



        audioSource = GetComponent<AudioSource>();
        //If not server than there shouldn't be audio source
 



    }



    // Update is called once per frame
    void FixedUpdate()
    {

        GetSpectrumAudioSource();
    }

    void GetSpectrumAudioSource()
    {
        //If no audio clip then return
        if (audioSource.clip == null) return;

        //create float array for spectrum
        float[] samplesArray = new float[spectrumLength];

        //get spectrum data
        audioSource.GetSpectrumData(samplesArray, 0, FFTWindow.Blackman);

        ////Fill sample sync array 
        //for(int i = 0; i < spectrumLength; i++)
        //{
        //    samples[i] = samplesArray[i];
        //}

        visulizer.updateVisulizer(samplesArray);
        ////Update all visulizer in network
        //RpcUpdateVisulizer(samplesArray);
    }

    //[ClientRpc]
    //void RpcUpdateVisulizer(float[] samples)
    //{
    //    visulizer.updateVisulizer(samples);
    //}



}
