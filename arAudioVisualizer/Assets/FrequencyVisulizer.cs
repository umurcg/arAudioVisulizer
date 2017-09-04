using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum axis { x=0,y=1,z=2};

public class FrequencyVisulizer : MonoBehaviour {

    public static List<FrequencyVisulizer> visulizerList;

    public int numberOfCubes = 512;
    public GameObject[] cubes;
    public float radius;
    public float maxScale = 10;
    public float cubeScale = 1;

    float heightLevel;

    //public axis Axis=axis.y;

    //AudioPeerNetwork apn;

    public GameObject barPrefab;

    //private void Start()
    //{
    //    //Try to find audio peer for getting sound data and updates from that
    //    //apn = GameObject.FindGameObjectWithTag("Audio Source").GetComponent<AudioPeerNetwork>();


    //}


    private void Awake()
    {

        if (visulizerList == null)
        {
            visulizerList = new List<FrequencyVisulizer>();
        }
      
         visulizerList.Add(this);
     
    }

    // Use this for initialization
    void Start () {



        //apn = AudioPeerNetwork.audioPeer;


        //Then register visuilizer to audio peer script
        //apn.registerVisulizer(this);


        cubes = new GameObject[numberOfCubes];

        heightLevel = transform.position.y;

        for (int i = 0; i < numberOfCubes; i++)
        {
            GameObject cube = Instantiate(barPrefab);
            cube.transform.position = getPosOnCricle(transform.position, radius, i * 360 / numberOfCubes);
            cube.transform.LookAt(transform.position);
            cube.transform.parent = transform;
            cube.transform.localScale = Vector3.one * cubeScale;
            cubes[i] = cube;

        }
	}
	
	
    public void updateVisulizer(float[] samples)
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 scale = Vector3.one * cubeScale;
            scale.y = samples[i] * maxScale;

            cubes[i].transform.localScale = scale;

            Vector3 pos = cubes[i].transform.position;
            pos.y = heightLevel + (scale.y / 2);
            cubes[i].transform.position = pos;

        }
    }

    Vector3 getPosOnCricle(Vector3 center, float radius, float angle)
    {
        angle = angle * Mathf.Deg2Rad;
        return center + (Mathf.Sin(angle) * transform.right + Mathf.Cos(angle) * transform.forward) * radius;
    }




}
