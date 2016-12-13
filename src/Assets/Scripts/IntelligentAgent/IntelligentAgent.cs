using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IntelligentAgent {
    public static void MakeDecision(FieldModel Field, PlayerType Player)
    {
        // Insert agent's logic and save the command in the shared memory class
        Command Decision = new Command(Player);

        if (ShouldPanic(Field, Player))
        {
            Decision.Kick(RodType.Defense, DIRECTION.FORWARD, 1);
        }

        if (Player == PlayerType.Host)
        {
            ControlRod(Player, Field, Field.DefenseRodHost, Decision);
            ControlRod(Player, Field, Field.OffenseRodHost, Decision);

        }
        else
        {
            ControlRod(Player, Field, Field.DefenseRodGuest, Decision);
            ControlRod(Player, Field, Field.OffenseRodGuest, Decision);
        }

        SharedMemory.PlayerCommand = Decision;
    }

    private static bool ShouldPanic(FieldModel Field, PlayerType Player) {
        if (Player == PlayerType.Host && Field.Ball.Column == 0 && Field.Ball.Row >= 2 && Field.Ball.Row <= 4) {
            return true;
        }

        if (Player == PlayerType.Guest && Field.Ball.Column == 10 && Field.Ball.Row >= 2 && Field.Ball.Row <= 4) {
            return true;
        }

        return false;
    }

    private static void ControlRod(PlayerType Player, FieldModel Field, RodModel Rod, Command Decision) {
        Anticipate Anticipated = Field.Ball.getRowIntersection(Rod.Column);
        int Coeff = Player == PlayerType.Host ? 1 : -1;

        if (Anticipated.Row == -1) {
            if (Rod.Position == RodPosition.Top) {
                Decision.MoveDown(Rod.Type);
            } else if (Rod.Position == RodPosition.Bottom) {
                Decision.MoveUp(Rod.Type);
            } else {
                Decision.NoAction(Rod.Type);
            }
            return;
        }

        if (!Rod.RowInPlayerReach(Anticipated.Row)) {
            // move to AnticipateDefend.Row
            if (Rod.Position == RodPosition.Middle && Anticipated.Row == 6) {
                Decision.MoveDown(Rod.Type);
            } else if (Rod.Position == RodPosition.Middle) {
                Decision.MoveUp(Rod.Type);
            } else if (Rod.Position == RodPosition.Top) {
                Decision.MoveDown(Rod.Type);
            } else {
                Decision.MoveUp(Rod.Type);
            }
            return;
        }

        if (!Anticipated.Near) {
            Decision.NoAction(Rod.Type);
            return;
        }

        //Check here
        // Here the ball is in reach and the rod is in position and ready to shoot
        if (Anticipated.Column == (-1 * Coeff) && !(Player==PlayerType.Guest && Field.Ball.Column == 5)) {
            Decision.Kick(Rod.Type, DIRECTION.FORWARD, 1);
            return;
        }

        if (Anticipated.Column == 0) {
            Decision.Kick(Rod.Type, getKickDirection(Player, Anticipated.Row, 1), 1);
        }

        if (Anticipated.Column == (1 * Coeff)) {
            if (Rod.Type == RodType.Defense) {
                Decision.Kick(Rod.Type, getKickDirection(Player, Anticipated.Row, 5), 5);
            } else {
                Decision.Kick(Rod.Type, getGoalDirection(Player, Anticipated.Row), 5);
            }
        }
    }

    public static DIRECTION getKickDirection(PlayerType Player, int Row, int Power) {
        //Direction[0] : LEFT, Direction[1] : FORWARD, Direction[2] : RIGHT

        DIRECTION Up = Player == PlayerType.Host ? DIRECTION.LEFT : DIRECTION.RIGHT;
        DIRECTION Down = Player == PlayerType.Host ? DIRECTION.RIGHT : DIRECTION.LEFT;


        double[] KickDirection = { 0.0, 1.0, 0.0 };
        if (Power == 1) {
            if (Row >= 1) {
                KickDirection[(int)Up + 1] = 1.0;
            }

            if (Row <= 5) {
                KickDirection[(int)Down + 1] = 1.0;
            }
        } else {
            if (Row >= 3) {
                KickDirection[(int)Up + 1] = 1.0;
            }

            if (Row <= 3) {
                KickDirection[(int)Down + 1] = 1.0;
            }
        }


        double Sum = KickDirection.Sum();
        double Probability = (new System.Random(0)).NextDouble() * Sum;

        for (int i = 0; i < KickDirection.Length; i++) {
            Probability -= KickDirection[i];
            if (Probability <= 0) {
                return (DIRECTION)(i - 1);
            }
        }
        return DIRECTION.FORWARD;
    }

    public static DIRECTION getGoalDirection(PlayerType Player, int Row) {
        DIRECTION Up = Player == PlayerType.Host ? DIRECTION.LEFT : DIRECTION.RIGHT;
        DIRECTION Down = Player == PlayerType.Host ? DIRECTION.RIGHT : DIRECTION.LEFT;

        if (Row <= 1) {
            return Down;
        } else if (Row >= 5) {
            return Up;
        } else {
            return DIRECTION.FORWARD;
        }
    }
}

