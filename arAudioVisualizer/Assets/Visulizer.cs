using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visulizer : MonoBehaviour {

    public static List<Visulizer> visulizers;


    float[] samples;


    public static void updateAllVisulizers(float[] samples)
    {
        if (visulizers == null) return;

        foreach(Visulizer v in visulizers)
        {
            v.updateVisulizer(samples);
        }
    }


	public virtual void Awake()
    {
        if(visulizers==null)
            visulizers = new List<Visulizer>();

        visulizers.Add(this);
    }

    //Send samples with this method
    public virtual void updateVisulizer(float[] samples)
    {
        this.samples = samples;
    }

}
