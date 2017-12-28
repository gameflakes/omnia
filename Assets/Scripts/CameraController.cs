using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject followTarget;
	private Vector3 targetPos;
	public float moveSpeed;

    private static bool cameraExists;

    // Use this for initialization
    void Start () {

        if (!cameraExists)
        {

            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);

        }
        else
        {

            Destroy(gameObject);

        }

    }
	
	// Update is called once per frame
	void Update () {

		moveToTarget ();

	}

	/* Usa função Lerp, que faz uma interpolação linear entre dois vetores, ou seja,
	 * faz com que o vetor vá, com velocidade 'moveSpeed', na direção e sentido do 'targetPos'
	 */
	void moveToTarget () {
		
		targetPos = new Vector3 (followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp (transform.position, targetPos, moveSpeed * Time.deltaTime);

	}
}
