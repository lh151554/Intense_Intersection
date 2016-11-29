using UnityEngine;
using System.Collections;

public class AIControl : MonoBehaviour
{

    private Rigidbody rb;
    public float speed;
    int i;
    Vector3 stop = new Vector3(0.0f, 0.0f, 0.0f);
    bool pass = false;
    bool clicked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = transform.forward * speed;
    }

    void Update()
    {
        if(!pass || clicked)
        {
            rb.velocity = transform.forward * speed;
        }

        if (transform.position.z > 20 && !clicked)
        {
            rb.velocity = stop;
            pass = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            if(hit.transform == rb.transform)
            {
                rb.velocity = transform.forward * speed;
                clicked = true;
            }
        }
        else
        {
            for (i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    rb.velocity = transform.forward * speed;
                    clicked = true;
                }
            }
        }
    }
}
