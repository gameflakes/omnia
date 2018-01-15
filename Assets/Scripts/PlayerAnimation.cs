using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Variável de animação")]
    private Animator anim;
    private bool playerMoving;
    public Vector2 lastMove;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void FixedUpdate ()
    {
        SetAnimationVariables();
    }

    void SetAnimationVariables ()
    {
        float[] input = GetInput();

        // Reinicia estado
        playerMoving = false;

        // Verifica existência de movimentação
        if ((input[0] > 0.5f || input[0] < -0.5f) ||
            (input[1] > 0.5f || input[1] < -0.5f))
        {
            playerMoving = true;
            lastMove = new Vector2(input[0], input[1]);
        }

        // Atualização das variáveis de animação
        anim.SetFloat("MoveX", input[0]);
        anim.SetFloat("MoveY", input[1]);

        anim.SetBool("PlayerMoving", playerMoving);

        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }

    float[] GetInput()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        return new float[] { hor, ver };
    }
}
