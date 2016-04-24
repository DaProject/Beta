using UnityEngine;
using System.Collections;

public class TrailActivation : MonoBehaviour {

    public PlayerManagerBackup playerManagerBackup;

	// Use this for initialization
	void Start ()
    {
        playerManagerBackup = GetComponentInParent<PlayerManagerBackup>();
      
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (playerManagerBackup.trailActivation == true)
        {
            Debug.Log("trail activation!!!!");

            gameObject.SetActive(true);
        }

       // else if (playerAttack.trailActivation == false)
        //{

        //    gameObject.SetActive(false);
       // }

        else gameObject.SetActive(false);
    }
}
