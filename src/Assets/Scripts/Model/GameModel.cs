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

    public void UpdateState(Command decision) {

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
