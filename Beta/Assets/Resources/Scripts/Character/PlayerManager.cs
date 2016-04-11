using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour
{

	// States of the player

	public enum PlayerStates {AWAKE, IDLE, DAMAGED, STUNNED, DEAD, VICTORY}

	[Header("States")]
	public PlayerStates state;

	// Health
	[Header("Health")]
	public int maxHealth;
	public int currentHealth;

    // Controller
    [Header("Controller")]
    public float moveHorizontal;                    // Variable that gets the horizontal axis value.
    public float moveVertical;                      // Variable that gets the vertical axis value.
    private Vector3 movement;                       // Vector 3 with the values of the movement.

    // Movement
    [Header("Movement")]
    public float turnSmoothing;                     // A smoothing value for turning the player.
    public float speedDampTime;                     // The damping for the speed parameter.

    // Damage
    [Header("Attacks & Habilities")]
    public bool sword10Active;                      // Bool that allows to use the sword10 ability.

    //Stunned
    [Header("Stunned")]
    public float timeStunned;
    public float timeStunnedIni;

    //Sounds
    [Header("Sounds")]
    public AudioClip hurtClip;                      // Sound that plays when the player gets hurt.
	public AudioClip lowHpClip;                     // Sound that plays when the player is at low health.
	public AudioClip deathClip;                     // Sound that plays when the player dies.
	public AudioClip swordSwipeClip;                // Sound that plays when the player swings the sword. TODO: Need more sounds, at least one per attack.
    AudioSource playerAudio;

	// UI Player
	[Header("UI")]
	public Slider healthSlider;                     // It shows the health bar.
	public Image damageImage;                       // The UI image that shows when the player gets hit.
    public Image sword10Sprite;                     // Image that shows the sword10 ability
	public Color flashColor;                        // The color of the damageImage.

	// Timers
    [Header("Timers")]
    //TODO: review the temp. What does?
    public float tempDamage;                        // Counter that determinates how much time the player has to be in the DAMAGED state.
	public float tempDamageAux;                     // Auxiliar variable that allways has the base tempDamage.
    public float attackStateCounter;                // Auxiliar variable that says how much time the player has to be in the ATTACKXX state. It gets the value from the different counters of each attack.

    // Control player
    [Header("Control player")]
    //public GameObject sword;
    public Material swordMaterial;                      // Material from the sword. TODO: make it work.
    public bool godMode;                                // Bool used for the god mode.
    public float attack01ColliderRadius;			    // Auxiliar variable that sets the radius of the attack01Collider.
    private PlayerAttack playerAttack;                  // PlayerAttack script.
    private PlayerAnimation playerAnimation;            // PlayerAnimation script.
    private PlayerController playerController;          // PlayerController script.

    // Colliders
    [Header("Colliders")]
    public BoxCollider attack10Collider;                // Gets the attackAction collider from the AttackAction child's player.
    public SphereCollider sword10Collider;              // Gets the player sword10Collider.
    public SphereCollider attack01Collider;		        // Gets the attack01Action collider from the attack01Collider child's player.
    public SphereCollider playerSphereCollider;         // Gets the player spherecollider.
    public CapsuleCollider playerCapsuleCollider;       // Gets the player capsulecollider.


    // Animations
    Animator anim;                                      // The animator component from the player.
    ChainAnimTrigger chainTransition;                   // sword-->chain animation transition.

    public bool fadeAlpha = false;
    public float fadeCounter = 0.0f;
    public float alphaValue;
    private float sword10ColorRValue;
    public float sword10ColorRValueAux;
    private float sword10ColorGValue;
    public float sword10ColorGValueAux;
    private float sword10ColorBValue;
    public float sword10ColorBValueAux;

    //Lost&Victory
    [Header("Lost&Victory")]
    public Image blackScreen;                           // Used to go to the main menu after winning or losing.
    public float counterMenu;                           // Counter that says how much time ara going to pass between winning or losing and goint to the main menu.
    public float counterMenuAux;                        // Auxiliar variable used as a base time for the counterMenu.
    public Text victoryText;                            // Text that appears after winning.
    public Text lostText;                               // Text that appears after losing.
    public Text cooldownSword10;                        // Text that says the amount of CD the sword10Ability has.
    public GameObject pointsText;
    public PointCounter score;
    public Text newAbility;

    public CameraShake cameraShake;

    // Use this for initialization
    void Start ()
    {
		setAwake ();                                // Call the setAwake function.
    }
	
	// Update is called once per frame
    void FixedUpdate()
    {
        switch(state)
        {
            case PlayerStates.IDLE:
                IdleBehaviour();
                break;
            case PlayerStates.DAMAGED:
                DamagedBehaviour();
                break;
        }
    }

    // TODO: Put the attack conditions on the Update()
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.G)) godMode = !godMode;        // Changes between godmode and normal mode whenever the G key is pressed. G for God
        if (godMode)
        {
            playerSphereCollider.enabled = false;                         // Sets the sphereCollider from the player to false.
            playerCapsuleCollider.enabled = false;                        // Sets the capsuleCollider from the player to false.
            playerController.rigidBody.useGravity = false;                           // Deactivates the gravity from the player. Can no longer falls.
        }
        else
        {
            playerSphereCollider.enabled = true;                         // Sets the sphereCollider from the player to false.
            playerCapsuleCollider.enabled = true;                        // Sets the capsuleCollider from the player to false.
            playerController.rigidBody.useGravity = true;                            // Activates the gravity from the player. Can fall.
        }

		switch (state)
        {
			case PlayerStates.AWAKE:
				AwakeBehaviour();
				break;
            case PlayerStates.STUNNED:
                StunnedBehaviour();
                break;
            case PlayerStates.DEAD:
				DeadBehaviour();
				break;
			case PlayerStates.VICTORY:
				VictoryBehaviour();
				break;
		}

		if (currentHealth <= 10) 
		{
			playerAudio.clip = lowHpClip;
			playerAudio.Play();  
		}

        if (score.pointsCounter >= 100)
        {
            sword10Active = true;
            sword10Sprite.enabled = true;
            if (alphaValue >= 0) newAbility.color = new Color(1.0f, 1.0f, 1.0f, alphaValue -= 0.2f * Time.deltaTime);
        }

        if (playerAttack.sword10CD >= 0)
        {
            playerAttack.sword10CD -= 1 * Time.deltaTime;
            cooldownSword10.enabled = true;
            cooldownSword10.text = "" + playerAttack.sword10CD;
            sword10Sprite.color = new Color(1.0f, sword10ColorGValue += 1 / playerAttack.sword10CDAux * Time.deltaTime, sword10ColorBValue += 1 / playerAttack.sword10CDAux * Time.deltaTime);
        }
        else if (playerAttack.sword10CD <= 0)
        {
            cooldownSword10.enabled = false;
            sword10Sprite.color = new Color(1.0f, 1.0f, 1.0f);
        }
    }

	// Behaviours
	private void AwakeBehaviour()
	{
		setIdle ();                                                                 // Initalization.
	}

	private void IdleBehaviour()
    {
        //TODO: Re-do the animation behaviour
        /*
        if (Input.GetKey(KeyCode.W))
        {
            playerController.MoveForward(playerController.playerSpeed);
            anim.SetBool("IsWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.W)) anim.SetBool("IsWalking", false);
        if (Input.GetKey(KeyCode.S))
        {
            playerController.MoveBackward(playerController.playerSpeed);
            anim.SetBool("IsWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.S)) anim.SetBool("IsWalking", false);
        if (Input.GetKey(KeyCode.D)) playerController.RotateRight();
        if (Input.GetKey(KeyCode.A)) playerController.RotateLeft();
        */
        playerController.ControllerAction(playerController.playerSpeed);

        playerController.MovementManagement(moveHorizontal, moveVertical);

        playerController.Animating(moveHorizontal, moveVertical);

        playerController.Rotating(moveHorizontal, moveVertical);
        
        if (currentHealth <= 0) setDead();

        if(attackStateCounter >= 0)
        {
            attackStateCounter -= Time.deltaTime;
            playerController.transform.position += playerController.transform.forward * playerController.playerDisplacementSpeed * Time.deltaTime;
            //playerController.rigidBody.isKinematic = false;
        }
        else
        {
            attack10Collider.enabled = false;
            attack01Collider.enabled = false;
            sword10Collider.enabled = false;
            //playerController.rigidBody.isKinematic = true;

            if (Input.GetMouseButtonDown(0)) playerAttack.Attack10(playerAttack.damageAttack10);                                        // Calls the setAttack10 function if mouse left button is pressed.
            else if (Input.GetMouseButtonDown(1)) playerAttack.Attack01(playerAttack.damageAttack01);                                   // Calls the setAttack01 function if mouse right button is pressed.
		    else if (Input.GetKeyDown(KeyCode.Alpha1) && sword10Active) playerAttack.Sword10(playerAttack.damageSword10);               // Calls the setSword10 function if the 1 keypad is pressed, and if is not in chain mode.
            else if (Input.GetKeyDown(KeyCode.Alpha2)) playerAttack.Chain01(playerAttack.damageChain01);                                // If the chain mode is active, goes to the setChain01.
            else if (Input.GetKeyDown(KeyCode.LeftShift)) playerAttack.Dash();                                                          // Calls the setDash function if left shift key is pressed.
        }
        
    }

    private void DamagedBehaviour()
    {
        /*
        if (Input.GetKey(KeyCode.W))
        {
            playerController.MoveForward(playerController.playerSpeed);
            anim.SetBool("IsWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.W)) anim.SetBool("IsWalking", false);
        if (Input.GetKey(KeyCode.S))
        {
            playerController.MoveBackward(playerController.playerSpeed);
            anim.SetBool("IsWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.S)) anim.SetBool("IsWalking", false);
        if (Input.GetKey(KeyCode.D)) playerController.RotateRight();
        if (Input.GetKey(KeyCode.A)) playerController.RotateLeft();
        */
        playerController.ControllerAction(playerController.playerSpeed);

        playerController.MovementManagement(moveHorizontal, moveVertical);

        playerController.Animating(moveHorizontal, moveVertical);
        
        if (attackStateCounter >= 0)
        {
            attackStateCounter -= Time.deltaTime;
            playerController.transform.position += playerController.transform.forward * playerController.playerDisplacementSpeed * Time.deltaTime;
            playerController.rigidBody.isKinematic = false;
        }
        else
        {
            attack10Collider.enabled = false;
            attack01Collider.enabled = false;
            sword10Collider.enabled = false;
            playerController.rigidBody.isKinematic = true;

            if (Input.GetMouseButtonDown(0)) playerAttack.Attack10(playerAttack.damageAttack10);                                                         // Calls the setAttack10 function if mouse left button is pressed.
            else if (Input.GetMouseButtonDown(1)) playerAttack.Attack01(playerAttack.damageAttack01);                                                    // Calls the setAttack01 function if mouse right button is pressed.
		    else if (Input.GetKeyDown(KeyCode.Alpha1) && sword10Active) playerAttack.Sword10(playerAttack.damageSword10);                                // Calls the setSword10 function if the 1 keypad is pressed, and if is not in chain mode.
            else if (Input.GetKeyDown(KeyCode.Alpha2)) playerAttack.Chain01(playerAttack.damageChain01);                                // If the chain mode is active, goes to the setChain01.
            else if (Input.GetKeyDown(KeyCode.LeftShift)) playerAttack.Dash();        // Calls the setDash function if left shift key is pressed.
        }

        tempDamage -= Time.deltaTime;

        damageImage.color = Color.Lerp(damageImage.color, Color.clear, tempDamage * Time.deltaTime);                    // Sets the difumination for the damageImage

        if (tempDamage <= 0) setIdle();                                                            // If the player has not been attacked for a while, goes back to setIdle function
	}

    private void StunnedBehaviour()
    {
        timeStunned -= Time.deltaTime;

        if (timeStunned <= 0) setIdle();
    }

    private void DeadBehaviour()
	{
        // TODO: What happens when the player die.
	}

	private void VictoryBehaviour()
	{
        // TODO: WHat happens when the player wins.
	}

	// Sets
	public void setAwake()
    {

        currentHealth = maxHealth;                          		        // Sets the player health to the value of maxHealth that you indicated.

		sword10Active = false;                                		        // Sets the sword10Active bool to false by default.
        //sword20Active = false;                                		    // Sets the sword20Active bool to false by default.
        //chain10Active = false;                                		        // Sets the chain10Active bool to false by default.

        playerSphereCollider = GetComponent<SphereCollider>();
        playerCapsuleCollider = GetComponent<CapsuleCollider>();
        attack10Collider = GetComponentInChildren<BoxCollider>();      	        // Gets the BoxCollider of the PlaceHolder_Sword children.
		attack01ColliderRadius = attack01Collider.radius;   

        playerAudio = GetComponent<AudioSource>();          		        // Gets the component AudioSource from the player.

        timeStunned = timeStunnedIni;

        flashColor = new Color(1.0f, 0.0f, 0.0f, 0.1f);           		    // Sets the color values for the damageImage.
        //swordSprite.enabled = true;
        //chainSprite.enabled = false;
        sword10Sprite.enabled = false;
        //sword20Sprite.enabled = false;
        //chain10Sprite.enabled = false;

        swordMaterial = GetComponent<Renderer>().material;
        playerAttack = GetComponent<PlayerAttack>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerController = GetComponent<PlayerController>();
        chainTransition = GetComponentInChildren<ChainAnimTrigger>();
        anim = GetComponent<Animator>();

        blackScreen.enabled = false;
        victoryText.enabled = false;
        lostText.enabled = false;
        cooldownSword10.enabled = false;

        state = PlayerStates.AWAKE;                         		        // Cals the AWAKE state.
	}

	public void setIdle()
    {
        //Debug.Log("Idle");

		attack10Collider.enabled = false;                       // Deactivates the collider of the attack10 attack (sword).
		attack01Collider.enabled = false;                       // Deactivates the collider of the chain01 hability (chain).
        attack01Collider.radius = attack01ColliderRadius;       // Gives the chain01Collider it's previous radius value.
        sword10Collider.enabled = false;
        playerSphereCollider.enabled = true;
        playerCapsuleCollider.enabled = true;

        chainTransition.chainAnim = false;

        playerController.rigidBody.isKinematic = false;         // Deactivates the isKinematic bool of the rigidbody.
        tempDamage = 0.0f;

        damageImage.enabled = false;                            // Deactivation of the damageImage.

        state = PlayerStates.IDLE;                              // Calls the IDLE state.

        timeStunned = timeStunnedIni;
	}

    public void setDamaged(int damage)
	{
        damageImage.enabled = true;                             // Activation of the damage image.
        damageImage.color = flashColor;                         // Sets the color for the damageImage.

		tempDamage = tempDamageAux;

		currentHealth -= damage;                                // Applies the damage recieved.

		playerAudio.clip = hurtClip;
        playerAudio.Play();                                     // Plays the hurt sound when the player gets hit.

        healthSlider.value = currentHealth;                     // Sets the value of the slider from the currentHealth of the player.

		if (currentHealth <= 0) setDead ();                     // Calls the setDead function if the player has died.
		else state = PlayerStates.DAMAGED;                      // If the player is still alive, calls the DAMAGED state.
	}

    public void setStunned()
    {
        anim.SetTrigger("Stunned");

        state = PlayerStates.STUNNED;
    }


    public void setDead()
	{
		currentHealth = 0;                                      // Sets the health to 0.

        playerSphereCollider.enabled = false;                   // Sets the sphereCollider from the player to false.
        playerCapsuleCollider.enabled = false;                  // Sets the capsuleCollider from the player to false.

        anim.SetTrigger("Die");                                 // Plays the die animation.

        playerAudio.clip = deathClip;                           // Plays the hurt sound when you die.
        playerAudio.Play();

		state = PlayerStates.DEAD;                              // Calls the DEAD state.
	}

	public void setVictory()
	{
		state = PlayerStates.VICTORY;                           // Calls the VICTORY state.
	}

    // Functions
}