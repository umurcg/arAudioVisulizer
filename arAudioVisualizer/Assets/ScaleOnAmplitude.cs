using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour {

    public static List<ScaleOnAmplitude> amplitudeList;

    public float startScale = 1;
    public float maxScale = 30;
    Material mat;

    AudioPeerNetwork apn;
    public bool useBuffer = true;

    public float red, green, blue;


    private void Awake()
    {

        if (amplitudeList == null)
        {
            amplitudeList = new List<ScaleOnAmplitude>();
        }
       
      
        amplitudeList.Add(this);
       
    }

    // Use this for initialization
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

        //apn = AudioPeerNetwork.audioPeer;
        //apn.registerAmplitude(this);


    }
    // Update is called once per frame
    public void updateScale(float amplitude, float amplitudeBuffer) {

        float value = useBuffer ? amplitudeBuffer : amplitude;

        transform.localScale = Vector3.one * value * maxScale;
        Color col = new Color(red * value, green * value, blue * value);
        mat.SetColor("_EmissionColor", col);

        

	}
}
