using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    public Payload[] payloads; // array to store the payloads that need to be carried
    public Rocket rocket; // reference to the rocket game object
    public TextMeshProUGUI gameStateUI; // reference to the UI element to show the game state
    public GameObject gameOverUI; // reference to the game over UI element
    public TextMeshProUGUI gameOverText; // reference to the UI element to show the game over message
    public bool gameOver = false; // flag to check if the game is over or not

    void Start()
    {
        gameOverUI.SetActive(false); // hide the game over UI element at the start
    }

    // Update is called once per frame
    void Update()
    {
        int dropedPayloads = getNumberOfPayloadsDropped(); // get the number of payloads dropped

        if (!gameOver)
        {
            // update the game state UI element with fuel left and drops left
            gameStateUI.text = string.Format(
                "Energy      : {0:0.##}/100\nDrops left  : {1}",
                rocket.fuelLeft,
                3 - dropedPayloads
            );
        }
        else
        {
            // if the game is over, listen for the enter key to restart the scene
            if (Input.GetKey(KeyCode.Return))
            {
                // http://answers.unity.com/answers/1113752/view.html
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
                );
            }
        }

        if (dropedPayloads == 3)
        {
            // if all payloads are dropped, end the game with success message
            endTheGame("You successfully delivered all packages");
        }

        if (rocket.fuelLeft <= 0)
        {
            // if fuel runs out, end the game with failure message
            endTheGame("Fuel ran out, Try again!");
        }
    }

    // Get number of boxes present the the drop off area
    int getNumberOfPayloadsDropped()
    {
        int nPayloads = 0;

        // iterate through the payloads array to count the number of payloads dropped
        foreach (Payload payload in payloads)
        {
            if (payload.inDropZone)
            {
                nPayloads += 1;
            }
        }

        return nPayloads;
    }

    // This play the end game title card
    public void endTheGame(string message, bool freezePosition = true)
    {
        gameOver = true;

        gameOverUI.SetActive(true);
        if (freezePosition)
        {
            rocket.rb.constraints = RigidbodyConstraints.FreezePosition; // freeze the rocket position if freezePosition is true
        }

        rocket.enabled = false; // disable the rocket
        gameOverText.text = message; // update the game over message
        gameStateUI.text = ""; // clear the game state UI element
    }
}

