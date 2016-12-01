using UnityEngine;
using System.Collections;

public class AIControl : MonoBehaviour
{

    private Rigidbody rb; //Rigibody to get the car
    public float speed; //Speed of the car that can be changed in Unity
    Vector3 stop = new Vector3(0.0f, 0.0f, 0.0f); //Vector 0 that stop the car
    bool pass = false; //Boolean to know if the car already passed the intersection or not
    bool clicked = false; //Boolean to know if the player already clicked on the car or not

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Get the car rigidbody to apply movement on it
    }

    void Update()
    {
        if (!pass || clicked) //Make the car move
        {
            rb.velocity = transform.forward * speed;
        }

        if (transform.position.z > 17 && transform.position.x > 27 && transform.position.x < 29 && !clicked) //Stop the car that go to the top
        {
            rb.velocity = stop;
            pass = true;
        }

        if (transform.position.z < 43 && transform.position.x < 24 && transform.position.x > 22 && !clicked) //Stop the car that go to the bottom
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
            }
        }

        else
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) //Get the click of the player (Mobile Version)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);

                if (hit.transform == rb.transform) //Get the position of the click
                {
                    rb.velocity = transform.forward * speed;
                    clicked = true;
                }
            }
        }

        if(transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
