using UnityEngine;
using System.Collections;

public class InputCapture : MonoBehaviour {

    Movement characterMovement;

	// Use this for initialization
	void Start () {
        characterMovement = this.GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
        MovementCapture();
        JumpCapture();
        AttackCapture();
	}

    /// <summary>
    /// Captures inputs for player movement
    /// </summary>
    void MovementCapture() {
        //Movement left - right
        if (Input.GetKey(KeyCode.A)) {
            Debug.Log("Going to the left");
            characterMovement.Move(Directions.left);
        }

        if (Input.GetKey(KeyCode.D)) {
            Debug.Log("Going to the right");
            characterMovement.Move(Directions.right);
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("Cancel jump");
            characterMovement.JumpCancel();
        }
    }

    /// <summary>
    /// Captures inputs for player jump
    /// </summary>
    void JumpCapture() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player is jumping");
            characterMovement.Jump();
        }
    }

    /// <summary>
    /// Captures inputs for player actions
    /// </summary>
    void AttackCapture()
    {
        if (Input.GetKeyDown(KeyCode.P) ||
            Input.GetMouseButtonDown(0))
        {
            Debug.Log("Player is attacking");
        }
    }
}
