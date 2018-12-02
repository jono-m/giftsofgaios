using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceItem : MonoBehaviour {
    public string choiceName;
    public string choiceDescription;

    public TextMeshProUGUI nameField;

    private void Start() {
        nameField.text = choiceName;
    }
}
