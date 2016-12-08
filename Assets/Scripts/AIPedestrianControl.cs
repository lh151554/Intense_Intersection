using UnityEngine;
using System.Collections;

public class AIPedestrianControl : MonoBehaviour
{

    private Rigidbody rb; //Rigibody to get the pedestrian
    public float speed; //Speed of the pedestrian

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Get the pedestrian rigidbody to apply movement on it
    }

    void Update()
    {
        rb.velocity = transform.forward * speed;
    }
}