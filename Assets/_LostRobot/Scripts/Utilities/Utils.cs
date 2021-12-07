using System;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Utils {

    public static bool CanSeeTransform(Transform viewer, Transform target, float maxDegrees = 45f, float maxDistance = 100f) {
        float degree = Vector3.Angle(target.position - viewer.position, viewer.forward);
        Vector3 targetDirection = (target.position - viewer.position);

        RaycastHit hit;
        if (degree < maxDegrees) {
            if (Physics.Raycast(viewer.position, targetDirection, out hit, maxDistance)) {
                if (hit.collider.transform == target)
                    return true;
            }
        } else if (degree < 90 && Vector3.Distance(viewer.position, target.position) < 4f  && Physics.Raycast(viewer.position, targetDirection, out hit, maxDistance)) {
            if (hit.collider.transform == target)
                return true;
        }
        return false;
    }

    public static bool IsDiagonal(WorldTile a, WorldTile b) {
        return a.position.x != b.position.x && a.position.z != b.position.z;
    }

    public static bool HasAI(Vector3 position, float radius = 1f) {
        Collider[] colliders = Physics.OverlapSphere(position, radius, 1, QueryTriggerInteraction.Ignore);

        foreach (var collider in colliders){
            if (collider.GetComponent<AIManager>() != null)
                return true;
        }
        return false;
    }

    public static string GetUniqueId() {
        return Guid.NewGuid().ToString();
    }

}
