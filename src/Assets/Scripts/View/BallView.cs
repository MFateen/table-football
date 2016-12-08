using UnityEngine;
using System.Collections;

public class BallView : MonoBehaviour {

    CustomGrid grid;

    void Start () {
        grid = GameObject.FindObjectOfType<CustomGrid>();
    }


     //From (0,0) to (6,10)
    public void Draw(int _X, int _Y) {

        float zValue = grid.CalculateZ(_Y, CustomGrid.Direction.NONE);
        float xValue = grid.CalculateX(_X, CustomGrid.Direction.NONE);
        Vector3 newPos = new Vector3(xValue, transform.position.y, zValue);

        transform.localPosition = newPos;
    }
}
