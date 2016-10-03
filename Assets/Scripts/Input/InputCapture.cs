using UnityEngine;
using System.Collections;

public class InputCapture : MonoBehaviour {

    Movement characterMovement;
    bool idleToRunSet;
    bool runToIdleSet;

    // Use this for initialization
    void Start() {
        characterMovement = this.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update() {
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
            SetIdleToRunTrigger();
            characterMovement.Move(Directions.left);
        }

        if (Input.GetKey(KeyCode.D)) {
            Debug.Log("Going to the right");
            SetIdleToRunTrigger();
            characterMovement.Move(Directions.right);
        }

        if (Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.D)
            )
        {
            SetRunToIdleTrigger();
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

    void SetIdleToRunTrigger()
    {
        if (!idleToRunSet)
        {
            this.GetComponent<Animator>().SetTrigger("idleToRun");
            idleToRunSet = true;
            runToIdleSet = false;
        }
    }


    void SetRunToIdleTrigger() {
        if (!runToIdleSet) {
            this.GetComponent<Animator>().SetTrigger("runToIdle");
            idleToRunSet = false;
            runToIdleSet = true;
        }
    }

}
