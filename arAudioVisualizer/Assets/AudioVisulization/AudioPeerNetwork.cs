using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Linq;
using System.Collections.Generic;
/// <summary>
/// Gets spectrum of frequencies at every fixed update and share this through network
/// </summary>

public class AudioPeerNetwork : NetworkBehaviour {

    //Singleton class
    //public static AudioPeerNetwork audioPeer;

    AudioSource audioSource;

    public float[] samples;
    public float[] eightBand;

    public float[] bandBuffer = new float[8];
    public float[] bufferDecrese = new float[8];
    
    float[] highestBand = new float[8];

    public float Amplitude, AmplitudeBuffer;
    float AmplitudeHighest;


    //Frequency visulizer of scene
    //public FrequencyVisulizer[] visulizers;
    //List<FrequencyVisulizer> visulizerList;
    //List<EightBandVis> eightBandVisList;
    //List<ScaleOnAmplitude> amplitudeList;
    
    public int spectrumLength = 512;

    NetworkClient myClient;




    private void Awake()
    {

        //if (audioPeer == null)
        //{
        //    audioPeer = this;
        //}
        //else
        //{            
        //    Destroy(this);
        //}

        samples = new float[spectrumLength];

        //if (visulizerList == null)  visulizerList = new List<FrequencyVisulizer>();
        //if (eightBandVisList == null) eightBandVisList = new List<EightBandVis>();
        //if (amplitudeList == null) amplitudeList = new List<ScaleOnAmplitude>();
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

    //public void registerAmplitude(ScaleOnAmplitude amp)
    //{
    //    if (amplitudeList == null) amplitudeList = new List<ScaleOnAmplitude>();

    //    amplitudeList.Add(amp);
    //}

    //public void registerVisulizer(FrequencyVisulizer fv)
    //{
    //    if (visulizerList == null) visulizerList = new List<FrequencyVisulizer>();

    //    visulizerList.Add(fv);
    //}

    //public void registerEightBand(EightBandVis ebv)
    //{
    //    if (eightBandVisList == null) eightBandVisList = new List<EightBandVis>();

    //    eightBandVisList.Add(ebv);
    //}



	// Update is called once per frame
	void FixedUpdate () {

        if (!isServer) return;

         //If server than get spectrum from audio
        GetSpectrumAudioSource();
        MakeFrequencyBands(samples);
        GetAmplitude(out Amplitude, out AmplitudeBuffer);
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

    public float[] MakeFrequencyBands(float[] samples)
    {
        /*
        20-60 Hertz
        60-250 Hertz
        250-600Hertz
        500-2000 Hertz
        2000- 4000 Hertz
        4000-6000Hertz
        6000-20000Hertz

        0-2
        1-4
        2-8
        3-16
        4-32
        5-64
        6-128
        7-256
        510 + 2
         */

        float[] frequencyBand=new float[8];

        int count = 0;

        for(int i = 0; i < 8; i++)
        {

            float avarage = 0;
            int samplesCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7) samplesCount += 2;

            for(int j = 0; j < samplesCount; j++)
            {
                avarage += samples[count] * (count+1);
                count++;
            }

            avarage /= count;

            frequencyBand[i] = avarage * 10;
            
        }

        eightBand = frequencyBand;

        BandBuffer(eightBand);

        RpcUpdateEightBands(eightBand, bandBuffer, bufferDecrese);

        return frequencyBand;

    }

    public void GetAmplitude(out float  amplitude, out float  amplitudeBuffer)
    {
        amplitude = 0;
        amplitudeBuffer = 0;

        for(int i = 0; i < 8; i++)
        {
            amplitude += eightBand[i];
            amplitudeBuffer += bandBuffer[i];
        }

        //Assign highest amplitude
        if (amplitude > AmplitudeHighest) AmplitudeHighest = amplitude;

        //Normilized
        amplitude = amplitude / AmplitudeHighest;
        amplitudeBuffer = amplitudeBuffer / AmplitudeHighest;

        RpcUpdateScaleAmplitude(amplitude, amplitudeBuffer);

        

    }

    [ClientRpc]
    void RpcUpdateVisulizer(float[] samples)
    {
        this.samples = samples;

        if (FrequencyVisulizer.visulizerList != null)
        {
            //Debug.Log("Number of visulizer is " + FrequencyVisulizer.visulizerList.Count);
            foreach (FrequencyVisulizer fv in FrequencyVisulizer.visulizerList)
                fv.updateVisulizer(samples);
        }
        else
        {
            Debug.Log("Coudlnt found any frequency visulizer script.");
        }
    }

    [ClientRpc]
    void RpcUpdateEightBands(float[] bands, float[] bandBuffer, float[] bufferDecrease)
    {
        this.eightBand = bands;
        this.bandBuffer = bandBuffer;
        this.bufferDecrese = bufferDecrease;

        if (EightBandVis.eightBandVisList != null)
        {
            foreach (EightBandVis ebv in EightBandVis.eightBandVisList)
            {
                ebv.updateBands(bands, bandBuffer);
                ebv.updateColors(normalizeBand(bands));
            }


        }
        else
        {
            Debug.Log("Coudlnt found any eigh band vis script.");
        }
    }

    [ClientRpc]
    void RpcUpdateScaleAmplitude(float Amplitude, float AmplitudeBuffer)
    {
        this.Amplitude = Amplitude;
        this.AmplitudeBuffer = AmplitudeBuffer;

        if (ScaleOnAmplitude.amplitudeList != null)
        {
            foreach (ScaleOnAmplitude soa in ScaleOnAmplitude.amplitudeList)
            {
                soa.updateScale(Amplitude, AmplitudeBuffer);
            }
        }
        else
        {
            Debug.Log("Coudlnt found any scale on amlitude script.");
        }

    }


    float[] normalizeBand(float[] bands)
    {
        float[] normilizedBand = new float[8];

        for (int i = 0; i < 8; i++)
        {
            if (bands[i] > highestBand[i])
            {
                highestBand[i] = bands[i];

            }

            normilizedBand[i] = bands[i] / highestBand[i];
        }

        return normilizedBand;
    }

    //Makes smoother downs for bars
    void BandBuffer(float[] bands)
    {


        for (int i = 0; i < 8; i++)
        {

            if (bands[i] > bandBuffer[i])
            {
                bandBuffer[i] = bands[i];
                bufferDecrese[i] = 0.005f;
            }
            else
            {
                bandBuffer[i] -= bufferDecrese[i];
                bufferDecrese[i] *= 1.2f;
            }

        }



    }
}
