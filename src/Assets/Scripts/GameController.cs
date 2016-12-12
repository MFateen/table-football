using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameModel Model { get; set; }
    private bool GameStarted { get; set; }

    void start()
    {
        GameStarted = false;
    }

    // Use this for initialization
    public void StartGame (PlayerType Player) {
        Model = new GameModel(Player, 3);
        Thread Rec = new Thread(() => Communication.Receive_Command(Communication.nwStream, Communication.client));
        Rec.Start();
        GameStarted = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (GameStarted)
        {
            Model.GameLoop();
        }
    }
}   
