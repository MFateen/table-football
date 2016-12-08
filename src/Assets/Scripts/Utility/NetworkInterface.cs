using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class NetworkInterface {
    public static bool SendCommand(Command command) {
        return true;
    }

    public static Command ReceiveCommand() {
        return new Command("", null, "");
    }
}