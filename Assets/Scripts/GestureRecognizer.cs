using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;

public class GestureRecognizer : MonoBehaviour {

    Gesture[] gestureSet = new Gesture[]
    {
        new Gesture(new Point[]{ new Point(-5, 0, 0), new Point(-4, 0, 0), new Point(-3, 0, 0), new Point(-2, 0, 0),
                new Point(-1, 0, 0), new Point(0, 0, 0), new Point(1, 0, 0), new Point(2, 0, 0), new Point(3, 0, 0),
                new Point(4, 0, 0), new Point(5, 0, 0)}, "line_Horizontal")      // point data goes here
    };

    Point[] newPoints = new Point[] { /* points come from Painter */ };

    Gesture candidate;

    string gestureClass;

    public Material red;
    public Material gray;
    public Material green;

    public GameObject ball;

    public void CheckCandidateGesture(Point[] points) {      // check if candidate matches something in our gestureSet
        if (points.Length <= 1) {
            print("only one freaking point!");
            return;
        }
        newPoints = points;
        candidate = new Gesture(newPoints);
        gestureClass = PointCloudRecognizer.Classify(candidate, gestureSet);
        print(gestureClass);
        GotGestureClass(gestureClass);
        switch (gestureClass) {
            case "line_Horizontal": break;
        }
    }

    void GotGestureClass(string gestClass) {
        if (gestClass == "line_Horizontal") {
            // change to green, wait 2 secs change to gray
            StartCoroutine(BallColorThing(true));
        } else {
            // change to red, wait 2 secs change to gray
            StartCoroutine(BallColorThing(false));
        }
    }

    IEnumerator BallColorThing(bool wasRight) {
        ball.GetComponent<MeshRenderer>().material = wasRight ? green : red;
        yield return new WaitForSeconds(2);
        ball.GetComponent<MeshRenderer>().material = gray;
    }
}
