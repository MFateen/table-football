﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BallModel {
    //Position coordinates
    public int Row { get; private set; }
    public int Column { get; private set; }
    public int RowVelocity { get; set; }
    public int ColumnVelocity { get; set; }
    public int Power { get; set; }
    private BallView View { get; set;}

    public BallModel(int _Row, int _Column) {
        Row = _Row;
        Column = _Column;
        RowVelocity = ColumnVelocity = Power = 0;
        View = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallView>();
    }

    /*
     * Moves ball and returns:
     *  0 -> No goal
     * -1 -> Goal in player
     *  1 -> Goal in enemy
    */
    public int Move(FieldModel field) {
        if (Power == 0) {
            RowVelocity = ColumnVelocity = 0;
            return 0;
        }
        Power--;
        Row += RowVelocity;
        Column += ColumnVelocity;
        if (Row < 0 || Row >= field.Width || Column < 0 || Column >= field.Length) {
            int inGoal = isInGoal();
            if (inGoal != 0) {
                Row = 3;
                Column = 5;
                Power = RowVelocity = ColumnVelocity = 0;
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
     * -1 -> Goal in player
     *  1 -> Goal in enemy
    */
    public int isInGoal() {
        if (Row >= 2 && Row <= 4) {
            if (Column == -1) {
                return -1;
            }
            if (Column == 11) {
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
        return new Anticipate(-1, 0, false);
    }

    public void Draw() {
        View.Draw(Column, Row);
    }
}