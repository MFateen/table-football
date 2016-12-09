﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RodModel {
    // Rod properties
    public PlayerType Player { get; set; }
    public int Column { get; set; }

    // Absolute position of the rod
    public RodPosition Position { get; set; }

    //Absolute positions of the players - Row wise
    List<int> PlayersPositions { get; set; }

    // View object
    private RodView View { get; set; }

    public RodModel(PlayerType _Player, string rodTag, int _Column, RodPosition _Position = RodPosition.Middle) {
        Player = _Player;
        Column = _Column;
        Position = _Position;
        PlayersPositions = new List<int>();
        PlayersPositions.Add(GameSettings.RodFirstPlayerRelativePosition);
        PlayersPositions.Add(GameSettings.RodSecondPlayerRelativePosition);
        PlayersPositions.Add(GameSettings.RodThirdPlayerRelativePosition);
        UpdatePlayersPositions();
        View = GameObject.FindGameObjectWithTag(rodTag).GetComponent<RodView>();
    }

    private void UpdatePlayersPositions() {
        for (int i = 0; i < PlayersPositions.Count; i++) {
            PlayersPositions[i] += (int)Position;
        }
    }

    public bool MoveUp() {
        if (Position != RodPosition.Top) {
            Position--;
            UpdatePlayersPositions();
            return true;
        }
        return false;
    }

    public bool MoveDown() {
        if (Position != RodPosition.Bottom) {
            Position++;
            UpdatePlayersPositions();
            return true;
        }
        return false;
    }

    /*
     * Responsible for drawing the fields and the rods.
     * May be unnecessary.
    */
    public void Draw() {
        //Draw Rod
        //View.draw();
    }

    /*
     * Checks if the ball is in the reach of the rod
     */
    public bool BallInPlayerReach(BallModel Ball) {
        //If the ball is not in the reach of the rod column-wise
        if (Ball.Column != Column && Ball.Column != Column - 1 && Ball.Column != Column + 1) {
            return false;
        }

        //If the ball is in the reach of the rod row-wise
        for (int i = 0; i < PlayersPositions.Count; i++) {
            if (Ball.Row == PlayersPositions[i]) {
                return true;
            }
        }

        return false;
    }


    public bool RowInPlayerReach(int Row) {
        for (int i = 0; i < PlayersPositions.Count; i++) {
            if (Row == PlayersPositions[i]) {
                return true;
            }
        }
        return false;
    }

    /*
     * Kicks the ball
     */
    public bool Kick(int direction, int power, BallModel Ball) {
        if (!BallInPlayerReach(Ball)) {
            return false;
        }
        Ball.RowVelocity = direction;
        if (Player == PlayerType.Host) {
            Ball.ColumnVelocity = 1;
        } else {
            Ball.ColumnVelocity = -1;
        }
        Ball.Power = power;
        return true;
    }
}