using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IntelligentAgent {
    public static void MakeDecision(FieldModel Field, PlayerType Player) {
        // Insert agent's logic and save the command in the shared memory class
        if (Player == PlayerType.Host) {
            MakeHostDecision(Field);
        } else {
            MakeGuestDecision(Field);
        }

        //SharedMemory.Decision = new Command("", null, "");
    }

    public static void MakeHostDecision(FieldModel Field) {
        if (Field.Ball.Column == 0 && Field.Ball.Row >= 2 && Field.Ball.Row <= 4) {
            // Panic mode XD
            // TODO send Commands.KICK to Field.DefenseRodHost
        }

        ControlHostRod(Field, Field.DefenseRodHost);
        ControlHostRod(Field, Field.OffenseRodHost);
    }

    public static void ControlHostRod(FieldModel Field, RodModel Rod) {
        Anticipate Anticipated = Field.Ball.getRowIntersection(Rod.Column);

        if (Anticipated.Row == -1) {
            if (Rod.Position == RodPosition.Top) {
                // move down
            } else if (Rod.Position == RodPosition.Bottom) {
                // move up
            } else {
                //no action
            }
            return;
        }

        if (!Rod.RowInPlayerReach(Anticipated.Row)) {
            // move to AnticipateDefend.Row
            if (Rod.Position == RodPosition.Middle && Anticipated.Row == 6) {
                // send move down (right)
            } else if (Rod.Position == RodPosition.Middle) {
                // send move up (left)
            } else if (Rod.Position == RodPosition.Top) {
                // send move down
            } else {
                // send move up
            }
            return;
        }

        if (!Anticipated.Near) {
            // Send Commands.NO_ACTION
            return;
        }

        // Here the ball is in reach and the rod is in position and ready to shoot
        if (Anticipated.Column == -1) {
            // Send Commands.KICK Power = 1 direction = forward 
            return;
        }

        if (Anticipated.Column == 0) {
            // Send Commands.KICK Power = 1 and direction =
            getKickDirection(Anticipated.Row, 1);
        }

        if (Anticipated.Column == 1) {
            if (Rod == Field.DefenseRodHost) {
                // send Commands.KICK power = 5 and the following direction
                getKickDirection(Anticipated.Row, 5);
            } else {
                // send Commands.KICK power = 5 and the following direction
                getGoalDirection(Anticipated.Row);
            }
        }
    }

    public static void MakeGuestDecision(FieldModel Field) {

        //Commands.NO_ACTION;
    }

    public static KICK getKickDirection(int Row, int Power) {
        //Direction[0] : LEFT, Direction[1] : FORWARD, Direction[2] : RIGHT
        //
        double[] Direction = { 0.0, 1.0, 0.0 };
        if (Power == 1) {
            if (Row >= 1) {
                Direction[(int)KICK.LEFT] = 1.0;
            }

            if (Row <= 6) {
                Direction[(int)KICK.RIGHT] = 1.0;
            }
        } else {
            if (Row >= 3) {
                Direction[(int)KICK.LEFT] = 1.0;
            }

            if (Row <= 3) {
                Direction[(int)KICK.RIGHT] = 1.0;
            }
        }
        

        double Sum = Direction.Sum();
        double Probability = (new System.Random(0)).NextDouble() * Sum;

        for (int i = 0; i < Direction.Length; i++) {
            Probability -= Direction[i];
            if (Probability <= 0) {
                return (KICK)i;
            }
        }
        return KICK.FORWARD;
    }

    public static KICK getGoalDirection(int Row) {
        if (Row <= 1) {
            return KICK.RIGHT;
        } else if (Row >= 5) {
            return KICK.LEFT;
        } else {
            return KICK.FORWARD;
        }
    }
}

