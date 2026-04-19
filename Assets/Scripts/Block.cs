using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public List<SnapPoint> snapPoints;

    public SnapPoint GetClosestSnap(Vector3 normal)
    {
        SnapPoint best = null;
        float bestDot = -0.5f;

        foreach (var snap in snapPoints)
        {
            if (snap.isOccupied) continue;

            float dot = Vector3.Dot(snap.transform.forward, normal);

            if (dot > bestDot)
            {
                bestDot = dot;
                best = snap;
            }
        }

        return best;
    }
}