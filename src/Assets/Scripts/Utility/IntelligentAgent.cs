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
            // Send Commands.KICK Power = 1
            return;
        }

        



        if (Field.Ball.Column < Field.Length / 2) { // Defensive mode
            // TODO check with the ball direction a7san
            if (AnticipateDefend.Row == 6) {
                //Move to RodPosition.Top
            } else if (AnticipateDefend.Row % 2 == 0) { // == 0 || 2 || 4 is probably faster
                //Move to RodPosition.Bottom
            } else { // if no intersection or intersection with an odd row
                // NO_ACTION (Move to middle)
            }
        } else { // Offensive mode

        }
        //Commands.NO_ACTION;
    }

    public static void Controloffense(FieldModel Field) {
        Anticipate Anticipateoffend = Field.Ball.getRowIntersection(Field.OffenseRodHost.Column);

    }

    public static void MakeGuestDecision(FieldModel Field) {

        //Commands.NO_ACTION;
    }
}

