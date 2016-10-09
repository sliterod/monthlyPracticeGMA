using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

    Transform respawnPlatform;

    // Use this for initialization
    void Awake() {
        respawnPlatform = this.transform
            .FindChild("platform");

        respawnPlatform.localScale = Vector2.zero;
    }
    
    public void RespawnCharacter() {
        Debug.Log("Respawning...");
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine() {

        ActivatePlayerColliders(false);
        ChangePlayerPosition();
        
        yield return new WaitForSeconds(1.0f);
        Debug.Log("...Returning character to battle...");

        RespawnAnimation();

        yield return new WaitForSeconds(3.0f);
        Debug.Log("...Activating, colliders...");

        ActivatePlayerColliders(true);
    }

    void ActivatePlayerColliders(bool state)
    {
        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
        BoxCollider2D collider = this.GetComponent<BoxCollider2D>();

        if (state)
        {
            collider.enabled = true;
            rigidbody.gravityScale = 1.0f;
            rigidbody.isKinematic = false;

            respawnPlatform.localScale = Vector2.zero;
        }
        else
        {
            collider.enabled = false;
            rigidbody.gravityScale = 0.0f;
            rigidbody.isKinematic = true;
        }
    }

    /// <summary>
    /// Changes player position to the top of the screen
    /// </summary>
    void ChangePlayerPosition() {
        this.transform.position =
            new Vector2(0.0f, 9.0f);
    }

    /// <summary>
    /// Moves the character to a fixed place on the stage
    /// </summary>
    void RespawnAnimation() {

        respawnPlatform.localScale = Vector2.one;

        iTweenEvent.GetEvent(gameObject,
                             "respawnme").Play();
    }
}
