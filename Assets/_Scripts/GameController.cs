/*
 * Sourcefile name : GameController
 * Author’s name: Anjith Babu
 * Last	Modifiedby: Anjith Babu
 * Date	lastModified : Feb 29, 2016	
 * Program	description: The StartCoroutine is obtained after referencing following link
 * http://answers.unity3d.com/questions/569864/blink-objec-on-collision.html. 
 * Game controller is used to control game objects in the main scene like score, lives etc.
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
   
    // private instance variables
    private static int scoreValue;
    private int livesValue;
    private static int checkLoop = 0;

    // public instance variables
    public Text LivesLabel;
    public Text ScoreLabel;
    public Text HighScoreLabel;
    public Button RestartButton;
    public Transform transform;

    // public access methods
    public int ScoreValue
    {
        get
        {
            return scoreValue;
        }

        set
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                scoreValue = value;
                this.ScoreLabel.text = "Score: " + scoreValue;
            }
        }
    }

    public int LivesValue
    {
        get
        {
            return this.livesValue;
        }

        set
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                this.livesValue = value;
                if (this.livesValue <= 0)
                {
                    this._endGame();
                }
                else
                {
                    this.LivesLabel.text = "Lives: " + this.livesValue;
                }
            }
        }
    }


    // Use this for initialization
    void Start()
    {
        this._initialize();
    }

    // Update is called once per frame
    void Update()
    {
        checkIfEndScene();
    }

    //Initial Method
    private void _initialize()
    {
        this.ScoreValue = 0;
        this.LivesValue = 5;
    }

    // Things to do when game ends

    private void _endGame()
    {
        SceneManager.LoadScene("end");
    }

    // check to see if game has reached end scene and do stuff related to
    // end scene
    public void checkIfEndScene()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 2 && checkLoop < 1)
        {
            this.HighScoreLabel.text = "High Score: " + scoreValue;
            checkLoop++;
        }
    }
}
