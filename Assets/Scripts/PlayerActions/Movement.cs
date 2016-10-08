using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    //Classes
    Rigidbody2D rigidbody;

    //Variables
    public float movementSpeed = 0.8f;
    public float jumpSpeed = 0.8f;

    int jumpCounter = 0;
    float yMax = 2.0f;
    float yMaxJumping = 0.0f;

    //Bool
    bool isPlayerJumping;
    bool isJumpCancelled;
    bool isPlayerEvading;

    void Start() {
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (isPlayerJumping)
            JumpAction();
    }

    public void Move(Directions direction) {

        float addedValueToPos = 0.0f;
        Vector2 vectorPosition;

        switch (direction) {
            case Directions.left:
                addedValueToPos -= 0.1f;
                this.GetComponent<SpriteRenderer>().flipX = false;
                break;

            case Directions.right:
                addedValueToPos += 0.1f;
                this.GetComponent<SpriteRenderer>().flipX = true;
                break;
        }

        vectorPosition = this.transform.position;

        this.transform.position = new Vector2(vectorPosition.x + addedValueToPos * movementSpeed,
                                               vectorPosition.y);
    }

    public void Jump() {
        rigidbody.gravityScale = 0.0f;
        isPlayerJumping = true;

        yMaxJumping = this.transform.position.y + yMax;
        jumpCounter += 1;

        if (jumpCounter == 1) {
            this.GetComponent<InputCapture>().SetTrigger("jump");
        }
        else if (jumpCounter == 2)
        {
            this.GetComponent<InputCapture>().SetTrigger("jumpToMidAir");
        }
    }

    void JumpAction() {

        Vector2 vectorPosition;
        vectorPosition = this.transform.position;

        if ((vectorPosition.y < yMaxJumping) &&
            jumpCounter <= 2)
        {
            this.transform.position =
                new Vector2(vectorPosition.x, 
                            vectorPosition.y + 0.2f * jumpSpeed);
        }
        else
        {
            Debug.Log("Jumping is finished");
            rigidbody.gravityScale = 1.0f;
            isPlayerJumping = false;
            this.GetComponent<InputCapture>().SetTrigger("fall");
            this.GetComponent<InputCapture>().ResetIdle();
        }
    }

    public void JumpCancel() {
        if (!isJumpCancelled) {
            rigidbody.gravityScale = 5.0f;
            isJumpCancelled = true;

            this.GetComponent<InputCapture>().SetTrigger("fall");
            this.GetComponent<InputCapture>().ResetIdle();
        }
    }

    public void JumpEvade()
    {
        if (!isJumpCancelled && 
            isPlayerJumping &&
            !isPlayerEvading)
        {
            isPlayerEvading = true;
            //this.GetComponent<InputCapture>().SetTrigger("jumpEvade");
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Stage") {
            Debug.Log("Returning to floor");
            jumpCounter = 0;
            isJumpCancelled = false;
            isPlayerEvading = false;
            rigidbody.gravityScale = 1.0f;

            this.GetComponent<InputCapture>().SetBool("fallToLand", true);
            this.GetComponent<InputCapture>().ResetIdle();
        }
    }
}
