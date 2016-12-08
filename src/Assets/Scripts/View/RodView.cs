using UnityEngine;
using System.Collections;

public class RodView : MonoBehaviour {

    CustomGrid grid;
    int grid_X;
    int grid_Y;

    void Start() {
        grid = GameObject.FindObjectOfType<CustomGrid>();
        calculateMyGridCoordinate();
    }

    //Uncomment for testing
    //void Update()
    //{
    //    Draw(1);
    //}

    public void Draw(int position) {
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

        Vector3 newPos = new Vector3(xValue, transform.position.y, zValue);
        transform.localPosition = newPos;
    }

    void calculateMyGridCoordinate() {
        grid_X = grid.X_WorldTo_X_Grid(transform.position.x);
        grid_Y = grid.Z_WorldTo_Y_Grid(transform.position.z);
     }

    //public void Kick() ??
}







































//public void MoveUp() {
//    float xValue = transform.position.x + 1.0f;
//    Vector3 newPos = new Vector3(xValue, transform.position.y, transform.position.z);

//    transform.localPosition = newPos;
//}

//public void MoveDown() {
//    float xValue = transform.position.x - 1.0f;
//    Vector3 newPos = new Vector3(xValue, transform.position.y, transform.position.z);

//    transform.localPosition = newPos;
//}