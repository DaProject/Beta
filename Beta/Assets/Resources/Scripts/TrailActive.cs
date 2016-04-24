using UnityEngine;
using System.Collections;

public class TrailActive : MonoBehaviour {

    public GameObject crackGround;

    // Use this for initialization
    void Start () {

        crackGround.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {


        crackGround.SetActive(true);
    }
}
