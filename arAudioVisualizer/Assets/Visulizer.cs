using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visulizer : MonoBehaviour {

    public static List<Visulizer> visulizers;


    protected float[] samples;


    public static void updateAllVisulizers(float[] samples)
    {
        if (visulizers == null) return;

        foreach(Visulizer v in visulizers)
        {
            v.updateVisulizer(samples);
        }
    }


	public virtual void OnEnable()
    {
        if(visulizers==null)
            visulizers = new List<Visulizer>();

        visulizers.Add(this);
    }

    public virtual void OnDisable()
    {
        if (visulizers.Contains(this)) visulizers.Remove(this);
    }

    public virtual void OnDestroy()
    {
        if (visulizers.Contains(this)) visulizers.Remove(this);
    }


    //Send samples with this method
    public virtual void updateVisulizer(float[] samples)
    {
        this.samples = samples;
    }

}
