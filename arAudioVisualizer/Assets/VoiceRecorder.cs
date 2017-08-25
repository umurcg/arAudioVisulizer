using UnityEngine;
using System.Collections;
//using UnityEditor;

public class VoiceRecorder : MonoBehaviour {

    //Microphone mic;
     string deviceName;
    public AudioSource ac;
    public int deviceIndex;
    public bool recordAtStart = false;
	// Use this for initialization
	void Start () {
        if (deviceIndex >= Microphone.devices.Length)
        {
            Debug.Log("Your device index exceed length of devices");
            enabled = false;

        }


        deviceName=Microphone.devices[deviceIndex];

        if (recordAtStart) startToRecord();
    }

    public void startToRecord()
    {

     ac.clip=   Microphone.Start(deviceName, true, 1000, 44100);
        ac.Play();
     

        
    }

    public void endRecord()
    {
        Microphone.End(deviceName);
    }
	
    public void printDevices()
    {
       string[] allDevices= Microphone.devices;
        foreach (string device in allDevices) Debug.Log(device);
    }



	// Update is called once per frame
	void Update () {
	
	}



}
#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(VoiceRecorder), true)]
public class VoiceRecorderEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        VoiceRecorder script = (VoiceRecorder)target;
        if (GUILayout.Button("Print devices "))
        {
            script.printDevices();

        }

        if (GUILayout.Button("start "))
        {
            script.startToRecord();

        }
        if (GUILayout.Button("end "))
        {
            script.endRecord();

        }
    }
}
#endif