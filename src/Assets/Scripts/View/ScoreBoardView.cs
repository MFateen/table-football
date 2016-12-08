using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardView : MonoBehaviour {

    public Text BlueScore, RedScore;
	
    public void Draw(int _RedScore, int _BlueScore)
    {
        RedScore.text = _RedScore.ToString();
        BlueScore.text = _BlueScore.ToString();
    }

}
