using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowTest : MonoBehaviour {

    public Material black;
    public Material glowOrange;

	public void ChangeMaterial(bool toGlow) {
        if (toGlow) {
            GetComponent<MeshRenderer>().material = glowOrange;
        } else {
            GetComponent<MeshRenderer>().material = black;
        }
    }
}
