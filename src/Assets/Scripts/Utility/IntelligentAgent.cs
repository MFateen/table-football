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

        // TODO always return the rods to middle

        // TODO: Send Cmnd !!!!!
        SharedMemory.Decision = new Command("", null, "");
    }

    public static void MakeHostDecision(FieldModel Field) {
        if (Field.Ball.Column == 0 && Field.Ball.Row >= 2 && Field.Ball.Row <= 4) {
            // Panic mode XD
            // TODO send Commands.KICK to Field.DefenseRodHost
        }

        ControlDefenseHost(Field);
        Controloffense(Field);

    }

    public static void ControlDefenseHost(FieldModel Field) {
        Anticipate AnticipateDefend = Field.Ball.getRowIntersection(Field.DefenseRodHost.Column);

        if (AnticipateDefend.Row == -1) {
            // move to middle
            return;
        }

        if (!Field.DefenseRodHost.RowInPlayerReach(AnticipateDefend.Row)) {
            // move to AnticipateDefend.Row
            return;
        }

        if (!AnticipateDefend.Near) {
            // Send Commands.NO_ACTION
            return;
        }

        // Here the ball is in reach and the rod is in position and ready to shoot
        if (AnticipateDefend.Column == -1) {
            // Send Commands.KICK Power = 1 direction = forward 
            return;
        }

        if (AnticipateDefend.Column == 0) {
            // TODO compute direction
            // Send Commands.KICK Power = 1 
        }

        if (AnticipateDefend.Column == 1) {
            // TODO compute direction
            // send Commands.KICK power = 5
        }
    }

    public static void Controloffense(FieldModel Field) {
        Anticipate Anticipateoffend = Field.Ball.getRowIntersection(Field.OffenseRodHost.Column);

    }

    public static void MakeGuestDecision(FieldModel Field) {

        //Commands.NO_ACTION;
    }

    public static int getKickDirection(int Row) {
        //Direction[0] : LEFT, Direction[1] : FORWARD, Direction[2] : RIGHT
        //
        double[] Direction = { 0.0, 1.0, 0.0 };
        if (Row >= 3) {
            Direction[0] = 1.0;
        }

        if (Row <= 3) {
            Direction[2] = 1.0;
        }
        // divide each member by the sum and then get one of them according to it's probability
        return 0;
    }

    public static int getGoalDirection(int Row) {
        if (Row <= 1) {
            return 0;
        } else if (Row >= 5) {
            return 2;
        } else {
            return 1;
        }
    }
}

