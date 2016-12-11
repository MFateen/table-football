using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RandomAgent
{

    public static void MakeDecision(FieldModel Field, PlayerType Player)
    {
        System.Random RandomGenerator = new System.Random();
        double RndNumber;
        int ActionsNum = 6;
        int randomPower;
        Command command = new Command(Player);
        // Insert agent's logic and save the command in the shared memory class
        if (Player == PlayerType.Host)
        {
            //MakeHostDecision(Field);
            // Possible actions for deffense rod: no action - move up - down - kick front - left - RIGHT
            // Random number generation: min + (rng.NextDouble() * (max - min));
            RndNumber = RandomGenerator.NextDouble();// random number between 0 and 1
            randomPower = RandomGenerator.Next(0, 5);
            // for DEFENSIVE ROD
            if (RndNumber >= (1.0 / ActionsNum) && RndNumber < (2.0 / ActionsNum))
                command.MoveUp(RodType.Defense);// Command move up
            if (RndNumber >= (2.0 / ActionsNum) && RndNumber < (3.0 / ActionsNum))
                command.MoveDown(RodType.Defense);// Command move down
            if (RndNumber >= (3.0 / ActionsNum) && RndNumber < (4.0 / ActionsNum))
                command.Kick(RodType.Defense, DIRECTION.FORWARD, 5);// COmmand Kick front
            if (RndNumber >= (4.0 / ActionsNum) && RndNumber < (5.0 / ActionsNum))
                command.Kick(RodType.Defense, DIRECTION.RIGHT, 5);// Command Kick right
            if (RndNumber >= (5.0 / ActionsNum) && RndNumber < (6.0 / ActionsNum))
                command.Kick(RodType.Defense, DIRECTION.LEFT, 5);// Command Kick left 

            // regenerate random number for OFFENSIVE ROD move
            RndNumber = RandomGenerator.NextDouble();
            randomPower = RandomGenerator.Next(0, 5);
            if (RndNumber >= (1.0 / ActionsNum) && RndNumber < (2.0 / ActionsNum))
                command.MoveUp(RodType.Offense);// Command move up
            if (RndNumber >= (2.0 / ActionsNum) && RndNumber < (3.0 / ActionsNum))
                command.MoveDown(RodType.Offense);// Command move down
            if (RndNumber >= (3.0 / ActionsNum) && RndNumber < (4.0 / ActionsNum))
                command.Kick(RodType.Offense, DIRECTION.FORWARD, 5);// COmmand Kick front
            if (RndNumber >= (4.0 / ActionsNum) && RndNumber < (5.0 / ActionsNum))
                command.Kick(RodType.Offense, DIRECTION.RIGHT, 5);// Command Kick right
            if (RndNumber >= (5.0 / ActionsNum) && RndNumber < (6.0 / ActionsNum))
                command.Kick(RodType.Offense, DIRECTION.LEFT, 5);// Command Kick left 

        }
        else if(Field.Ball.Column!=5)
        {// if guest
            RndNumber = RandomGenerator.NextDouble();// random number between 0 and 1
            randomPower = RandomGenerator.Next(0, 5);
            // for DEFENSIVE ROD
            if (RndNumber >= (1.0 / ActionsNum) && RndNumber < (2.0 / ActionsNum))
                command.MoveUp(RodType.Defense);// Command move up
            if (RndNumber >= (2.0 / ActionsNum) && RndNumber < (3.0 / ActionsNum))
                command.MoveDown(RodType.Defense);// Command move down
            if (RndNumber >= (3.0 / ActionsNum) && RndNumber < (4.0 / ActionsNum))
                command.Kick(RodType.Defense, DIRECTION.FORWARD, 5);// COmmand Kick front
            if (RndNumber >= (4.0 / ActionsNum) && RndNumber < (5.0 / ActionsNum))
                command.Kick(RodType.Defense, DIRECTION.RIGHT, 5);// Command Kick right
            if (RndNumber >= (5.0 / ActionsNum) && RndNumber < (6.0 / ActionsNum))
                command.Kick(RodType.Defense, DIRECTION.LEFT, 5);// Command Kick left 

            // regenerate random number for OFFENSIVE ROD move
            RndNumber = RandomGenerator.NextDouble();
            randomPower = RandomGenerator.Next(0, 5);
            if (RndNumber >= (1.0 / ActionsNum) && RndNumber < (2.0 / ActionsNum))
                command.MoveUp(RodType.Offense);// Command move up
            if (RndNumber >= (2.0 / ActionsNum) && RndNumber < (3.0 / ActionsNum))
                command.MoveDown(RodType.Offense);// Command move down
            if (RndNumber >= (3.0 / ActionsNum) && RndNumber < (4.0 / ActionsNum))
                command.Kick(RodType.Offense, DIRECTION.FORWARD, 5);// COmmand Kick front
            if (RndNumber >= (4.0 / ActionsNum) && RndNumber < (5.0 / ActionsNum))
                command.Kick(RodType.Offense, DIRECTION.RIGHT, 5);// Command Kick right
            if (RndNumber >= (5.0 / ActionsNum) && RndNumber < (6.0 / ActionsNum))
                command.Kick(RodType.Offense, DIRECTION.LEFT, 5);// Command Kick left 
        }

        SharedMemory.Decision = command;

        // TODO: Send Cmnd !!!!!
    }
}
