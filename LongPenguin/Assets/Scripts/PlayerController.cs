using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float MovementForce = 20f;
    public int PlayerID;
    public Text text;
    public Rigidbody2D rb;

    float horizontalMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        //horizontalMove = Input.GetAxis("Horizontal");
        horizontalMove = 0f;
        if (PlayerID == 0)
        {
            if (Input.GetKey(KeyCode.J))
            {
                horizontalMove = -1f;
            }
            if (Input.GetKey(KeyCode.L))
            {
                horizontalMove = 1f;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                horizontalMove = -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                horizontalMove = 1f;
            }
        }    
        //transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * MovementSpeed;
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(horizontalMove*MovementForce,0));
        text.text = rb.velocity.magnitude.ToString();
    }
}
