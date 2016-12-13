using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using CielaSpike;

public class GameModel
{
    public PlayerType Player { private set; get; }
    public int HostScore { private set; get; }
    public int GuestScore { private set; get; }
    public FieldModel Field { private set; get; }
    ScoreBoardView scoreBoardView;

    public GameModel(PlayerType _Player, ScoreBoardView _scoreBoardView)
    {
        Player = _Player;
        HostScore = GuestScore = 0;
        Field = new FieldModel();
        scoreBoardView = _scoreBoardView;
    }

    public void UpdateStateHostKick(Command decision)
    {
        if (decision == null)
        {
            return;
        }
        if (decision.OffenceAction == ActionType.KICK)
        {
            int power = decision.OffenceActionParameters[0];
            int direction = decision.OffenceActionParameters[1];
            

            Field.OffenseRodHost.Kick(direction, power, Field.Ball);
        }
        if (decision.DefenseAction == ActionType.KICK)
        {
            int power = decision.DefenseActionParameters[0];
            int direction = decision.DefenseActionParameters[1];
            

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
            int power = decision.OffenceActionParameters[0];
            int direction = decision.OffenceActionParameters[1];
            

            Field.OffenseRodGuest.Kick(direction, power, Field.Ball);
        }
        /// Defense Rod
        if (decision.DefenseAction == ActionType.KICK)
        {
            int power = decision.DefenseActionParameters[0];
            int direction = decision.DefenseActionParameters[1];
            

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
    public IEnumerator GameRoutine()
    {
        if(Player == PlayerType.Host)
        {
            Communication.SendNewStep(Communication.nwStream);
        }

        // 1- Perform previous commands
        //// Do Kicks First
        if (Player == PlayerType.Guest)
        {
            UpdateStateGuestKick(SharedMemory.PlayerCommand);
            UpdateStateHostKick(SharedMemory.EnemyCommand1);
            UpdateStateHostKick(SharedMemory.EnemyCommand2);
            //// Move Ball
            int goal = Field.Ball.Move(Field);
            if(goal == -1)
            {
                //Goal in host
                GuestScore++;
            } else if(goal == 1)
            {
                HostScore++;
            }
            //// Do other actions
            UpdateStateGuestNoKick(SharedMemory.PlayerCommand);
            UpdateStateHostNoKick(SharedMemory.EnemyCommand1);
            UpdateStateHostNoKick(SharedMemory.EnemyCommand2);
        }
        else
        {
            UpdateStateHostKick(SharedMemory.PlayerCommand);
            UpdateStateGuestKick(SharedMemory.EnemyCommand1);
            UpdateStateGuestKick(SharedMemory.EnemyCommand2);
            //// Move Ball
            int goal = Field.Ball.Move(Field);
            if (goal == -1)
            {
                //Goal in host
                GuestScore++;
            }
            else if (goal == 1)
            {
                HostScore++;
            }
            //// Do other actions
            UpdateStateHostNoKick(SharedMemory.PlayerCommand);
            UpdateStateGuestNoKick(SharedMemory.EnemyCommand1);
            UpdateStateGuestNoKick(SharedMemory.EnemyCommand2);
        }

        // 3- Make Decision
        //// Make Decision
        IntelligentAgent.MakeDecision(Field, Player);
        ////// SharedMemory.PlayerCommand = new Command(Player);

        // 4- Send decision in background
        Communication.SendCommand(SharedMemory.PlayerCommand, Communication.nwStream);

        // 2- Draw current state
        yield return Ninja.JumpToUnity;
        Draw();
        yield return Ninja.JumpBack;

    }

    public void Draw()
    {
        Field.Draw();
        scoreBoardView.Draw(HostScore, GuestScore);
    }
}
