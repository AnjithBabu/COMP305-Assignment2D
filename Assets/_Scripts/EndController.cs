/*
 * Sourcefile name : EndController
 * Author’s name: Anjith Babu
 * Last	Modifiedby: Anjith Babu
 * Date	lastModified : Feb 29, 2016	
 * Program	description: End controller is used to load main scene when game ends
 */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour {

    public AudioSource _gameSound;

	// Use this for initialization
	void Start () {
        _gameSound.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RestartButtonClick()
    {
        SceneManager.LoadScene("main");
    }
}
