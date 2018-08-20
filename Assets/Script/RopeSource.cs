using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSource : MonoBehaviour {

    [SerializeField]
    private GameProcess gameProcess = null;

    [SerializeField]
    private GameObject ropeSegmentPrefab = null;

    [SerializeField]
    private GameObject JointPrefab = null;

    private GameObject currentSegment = null;

    [SerializeField]
    private Transform baseAnchor = null;
    private Transform currentAnchor = null;

    [SerializeField]
    private Transform subject = null;

    [SerializeField]
    private float rope = 1f;

    private float bindedRope = 0f;
    private float segmentLength = 0f;
    private int bodyMask = 0;
    
    public float RopeLength
    {
        get
        {
            return this.bindedRope + this.segmentLength;
        }
    }

    private void Start()
    {

        this.bodyMask = LayerMask.GetMask("Water") | LayerMask.GetMask("TransparentFX");
        this.currentAnchor = this.baseAnchor;
        this.CreateSegment();
    }

    private void Update()
    {
        segmentLength = this.MatchSegmentToCurrentAnchor(transform.position);
        this.CheckForNewLink();
    }

    private void CreateSegment()
    {
        this.currentSegment = GameObject.Instantiate(this.ropeSegmentPrefab);
        this.currentSegment.transform.position = this.transform.position;
        this.currentSegment.GetComponentInChildren<Collider>().enabled = false;
        segmentLength = this.MatchSegmentToCurrentAnchor(transform.position);
    }

    private float MatchSegmentToCurrentAnchor(Vector3 source)
    {
        this.currentSegment.transform.position = source;
        this.currentSegment.transform.LookAt(this.currentAnchor);
        float length = Vector3.Distance(source, this.currentAnchor.transform.position);
        this.currentSegment.transform.localScale = new Vector3(this.rope, this.rope, length / 2);
        return length;
    }

    private void CheckForNewLink()
    {
        Vector3 direction = (currentAnchor.position - transform.position).normalized;
        RaycastHit hitInfo;
        
        bool hit = Physics.SphereCast(transform.position, this.rope * .9f, direction, out hitInfo, float.MaxValue, this.bodyMask);
        if (hit)
        {
            Vector3 hitPosition = hitInfo.point;

            if (Vector3.Distance(hitPosition, currentAnchor.position) > this.rope * 2f)
            {
                Vector3 start = this.currentAnchor.position;
                Vector3 end = hitPosition;
                bindedRope += this.MatchSegmentToCurrentAnchor(hitPosition);
                this.currentSegment.transform.SetParent(this.subject, true);
                this.currentAnchor = this.currentSegment.transform;
                this.currentSegment.GetComponentInChildren<Collider>().enabled = true;

                var joint = GameObject.Instantiate(this.JointPrefab);
                joint.transform.SetParent(this.subject, false);
                joint.transform.position = hitPosition;
                joint.transform.localScale = new Vector3(this.rope, this.rope, this.rope);

                //gameProcess.NotifyNewSegment(start, end);

                this.CreateSegment();
            }
        }
    }
}
