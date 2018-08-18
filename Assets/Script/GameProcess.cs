using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProcess : MonoBehaviour
{

    [SerializeField]
    private RopeSource ropeSource = null;

    [SerializeField]
    private Text ropeLengthLabel = null;

    [SerializeField]
    private List<Transform> wayPoints = null;

    private float lengthFactor = .5f;

    private void Update()
    {
        float length = ropeSource.RopeLength * this.lengthFactor;
        string ropeLabel = string.Format("Rope used {0:#}", length);
        this.ropeLengthLabel.text = ropeLabel;
    }

    public void NotifyNewSegment(Vector3 start, Vector3 end)
    {
        Vector3 dir = (end - start).normalized;
        for(int index = wayPoints.Count - 1; index >= 0; --index)
        {
            Transform waypoint = wayPoints[index];
            if (IntersectSegmentSphere(start, end, waypoint.position, waypoint.localScale.x))
            {
                waypoint.gameObject.SetActive(false);
                wayPoints.RemoveAt(index);
            }
        }
    }
    
    bool IntersectSegmentSphere(Vector3 p1, Vector3 p2, Vector3 spherePos, float radius)
    {
        float squareRadius = radius * radius;
        if ((p1 - spherePos).sqrMagnitude <= squareRadius ||
            (p2 - spherePos).sqrMagnitude <= squareRadius)
        {
            return true;
        }


        return false;
    }
}
