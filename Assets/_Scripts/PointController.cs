/*
 * Sourcefile name : PointController
 * Author’s name: Anjith Babu
 * Last	Modifiedby: Anjith Babu
 * Date	lastModified : Feb 29, 2016	
 * Program	description: Point controller is used to check if the collision occured
 * with player.
 */
using UnityEngine;
using System.Collections;

public class PointController : MonoBehaviour {

    // public variables
    public GameController gameContoller;

    //private variables
    private AudioSource[] audioSources;
    private AudioSource pointSound;

	// Use this for initialization
	void Start () {
        this.audioSources = gameObject.GetComponents<AudioSource>();
        this.pointSound = this.audioSources[0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // check if player took point
    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            this.pointSound.Play();
            this.gameContoller.ScoreValue += 100;
            this.transform.position = new Vector2(-300, 678);
            //GameObject.Destroy(gameObject);
        }
    }
}
