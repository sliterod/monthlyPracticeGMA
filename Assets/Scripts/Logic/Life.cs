using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

    /********* PUBLIC VALUES ***********/
    public float lifePct;

    /********* SCRIPTS ***********/
    Break breakMechanic;

    /********* UI ***********/
    LifeBar lifeBarUI;

    // Use this for initialization
    void Start () {
        InitializeComponents();
        InitializeLifeUI();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L)) {
            ChangeLifeValue(0.2f);
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            this.SendMessage("BreakResetByInterruption");
        }
	}

    /// <summary>
    /// Changes life value by adding sustained damage to life value
    /// </summary>
    /// <param name="damageSustained">Amount of damage sustained</param>
    public void ChangeLifeValue(float damageSustained) {
        lifePct += damageSustained;

        if (lifePct < 1.0f)
        {
            Debug.Log("Life: " + lifePct);
            Debug.Log("Updating Life UI");
            lifeBarUI.IncreaseBarSize(lifePct);
        }
        else if (lifePct >= 1.0f)
        {
            Debug.Log("Starting break, refilling gauge");
            lifeBarUI.IncreaseBarSize(lifePct);
            breakMechanic.ActivateBreak(true);
        }
    }

    /// <summary>
    /// Initializes script components
    /// </summary>
    void InitializeComponents() {
        Debug.Log("Initializing break mechanic");
        breakMechanic = this.GetComponent<Break>();
    }

    /// <summary>
    /// Restores life percentage
    /// </summary>
    void RestoreLife() {
        lifePct = 0.0f;

        Debug.Log("Life restored to its full: " + (lifePct * 100) + "%");
    }

    /// <summary>
    /// Initializes life UI
    /// </summary>
    void InitializeLifeUI() {
        lifeBarUI = this.GetComponent<LifeBar>();
    }
}