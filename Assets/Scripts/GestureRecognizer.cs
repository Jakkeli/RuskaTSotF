using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using DaydreamElements.ClickMenu;
using UnityEngine.UI;

public class GestureRecognizer : MonoBehaviour {

    Gesture[] gestureSet = new Gesture[]
    {
        new Gesture(new Point[]{ new Point(-5, 0, 0), new Point(-4, 0, 0), new Point(-3, 0, 0), new Point(-2, 0, 0),
                new Point(-1, 0, 0), new Point(0, 0, 0), new Point(1, 0, 0), new Point(2, 0, 0), new Point(3, 0, 0),
                new Point(4, 0, 0), new Point(5, 0, 0)}, "line_Horizontal")
    };

    Point[] newPoints = new Point[] { /* points come from Painter */ };

    Gesture candidate;

    string gestureClass;

    public Material red;
    public Material gray;
    public Material green;

    public GameObject ball;

    public string currentGestureName = "";

    public Painter painter;

    public float minMatchValue = 1;

    public Slider minMatchValueSlider;

    public void SetGestureToCheckAgainst(Gesture gesture) {
        gestureSet = new Gesture[] { gesture };
        currentGestureName = gesture.Name;
    }

    public void SetMinMatch(float newValue) {
        minMatchValue = newValue;
    }

    public void SetFromSlider() {
        minMatchValue = minMatchValueSlider.value;
    }

    public void CheckCandidateGesture(Point[] points) {      // check if candidate matches something in our gestureSet
        if (points.Length <= 1) {
            print("only one freaking point!");
            painter.CanPaint();
            return;
        }
        newPoints = points;
        candidate = new Gesture(newPoints);
        painter.VisualStuff(candidate);
        gestureClass = PointCloudRecognizer.Classify(candidate, gestureSet, minMatchValue);
        //print(gestureClass);
        GotGestureClass(gestureClass);
        switch (gestureClass) {
            case "line_Horizontal": break;
        }
    }

    void GotGestureClass(string gestClass) {
        if (gestClass == "square") {
            print("square recognized!!!");
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
        painter.CanPaint();
    }
}
