using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Command {
    public PlayerType Player { get; set; }
    public ActionType OffenceAction { get; set; }
    public List<int> OffenceActionParameters { get; set; }
    public ActionType DefenseAction { get; set; }
    public List<int> DefenseActionParameters { get; set; }

    public Command(PlayerType _Player, ActionType _Offence, List<int> _OffenceParameters, ActionType _Defense, List<int> _DefenseParameters) {
        Player = _Player;
        OffenceAction = _Offence;
        OffenceActionParameters = _OffenceParameters;
        DefenseAction = _Defense;
        DefenseActionParameters = _DefenseParameters;
    }

    public Command(PlayerType _Player) {
        Player = _Player;
        OffenceAction = ActionType.NO_ACTION;
        OffenceActionParameters = null;
        DefenseAction = ActionType.NO_ACTION;
        DefenseActionParameters = null;
    }

    public void MoveUp(RodType Rod) {
        if (Player == PlayerType.Host) {
            if (Rod == RodType.Defense) {
                DefenseAction = ActionType.MOVEROD;
                DefenseActionParameters = new List<int>(new int[] { (int)DIRECTION.LEFT });
            } else {
                OffenceAction = ActionType.MOVEROD;
                OffenceActionParameters = new List<int>(new int[] { (int)DIRECTION.LEFT });
            }
        } else {
            if (Rod == RodType.Defense) {
                DefenseAction = ActionType.MOVEROD;
                DefenseActionParameters = new List<int>(new int[] { (int)DIRECTION.RIGHT });
            } else {
                OffenceAction = ActionType.MOVEROD;
                OffenceActionParameters = new List<int>(new int[] { (int)DIRECTION.RIGHT });
            }
        }
    }

    public void MoveDown(RodType Rod) {
        if (Player == PlayerType.Host) {
            if (Rod == RodType.Defense) {
                DefenseAction = ActionType.MOVEROD;
                DefenseActionParameters = new List<int>(new int[] { (int)DIRECTION.RIGHT });
            } else {
                OffenceAction = ActionType.MOVEROD;
                OffenceActionParameters = new List<int>(new int[] { (int)DIRECTION.RIGHT });
            }
        } else {
            if (Rod == RodType.Defense) {
                DefenseAction = ActionType.MOVEROD;
                DefenseActionParameters = new List<int>(new int[] { (int)DIRECTION.LEFT });
            } else {
                OffenceAction = ActionType.MOVEROD;
                OffenceActionParameters = new List<int>(new int[] { (int)DIRECTION.LEFT });
            }
        }
    }

    public void NoAction(RodType Rod) {
        if (Rod == RodType.Defense) {
            DefenseAction = ActionType.NO_ACTION;
            DefenseActionParameters = null;
        } else {
            OffenceAction = ActionType.NO_ACTION;
            OffenceActionParameters = null;
        }
    }

    public void Kick(RodType Rod, DIRECTION KickDirection, int Power) {
        if (Rod == RodType.Defense) {
            DefenseAction = ActionType.KICK;
            DefenseActionParameters = new List<int>(new int[] { (int)KickDirection, Power });
        } else {
            OffenceAction = ActionType.KICK;
            OffenceActionParameters = new List<int>(new int[] { (int)KickDirection, Power });
        }
    }

}
