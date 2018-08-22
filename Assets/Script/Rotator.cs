using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    private Vector3 startPosition;
    private Quaternion startRotation;
    [SerializeField]
    private Transform subTransform;

    void Start () {
		
	}
	
	
    void Update () {
        if (!GameProcess.Instance.IsPlaying)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseDelta = Input.mousePosition - this.startPosition;
            this.transform.Rotate(mouseDelta.y, -mouseDelta.x, 0f);
            this.startPosition = Input.mousePosition;
        }
        
        this.startPosition = Input.mousePosition;
        this.startRotation = this.transform.rotation;

        if (Input.GetMouseButtonUp(0))
        {
            var rot = this.subTransform.rotation;
            this.transform.rotation = Quaternion.identity;
            this.subTransform.rotation = rot;
        }
    }
}
