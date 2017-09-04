using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectAroundSphere : MonoBehaviour {

    public GameObject prefab;
    public int numberOfCubes;
    public float radius;

	// Use this for initialization
	void Start () {
		
	}
	
    [ContextMenu("Create Cubes")]
    public void create()
    {
        for(int i = 0; i < numberOfCubes; i++)
        {
            GameObject spawnedObject = Instantiate(prefab, transform);


            spawnedObject.transform.position = getPosOnCricle(transform.position, radius, ((float)i / (float)numberOfCubes) * 360);
            var lookDir = spawnedObject.transform.position - transform.position;
            lookDir=lookDir.normalized;

            spawnedObject.transform.LookAt(transform.position + lookDir);
      

        }
    }

	// Update is called once per frame
	void Update () {
  

	}

    Vector3 getPosOnCricle(Vector3 center, float radius, float angle)
    {
        angle = angle * Mathf.Deg2Rad;
        return center + (Mathf.Sin(angle) * transform.right + Mathf.Cos(angle) * transform.forward) * radius;
    }
}
