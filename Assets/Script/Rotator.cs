using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start () {
		
	}
	
	
    void Update () {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseDelta = Input.mousePosition - this.startPosition;
            Quaternion nextRotation = Quaternion.AngleAxis(mouseDelta.x, new Vector3(0, -1, 0)) * Quaternion.AngleAxis(mouseDelta.y, new Vector3(1,0,0));
            this.transform.Rotate(mouseDelta.y, mouseDelta.x, 0f);
            this.startPosition = Input.mousePosition;
        }
        
        this.startPosition = Input.mousePosition;
        this.startRotation = this.transform.rotation;
    }
}
