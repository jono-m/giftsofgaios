using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
[ExecuteInEditMode]
public class HealthTrackerTextDisplay : MonoBehaviour {
    public HealthTracker trackerToDisplay;

    private TextMeshProUGUI text;

	// Use this for initialization
	void Start () {
        text = GetComponent<TextMeshProUGUI>();	
	}
	
	// Update is called once per frame
	void Update () {
		if(trackerToDisplay != null) {
            text.text = string.Format("{0}/{1}", trackerToDisplay.currentHealth, trackerToDisplay.maxHealth);
        }
	}

    private void OnValidate() {
        Start();
    }
}
