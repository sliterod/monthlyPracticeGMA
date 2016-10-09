using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeBar : MonoBehaviour {

    //Life bar
    public Image lifeBarPlayer;
    public RectTransform lifeBarRect;

    //Control
    bool canDecreaseLifeBar;
    float decreasingScaleNumber;

    void FixedUpdate() {
        if (canDecreaseLifeBar) {
            DecreaseSize();
        }
    }

    /// <summary>
    /// Changes the color of the life bar
    /// </summary>
    /// <param name="currentPctDamage">Current percentage damage</param>
    void ChangeColor(float currentPctDamage) {

        if (currentPctDamage >= 0.0f &&
            currentPctDamage <= 0.5f)
        {
            lifeBarPlayer.color = new Color(0.0f, 0.67f, 0.0f);
        }

        if (currentPctDamage > 0.5f &&
            currentPctDamage <= 0.8f)
        {
            lifeBarPlayer.color = new Color(0.67f, 0.67f, 0.0f);
        }

        if (currentPctDamage > 0.8f)
        {
            lifeBarPlayer.color = new Color(0.67f, 0.0f, 0.0f);
        }
    }

    /// <summary>
    /// Increases the size of the bar
    /// </summary>
    /// <param name="percentage">Current percentage bar</param>
    public void IncreaseBarSize(float percentage) {
        lifeBarRect.localScale = new Vector2(percentage, 1.0f);
        ChangeColor(percentage);
    }

    /// <summary>
    /// Activate bool to initialze bar reduction
    /// </summary>
    /// <param name="activation">State of activation</param>
    /// <param name="timer">Break time</param>
    public void ActivateBarReduction(bool activation, float timer) {
        Debug.Log("Life bar reduction is active: " + activation.ToString().ToUpper());
        if (activation) {
            decreasingScaleNumber = 0.02f / timer;
        }
        Debug.Log("Decreasing Number: " + decreasingScaleNumber);
        canDecreaseLifeBar = activation;
    }

    /// <summary>
    /// Decreases the size of the bar with an iTween event
    /// </summary>
    /// <param name="time">Currrent break time</param>
    void DecreaseSize() {

        float xScale = lifeBarRect.localScale.x - decreasingScaleNumber;

        if (lifeBarRect.localScale.x > 0) {
            lifeBarRect.localScale = new Vector2(xScale, 1.0f);
        }
        else if (lifeBarRect.localScale.x <= 0.0f)
        {
            ActivateBarReduction(false, 0.0f);
        }
    }

    /// <summary>
    /// Resets life bar size
    /// </summary>
    public void ResetLifeBar() {
        lifeBarRect.localScale = new Vector2(0.0f, 1.0f);
        lifeBarPlayer.color = new Color(0.0f, 0.67f, 0.0f);

        canDecreaseLifeBar = false;
    }
}