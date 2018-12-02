using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NextLevelDetector : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        NextLevelRegion region = collision.GetComponent<NextLevelRegion>();
        if(region != null) {
            PlayerChoices.Instance.ReachedEndOfLevel();
        }
    }
}
