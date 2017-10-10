using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureVisualizer : MonoBehaviour {

    public bool visualize;

    public GameObject visualPrefab;

    public List<GameObject> visuals;

    GameObject visualClone;

    public int numberOfVisuals = 32;

    Vector3 startPos = new Vector3(0, -500, 0);

    int index;

    public List<Vector3> originalPositions;

	void Start () {
        if (!visualize) return;
        CreateVisuals();
	}

    void CreateVisuals() {
        for (int i = 0; i < numberOfVisuals; i++) {
            visualClone = Instantiate(visualPrefab, startPos + new Vector3(i * 5, 0, 0), Quaternion.identity);
            visuals.Add(visualClone);
            originalPositions.Add(visualClone.transform.position);
        }
    }

    public void PlaceBalls(List<Vector3> posList) {
        if (!visualize) return;
        for (int i = 0; i < posList.Count; i++) {
            VisualBall().transform.position = posList[i];
        }
    }

    public void ResetBallPositions() {
        if (!visualize) return;
        if (visuals.Count != originalPositions.Count) {
            print("WTF");
            return;
        }
        for (int i = 0; i < visuals.Count; i++) {
            visuals[i].transform.position = originalPositions[i];
        }
    }

    public GameObject VisualBall() {
        GameObject returnObject;
        returnObject = visuals[index];
        index++;
        if (index >= visuals.Count) index = 0;
        return returnObject;
    }
}
