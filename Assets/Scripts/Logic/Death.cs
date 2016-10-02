using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boundary")
        {
            Debug.Log("Boundary hit. Character is dead, Respawning");

            this.transform
                .FindChild("PlayerElements")
                .GetComponent<Break>()
                .BreakResetByInterruption();

            this.transform
                .FindChild("PlayerElements")
                .GetComponent<LifeBar>()
                .ResetLifeBar();

            this.transform
                .FindChild("PlayerElements")
                .GetComponent<BreakTimer>()
                .ChangeBreakTimerValue(0.0f);

            this.GetComponent<Respawn>().RespawnCharacter();
        }
    }
}
