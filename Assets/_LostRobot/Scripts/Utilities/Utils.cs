using UnityEngine;

public static class Utils {

    public static bool CanSeeTransform(Transform viewer, Transform target, float maxDegrees = 45f, float maxDistance = 100f) {
        float degree = Vector3.Angle(target.position - viewer.position, viewer.forward);
        Vector3 targetDirection = (target.position - viewer.position);

        if (degree < 90 && Vector3.Distance(viewer.position, target.position) < 4f)
            return true;
        
        if (degree < maxDegrees) {
            RaycastHit hit;
            if (Physics.Raycast(viewer.position, targetDirection, out hit, maxDistance, 1, QueryTriggerInteraction.Ignore)) {
                if (hit.collider.transform == target)
                    return true;
            }
        }
        return false;
    }

    public static bool IsDiagonal(WorldTile a, WorldTile b) {
        return a.position.x != b.position.x && a.position.z != b.position.z;
    }

}
