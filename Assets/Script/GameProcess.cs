using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProcess : MonoBehaviour {

    [SerializeField]
    private RopeSource ropeSource = null;

    [SerializeField]
    private Text ropeLengthLabel = null;

    private float lengthFactor = .5f;

    private void Update()
    {
        float length = ropeSource.RopeLength * this.lengthFactor;
        string ropeLabel = string.Format("Rope used {0:#}", length);
        this.ropeLengthLabel.text = ropeLabel;
    }
}
