using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject car; //Reference to car
    public float startWait; //Wait time before cars start to spawn
    public float waveWait; //Wait time between each car spawn

    void Start()
    {
        StartCoroutine(SpawnWaves()); //Spawning car
    }

    IEnumerator SpawnWaves() //Function to spawn cars to a certain position
    {
        yield return new WaitForSeconds(startWait); //Wait a few moment befoer starting the spawn

        while (true) //Infinite loop
        {
            int[] randomValue = new int[] { 1, 2, 3, 4 }; //4 possibles values for spawning cars
            int x = randomValue[Random.Range(0, randomValue.Length)]; //Take a random value of this 4
            Vector3 spawnPosition = new Vector3(0, 0, 0); //Initial position initialization (0,0,0)
            Quaternion spawnRotation = Quaternion.identity; //Initial rotation initialization (0,0,0,0)

            switch (x)
            {
                case 1: //Car goes to the top
                    spawnPosition = new Vector3(23, 1, 65);
                    spawnRotation = Quaternion.AngleAxis(180, Vector3.up);
                    break;

                case 2: //Car goes to the bottom
                    spawnPosition = new Vector3(28, 1, -5);
                    break;

                case 3: //Car goes to the right
                    spawnPosition = new Vector3(-5, 1, 27.5f);
                    spawnRotation = Quaternion.AngleAxis(90, Vector3.up);
                    break;

                case 4: //Car goes to the left
                    spawnPosition = new Vector3(56, 1, 32.5f);
                    spawnRotation = Quaternion.AngleAxis(270, Vector3.up);
                    break;

            }

            Instantiate(car, spawnPosition, spawnRotation); //Instantiante the car with it specific position and rotation
            yield return new WaitForSeconds(waveWait); //Wait between each spawn
        }

    }
}
