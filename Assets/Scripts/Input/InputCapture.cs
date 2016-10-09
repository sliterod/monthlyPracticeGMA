using UnityEngine;
using System.Collections;

public class InputCapture : MonoBehaviour {

    Movement characterMovement;
    Attack characterAttack;
    Shield shield;
    bool idleToRunSet;
    bool runToIdleSet;

    // Use this for initialization
    void Start() {
        characterMovement = this.GetComponent<Movement>();
        characterAttack = this.GetComponent<Attack>();
        shield = this.GetComponent<Shield>();
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
        
        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("Player is performing an evade");
            characterMovement.JumpEvade();
        }
    }

    /// <summary>
    /// Captures inputs for player actions
    /// </summary>
    void AttackCapture()
    {
        if (Input.GetMouseButtonDown(0) && 
            !characterMovement.isPlayerInTheAir)
        {
            Debug.Log("Player is attacking");
            characterAttack.AttackAction();
        }

        if (Input.GetMouseButtonDown(0) && 
            !characterMovement.isPlayerEvading &&
            characterMovement.isPlayerInTheAir)
        {
            Debug.Log("Player is attacking in the air");
            characterAttack.AttackJump(); ;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            shield.ShowBarrier(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            shield.ShowBarrier(false);
        }

    }

    void SetIdleToRunTrigger()
    {
        if (!idleToRunSet)
        {
            SetTrigger("idleToRun");
            idleToRunSet = true;
            runToIdleSet = false;
        }
    }


    void SetRunToIdleTrigger() {
        if (!runToIdleSet) {
            SetTrigger("runToIdle");
            idleToRunSet = false;
            runToIdleSet = true;
        }
    }

    public void ResetIdle() {
        idleToRunSet = false;
        runToIdleSet = false;
    }

    public void SetTrigger(string trigger) {
        this.GetComponent<Animator>().SetTrigger(trigger);
    }

    public void SetBool(string boolean, bool state) {
        this.GetComponent<Animator>().SetBool(boolean, state);
    }
}
