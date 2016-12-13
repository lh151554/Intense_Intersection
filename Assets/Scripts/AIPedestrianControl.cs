using UnityEngine;
using System.Collections;

public class AIPedestrianControl : MonoBehaviour
{

    private Rigidbody rb; //Rigibody to get the pedestrian
    public float speed; //Speed of the pedestrian
	private GameController gameController; //Reference to the GameController
	public int scoreValue;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Get the pedestrian rigidbody to apply movement on it
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

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Pedestrian")
		{
			Destroy(gameObject); //Destroy the car when they collide
			gameController.GameOver(); //End the game
		}
	}

    void Update()
    {
        rb.velocity = transform.forward * speed;

		if (transform.position.x < -10 || transform.position.x > 70 || transform.position.z < -10 || transform.position.z > 70) //Destroy car when it leaves the map area
		{
			gameController.AddScore(scoreValue); //Add the score of one car to the total score
			Destroy(gameObject);
		}

    }
}