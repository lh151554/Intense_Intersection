using UnityEngine;
using System.Collections;

public class AIControl : MonoBehaviour
{

    private Rigidbody rb; //Rigibody to get the car
    public float speed; //Speed of the car
    Vector3 stop = new Vector3(0.0f, 0.0f, 0.0f); //Vector 0 that stop the car
    bool pass = false; //Boolean to know if the car already passed the intersection or not
    bool clicked = false; //Boolean to know if the player already clicked on the car or not
    Vector3 testPosition = new Vector3(0.0f, 0.0f, 0.0f); //Vector for testing if a car is present in front of another one

    public int scoreValue; //Score for each car that pass the road safely
    private GameController gameController; //Reference to the GameController

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Get the car rigidbody to apply movement on it
        GameObject gameControllerObject = GameObject.FindWithTag("GameController"); //Get the reference to the GameController

        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent <GameController>();
        }

        if (gameControllerObject == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(col.gameObject); //Destroy the car when they collide
        gameController.GameOver(); //End the game
    }

    void Update()
    {
        if (!pass || clicked) //Make the car move if it is not already passed or if it is clicked
        {
            rb.velocity = transform.forward * speed;
        }

        if(!clicked) //Before the player clicked it, we test if there is another car in front of the one which is moving and stopping it just behind it if there is one
        {
            testPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 3); //Test position for car which go top
            if (Physics.CheckSphere(testPosition, 0.1f))
            {
                rb.velocity = stop;
            }

            testPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z - 3); //Test position for car which go bottom
            if (Physics.CheckSphere(testPosition, 0.1f))
            {
                rb.velocity = stop;
            }

            testPosition = new Vector3(transform.position.x + 3, transform.position.y + 1, transform.position.z); //Test position for car which go right
            if (Physics.CheckSphere(testPosition, 0.1f))
            {
                rb.velocity = stop;
            }

            testPosition = new Vector3(transform.position.x - 3, transform.position.y + 1, transform.position.z); //Test position for car which go left
            if (Physics.CheckSphere(testPosition, 0.1f))
            {
                rb.velocity = stop;
            }
        }

        if (transform.position.z > 16.5f && transform.position.x > 27 && transform.position.x < 29 && !clicked) //Stop the car that go to the top
        {
            rb.velocity = stop;
            pass = true;
        }

        if (transform.position.z < 43.5 && transform.position.x < 24 && transform.position.x > 22 && !clicked) //Stop the car that go to the bottom
        {
            rb.velocity = stop;
            pass = true;
        }

        if (transform.position.x > 12 && transform.position.z > 27 && transform.position.z < 29 && !clicked) //Stop the car that go to the right
        {
            rb.velocity = stop;
            pass = true;
        }

        if (transform.position.x < 38 && transform.position.z < 33 && transform.position.z > 31 && !clicked) //Stop the car that go to the left
        {
            rb.velocity = stop;
            pass = true;
        }

        if (Input.GetMouseButtonDown(0)) //Get the click of the player (Computer Version)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            if (hit.transform == rb.transform) //Get the position of the click
            {
                rb.velocity = transform.forward * speed;
                clicked = true;
                if (transform.position.x > 27 && transform.position.x < 29) //Remove one to the car counter
                {
                    gameController.carTop--;
                }

                if (transform.position.x < 24 && transform.position.x > 22) //Remove one to the car counter
                {
                    gameController.carBottom--;
                }

                if (transform.position.z > 27 && transform.position.z < 29) //Remove one to the car counter
                {
                    gameController.carRight--;
                }

                if (transform.position.z < 33 && transform.position.z > 31) //Remove one to the car counter
                {
                    gameController.carLeft--;
                }
            }
        }

        if (transform.position.y < 0) //Destroy car when it leaves the map area
        {
            gameController.AddScore(scoreValue); //Add the score of one car to the total score
            Destroy(gameObject);
        }

        if(gameController.carTop > 4 || gameController.carBottom > 4 || gameController.carRight > 3 || gameController.carLeft > 3) //If there is too much car waiting before the stop mark, the player will lose the game
        {
            gameController.GameOver();
        }
    }
}