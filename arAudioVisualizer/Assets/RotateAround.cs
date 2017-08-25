using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class RotateAround : MonoBehaviour {

    Vector3 center;
    public float raidus;
    public float speed;
    public float delta;
    Vector3 currentAim;
    float currentAngel;

    Vector3 prevAim;


	// Use this for initialization
	void Start () {
        center = transform.position;

        Timing.RunCoroutine(rotate());

	}

    public IEnumerator<float> rotate()
    {
        prevAim = transform.position;
        currentAim = getPosOnCricle(center, raidus, 0);
        bool finishedRotation = false;

        while (!finishedRotation)
        {
            if(Vector3.Distance(transform.position, currentAim) < 0.1f){
                currentAngel += delta;
                prevAim = transform.position;
                currentAim = getPosOnCricle(center, raidus, currentAngel);
                //Debug.Log("new aim");
                //GameObject spawnedCube=GameObject.CreatePrimitive(PrimitiveType.Cube);
                //spawnedCube.transform.position = currentAim;
                //spawnedCube.GetComponent<Collider>().enabled = false;
            }

            //Debug.Log("lerping");

            Timing.WaitUntilDone(Timing.RunCoroutine(_Tween(gameObject, currentAim, speed)));

            //Debug.Log(currentAngel);

            if (currentAngel >= 360) Timing.WaitUntilDone(Timing.RunCoroutine(_Tween(gameObject, center, speed)));



            yield return 0;

        }

        yield break;
    }
	



    Vector3 getPosOnCricle(Vector3 center,float radius, float angle)
    {
        angle = angle * Mathf.Deg2Rad;
       return center+ (Mathf.Sin(angle)*transform.right  + Mathf.Cos(angle)*transform.forward  ) * radius;
    }


    public static IEnumerator<float> _Tween(GameObject go, Vector3 aim, float speed)
    {
        speed = speed / Vector3.Distance(go.transform.position, aim);

        Vector3 initialPosition = go.transform.position;

        float ratio = 0;



        while (ratio < 1)
        {
            ratio += Time.deltaTime * speed;


            go.transform.position = Vector3.Lerp(initialPosition, aim, ratio);
            yield return 0;

        }
        go.transform.position = aim;

        yield break;
    }
}
