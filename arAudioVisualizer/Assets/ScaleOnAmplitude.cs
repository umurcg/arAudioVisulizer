using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : EightBandVis {



    
    public float maxScale = 30;
    Material mat;

    AudioPeerNetwork apn;
    

    public float red, green, blue;


    public float Amplitude, AmplitudeBuffer;
    float AmplitudeHighest;

    // Use this for initialization
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;


    }

    public override void updateVisulizer(float[] samples)
    {
        

        //Create an array with 8 elements of frequencies
        eightBand = MakeFrequencyBands(samples);

        //Buffer the bands for smooth decrease on bars
        BandBuffer(eightBand);

        //Calculate amplitude
        GetAmplitude();

        //Update scale according to amplitude
        updateScale(Amplitude, AmplitudeBuffer);

        

    }

    // Update is called once per frame
    public void updateScale(float amplitude, float amplitudeBuffer) {

        float value = useBuffer ? amplitudeBuffer : amplitude;

        transform.localScale = Vector3.one * value * maxScale;
        Color col = new Color(red * value, green * value, blue * value);
        mat.SetColor("_EmissionColor", col);

        

	}


    public void GetAmplitude()
    {
        Amplitude = 0;
        AmplitudeBuffer = 0;

        for (int i = 0; i < 8; i++)
        {
            Amplitude += eightBand[i];
            AmplitudeBuffer += bandBuffer[i];
        }

        //Assign highest amplitude
        if (Amplitude > AmplitudeHighest) AmplitudeHighest = Amplitude;

        //Normilized
        Amplitude = Amplitude / AmplitudeHighest;
        AmplitudeBuffer = AmplitudeBuffer / AmplitudeHighest;
              
        

    }




}
