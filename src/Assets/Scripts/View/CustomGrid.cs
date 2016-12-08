using UnityEngine;
using System.Collections;

public class CustomGrid : MonoBehaviour {

    public GameObject GridStartPoint;

    float World_X_Start, World_Y_Start;
    int quantum = 1;

	void Awake () {
        World_X_Start = GridStartPoint.transform.position.z;    //decrease world_Z = increase Grid_X
        World_Y_Start = GridStartPoint.transform.position.x;    //decrease world_X = increase Grid_Y
    }

    public float CalculateZ (int _GridY, Direction Ydirection) {
        float distanceFromStart = (float)_GridY * quantum;
        switch (Ydirection)
        {
            case Direction.NONE:
                return World_X_Start - distanceFromStart;
            case Direction.LEFT:
                return World_X_Start - distanceFromStart + quantum;
            case Direction.RIGHT:
                return World_X_Start - distanceFromStart - quantum;
            default:
                Debug.LogError("CustomGrid: Can't move in CalculateX");
                return -1;
        }
    }
    public float CalculateX (int _GridX, Direction Xdirection) {

        float distanceFromStart = (float)_GridX * quantum;
        switch (Xdirection)
        {
            case Direction.NONE:
                return World_Y_Start - distanceFromStart;
            case Direction.UP:
                return World_Y_Start - distanceFromStart + quantum;
            case Direction.DOWN:
                return World_Y_Start - distanceFromStart - quantum;
            default:
                Debug.LogError("CustomGrid: Can't move in CalculateY");
                return -1;
        }
    }

    public int  X_WorldTo_X_Grid(float XworldPoint)
    {
        return (int)(World_Y_Start - XworldPoint);
    }

    public int Z_WorldTo_Y_Grid(float ZworldPoint)
    {
        return (int)(World_X_Start - ZworldPoint);
    }


    public enum Direction
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    };
}
