using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class GameSettings {
    public static int FieldLength { get { return 11; } }
    public static int FieldWidth { get { return 7; } }
    public static int DefenseRodHostColumn { get { return 1; } }
    public static int OffenseRodHostColumn { get { return 6; } }
    public static int DefenseRodGuestColumn { get { return 9; } }
    public static int OffenseRodGuestColumn { get { return 4; } }
    public static string DefenseRodHostTag { get { return "DefenseRodHost"; } }
    public static string OffenseRodHostTag { get { return "OffenseRodHost"; } }
    public static string DefenseRodGuestTag { get { return "DefenseRodGuest"; } }
    public static string OffenseRodGuestTag { get { return "OffenseRodGuest"; } }
    public static int RodFirstPlayerRelativePosition { get { return 1; } }
    public static int RodSecondPlayerRelativePosition { get { return 3; } }
    public static int RodThirdPlayerRelativePosition { get { return 5; } }
}
