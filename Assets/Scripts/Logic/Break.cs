using UnityEngine;
using System.Collections;

public class Break : MonoBehaviour {

    //Timers
    float defaultBreakTime;     //Default break time.
    float currentBreakTime;     //Break time variable. Will be increased while in game
    float breakTime;            //Break time variable. Used by the countdown timer

    //Increased values
    float breakAddedTime;       //Time to be added after break reset

    //Control
    bool isBreakActive;         //Break activation. Enables time counter for break
    Player currentPlayer;       //The number of this player

    //UI
    BreakTimer breakTimerUI;    //UI for break timer

    public float BreakTime
    {
        get
        {
            return breakTime;
        }

        set
        {
            breakTime = value;
        }
    }

    public bool IsBreakActive
    {
        get
        {
            return isBreakActive;
        }

        set
        {
            isBreakActive = value;
        }
    }

    // Use this for initialization
    void Start () {
        SetDefaultBreakTime();
        InitializeBreakUI();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (IsBreakActive) {
            BreakCountdown();
        }
	}

    /// <summary>
    /// Sets default break time
    /// </summary>
    void SetDefaultBreakTime() {
        Debug.Log("Setting default times");
        if (defaultBreakTime == 0.0f) {
            defaultBreakTime = 2.0f;
        }

        if (breakAddedTime == 0.0f) {
            breakAddedTime = 1.5f;
        }

        currentBreakTime = defaultBreakTime;
        breakTime = currentBreakTime;

        Debug.Log("Current break time: " + breakTime + "; secs to be added on reset: " + breakAddedTime);
    }

    /// <summary>
    /// Activates break mechanic countdown
    /// </summary>
    /// <param name="activationState">Current state of break</param>
    public void ActivateBreak(bool activationState) {
        Debug.Log("Break is active: " + activationState.ToString().ToUpper());
        IsBreakActive = activationState;

        if (activationState) {
            this.GetComponent<LifeBar>().ActivateBarReduction(true, BreakTime);
        }
    }

    /// <summary>
    /// Starts break countdown
    /// </summary>
    void BreakCountdown() {
        if (BreakTime > 0.0f)
        {
            BreakTime -= Time.fixedDeltaTime;
            Debug.Log("Break time: " + BreakTime);
            breakTimerUI.ChangeBreakTimerValue(BreakTime);
        }
        else if (BreakTime <= 0.0f)
        {
            BreakReset();
        }
    }

    /// <summary>
    /// Stops break after reaching zero. Adds a few seconds to the timer.
    /// Sends a message to life script
    /// </summary>
    void BreakReset() {

        Debug.Log("Reseting break...");

        //Stoping mechanic
        ActivateBreak(false);

        //Adding seconds to break timer
        IncreaseBreakTime();

        //Restoring life
        this.SendMessage("RestoreLife");
    }

    /// <summary>
    /// Increases break time after reset
    /// </summary>
    void IncreaseBreakTime() {

        Debug.Log("Increasing... Old break time: " + currentBreakTime);
        currentBreakTime = currentBreakTime + breakAddedTime;

        BreakTime = currentBreakTime;
        Debug.Log("Increasing... Increased to: " + BreakTime + " seconds.");

    }

    /// <summary>
    /// Resets all break values after an interruption (character dead)
    /// </summary>
    void BreakResetByInterruption() {
        //Stopping break
        ActivateBreak(false);

        //Resetting timer
        currentBreakTime = defaultBreakTime;
        BreakTime = currentBreakTime;
        Debug.Log("Break Time restored to its original default value: " + BreakTime);

        //Restoring life
        this.SendMessage("RestoreLife");
    }

    /// <summary>
    /// Initializes break timer UI object
    /// </summary>
    void InitializeBreakUI() {
        breakTimerUI = this.GetComponent<BreakTimer>();
    }
}