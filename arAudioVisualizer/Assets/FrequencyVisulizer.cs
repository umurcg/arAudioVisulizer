using UnityEngine;
using System.Collections;

public enum axis { x=0,y=1,z=2};

public class FrequencyVisulizer : MonoBehaviour {

    public int numberOfCubes = 512;
    public GameObject[] cubes;
    public float radius;
    public float maxScale = 10;
    public float cubeScale = 1;
   
    //public axis Axis=axis.y;

    AudioPeerNetwork apn;

    private void Awake()
    {
        //Try to find audio peer for getting sound data and updates from that
        apn = GameObject.FindGameObjectWithTag("Audio Source").GetComponent<AudioPeerNetwork>();


        //Then register visuilizer to audio peer script
        apn.registerVisulizer(this);
    }


    // Use this for initialization
    void Start () {
        cubes = new GameObject[numberOfCubes];

	    for(int i = 0; i < numberOfCubes; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
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

            //switch (Axis)
            //{
            //    case (axis.x):
            //        scale.x= samples[i] * maxScale;
            //        break;
            //    case (axis.y):
            //        scale.y= samples[i] * maxScale;
            //        break;
            //    case (axis.z):
            //        scale.z= samples[i] * maxScale;
            //        break;

            //}
               

            //scale.y = samples[i] * maxScale;
            cubes[i].transform.localScale = scale;

        }
    }

    Vector3 getPosOnCricle(Vector3 center, float radius, float angle)
    {
        angle = angle * Mathf.Deg2Rad;
        return center + (Mathf.Sin(angle) * transform.right + Mathf.Cos(angle) * transform.forward) * radius;
    }


}
