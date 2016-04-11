using UnityEngine;
using System.Collections;

public class ChainAnimTrigger : MonoBehaviour {

    Animator anim;
    PlayerManager playerManager;
    public bool chainAnim;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerManager = GetComponentInParent<PlayerManager>();
        chainAnim = false;
    }

    void Update()

    {
        if (chainAnim == true)
        {
            anim.SetBool("chainAnim", chainAnim = true);
        }

        else
        {
            anim.SetBool("chainAnim", chainAnim = false);
        }
    }
}
