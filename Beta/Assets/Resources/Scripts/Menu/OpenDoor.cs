using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour
{
    public Transform trans;
    public float counter;
    public float speed;
    public GameObject energyHealth;


    void Start()
    {
        trans = GetComponent<Transform>();
        //energyHealth = GameObject.FindGameObjectWithTag("EnergyHealth");

        energyHealth.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            energyHealth.SetActive(true);

            if (counter >= 0)
            {
                counter -= 1 * Time.deltaTime;
                trans.localPosition += trans.forward * speed * Time.deltaTime;
            }
        }
            
    }
}