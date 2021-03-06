﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FieldModel {
    // Field properties
    public int Length { get; private set; }
    public int Width { get; private set; }

    // Rod object
    public RodModel DefenseRodHost { get; private set; }
    public RodModel OffenseRodHost { get; private set; }
    public RodModel DefenseRodGuest { get; private set; }
    public RodModel OffenseRodGuest { get; private set; }

    //Ball object
    public BallModel Ball { get; private set; }

    public FieldModel() {
        Length = GameSettings.FieldLength;
        Width = GameSettings.FieldWidth;

        DefenseRodHost = new RodModel(PlayerType.Host, RodType.Defense, GameSettings.DefenseRodHostTag, GameSettings.DefenseRodHostColumn);
        OffenseRodHost = new RodModel(PlayerType.Host, RodType.Offense, GameSettings.OffenseRodHostTag, GameSettings.OffenseRodHostColumn);
        DefenseRodGuest = new RodModel(PlayerType.Guest, RodType.Defense, GameSettings.DefenseRodGuestTag, GameSettings.DefenseRodGuestColumn);
        OffenseRodGuest = new RodModel(PlayerType.Guest, RodType.Offense, GameSettings.OffenseRodGuestTag, GameSettings.OffenseRodGuestColumn);

        Ball = new BallModel(Width / 2, Length / 2);
    }

    /*
     * Responsible for drawing the fields and the rods.
     * May be unnecessary.
    */
    public void Draw() {
        //Draw Rods
        OffenseRodHost.Draw();
        DefenseRodHost.Draw();
        OffenseRodGuest.Draw();
        DefenseRodGuest.Draw();

        //Draw Ball
        Ball.Draw();
    }
}