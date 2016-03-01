/*
 * Sourcefile name : StartController
 * Author’s name: Anjith Babu
 * Last	Modifiedby: Anjith Babu
 * Date	lastModified : Feb 29, 2016	
 * Program	description: Start controller is used to load main scene when game starts
 */


using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour {

    public AudioSource _gameSound;

	// Use this for initialization
	void Start () {
        _gameSound.Play();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}


    public void StartButtonClick()
    {
        SceneManager.LoadScene("main");
    }
}
