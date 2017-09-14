using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifetimeVisulizer : Visulizer {

    //8 lifetime band for each frequency

    float initialLifeTime;
    ParticleSystem ps;
    float bandLenght;

    public float r, g, b = 1;

    float[] highestBand = new float[8];



    // Use this for initialization
    void Start () {

        ps = GetComponent<ParticleSystem>();
        initialLifeTime = ps.main.startLifetime.constant;
        bandLenght = initialLifeTime / 8;

        Debug.Log(valueToColor(0.5f));

	}

    public override void updateVisulizer(float[] samples)
    {
        base.updateVisulizer(samples);
        updateParticleColors();
    }

    void updateParticleColors()
    {
        //Debug.Log("Updating particles");
        ParticleSystem.Particle[] particles=new ParticleSystem.Particle[ps.main.maxParticles];
        
        

        int numberOfParticles = ps.GetParticles(particles);

        float[] eightBand = normalizeBand(EightBandVis.MakeFrequencyBands(samples));

        for (int i=0 ; i < particles.Length;  i++)
        {
            ParticleSystem.Particle particle = particles[i];

            int index = (int)particle.remainingLifetime / 2;
            float value = eightBand[index];
            particle.startColor = valueToColor(value);
            //particle.startColor = new Color(r,g,b);

            particles[i] = particle;

        }

        ps.SetParticles(particles, numberOfParticles);




    }

    Color valueToColor(float value)
    {
       float R,B,G = 0;

        if (0 <= value && value <= 1f/8f)
        {
            R = 0;
            G = 0;
            B = 4 * value + 0.5f; // .5 - 1 // b = 1/2
        }
        else if (1f / 8f < value && value <= 3f / 8f)
        {
            R = 0;
            G = 4 * value - .5f; // 0 - 1 // b = - 1/2
            B = 1; // small fix
        }
        else if (3f / 8f < value && value <= 5f / 8f)
        {
            //Debug.Log("hEYYYYYYYY");
            R = 4 * value - 1.5f; // 0 - 1 // b = - 3/2
            G = 1;
            B = -4 * value + 2.5f; // 1 - 0 // b = 5/2
        }
        else if (5f / 8f < value && value <= 7f / 8f)
        {
            R = 1;
            G = -4 * value + 3.5f; // 1 - 0 // b = 7/2
            B = 0;
        }
        else if (7f / 8f < value && value <= 1f)
        {
            R = -4 * value + 4.5f; // 1 - .5 // b = 9/2
            G = 0;
            B = 0;
        }
        else
        {    // should never happen - value > 1
            R = .5f;
            G = 0;
            B = 0;
        }

        Color color=(new Color(R, G, B));
        //Debug.Log(R+","+G+","+B);

        return color;

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


    // Update is called once per frame
    void Update () {


		
	}
}
