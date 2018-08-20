using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.layer & 1) > 0)
        {
            GameProcess.Instance.NotifyWayPointTouched(this.gameObject);
        }
    }
}
