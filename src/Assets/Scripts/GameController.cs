using System;
using UnityEngine;
using CielaSpike;
using System.Collections;

public class GameController : MonoBehaviour {

    private GameModel Model { get; set; }
    private DateTime PreviousTime { get; set; }
    private Double TimeStep = 2000;
    private bool GameStarted = false;

    void start()
    {
       
    }

    // Use this for initialization
    public void StartGame (PlayerType Player) {
        Model = new GameModel(Player);
        GameStarted = true;
        this.StartCoroutineAsync(ReceivingRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStarted)
        {
            if (Model.Player == PlayerType.Host && DateTime.Now.Subtract(PreviousTime).TotalMilliseconds >= TimeStep)
            {
                PreviousTime = DateTime.Now;
                this.StartCoroutineAsync(Model.GameRoutine());
            }
        }
    }

    IEnumerator ReceivingRoutine()
    {
        while (true)
        {
            bool receivedNewStep = false;
            try
            {
                receivedNewStep = Communication.ReceiveCommand(Communication.nwStream, Communication.client);
            }
            catch
            {
                continue;
            }
            yield return Ninja.JumpToUnity;
            if (receivedNewStep)
            {
                // It is a new step
                // Start the game routine
                this.StartCoroutineAsync(Model.GameRoutine());
            }
            yield return Ninja.JumpBack;
        }
    }
}   
