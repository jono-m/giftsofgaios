using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerChoices : Singleton<PlayerChoices> {
    public bool canInfiniJump;
    public bool canDash;
    public bool hasFullVision;
    public bool canAttack;
    public bool canSlowTime;

    public int currentLevel;

    private void Awake() {
        ResetChoices();
    }

    public void ResetChoices() {
        canInfiniJump = true;
        canDash = true;
        hasFullVision = true;
        canAttack = true;
        canSlowTime = true;
        currentLevel = 1;
    }

    public static T FindGaiosComponent<T>() where T : MonoBehaviour {
        return FindObjectOfType<GaiosChoiceApplier>().GetComponent<T>();
    }

    public void ReachedEndOfLevel() {
        SceneManager.LoadScene("Choice");
        currentLevel++;
    }

    public void FinishedChoosing() {
        SceneManager.LoadScene("Level " + currentLevel);
    }

    public void SacrificeSelf() {
        SceneManager.LoadScene("End Game");
    }
}
