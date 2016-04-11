using UnityEngine;
using System.Collections;

public class DisplacementAction : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * 5);
            Debug.Log("Enemy moved");
        }
    }
}
