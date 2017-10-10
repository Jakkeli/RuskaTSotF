using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using DaydreamElements.ClickMenu;

public enum GestureType { Square, Circle, Triangle, Return, Done };

public class FireFlyTest : MonoBehaviour {

    public GestureType gestureType;

    public float maxDistance;

    public Transform showGestureStart;
    public Transform showGestureEnd;

    Vector3 showGestureStartPos;
    Vector3 showGestureEndPos;

    public int waypointIndex;
    public float tolerance = 0.05f;
    public float speed = 1;

    Vector3 targetPos;

    public Material blue;
    public Material glowyStuff;

    public bool newMaterialSet;

    public Vector3[] positions;

    public bool isFinalMove;

    Vector3 wandPos;

    public Transform wandEndPoint;

    List<Point> pointList = new List<Point>() { };
    Point[] pointArray;
    Gesture gesture;

    Vector3 previousRecorderPointPos;

    public float pointRecordInterval = 0.05f;

    public GestureRecognizer gestRecog;
    public Painter painter;

    void Start () {
        GetComponent<MeshRenderer>().material = glowyStuff;
        showGestureStartPos = showGestureStart.position;
        showGestureEndPos = showGestureEnd.position;
        targetPos = showGestureStartPos;
	}

    Vector3[] SquarePositions() {
        List<Vector3> posList = new List<Vector3>() { };
        posList.Add(showGestureStartPos + new Vector3(0, maxDistance, 0));
        posList.Add(showGestureStartPos + new Vector3(-maxDistance, maxDistance, 0));
        posList.Add(showGestureStartPos + new Vector3(-maxDistance, 0, 0));
        posList.Add(showGestureStartPos);
        return posList.ToArray();
    }

    void Square() {

        if (newMaterialSet) {
            if ((transform.position - targetPos).magnitude < tolerance) {

                if (isFinalMove) {
                    GetComponent<MeshRenderer>().material = glowyStuff;
                    pointArray = pointList.ToArray();
                    gesture = new Gesture(pointArray, "square");
                    gestRecog.SetGestureToCheckAgainst(gesture);
                    painter.CanPaint();
                    gestureType = GestureType.Done;
                }

                waypointIndex++;
                targetPos = positions[waypointIndex];
                if (waypointIndex >= positions.Length - 1) {
                    waypointIndex = 0;
                    isFinalMove = true;
                }
            }

            if ((transform.position - previousRecorderPointPos).magnitude > pointRecordInterval) {
                pointList.Add(new Point(transform.position.x, transform.position.y, 0));
                previousRecorderPointPos = transform.position;
            }

            transform.position += (targetPos - transform.position).normalized * speed * Time.deltaTime;

        } else if ((transform.position - showGestureStartPos).magnitude < tolerance) {
            //startTheGesture
            if (!newMaterialSet) {
                
                pointList.Add(new Point(transform.position.x, transform.position.y, 0));
                previousRecorderPointPos = transform.position;
                positions = SquarePositions();
                targetPos = positions[waypointIndex];
                GetComponent<MeshRenderer>().material = blue;
                newMaterialSet = true;
            }
        } else {
            transform.position += (showGestureStartPos - transform.position).normalized * speed * Time.deltaTime;
        }


    }
	
	void Update () {
		if (gestureType == GestureType.Square) {
            Square();
        } else if (gestureType == GestureType.Circle) {

        } else if (gestureType == GestureType.Triangle) {

        }
	}
}
