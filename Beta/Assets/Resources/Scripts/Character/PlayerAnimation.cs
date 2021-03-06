﻿using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerAttack playerAttack;
    public CharacterBehaviour characterBehaviour;

    public float attackRatio;                                           // Counter used to get the lenght of the diferent attack animations.

    public bool walking;                                                // Bool for the walking animation.

    // Animations
    public Animator anim;                                               // The animator component from the player.

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();                  // Gets the PlayerManager script.
        playerAttack = GetComponent<PlayerAttack>();                    // Gets the PlayerAttack script.
        characterBehaviour = GetComponent<CharacterBehaviour>();        // Gets the CharacterBehavior script.
        anim = GetComponent<Animator>();

        walking = false;
    }

   void Update()
    {
        if (characterBehaviour.moveDirection.x != 0 || characterBehaviour.moveDirection.z != 0)     // Checks if the player is moving
        {
            walking = true;
            anim.SetBool("IsWalking", walking);
        }
        else
        {
            walking = false;
            anim.SetBool("IsWalking", walking);
        }
    }

    public void Attack10Animation()
    {
        anim.SetTrigger("Attack10");                                    // Triggers the condition for the Attack10 animation.

        attackRatio = anim.GetCurrentAnimatorClipInfo(0).Length;        // Gets the lenght of the current animation.

        playerManager.attackStateCounter = attackRatio;

        Debug.Log("playing Attack10Animation");
    }

    public void Attack01Animation()
    {
        anim.SetTrigger("Attack01");

        attackRatio = anim.GetCurrentAnimatorClipInfo(0).Length;        // Gets the lenght of the current animation.

        playerManager.attackStateCounter = attackRatio;                 // Gets the lenght of the current animation.

        Debug.Log("playing Attack01Animation");
    }

    public void Sword10Animation()
    {
        anim.SetTrigger("Sword10");

        attackRatio = anim.GetCurrentAnimatorClipInfo(0).Length;        // Gets the lenght of the current animation.

        playerManager.attackStateCounter = attackRatio;                 // Gets the lenght of the current animation.

        Debug.Log("playing Sword10Animation");
    }

    public void Chain01Animation()
    {
        anim.SetTrigger("Chain01");

        attackRatio = anim.GetCurrentAnimatorClipInfo(0).Length;        // Gets the lenght of the current animation.

        playerManager.attackStateCounter = attackRatio;                 // Gets the lenght of the current animation.

        Debug.Log("playing Chain01Animation");
    }

    public void DashAnimation()
    {
        anim.SetTrigger("Dash");

        attackRatio = anim.GetCurrentAnimatorClipInfo(0).Length;        // Gets the lenght of the current animation.

        playerManager.attackStateCounter = attackRatio;                 // Gets the lenght of the current animation.

        Debug.Log("playing DashAnimation");
    }
}
