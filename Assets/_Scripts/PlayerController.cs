/*
 * Sourcefile name : PlayerController
 * Author’s name: Anjith Babu
 * Last	Modifiedby: Anjith Babu
 * Date	lastModified : Feb 29, 2016	
 * Program	description: Player controller is used to controll player behaviour	
 **
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VelocityRange {
    //variables
    public float min;
    public float max;

    //Constructor
    public VelocityRange(float min, float max)
    {
        this.min = min; 
        this.max = max;
    }
}

public class PlayerController : MonoBehaviour {
    //private instance variables
    private Animator heroAnimator;
    private float moveX;
    private float jumpY;
    private bool moveRightSide;
    private Rigidbody2D playerRigidBd;
    private bool isGrounded;
    private float forceX;
    private float forceY;
    private float playerX;
    private float playerY;
    private AudioSource[] audioSources;
    private AudioSource hitSound;
    private AudioSource jumpSound;
    private bool levelTwoFlag = true;
    private bool levelThreeFlag = true;


    
    //public instance variables
    public VelocityRange velocityRange;
    public Transform groundCheck;
    public float moveForce;
    public float jumpForce;
    public Transform mainCamera;
    public GameController gameController;
    public Button StartButton;
    public Text LevelLabel;
    public Text HitLabel;
 

	// Use this for initialization
	void Start () {
        this.audioSources = gameObject.GetComponents<AudioSource>();
        this.hitSound = audioSources[0];
        this.jumpSound = audioSources[1];
        this.velocityRange = new VelocityRange(200f, 10000f);
        this.playerRigidBd = gameObject.GetComponent<Rigidbody2D>();
	    this.heroAnimator = gameObject.GetComponent<Animator>();
        this.heroAnimator.SetInteger("AnimState", 0);
        this.HitLabel.gameObject.SetActive(false);
        this.moveX = 0f;
        this.jumpY = 0f;
        this.moveForce = 2200000f;
        this.jumpForce = 59000000f;
        this.moveRightSide = true;       
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 currentPosition = new Vector3(this.transform.position.x + 100f, this.transform.position.y, -10);
        this.mainCamera.position = currentPosition;


        this.forceX = 0f;
        this.forceY = 0f;
        this.isGrounded = Physics2D.Linecast(this.transform.position,
                            this.groundCheck.position,
                            1 << LayerMask.NameToLayer("Ground"));
        float absVelX = Mathf.Abs(this.playerRigidBd.velocity.x);
        float absVelY = Mathf.Abs(this.playerRigidBd.velocity.y);


        // things to do when player is grounded
        if (this.isGrounded)
        {
            this.moveX = Input.GetAxis("Horizontal");
            this.jumpY = Input.GetAxis("Vertical");

            if (this.moveX != 0)
            {
                if (this.moveX > 0)
                {
                    if (absVelX < this.velocityRange.max)
                    {
                        forceX = this.moveForce;
                    }
                    this.moveRightSide = true;
                }
                else if (this.moveX < 0)
                {
                    if (absVelX < this.velocityRange.max)
                    {
                        forceX = -this.moveForce;
                    }
                    this.moveRightSide = false;
                }
                this.flipPlayerDirection();
                this.heroAnimator.SetInteger("AnimState", 1);
            }
            else if (this.jumpY > 0)
            {
                this.jumpSound.Play();
                if (absVelY < this.velocityRange.max)
                {
                    forceY = this.jumpForce;
                }

                this.heroAnimator.SetInteger("AnimState", 2);
            }
            else
            {
                this.heroAnimator.SetInteger("AnimState", 0);
            }

            //apply force to the player
            this.playerRigidBd.AddForce(new Vector2(forceX, forceY));

        }
        else
        {
            //this.heroAnimator.SetInteger("AnimState", 2);
        }

        updateLevel(); // update level based on player x
        checkIfHeroFellDown();          
	}

    // flip player's side
    private void flipPlayerDirection()
    {
        this.transform.localScale = (this.moveRightSide) ? new Vector2(1, 1) 
                                                         : new Vector2(-1, 1);
    }

    // check if hero fell down
    private void checkIfHeroFellDown()
    {
        if (this.transform.position.y < -328)
        {
            this.hitSound.Play();
            this.gameController.LivesValue -= 1; 
            this.transform.position = new Vector2(this.playerX, this.playerY);

        }
      
    }

    // check if player has collided with enemy
    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Enemy"))
        {
            this.hitSound.Play();
            forceY = this.jumpForce;
            this.gameController.LivesValue -= 1;
            this.HitLabel.gameObject.SetActive(true);
            StartCoroutine(makePalyerDisappear());

            
        }
    }

    // update level based on x position
    private void updateLevel()
    {
        if (this.transform.position.x > 681 && this.transform.position.x < 1600)
        {
            this.LevelLabel.text = "Level: 2";
            this.playerX = 752f;
            this.playerY = 204f;
            this.gameController.ScoreValue = (this.levelTwoFlag) ? this.gameController.ScoreValue + 200 :
                this.gameController.ScoreValue;
            levelTwoFlag = false;
        }
        else if (this.transform.position.x >= 1601)
        {
            this.LevelLabel.text = "Level: 3";
            this.playerX = 1706f;
            this.playerY = 9f;
            this.gameController.ScoreValue = (this.levelThreeFlag) ? this.gameController.ScoreValue + 200 :
                this.gameController.ScoreValue;
            levelThreeFlag = false;
        }
        else
        {
            this.LevelLabel.text = "Level: 1";
            this.playerX = -298f;
            this.playerY = 30f;
        }
        
    }

    // animate player on hit
    IEnumerator makePalyerDisappear()
    {
        this.heroAnimator.SetInteger("AnimState", 3);
        //wait for a bit
        yield return new WaitForSeconds(1);

        this.heroAnimator.SetInteger("AnimState", 0);
        this.transform.position = new Vector2(this.playerX, this.playerY);
        this.HitLabel.gameObject.SetActive(false);
    }

}
