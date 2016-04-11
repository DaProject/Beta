using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("Controller")]
    private PlayerManager playerManager;
    private PlayerAnimation playerAnimation;
    private PlayerController playerController;

    public int attackDamage;

    public int damageAttack10;                      // Variable with the damage of the attack10 (sword).
    public int damageSword10;                       // Variable with the damage of the sword10 (sword hability).
    public int damageAttack01;                      // Variable with the damage of the attack01 (chain).
    public int damageChain01;                       // Variable with the damage of the chain01 (chain hability).
    public float attack10ColliderRadius;
    public float sword10CD;
    public float sword10CDAux;
    public float tempSword10Stun;                   // Counter that determinates when the sword10 attack is going to stun the enemies.

    public bool trailActivation;

	// Use this for initialization
	void Start ()
    {
        playerManager = GetComponent<PlayerManager>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerController = GetComponent<PlayerController>();

        //trailActivation = false;
    }

    public void Attack10(int damageDealt)
    {
        Debug.Log("Attack10");

        trailActivation = true;

        playerManager.attack10Collider.enabled = true;

        //playerController.playerDisplacementSpeed = playerController.AnimationDisplacement(playerController.distanceAttack10, playerAnimation.tempAttack10);
        //Debug.LogFormat(string.Format("localPosition.z: {0}, distanceAttack10: {1}, tempAttack: {2} displacementSpeed: {3}", playerController.trans.localPosition.z, playerController.distanceAttack10, playerAnimation.tempAttack10, playerController.playerDisplacementSpeed));

        playerAnimation.Attack10Animation();

        attackDamage = damageDealt;
    }

    public void Attack01(int damageDealt)
    {
        Debug.Log("Attack01");

        playerManager.attack01Collider.enabled = true;

        playerManager.attack01Collider.radius += attack10ColliderRadius * Time.deltaTime;        // Makes the attack01Collider get bigger during the attack01Behaviour.

        //playerController.playerDisplacementSpeed = playerController.AnimationDisplacement(playerController.distanceAttack01, playerAnimation.tempAttack01);

        playerAnimation.Attack01Animation();

        attackDamage = damageDealt;
    }

    public void Sword10(int damageDealt)
    {
        Debug.Log("Sword10");

        playerManager.sword10Collider.enabled = true;

        //playerController.playerDisplacementSpeed = playerController.AnimationDisplacement(playerController.distanceSword10, playerAnimation.tempSword10);

        playerAnimation.Sword10Animation();

        attackDamage = damageDealt;
    }

    public void Chain01(int damageDealt)
    {
        Debug.Log("Chain01");

        //playerManager.chain01Collider.enabled = true;

        //playerController.playerDisplacementSpeed = playerController.AnimationDisplacement(playerController.distanceChain01, playerAnimation.tempChain01);

        playerAnimation.Chain01Animation();

        attackDamage = damageDealt;
    }

    public void Dash()
    {
        //playerController.playerDisplacementSpeed = playerController.AnimationDisplacement(playerController.distanceDash, playerAnimation.tempDash);
        Debug.Log("dashing");
        playerAnimation.DashAnimation();
        playerManager.playerSphereCollider.enabled = false;
        playerManager.playerCapsuleCollider.enabled = false;
        playerController.rigidBody.useGravity = false;
    }
}
