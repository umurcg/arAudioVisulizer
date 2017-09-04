using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightBandVis : Visulizer {

    

    //AudioPeerNetwork apn;
    GameObject[] cubes = new GameObject[8];
    public GameObject cubePrefab;

    public float space = 5f;

    public float multiplier = 10;
    public float startScale = 1;

    float heightLevel;

    public float[] eightBand=new float[8];
    public float[] bandBuffer = new float[8];
    public float[] bufferDecrese = new float[8];
    float[] highestBand = new float[8];


    public bool scaleOnlyOneDirection = true;

    //float[] bandBuffer = new float[8];
    //float[] bufferDecrese = new float[8];

    //float[] highestBand = new float[8];

    public bool useBuffer = true;



    // Use this for initialization
    void Start() {
     

        heightLevel = transform.position.y;


        if (transform.childCount!=8) createCubes();
        else
        {
            for(int i=0;i<transform.childCount; i++)
            {
                cubes[i] = transform.GetChild(i).gameObject;
            }
        }
   

    }

    void createCubes()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject spawnedCube = Instantiate(cubePrefab, transform);
            spawnedCube.transform.localScale = Vector3.one * startScale;
            spawnedCube.transform.position = transform.position + transform.right * space * i;
            cubes[i] = spawnedCube;
            
        }
    }

    public override void updateVisulizer(float[] samples)
    {
        base.updateVisulizer(samples);
        
        //Create an array with 8 elements of frequencies
        eightBand= MakeFrequencyBands(samples);

        //Buffer the bands for smooth decrease on bars
        BandBuffer(eightBand);

        //Update band cubes
        updateBands();

        //Update colors of bands
        updateColors(normalizeBand(eightBand));
    }

    public static float[] MakeFrequencyBands(float[] samples)
    {

        float[] frequencyBand = new float[8];

        int count = 0;

        for (int i = 0; i < 8; i++)
        {

            float avarage = 0;
            int samplesCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7) samplesCount += 2;

            for (int j = 0; j < samplesCount; j++)
            {
                avarage += samples[count] * (count + 1);
                count++;
            }

            avarage /= count;

            frequencyBand[i] = avarage * 10;

        }
             

        return frequencyBand;

    }

    //Makes smoother downs for bars
    protected void BandBuffer(float[] bands)
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


    public void updateBands()
    {

        for (int i = 0; i < cubes.Length; i++)
        {
            GameObject cube = cubes[i];

            float scale;

            if (useBuffer && bandBuffer!=null)
            {
                scale = bandBuffer[i] * multiplier;
            }
            else
            {
                scale = eightBand[i] * multiplier;
            }

            cube.transform.localScale = new Vector3(cube.transform.localScale.x, scale, cube.transform.localScale.z);

            if (scaleOnlyOneDirection)
            {

                Vector3 pos = cube.transform.position;
                pos.y = heightLevel + scale / 2;

                cube.transform.position = pos;

            }


        }
        
    }

    public float[] normalizeBand(float[] bands)
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


    public virtual void updateColors(float[] normilized)
    {

        for (int i = 0; i < cubes.Length; i++)
        {
            GameObject cube = cubes[i];
            //Color
            MeshRenderer mr = cube.GetComponent<MeshRenderer>();
            Material mat = mr.material;
            float emmision = normilized[i];
            Color col = new Color(emmision, emmision, emmision);
            mat.SetColor("_EmissionColor", col);
            mr.material = mat;

        }
    }
}
