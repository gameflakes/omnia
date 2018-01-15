using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimentação")]
    public float moveSpeed;
    private Rigidbody2D playerRigidBody;
    public Vector2 lastMove;

    [Header("Instância")]
    private static bool playerExists;
    public string startPoint;

	// Use this for initialization
	void Start ()
    {		   
        playerRigidBody = GetComponent<Rigidbody2D>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);    
        }
        else
        {
            Destroy(gameObject);
        }

	}

    // Update is called once per frame
    void Update ()
    {

    }

    private void FixedUpdate ()
    {
        Movement();
    }

    void Movement ()
    {
        float[] input = GetMovementInput();

        // Movimentação horizontal
        if (input[0] > 0.5f || input[0] < -0.5f)
        {
            playerRigidBody.velocity = new Vector2(input[0] * moveSpeed, playerRigidBody.velocity.y);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(0f, playerRigidBody.velocity.y);
        }

        // Movimentação vertical
        if (input[1] > 0.5f || input[1] < -0.5f)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, input[1] * moveSpeed);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0f);
        }

        // Movimentação diagonal
        if (input[1] != 0 && input[0] != 0)
        {
            playerRigidBody.velocity = new Vector2(input[0] * moveSpeed / 1.357f, input[1] * moveSpeed / 1.357f);
        }

        // Verifica existência de movimentação
        if ((input[0] > 0.5f || input[0] < -0.5f) ||
            (input[1] > 0.5f || input[1] < -0.5f))
        {
            lastMove = new Vector2(input[0], input[1]);
        }
    }

    float [] GetMovementInput ()
    {
        float hor = Input.GetAxisRaw ("Horizontal");
        float ver = Input.GetAxisRaw ("Vertical");

        return new float [] { hor, ver };
    }

}
