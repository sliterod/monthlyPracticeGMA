using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BreakTimer : MonoBehaviour {

    public Text timerPlayer;

    /// <summary>
    /// Changes break timer value
    /// </summary>
    /// <param name="time">Remaining break time</param>
    public void ChangeBreakTimerValue(float time) {
        timerPlayer.text = time.ToString("0.0") + "s";
    }

}