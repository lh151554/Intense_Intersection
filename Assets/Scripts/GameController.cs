using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject car; //Reference to car
	public GameObject pedestrian; //Reference to pedestrian
    public float startWait; //Wait time before cars start to spawn
    public float waveWait; //Wait time between each car spawn

    public GUIText scoreText; //Reference to Score Text
    private int score; //Total score
    public GUIText restartText; //Reference to Restart Text
    public GUIText gameOverText; //Reference to Game Over Text
    private bool gameOver; //Boolean to know if game is over or not
    private bool restart; //Boolean to know if the player can restart the game or not

    public int carRight; //Number of cars waiting before stop mark when they go to the right
    public int carLeft; //Number of cars waiting before stop mark when they go to the left
    public int carTop; //Number of cars waiting before stop mark when they go to the top
    public int carBottom; //Number of cars waiting before stop mark when they go to the bottom

    public int carCount; //Count the number of cars to change the game after few time and make it harder

    void Start()
    {
        score = 0; //Set score to 0
        UpdateScore(); //Display the score
        gameOver = false; //Game is not over
        restart = false; //The player did not restart the game yet
        restartText.text = ""; //Don't display anything for restart
        gameOverText.text = ""; //Don't display anything for game over
        carTop = carRight = carLeft = carBottom = carCount = 0; //No cars have spawned yet
        StartCoroutine(SpawnWaves()); //Spawning car
    }

    void Update()
    {
        if(restart) //If the player car restart the game
        {
            if (Input.GetMouseButtonDown(0)) //If he clicks on the screen, the game restart
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves() //Function to spawn cars to a certain position
    {
        yield return new WaitForSeconds(startWait); //Wait a few moment befoer starting the spawn

        while (true) //Infinite loop
        {
            int[] randomValue = new int[] { 1, 2, 3, 4 }; //4 possibles values
			int[] randomValuePed = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int[] x = { 0, 0 }; //2 values table for spawning cars
			int xPed = randomValuePed[Random.Range(0, randomValuePed.Length)]; //Take a random value for the pedestrian spawn point.
            x[0] = randomValue[Random.Range(0, randomValue.Length)]; //Take a random value for the spawning of the car (position and rotation)
            if(x[0] < 3)
            {
                x[1] = x[0] + 2; //The second car goes to the other lane
            }
            else
            {
                x[1] = x[0] - 2; //The second car goes to the other lane
            }

            int[] randomValue2 = new int[] { 0, 1, 2 }; //3 possibles values
            int random = randomValue2[Random.Range(0, randomValue2.Length)]; //Take a random value of this 3 to know if 2 cars will spawn at teh same time
            int y = randomValue2[Random.Range(0, randomValue2.Length)]; //Take a random value of this 3  for waiting time before spawnign new car
            Vector3 spawnPosition = new Vector3(0, 0, 0); //Initial position initialization (0,0,0)
			Vector3 spawnPositionPed = new Vector3(0,0,0);
            Quaternion spawnRotation = Quaternion.identity; //Initial rotation initialization (0,0,0,0)
			Quaternion spawnRotationPed = Quaternion.identity;

            if(random == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    switch (x[i])
                    {
                        case 1: //Car goes to the bottom
                            spawnPosition = new Vector3(23, 1, 65);
                            spawnRotation = Quaternion.AngleAxis(180, Vector3.up);
                            carBottom++; //Add one to the counter
                            break;

                        case 2: //Car goes to the top
                            spawnPosition = new Vector3(28, 1, -5);
                            spawnRotation = Quaternion.AngleAxis(0, Vector3.up);
                            carTop++; //Add one to the counter
                            break;

                        case 3: //Car goes to the right
                            spawnPosition = new Vector3(-5, 1, 27.5f);
                            spawnRotation = Quaternion.AngleAxis(90, Vector3.up);
                            carRight++; //Add one to the counter
                            break;

                        case 4: //Car goes to the left
                            spawnPosition = new Vector3(56, 1, 32.5f);
                            spawnRotation = Quaternion.AngleAxis(270, Vector3.up);
                            carLeft++; //Add one to the counter
                            break;

                    }

                    Instantiate(car, spawnPosition, spawnRotation); //Instantiante the car with it specific position and rotation
                    carCount++; //Add one to the car counter
                }
            }
            

            else
            {
                switch (x[0])
                {
                    case 1: //Car goes to the bottom
                        spawnPosition = new Vector3(23, 1, 65);
                        spawnRotation = Quaternion.AngleAxis(180, Vector3.up);
                        carBottom++; //Add one to the counter
                        break;

                    case 2: //Car goes to the top
                        spawnPosition = new Vector3(28, 1, -5);
                        spawnRotation = Quaternion.AngleAxis(0, Vector3.up);
                        carTop++; //Add one to the counter
                        break;

                    case 3: //Car goes to the right
                        spawnPosition = new Vector3(-5, 1, 27.5f);
                        spawnRotation = Quaternion.AngleAxis(90, Vector3.up);
                        carRight++; //Add one to the counter
                        break;

                    case 4: //Car goes to the left
                        spawnPosition = new Vector3(56, 1, 32.5f);
                        spawnRotation = Quaternion.AngleAxis(270, Vector3.up);
                        carLeft++; //Add one to the counter
                        break;

                }

                Instantiate(car, spawnPosition, spawnRotation); //Instantiante the car with it specific position and rotation
                carCount++; //Add one to the car counter
            }
                
			switch (xPed)
			{
			case 1: //Pedestrian bottom right, going upwards;
				spawnPositionPed = new Vector3 (33.5f, 3, -6);
				spawnRotationPed = Quaternion.AngleAxis (0, Vector3.up);
				break;

			case 2: //Pedestrian bottom left, going upwards;
				spawnPositionPed = new Vector3 (19.25f, 3, -6);
				spawnRotationPed = Quaternion.AngleAxis (0, Vector3.up);
				break;

			case 3: //Pedestrian bottom, going right;
				spawnPositionPed = new Vector3 (-6, 3, 22);
				spawnRotationPed = Quaternion.AngleAxis (90, Vector3.up);
				break;

			case 4: //Pedestrian top, going right;
				spawnPositionPed = new Vector3 (-6, 3, 36.25f);
				spawnRotationPed = Quaternion.AngleAxis (90, Vector3.up);
				break;

			case 5: //Pedestrian left, going bottom;
				spawnPositionPed = new Vector3 (17.6f, 3, 66);
				spawnRotationPed = Quaternion.AngleAxis (180, Vector3.up);
				break;

			case 6: //Pedestrian right, going bottom;
				spawnPositionPed = new Vector3 (31.5f, 3, 66);
				spawnRotationPed = Quaternion.AngleAxis (180, Vector3.up);
				break;

			case 7: //Pedestrian top, going right;
				spawnPositionPed = new Vector3 (56, 3, 37.9f);
				spawnRotationPed = Quaternion.AngleAxis (270, Vector3.up);
				break;

			case 8: //Pedestrian bottom, going right;
				spawnPositionPed = new Vector3 (56, 3, 24);
				spawnRotationPed = Quaternion.AngleAxis (270, Vector3.up);
				break;
			}

			Instantiate(pedestrian, spawnPositionPed, spawnRotationPed);

        yield return new WaitForSeconds(waveWait + y); //Wait between each spawn

            if (gameOver) //If the game is over
            {
                restartText.text = "Touch the screen to restart"; //Display restart text
                restart = true;
                break; //End the loop
            }
        }

    }

    public void AddScore(int newScoreValue) //Add score to the total score
    {
        score += newScoreValue;
        UpdateScore(); //Display the new score
    }

    void UpdateScore() //Display the score
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver() //Display Game Over
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}