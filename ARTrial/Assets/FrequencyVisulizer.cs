using UnityEngine;
using System.Collections;

public class FrequencyVisulizer : MonoBehaviour {

    public int numberOfCubes = 512;
    public GameObject[] cubes;
    public float radius;
    public AudioPeer AP;
    public float maxYScale = 10;
    public float cubeScale = 1;
   

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
	
	// Update is called once per frame
	void Update ()
    {
        //updateVisulizer();

    }

    public void updateVisulizer()
    {
        if (!AP.gameObject.activeSelf || AP.samples == null)
        {
            if (AP.gameObject.activeSelf && AP.samples == null) { Debug.Log("No samples"); }

            return;
        }

        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 scale = Vector3.one * cubeScale;
            scale.y = AP.samples[i] * maxYScale;
            cubes[i].transform.localScale = scale;

        }
    }

    Vector3 getPosOnCricle(Vector3 center, float radius, float angle)
    {
        angle = angle * Mathf.Deg2Rad;
        return center + (Mathf.Sin(angle) * transform.right + Mathf.Cos(angle) * transform.forward) * radius;
    }


}
