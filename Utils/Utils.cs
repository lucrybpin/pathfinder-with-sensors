using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static RaycastHit[] GetWorldHitsFrom2DPosition (Vector2 point, Camera camera = null)
    {
        camera = ( camera == null ) ? Camera.main : camera;
        Ray ray = camera.ScreenPointToRay( point );
        RaycastHit [ ] hits = Physics.RaycastAll( ray, float.PositiveInfinity  );
        return hits;
    }

    public static Vector3 GetWorldPosition (Vector2 screenPoint, Camera camera = null)
    {
        camera = ( camera == null ) ? Camera.main : camera;
        Ray ray = camera.ScreenPointToRay( screenPoint );
        RaycastHit hit;
        bool hitSomething = Physics.Raycast( ray, out hit, float.PositiveInfinity );
        if (hitSomething)
            return hit.point;

        return Vector3.negativeInfinity;
    }
}
