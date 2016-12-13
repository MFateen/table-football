using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BallModel {
    //Position coordinates
    public int Row { get; private set; }
    public int Column { get; private set; }
	//Previous position of the ball
	public int PreviousRow { get; private set; }
	public int PreviousColumn { get; private set; }
    public int RowVelocity { get; set; }
    public int ColumnVelocity { get; set; }
    public int Power { get; set; }
    private BallView View { get; set;}

    public BallModel(int _Row, int _Column)
    {
        Row = _Row;
        Column = _Column;
        RowVelocity = ColumnVelocity = Power = PreviousRow = PreviousColumn = 0;
        View = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallView>();
    }

    /*
     * Moves ball and returns:
     *  0 -> No goal
     * -1 -> Goal in Host
     *  1 -> Goal in Guest
    */
    public int Move(FieldModel field)
    {
        if (Power == 0)
        {
            RowVelocity = ColumnVelocity = 0;
            return 0;
        }
        Power--;
		PreviousRow = Row;
        Row += RowVelocity;
		PreviousColumn = Column;
        Column += ColumnVelocity;
        if (Row < 0 || Row >= field.Width || Column < 0 || Column >= field.Length)
        {
            int inGoal = isInGoal();
            if (inGoal != 0)
            {
                Row = 3;
                Column = 5;
                Power = RowVelocity = ColumnVelocity = PreviousRow = PreviousColumn = 0;
                return inGoal;
            }
            Row -= RowVelocity;
            Column -= ColumnVelocity;
            Power = RowVelocity = ColumnVelocity = 0;
        }
        return 0;
    }

    /*
     *  0 -> No goal
     * -1 -> Goal in Host
     *  1 -> Goal in Guest
    */
    public int isInGoal()
    {
        if ((Row >= 2 && Row <= 4) || ((Row == 1 || Row == 5) && (PreviousRow >= 2 && PreviousRow <= 4)))
        {
            if (Column == -1)
            {
                return -1;
            }
            if (Column == 11)
            {
                return 1;
            }
        }
        return 0;
    }

    /// <summary>
    ///     A function that computes the intersection between the ball's path and the
    ///     passed Column or the 2 columns around it
    /// </summary>
    /// <param name="TargetColumn">
    ///     The column which we need to find the intersection with
    /// </param>
    /// <returns>
    ///     an Anticipate object with row = row of intersection or -1
    ///     and column = -1 (column before), 0(passed column), or 1(column after)
    /// </returns>
    public Anticipate getRowIntersection(int TargetColumn) {
        //TODO
        //get difference between TargetColumn and Column
        //if diff <= Power => an intersection will occur at Row + diff*RowVelocity
        Anticipate Result;
        int ColumnDifference = Math.Abs(TargetColumn-Column);
        bool MoveTowardsTarget;// condition to check if ball moves towards target column
        // Test if the ball is currently near
        if(Column<=TargetColumn+1 && Column>=TargetColumn-1)
            Result.Near = true;
        else
            Result.Near=false;
        // Checking against Target Column
        if(Column == TargetColumn)
        {
            Result.Row = Row;
            Result.Column = 0;
            return Result;
        }
        MoveTowardsTarget = (TargetColumn-Column>0 && ColumnVelocity==1) || (TargetColumn-Column<0 && ColumnVelocity==-1);
        if(Power>=ColumnDifference && MoveTowardsTarget)
        {
            Result.Row = Row + ColumnDifference*RowVelocity;
            Result.Column=0;
            return Result;
        }
        // check column to the right
        if(Column == TargetColumn+1)
        {
            Result.Row = Row;
            Result.Column = 1;
            return Result;
        }
        MoveTowardsTarget = (TargetColumn+1-Column>0 && ColumnVelocity==1) || (TargetColumn+1-Column<0 && ColumnVelocity==-1);
        ColumnDifference = Math.Abs(TargetColumn+1-Column);
        if(Power>=ColumnDifference && MoveTowardsTarget)
        {
            Result.Row = Row + ColumnDifference*RowVelocity;
            Result.Column=1;
            return Result;
        }

        // check column to the left
        if(Column == TargetColumn-1)
        {
            Result.Row = Row;
            Result.Column = -1;
            return Result;
        }
        MoveTowardsTarget = (TargetColumn-1-Column>0 && ColumnVelocity==1) || (TargetColumn-1-Column<0 && ColumnVelocity==-1);
        ColumnDifference = Math.Abs(TargetColumn-1-Column);
        if(Power>=ColumnDifference && MoveTowardsTarget)
        {
            Result.Row = Row + ColumnDifference*RowVelocity;
            Result.Column=-1;
            return Result;
        }
        // If non of the previous condition applies
        Result.Near = false;
        Result.Row = -1;
        Result.Column = -2;
        return Result;
    }

    public void Draw() {
        View.Draw(Row, Column);
    }
}
