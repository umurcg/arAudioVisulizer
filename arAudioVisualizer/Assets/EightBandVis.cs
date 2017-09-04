using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightBandVis : MonoBehaviour {

    public static List<EightBandVis> eightBandVisList;

    //AudioPeerNetwork apn;
    GameObject[] cubes = new GameObject[8];
    public GameObject cubePrefab;

    public float space = 5f;

    public float multiplier = 10;
    public float startScale = 1;

    float heightLevel;


    public bool scaleOnlyOneDirection = true;

    //float[] bandBuffer = new float[8];
    //float[] bufferDecrese = new float[8];

    //float[] highestBand = new float[8];

    public bool useBuffer = true;


    private void Awake()
    {

        if (eightBandVisList == null)
        {
            eightBandVisList = new List<EightBandVis>();
        }
        
        
        eightBandVisList.Add(this);
        
    }

    // Use this for initialization
    void Start() {
        //GameObject source = GameObject.FindGameObjectWithTag("Audio Source");
                
        
        //apn = AudioPeerNetwork.audioPeer;

        heightLevel = transform.position.y;

        //if (source != null)
        //    apn = source.GetComponent<AudioPeerNetwork>();

        //apn.registerEightBand(this);

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

    // Update is called once per frame
    void FixedUpdate()
    {

        //if (!apn || apn.enabled == false)
        //{
        //    if (!apn)
        //    {
        //        GameObject source = GameObject.FindGameObjectWithTag("Audio Source");
        //        apn = source.GetComponent<AudioPeerNetwork>();
        //    }

        //    Debug.Log("No apn");
        //    return;
        //}

        //updateBands();

    }



    public void updateBands(float[] bands, float[] bandBuffer=null)
    {


        //if (useBuffer && bandBuffer != null) bands = bandBuffer;

        //float[] normilizedBand = (useBuffer) ? normalizeBand(bands) : normalizeBand(bandBuffer);

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
                scale = bands[i] * multiplier;
            }

            cube.transform.localScale = new Vector3(cube.transform.localScale.x, scale, cube.transform.localScale.z);

            if (scaleOnlyOneDirection)
            {

                Vector3 pos = cube.transform.position;
                pos.y = heightLevel + scale / 2;

                cube.transform.position = pos;

            }

            //Debug.Log(emmision);

        }
        
    }

    public void updateColors(float[] normilized)
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
