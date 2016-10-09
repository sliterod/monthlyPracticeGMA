using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    int attackCounter = 0;

    public void AttackAction() {
        Debug.Log("Attacking now");

        attackCounter += 1;
        Debug.Log("AttackCounter: " + attackCounter);

        if (attackCounter == 1) {
            this.GetComponent<InputCapture>().SetTrigger("attack");
        }
        else if (attackCounter == 2)
        {
            this.GetComponent<InputCapture>().SetTrigger("attack_mid");
        }
        else if (attackCounter == 3)
        {
            this.GetComponent<InputCapture>().SetTrigger("attack_final");
        }
        else if (attackCounter > 3)
        {
            attackCounter = 0;
            AttackAction();
        }
    }

    public void AttackJump() {
        this.GetComponent<InputCapture>().SetBool("attack_air",true);
    }

    public void ResetAttackCounter() {
        Debug.Log("Attack counter reset...");
        attackCounter = 0;
    }
}
