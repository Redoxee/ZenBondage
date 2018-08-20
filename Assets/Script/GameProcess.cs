using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProcess : MonoBehaviour
{
    private static GameProcess instance = null;
    public static GameProcess Instance
    {
        get
        {
            return GameProcess.instance;
        }
    }

    [SerializeField]
    private RopeSource ropeSource = null;

    [SerializeField]
    private Text ropeLengthLabel = null;
    [SerializeField]
    private Image ropeGauge = null;

    [SerializeField]
    private List<Collider> wayPoints = null;

    private float lengthFactor = .5f;

    [SerializeField]
    private float maxRopeLength = 1000f;

    private void Awake()
    {
        GameProcess.instance = this;
    }

    private void Update()
    {
        float length = ropeSource.RopeLength * this.lengthFactor;
        string ropeLabel = string.Format("Rope {0:#}",  Mathf.Max(this.maxRopeLength - length,0f));
        this.ropeLengthLabel.text = ropeLabel;
        float ropeLeft = 1f - Mathf.Clamp01(length / maxRopeLength);
        this.ropeGauge.fillAmount = ropeLeft;
    }

    public void NotifyWayPointTouched(GameObject waypoint)
    {
        waypoint.SetActive(false);
    }

    public void NotifyNewSegment(Collider segmentCollider)
    {

        for(int index = wayPoints.Count - 1; index >= 0; --index)
        {
            Collider waypoint = wayPoints[index];
            {
                waypoint.gameObject.SetActive(false);
                wayPoints.RemoveAt(index);
            }
        }
    }
    
    bool IntersectSegmentSphere(Vector3 p1, Vector3 p2, Vector3 spherePos, float radius)
    {
        float squareRadius = radius * radius;
        Vector3 ps1 = spherePos - p1;
        Vector3 ps2 = spherePos - p2;
        if (ps1.sqrMagnitude <= squareRadius || ps2.sqrMagnitude <= squareRadius)
        {
            return true;
        }

        Vector3 segment = (p2 - p1);
        float dot = Vector3.Dot(ps1, segment);
        if (dot < 0 || dot > 1)
        {
            return false;
        }
        
        Vector3 k = p1 + segment.normalized * dot;

        if (k.sqrMagnitude < squareRadius)
        {
            return true;
        }

        return false;
    }
}
