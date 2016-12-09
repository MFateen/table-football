using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RandomAgent {
    System.Random RandomGenerator = new System.Random();
    double RndNumber;
    int ActionsNum = 6;
    int randomPower;
    public static void MakeDecision(FieldModel Field, PlayerType Player) {
        // Insert agent's logic and save the command in the shared memory class
        if (Player == PlayerType.Host) {
            //MakeHostDecision(Field);
            // Possible actions for deffense rod: no action - move up - down - kick front - left - RIGHT
            // Random number generation: min + (rng.NextDouble() * (max - min));
            RndNumber = RandomGenerator.NextDouble();// random number between 0 and 1
            randomPower = RandomGenerator.next(0,5);
            // for DEFENSIVE ROD
            if(RndNumber<(1.0/ActionsNum))
                ;// Command = no action
            if(RndNumber>=(1.0/ActionsNum) && RndNumber<(2.0/ActionsNum))
                ;// Command move up 
            if(RndNumber>=(2.0/ActionsNum) && RndNumber<(3.0/ActionsNum))
                ;// Command move down
            if(RndNumber>=(3.0/ActionsNum) && RndNumber<(4.0/ActionsNum))
                ;// COmmand Kick front
            if(RndNumber>=(4.0/ActionsNum) && RndNumber<(5.0/ActionsNum))
                ;// Command Kick right
            if(RndNumber>=(5.0/ActionsNum) && RndNumber<(6.0/ActionsNum))
                ;// Command Kick left 
            
            // regenerate random number for OFFENSIVE ROD move
            RndNumber = RandomGenerator.NextDouble();
            randomPower = RandomGenerator.next(0,5);
            if(RndNumber<(1.0/ActionsNum))
                ;// Command = no action
            if(RndNumber>=(1.0/ActionsNum) && RndNumber<(2.0/ActionsNum))
                ;// Command move up 
            if(RndNumber>=(2.0/ActionsNum) && RndNumber<(3.0/ActionsNum))
                ;// Command move down
            if(RndNumber>=(3.0/ActionsNum) && RndNumber<(4.0/ActionsNum))
                ;// COmmand Kick front
            if(RndNumber>=(4.0/ActionsNum) && RndNumber<(5.0/ActionsNum))
                ;// Command Kick right
            if(RndNumber>=(5.0/ActionsNum) && RndNumber<(6.0/ActionsNum))
                ;// Command Kick left  
            
        } else {// if guest
            RndNumber = RandomGenerator.NextDouble();// random number between 0 and 1
            randomPower = RandomGenerator.next(0,5);
            // for DEFENSIVE ROD
            if(RndNumber<(1.0/ActionsNum))
                ;// Command = no action
            if(RndNumber>=(1.0/ActionsNum) && RndNumber<(2.0/ActionsNum))
                ;// Command move up 
            if(RndNumber>=(2.0/ActionsNum) && RndNumber<(3.0/ActionsNum))
                ;// Command move down
            if(RndNumber>=(3.0/ActionsNum) && RndNumber<(4.0/ActionsNum))
                ;// COmmand Kick front
            if(RndNumber>=(4.0/ActionsNum) && RndNumber<(5.0/ActionsNum))
                ;// Command Kick right
            if(RndNumber>=(5.0/ActionsNum) && RndNumber<(6.0/ActionsNum))
                ;// Command Kick left 
            
            // regenerate random number for OFFENSIVE ROD move
            RndNumber = RandomGenerator.NextDouble();
            randomPower = RandomGenerator.next(0,5);
            if(RndNumber<(1.0/ActionsNum))
                ;// Command = no action
            if(RndNumber>=(1.0/ActionsNum) && RndNumber<(2.0/ActionsNum))
                ;// Command move up 
            if(RndNumber>=(2.0/ActionsNum) && RndNumber<(3.0/ActionsNum))
                ;// Command move down
            if(RndNumber>=(3.0/ActionsNum) && RndNumber<(4.0/ActionsNum))
                ;// COmmand Kick front
            if(RndNumber>=(4.0/ActionsNum) && RndNumber<(5.0/ActionsNum))
                ;// Command Kick right
            if(RndNumber>=(5.0/ActionsNum) && RndNumber<(6.0/ActionsNum))
                ;// Command Kick left  
        }

        // TODO: Send Cmnd !!!!!
        SharedMemory.Decision = new Command("", null, "");
    }
}