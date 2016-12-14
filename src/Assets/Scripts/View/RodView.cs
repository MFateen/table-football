using UnityEngine;
using System.Collections;

public class RodView : MonoBehaviour
{

    CustomGrid grid;
    int grid_X;
    int grid_Y;

    float rodSpeed = 4;


    Vector3 newPos;
    bool lerp = false;

    void Start()
    {
        grid = GameObject.FindObjectOfType<CustomGrid>();
        calculateMyGridCoordinate();
        newPos = transform.position;
    }

    //Uncomment for testing
    void Update()
    {
        if (lerp)
        {
            transform.position = Vector3.Lerp(transform.position, newPos, rodSpeed * Time.deltaTime);
        }
    }

    public void Draw(int position)
    {
        float zValue;
        float xValue;
        switch (position)
        {
            case -1:
                zValue = grid.CalculateZ(grid_Y, CustomGrid.Direction.NONE);
                xValue = grid.CalculateX(grid_X, CustomGrid.Direction.UP);
                break;
            case 0:
                zValue = grid.CalculateZ(grid_Y, CustomGrid.Direction.NONE);
                xValue = grid.CalculateX(grid_X, CustomGrid.Direction.NONE);
                break;
            case 1:
                zValue = grid.CalculateZ(grid_Y, CustomGrid.Direction.NONE);
                xValue = grid.CalculateX(grid_X, CustomGrid.Direction.DOWN);
                break;
            default:
                Debug.LogError("RodView: Error wrong value sent");
                return;
        }

        newPos = new Vector3(xValue, transform.position.y, zValue);
        lerp = true;
    }

    void calculateMyGridCoordinate()
    {
        grid_X = grid.X_WorldTo_X_Grid(transform.position.x);
        grid_Y = grid.Z_WorldTo_Y_Grid(transform.position.z);
    }

    //public void Kick() ??
}