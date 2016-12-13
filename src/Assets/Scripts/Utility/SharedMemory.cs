using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class SharedMemory
{
    public volatile static Command PlayerCommand = null;
    public volatile static Command EnemyCommand1 = null;
    public volatile static Command EnemyCommand2 = null;
}