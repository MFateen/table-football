using UnityEngine;
using System.Collections;

public class BallView : MonoBehaviour
{

    CustomGrid grid;
    bool lerp = false;
    Vector3 myPos, newPos;
    float rotSpeed, currentRotSpeed;
    int myCurrentX, myCurrentY;

    float ballSpeed = 6;
    void Start()
    {
        myCurrentX = 0;
        myCurrentY = 0;
        grid = GameObject.FindObjectOfType<CustomGrid>();
    }

    void Update()
    {
        if (lerp)
        {
            transform.position = Vector3.Lerp(transform.position, newPos, ballSpeed * Time.deltaTime);
            currentRotSpeed -= 1 * Time.deltaTime;
            if (currentRotSpeed <= 0)
            {
                currentRotSpeed = 0;
                lerp = false;
            }
        }
    }

    //From (0,0) to (6,10)
    public void Draw(int _X, int _Y)
    {
        if (_X != myCurrentX || _Y != myCurrentY)
        {
            myCurrentX = _X;
            myCurrentY = _Y;
            rotSpeed = 35;
            currentRotSpeed = rotSpeed;

            myPos = transform.position;
            float zValue = grid.CalculateZ(_Y, CustomGrid.Direction.NONE);
            float xValue = grid.CalculateX(_X, CustomGrid.Direction.NONE);
            newPos = new Vector3(xValue, myPos.y, zValue);

            //transform.localPosition = newPos;
            //Rot Anim Cacl
            Vector3 movDir = myPos - newPos;
            Vector3 axis = Vector3.Cross(movDir, Vector3.up);
            transform.Rotate(axis, currentRotSpeed, Space.World);
            lerp = true;
        }
    }
}
