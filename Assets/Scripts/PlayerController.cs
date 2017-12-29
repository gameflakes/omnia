using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Variável de movimentação")]
    public float moveSpeed;
   
	[Header("Variável de animação")]
	private Animator anim;
    private bool playerMoving;
    public Vector2 lastMove;

    // Variável de colisão
    private Rigidbody2D playerRigidBody;
    
    [Header("Variável de Instância")]
    private static bool playerExists;
    public string startPoint;

	// Use this for initialization
	void Start ()
    {		   
		anim = GetComponent <Animator> ();
        playerRigidBody = GetComponent <Rigidbody2D> ();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);    
        }
        else
        {
            Destroy (gameObject);
        }

	}

    // FixedUpdate é chamado a cada atualização da física da cena
    private void FixedUpdate ()
    {
        Movement ();
    }

    // Update is called once per frame
    void Update ()
    {

	}

	void Movement ()
    {
		playerMoving = false;

        float [] input = GetInput ();

		// Movimentação horizontal
		if (input[0] > 0.5f || input[0] < -0.5f)
		{
            playerRigidBody.velocity = new Vector2 (input[0] * moveSpeed, playerRigidBody.velocity.y);

            playerMoving = true;
			lastMove = new Vector2 (input[0], 0f);
		}

		// Movimentação vertical
		if (input[1] > 0.5f || input[1] < -0.5f)
		{
            playerRigidBody.velocity = new Vector2 (playerRigidBody.velocity.x, input[1] * moveSpeed);

            playerMoving = true;
			lastMove = new Vector2 (0f, input[1]);
		}

        // Movimentação diagonal
        if (input[1] != 0 && input[0] != 0)
        {
            playerRigidBody.velocity = new Vector2 (input[0] * moveSpeed / 1.357f, input[1] * moveSpeed / 1.357f);

            lastMove = new Vector2 (input[0], input[1]);
        }

		// Reset velocidade se não aperta botão
        if (input[0] < 0.5f && input[0] > -0.5f)
        {
            playerRigidBody.velocity = new Vector2 (0f, playerRigidBody.velocity.y); 
        }
        if (input[1] < 0.5f && input[1] > -0.5f)
        {
            playerRigidBody.velocity = new Vector2 (playerRigidBody.velocity.x, 0f);
        }
			

        anim.SetFloat ("MoveX", input[0]);
		anim.SetFloat ("MoveY", input[1]);

		anim.SetBool ("PlayerMoving", playerMoving);

		anim.SetFloat ("LastMoveX", lastMove.x);
		anim.SetFloat ("LastMoveY", lastMove.y);
	}

    float [] GetInput ()
    {
        float hor = Input.GetAxisRaw ("Horizontal");
        float ver = Input.GetAxisRaw ("Vertical");

        return new float [] { hor, ver };
    }

}
