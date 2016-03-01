/*
 * Sourcefile name : EnemyController
 * Author’s name: Anjith Babu
 * Last	Modifiedby: Anjith Babu
 * Date	lastModified : Feb 29, 2016	
 * Program	description: AI for the enemy. 
 */

using UnityEngine;
using System.Collections;

//public class for velocity range
public class EnemyVelocityRange
{
    //variables
    public float min;
    public float max;

    //Constructor
    public EnemyVelocityRange(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}

public class EnemyController : MonoBehaviour {

    //public instance variables
    public EnemyVelocityRange enemyVelocityRange;
    public float moveForce;

    //private variables
    private float moveX;
    private float forceX;
    private bool moveRightSide;
    private Rigidbody2D enemyRigidBd;


	// Use this for initialization
	void Start () {

        this.enemyVelocityRange = new EnemyVelocityRange(200f, 10000f);
        this.enemyRigidBd = gameObject.GetComponent<Rigidbody2D>();
        this.moveX = 0f;
        this.forceX = 0f;
        this.moveForce = 200f;
	
	}
	
	// Update is called once per frame
	void Update () {
        float absVelX = Mathf.Abs(this.enemyRigidBd.velocity.x);

        if (this.transform.position.x > 440f)
        {
            
            if (absVelX < this.enemyVelocityRange.max)
            {
                forceX = -this.moveForce;
            }
            this.moveRightSide = true;
        }
        else if (this.transform.position.x < 430f)
        {
            if (absVelX < this.enemyVelocityRange.max)
            {
                forceX = this.moveForce;
            }
            this.moveRightSide = false;
        }
        flipEnemyDirection();

        //apply force to the Enemy
        this.enemyRigidBd.AddForce(new Vector2(forceX, 0f));
	
	}

    //flip enemy when reaching the end of paltform
    private void flipEnemyDirection()
    {
        this.transform.localScale = (this.moveRightSide) ? new Vector2(1, 1)
                                                         : new Vector2(-1, 1);
    }
}
