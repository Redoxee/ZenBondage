using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSource : MonoBehaviour {

    [SerializeField]
    private GameObject ropeSegmentPrefab = null;

    private GameObject currentSegment = null;

    [SerializeField]
    private Transform baseAnchor = null;
    private Transform currentAnchor = null;

    [SerializeField]
    private Transform subject = null;

    [SerializeField]
    private float roapWidth = 1f;

    private float bindedRope = 0f;
    private float segmentLength = 0f;

    public float RopeLength
    {
        get
        {
            return this.bindedRope + this.segmentLength;
        }
    }

    private void Start()
    {
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
        this.currentSegment.transform.localScale = new Vector3(this.roapWidth, this.roapWidth, length / 2);
        return length;
    }

    private void CheckForNewLink()
    {
        Vector3 direction = (currentAnchor.position - transform.position).normalized;
        RaycastHit hitInfo;
        bool hit = Physics.SphereCast(transform.position, this.roapWidth, direction, out hitInfo);
        if (hit)
        {
            Vector3 pos = hitInfo.point;

            if (Vector3.Distance(pos, currentAnchor.position) > this.roapWidth * 2f)
            {

                bindedRope += this.MatchSegmentToCurrentAnchor(pos);
                this.currentSegment.transform.SetParent(this.subject, true);
                this.currentAnchor = this.currentSegment.transform;
                this.currentSegment.GetComponentInChildren<Collider>().enabled = true;
                this.CreateSegment();
            }
        }
    }
}
