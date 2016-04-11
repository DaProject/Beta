using UnityEngine;
using System.Collections;

public class ParticleSystemEmit : MonoBehaviour {

	public ParticleSystem hitParticle;
	ParticleSystem.EmissionModule hitParticleEmit;

	// Use this for initialization
	void Start () {

		hitParticle = GetComponent<ParticleSystem>();
		hitParticleEmit = hitParticle.emission;

	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.L))
        {
            /*if (!hitParticle.isPlaying)
            {
                hitParticle.Simulate(0.0f, true, true);
                hitParticleEmit.enabled = true;
            }
            */
            hitParticle.Play();

        }

        else
        {
            //if (hitParticle.isPlaying)
            //{
                //hitParticleEmit.enabled = false;
               // hitParticle.Stop();
            //}

        }
	}
}
