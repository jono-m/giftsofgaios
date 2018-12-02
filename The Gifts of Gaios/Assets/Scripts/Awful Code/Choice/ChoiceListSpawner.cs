﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoiceListSpawner : MonoBehaviour {
    public ChoiceItem itemPrefab;

    private void Start() {
        if(PlayerChoices.Instance.canDash) {
            ChoiceItem newItem = Instantiate(itemPrefab.gameObject, transform).GetComponent<ChoiceItem>();
            newItem.choiceName = "Agility";
        }

        if (PlayerChoices.Instance.canInfiniJump) {
            ChoiceItem newItem = Instantiate(itemPrefab.gameObject, transform).GetComponent<ChoiceItem>();
            newItem.choiceName = "Flight";
        }

        if (PlayerChoices.Instance.canSlowTime) {
            ChoiceItem newItem = Instantiate(itemPrefab.gameObject, transform).GetComponent<ChoiceItem>();
            newItem.choiceName = "Reflex";
        }

        if (PlayerChoices.Instance.canAttack) {
            ChoiceItem newItem = Instantiate(itemPrefab.gameObject, transform).GetComponent<ChoiceItem>();
            newItem.choiceName = "Strength";
        }

        if (PlayerChoices.Instance.hasFullVision) {
            ChoiceItem newItem = Instantiate(itemPrefab.gameObject, transform).GetComponent<ChoiceItem>();
            newItem.choiceName = "Perception";
        }

        if(transform.childCount == 0) {
            ChoiceItem newItem = Instantiate(itemPrefab.gameObject, transform).GetComponent<ChoiceItem>();
            newItem.choiceName = "Resilience";
        }

        ToggleGroup group = GetComponent<ToggleGroup>();
        bool found = false;
        foreach(Transform t in transform) {
            Toggle toggle = t.GetComponent<Toggle>();
            toggle.group = group;
            if(!found) {
                toggle.isOn = true;
                found = true;
            }
        }
    }

    public void MakeChoice() {
        ToggleGroup group = GetComponent<ToggleGroup>();
        foreach(Toggle t in group.ActiveToggles()) {
            ChoiceItem item = t.GetComponent<ChoiceItem>();
            switch(item.choiceName) {
                case "Agility":
                    PlayerChoices.Instance.canDash = false;
                    break;
                case "Flight":
                    PlayerChoices.Instance.canInfiniJump = false;
                    break;
                case "Strength":
                    PlayerChoices.Instance.canAttack= false;
                    break;
                case "Perception":
                    PlayerChoices.Instance.hasFullVision= false;
                    break;
                case "Reflex":
                    PlayerChoices.Instance.canSlowTime = false;
                    break;
                case "Resilience":
                    PlayerChoices.Instance.SacrificeSelf();
                    return;
            }
        }
        PlayerChoices.Instance.FinishedChoosing();
    }
}
