using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class SharedMemory
{
    public static PlayerType Player { get; set; }
    public static Command Decision { get; set; }
    public volatile static Queue<Command> PlayerCommands = new Queue<Command>();
    public volatile static Queue<Command> EnemyCommands = new Queue<Command>();
    public volatile static bool NewStepReceived = false;
}