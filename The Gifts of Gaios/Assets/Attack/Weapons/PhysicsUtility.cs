using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsUtility : MonoBehaviour {
    public static RaycastHit2D[] GravityRaycast(Vector2 pointStart, Vector2 initialVelocity, Vector2 pointEnd, float gravity, float deltaTime) {
        if(deltaTime == 0) {
            return null;
        }
        Vector2 currentPoint = pointStart;
        Vector2 currentVelocity = initialVelocity;

        bool travelingRight = initialVelocity.x > 0;

        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        while ((travelingRight && (currentPoint.x < pointEnd.x)) || (!travelingRight && (currentPoint.x > pointEnd.x))) {
            Vector2 nextPoint = currentPoint + currentVelocity * deltaTime + (Vector2.down * gravity * Mathf.Pow(deltaTime, 2))/(2.0f);
            currentVelocity += Vector2.down * gravity * deltaTime;

            hits.AddRange(Physics2D.LinecastAll(currentPoint, nextPoint));
            currentPoint = nextPoint;
        }

        return hits.ToArray();
    }
}
