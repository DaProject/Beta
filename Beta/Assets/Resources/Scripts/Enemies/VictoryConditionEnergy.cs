using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryConditionEnergy : MonoBehaviour
{

    public PlayerManagerBackup playerManagerBackup;
    public GameObject player;
    public VictoryConditionEnergy victoryCondition;


    public int currentHealth;
    public Slider healthSlider;

    // Use this for initialization
    void Start ()
    {

        victoryCondition = GetComponent<VictoryConditionEnergy>();

        player = GameObject.FindGameObjectWithTag("Player");

        playerManagerBackup = player.GetComponent<PlayerManagerBackup>();

        currentHealth = 100;
    }
	
	// Update is called once per frame
	void Update ()
    {
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            playerManagerBackup.setVictory();

            victoryCondition.enabled = false;
        }
	}
}
