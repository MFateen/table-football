using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Command
{
    public string name { private set; get; }
    public List<int> parameters { private set; get; }
    public string player { private set; get; }

    public Command(string name, List<int> parameters, string player)
    {
        this.name = name;
        this.player = player;
        this.parameters = parameters;
    }
}