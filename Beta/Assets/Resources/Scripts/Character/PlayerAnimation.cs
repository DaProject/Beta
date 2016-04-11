using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerController playerController;
    public PlayerAttack playerAttack;

    public float tempAttack10;                      // Counter that reflects how much the animation of the attack10 longs.
    public float tempSword10;                       // Counter that reflects how much the animation of the sword10 longs.
    public float tempSword20;                       // Counter that reflects how much the animation of the sword20 longs.
    public float tempAttack01;                      // Counter that reflects how much the animation of the attack01 longs.
    public float tempChain01;                       // Counter that reflects how much the animation of the chain10 longs.
    public float tempChain02;                       // Counter that reflects how much the animation of the chain02 longs.
    public float tempSlash;                         // Counter that reflects how much the animation of the slash longs.
    public float tempDash;                          // Counter that determinates how much time the player has to be in the DASH state.

    // Animations
    public Animator anim;                                  // The animator component from the player.

    public void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerController = GetComponent<PlayerController>();
        playerAttack = GetComponent<PlayerAttack>();
        anim = GetComponent<Animator>();
    }

    public void Attack10Animation()
    {
        anim.SetTrigger("Attack10");

        playerManager.attackStateCounter = tempAttack10;

        Debug.Log("playing Attack10Animation");
    }

    public void Attack01Animation()
    {
        anim.SetTrigger("Attack01");

        playerManager.attackStateCounter = tempAttack01;

        Debug.Log("playing Attack01Animation");
    }

    public void Sword10Animation()
    {
        anim.SetTrigger("Sword10");

        playerManager.attackStateCounter = tempSword10;

        Debug.Log("playing Sword10Animation");
    }

    public void Chain01Animation()
    {
        anim.SetTrigger("Chain01");

        playerManager.attackStateCounter = tempChain01;

        Debug.Log("playing Chain01Animation");
    }

    public void DashAnimation()
    {
        //anim.SetTrigger("Dash");

        playerManager.attackStateCounter = tempDash;

        Debug.Log("playing DashAnimation");
    }
}
