using UnityEngine;
using System.Collections;

public class BloodHitFX : MonoBehaviour {

    public float abilityCounter;

    public ParticleSystem blood;

    bool abilityActivated;


    // Use this for initialization
    void Start () {

    
        abilityActivated = false;

        blood.Stop(true);

    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey(KeyCode.U))
        {
				abilityCounter = 240;

				abilityActivated = true;

				blood.Play(true);
        }

        if (abilityCounter <= 0)
        {
            if (blood.isPlaying)
            {
                blood.Stop(true);
            }

            abilityActivated = false;
        }

        if (abilityActivated == true)
        {
            abilityCounter--;
        }
    }
}
