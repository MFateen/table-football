using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

public class GameModel
{
    public PlayerType Player { private set; get; }
    public int HostScore { private set; get; }
    public int GuestScore { private set; get; }
    public int TimeStep { private set; get; }
    public FieldModel Field { private set; get; }

    public GameModel(PlayerType _Player, int _TimeStep)
    {
        Player = _Player;
        HostScore = GuestScore = 0;
        TimeStep = _TimeStep;
        Field = new FieldModel();
    }

    public void UpdateStateHostKick(Command decision)
    {
        if (decision == null)
        {
            return;
        }
        if (decision.OffenceAction == ActionType.KICK)
        {
            int direction = decision.OffenceActionParameters[0];
            int power = decision.OffenceActionParameters[1];

            Field.OffenseRodHost.Kick(direction, power, Field.Ball);
        }
        if (decision.DefenseAction == ActionType.KICK)
        {
            int direction = decision.DefenseActionParameters[0];
            int power = decision.DefenseActionParameters[1];

            Field.DefenseRodHost.Kick(direction, power, Field.Ball);
        }
    }

    public void UpdateStateHostNoKick(Command decision)
    {
        if (decision == null)
        {
            return;
        }
        if (decision.OffenceAction == ActionType.MOVEROD)
        {
            if (decision.OffenceActionParameters[0] == -1)
            {
                Field.OffenseRodHost.MoveUp();
            }
            else
            {
                Field.OffenseRodHost.MoveDown();
            }
        }
        else if (decision.OffenceAction == ActionType.NO_ACTION)  // no action
        {
            ReboundDirection R = Field.OffenseRodHost.ShouldRebound(Player, Field.Ball);
            if (R != ReboundDirection.NoRebound)
            {
                Field.OffenseRodHost.Kick((int)R, 2, Field.Ball);
            }
        }

        /// Defense Rod
        if (decision.DefenseAction == ActionType.MOVEROD)
        {
            if (decision.DefenseActionParameters[0] == -1)
            {
                Field.DefenseRodHost.MoveUp();
            }
            else
            {
                Field.DefenseRodHost.MoveDown();
            }
        }
        else if (decision.DefenseAction == ActionType.NO_ACTION)  // no action
        {
            ReboundDirection R = Field.DefenseRodHost.ShouldRebound(Player, Field.Ball);
            if (R != ReboundDirection.NoRebound)
            {
                Field.DefenseRodHost.Kick((int)R, 2, Field.Ball);
            }
        }

    }
    public void UpdateStateGuestKick(Command decision)
    {
        if (decision == null)
        {
            return;
        }
        /// Offence Rod
        if (decision.OffenceAction == ActionType.KICK)
        {
            int direction = decision.OffenceActionParameters[0];
            int power = decision.OffenceActionParameters[1];

            Field.OffenseRodGuest.Kick(direction, power, Field.Ball);
        }
        /// Defense Rod
        if (decision.DefenseAction == ActionType.KICK)
        {
            int direction = decision.DefenseActionParameters[0];
            int power = decision.DefenseActionParameters[1];

            Field.DefenseRodGuest.Kick(direction, power, Field.Ball);
        }
    }
    public void UpdateStateGuestNoKick(Command decision)
    {
        if (decision == null)
        {
            return;
        }
        if (decision.OffenceAction == ActionType.MOVEROD)
        {
            if (decision.OffenceActionParameters[0] == -1)
            {
                Field.OffenseRodGuest.MoveUp();
            }
            else
            {
                Field.OffenseRodGuest.MoveDown();
            }
        }
        else if (decision.OffenceAction == ActionType.NO_ACTION)  // no action
        {
            ReboundDirection R = Field.OffenseRodGuest.ShouldRebound(Player, Field.Ball);
            if (R != ReboundDirection.NoRebound)
            {
                Field.OffenseRodGuest.Kick((int)R, 2, Field.Ball);
            }
        }

        if (decision.DefenseAction == ActionType.MOVEROD)
        {
            if (decision.DefenseActionParameters[0] == -1)
            {
                Field.DefenseRodGuest.MoveUp();
            }
            else
            {
                Field.DefenseRodGuest.MoveDown();
            }
        }
        else if (decision.DefenseAction == ActionType.NO_ACTION)  // no action
        {
            ReboundDirection R = Field.DefenseRodGuest.ShouldRebound(Player, Field.Ball);
            if (R != ReboundDirection.NoRebound)
            {
                Field.DefenseRodGuest.Kick((int)R, 2, Field.Ball);
            }
        }
    }



    /*
     * The main infinite game loop
     */
    public void GameLoop()
    {
        if (Player == PlayerType.Host)
        {
            GameLoopHost();
        }
        else
        {
            GameLoopGuest();
        }
    }

    public void GameLoopHost()
    {
        //Send new step
        Communication.Send_New_Step(Communication.nwStream);

        //Get previous commands
        ////Player command
        Command PlayerCommand = null;
        Command EnemyCommand1 = null;
        Command EnemyCommand2 = null;
        if (SharedMemory.PlayerCommands.Count > 0)
        {
            PlayerCommand = SharedMemory.PlayerCommands.Dequeue();
        }
        ////Enemy commands
        if (SharedMemory.EnemyCommands.Count > 0)
        {
            EnemyCommand1 = SharedMemory.EnemyCommands.Dequeue();
        }
        if (SharedMemory.EnemyCommands.Count > 0)
        {
            EnemyCommand2 = SharedMemory.EnemyCommands.Dequeue();
        }

        //Do Kicks First
        UpdateStateHostKick(PlayerCommand);
        UpdateStateGuestKick(EnemyCommand1);
        UpdateStateGuestKick(EnemyCommand2);

        // Move Ball
        Field.Ball.Move(Field);

        // Do other actions
        UpdateStateHostNoKick(PlayerCommand);
        UpdateStateGuestNoKick(EnemyCommand1);
        UpdateStateGuestNoKick(EnemyCommand2);

        // Draw current state
        Field.Draw();

        // Make Decision
        //IntelligentAgent.MakeDecision(Field, Player);
        SharedMemory.Decision = new Command(Player);

        // Save it for next time
        SharedMemory.PlayerCommands.Enqueue(SharedMemory.Decision);

        // Send Decision
        //Communication.Send_Command(SharedMemory.Decision, Communication.nwStream);

        Thread.Sleep(1000);
    }

    public void GameLoopGuest()
    {
        //Wait for new step to be received
        while (!SharedMemory.NewStepReceived) ;

        //Get previous commands
        ////Player command
        Command PlayerCommand = null;
        Command EnemyCommand1 = null;
        Command EnemyCommand2 = null;
        if (SharedMemory.PlayerCommands.Count > 0)
        {
            PlayerCommand = SharedMemory.PlayerCommands.Dequeue();
        }
        ////Enemy commands
        if (SharedMemory.EnemyCommands.Count > 0)
        {
            EnemyCommand1 = SharedMemory.EnemyCommands.Dequeue();
        }
        if (SharedMemory.EnemyCommands.Count > 0)
        {
            EnemyCommand2 = SharedMemory.EnemyCommands.Dequeue();
        }

        //Do Kicks First
        UpdateStateGuestKick(PlayerCommand);
        UpdateStateHostKick(EnemyCommand1);
        UpdateStateHostKick(EnemyCommand2);

        // Move Ball
        Field.Ball.Move(Field);

        // Do other actions
        UpdateStateGuestNoKick(PlayerCommand);
        UpdateStateHostNoKick(EnemyCommand1);
        UpdateStateHostNoKick(EnemyCommand2);

        // Draw current state
        Field.Draw();

        // Make Decision
        //IntelligentAgent.MakeDecision(Field, Player);

        SharedMemory.Decision = new Command(Player);

        // Save it for next time
        SharedMemory.PlayerCommands.Enqueue(SharedMemory.Decision);

        // Send Decision
        //Communication.Send_Command(SharedMemory.Decision, Communication.nwStream);

        SharedMemory.NewStepReceived = false;
    }

    public void Draw()
    {
        Field.Draw();
    }
}
