using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private GameModel Model { get; set; }

	// Use this for initialization
	void Start () {
        Model = new GameModel(PlayerType.Host, 3);
	}
	
	// Update is called once per frame
	void Update () {
        Model.GameLoop();
	}
}   
