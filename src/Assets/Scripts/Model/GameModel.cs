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

    public GameModel(PlayerType _Player, int _TimeStep) {
        Player = _Player;
        HostScore = GuestScore = 0;
        TimeStep = _TimeStep;
        Field = new FieldModel();
    }

    public void UpdateState(Command decision)
    {
        if (Player == PlayerType.Host)
        {
            UpdateStateHost(decision);
        }
        else
        {
            UpdateStateGuest(decision);
        }
    }

    public void UpdateStateHost(Command decision)
    {
        /// Offence Rod
        if (decision.OffenceAction == ActionType.KICK)
        {
            int direction = decision.OffenceActionParameters[0];
            int power = decision.OffenceActionParameters[1];

            Field.OffenseRodHost.Kick(direction, power, Field.Ball);
        }
        else if (decision.OffenceAction == ActionType.MOVEROD)
        {
            if (decision.OffenceActionParameters[0] == 1)
            {
                Field.OffenseRodHost.MoveUp();
            }
            else
            {
                Field.OffenseRodHost.MoveDown();
            }
        }
        else  // no action
        {
            ReboundDirection R = Field.OffenseRodHost.ShouldRebound(Player, Field.Ball);
            if (R != ReboundDirection.NoRebound)
            {
                Field.OffenseRodHost.Kick((int)R, 2, Field.Ball);
            }
        }

        /// Defense Rod
        if (decision.DefenseAction == ActionType.KICK)
        {
            int direction = decision.DefenseActionParameters[0];
            int power = decision.DefenseActionParameters[1];

            Field.DefenseRodHost.Kick(direction, power, Field.Ball);
        }
        else if (decision.DefenseAction == ActionType.MOVEROD)
        {
            if (decision.DefenseActionParameters[0] == 1)
            {
                Field.DefenseRodHost.MoveUp();
            }
            else
            {
                Field.DefenseRodHost.MoveDown();
            }
        }
        else  // no action
        {
            ReboundDirection R = Field.DefenseRodHost.ShouldRebound(Player, Field.Ball);
            if (R != ReboundDirection.NoRebound)
            {
                Field.DefenseRodHost.Kick((int)R, 2, Field.Ball);
            }
        }

    }

    public void UpdateStateGuest(Command decision)
    {
        /// Offence Rod
        if (decision.OffenceAction == ActionType.KICK)
        {
            int direction = decision.OffenceActionParameters[0];
            int power = decision.OffenceActionParameters[1];

            Field.OffenseRodGuest.Kick(direction, power, Field.Ball);
        }
        else if (decision.OffenceAction == ActionType.MOVEROD)
        {
            if (decision.OffenceActionParameters[0] == 1)
            {
                Field.OffenseRodGuest.MoveUp();
            }
            else
            {
                Field.OffenseRodGuest.MoveDown();
            }
        }
        else  // no action
        {
            ReboundDirection R = Field.OffenseRodGuest.ShouldRebound(Player, Field.Ball);
            if (R != ReboundDirection.NoRebound)
            {
                Field.OffenseRodGuest.Kick((int)R, 2, Field.Ball);
            }
        }

        /// Defense Rod
        if (decision.DefenseAction == ActionType.KICK)
        {
            int direction = decision.DefenseActionParameters[0];
            int power = decision.DefenseActionParameters[1];

            Field.DefenseRodGuest.Kick(direction, power, Field.Ball);
        }
        else if (decision.DefenseAction == ActionType.MOVEROD)
        {
            if (decision.DefenseActionParameters[0] == 1)
            {
                Field.DefenseRodGuest.MoveUp();
            }
            else
            {
                Field.DefenseRodGuest.MoveDown();
            }
        }
        else  // no action
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
        // 1- Draw current state
        Field.Draw();

        // 2- Make Decision from agent based on current state
        //// Start decision making in another thread
        Thread intelligentAgentThread = new Thread(() => IntelligentAgent.MakeDecision(Field, Player));
        intelligentAgentThread.Start();
        //// Wait for the timestep
        Thread.Sleep(TimeStep);
        //// After the timestep if the agent is finished take its decision from the shared memory
        //// and a random decision otherwise
        Command agentDecision;
        if (intelligentAgentThread.IsAlive) {
            intelligentAgentThread.Abort();
            // Random decision
            agentDecision = new Command("", null, "");
        } else {
            agentDecision = SharedMemory.Decision;
        }

        // 3- Send Decision to enemy team
        NetworkInterface.SendCommand(agentDecision);

        // 4- Receive Decision from enemy team
        Command enemyDecision = NetworkInterface.ReceiveCommand();

        // 5- Update state with both decisions
        UpdateState(agentDecision);
        UpdateState(enemyDecision);
    }

    public void Draw() {
        Field.Draw();
    }
}
