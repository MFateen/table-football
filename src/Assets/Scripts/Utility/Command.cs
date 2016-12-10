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

    public Command(PlayerType _Player, ActionType _Offence, List<int> _OffenceParameters, ActionType _Defense, List<int> _DefenseParameters)
    {
        Player = _Player;
        OffenceAction = _Offence;
        OffenceActionParameters = _OffenceParameters;
        DefenseAction = _Defense;
        DefenseActionParameters = _DefenseParameters;
    }
}
