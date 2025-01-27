using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    private float jumpForce;
    private float moveSpeed;
    private float moveH;
    private float moveV;
    private bool isJumping;

    public Transform spawnPoint;


    // Start is called before the first frame update
    void Start()
    {

        myRigidBody = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 1.5f;
        jumpForce = 50f;
        isJumping = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            myRigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");

        // Separate the vertical input for jumping
        //if (moveV > 0.1f && !isJumping)
        //{
        //    //isJumping = true;
        //    myRigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        //}
    }

    void FixedUpdate()
    {
        if (moveH > 0.1f || moveH < -0.1f)
        {
            myRigidBody.AddForce(new Vector2(moveH * moveSpeed, 0f), ForceMode2D.Impulse);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTER " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Platform"))
        {
            isJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("EXIT " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }

        if (collision.gameObject.CompareTag("Respawn"))
        {
            transform.position = spawnPoint.position;
        }
    }
}
