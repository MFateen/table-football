using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

//FUNCTIONS PARSE COMMAND, SEND COMMAND
//SHOULD BE STRONGLY REVISED
//ASSUMPTIONS WERE MADE WHEN DEALING WITH THE OBJECT COMMAND
/*
        action type is string: kick,move,no_action
 *      player type is boolean

 * 
 * 
*/
//to retrieve a command , dequeue from the command queue

/// <summary>
/// LOTS OF TEST PRINTS, CALLS ARE USED FOR THE SAKE OF TESTING
/// </summary>


static class Communication
{
    static string new_step = "new_step";
    static Logger logger = new Logger();
    static int EnemyCommandIdx = 0;
    public static string ip;
    public static TcpClient client;
    public static NetworkStream nwStream;

    public static void SendCommand(Command c, NetworkStream N)
    {

        //ACCESSING COMMAND PARAMETERS SHOULD BE REVISED!!

        string defense_command = "", offence_command = "";
        if (c.DefenseAction == ActionType.MOVEROD)
        {

            defense_command += -1*c.DefenseActionParameters[0];

            if (c.Player == PlayerType.Guest)//guest
            {
                defense_command += " 4";
            }
            else
            {
                defense_command += " 1";
            }

        }
        else if (c.DefenseAction == ActionType.KICK)
        {
            defense_command += "kick ";

            if (c.Player == PlayerType.Guest)//guest
            {
                defense_command += "4 ";
            }
            else
            {
                defense_command += "1 ";
            }

            defense_command += c.DefenseActionParameters[0]; //power

            defense_command += " ";
            //direction
            defense_command += -1*c.DefenseActionParameters[1];

        }
        else
        {
            defense_command += "no_action ";
            if (c.Player == PlayerType.Guest)//guest
            {
                defense_command += "4";
            }
            else
            {
                defense_command += "1";
            }
        }


        //ATTACK COMMAND

        if (c.OffenceAction == ActionType.MOVEROD)
        {

            offence_command += -1*c.OffenceActionParameters[0];

            if (c.Player == PlayerType.Guest)//guest
            {
                offence_command += " 2";
            }
            else
            {
                offence_command += " 3";
            }

        }
        else if (c.OffenceAction == ActionType.KICK)
        {
            offence_command += "kick ";

            if (c.Player == PlayerType.Guest)//guest
            {
                offence_command += "2 ";
            }
            else
            {
                offence_command += "3 ";
            }

            offence_command += c.OffenceActionParameters[0]; //power

            offence_command += " ";
            //direction
            offence_command += -1*c.OffenceActionParameters[1];

        }
        else
        {
            offence_command += "no_action ";
            if (c.Player == PlayerType.Guest)//guest
            {
                offence_command += "2";
            }
            else
            {
                offence_command += "3";
            }
        }
        char character = (char)defense_command.Length;
        defense_command = character + defense_command;
        byte[] x = ASCIIEncoding.ASCII.GetBytes(defense_command);
        N.Write(x, 0, x.Length);
        logger.Log("logsent.txt", defense_command, LogType.Send);

        character = (char)offence_command.Length;
        offence_command = character + offence_command;
        x = ASCIIEncoding.ASCII.GetBytes(offence_command);
        N.Write(x, 0, x.Length);
        logger.Log("logsent.txt", offence_command, LogType.Send);
    }

    static bool ParseMessage(string msg)
    {
        bool newStepReceived = false;
        while (msg.Length > 0)
        {
            msg = msg.Substring(1);
            logger.Log("logreceived.txt", msg, LogType.Receive);
            Command c = new Command();

            if (msg[0] == 'k')
            {

                if (msg[5] == '1' || msg[5] == '4') //defense rod
                {
                    c.DefenseAction = ActionType.KICK;


                    c.DefenseActionParameters.Add((int)msg[7] - 48);
                    if (msg[9] == '-')
                    {
                        c.DefenseActionParameters.Add(1);
                        logger.Log("logreceived.txt", msg.Substring(0, 11), LogType.Receive);
                        msg = msg.Substring(11);
                    }
                    else
                    {
                        c.DefenseActionParameters.Add(-1*((int)msg[9] - 48));
                        logger.Log("logreceived.txt", msg.Substring(0, 10), LogType.Receive);
                        msg = msg.Substring(10);
                    }
                }
                else
                {
                    c.OffenceAction = ActionType.KICK;
                    c.OffenceActionParameters.Add((int)msg[7] - 48);
                    if (msg[9] == '-')
                    {
                        c.OffenceActionParameters.Add(1);
                        logger.Log("logreceived.txt", msg.Substring(0, 11), LogType.Receive);
                        msg = msg.Substring(11);
                    }
                    else
                    {
                        c.OffenceActionParameters.Add(-1*((int)msg[9] - 48));
                        logger.Log("logreceived.txt", msg.Substring(0, 10), LogType.Receive);
                        msg = msg.Substring(10);
                    }
                }
            }
            else if (msg[0] == 'n' && msg[1] == 'o') //NO ACTION
            {
                if (msg[10] == '1' || msg[10] == '4')
                {
                    c.DefenseAction = ActionType.NO_ACTION;
                }
                else
                {
                    c.OffenceAction = ActionType.NO_ACTION;
                }
                logger.Log("logreceived.txt", msg.Substring(0, 11), LogType.Receive);
                msg = msg.Substring(11);
            }

            else if (msg[0] != 'n') //not new time step move only
            {
                if (msg[2] == '1' || msg[2] == '4') //defense rod
                {
                    c.DefenseAction = ActionType.MOVEROD;
                    c.DefenseActionParameters.Add(-1*((int)msg[0] - 48));
                    logger.Log("logreceived.txt", msg.Substring(0, 3), LogType.Receive);
                    msg = msg.Substring(3);
                }
                else if (msg[2] == ' ' && (msg[3] == '1' || msg[3] == '4'))
                {
                    c.DefenseAction = ActionType.MOVEROD;
                    c.DefenseActionParameters.Add(1);
                    logger.Log("logreceived.txt", msg.Substring(0, 4), LogType.Receive);
                    msg = msg.Substring(4);
                }
                else if (msg[2] != ' ')
                {
                    c.OffenceAction = ActionType.MOVEROD;
                    c.OffenceActionParameters.Add(-1*((int)msg[0] - 48));
                    logger.Log("logreceived.txt", msg.Substring(0, 3), LogType.Receive);
                    msg = msg.Substring(3);
                }
                else
                {
                    c.OffenceAction = ActionType.MOVEROD;
                    c.OffenceActionParameters.Add(1);
                    logger.Log("logreceived.txt", msg.Substring(0, 4), LogType.Receive);
                    msg = msg.Substring(4);
                }

            }

            if (msg.Length > 0 && msg[0] == 'n' && msg[1] == 'e') //newstep...unblock!
            {
                newStepReceived = true;
                logger.Log("logreceived.txt", msg.Substring(0, 8), LogType.Receive);
                msg = msg.Substring(8);
                continue;
            }
            if (EnemyCommandIdx == 0)
            {
                SharedMemory.EnemyCommand1 = c;
            }
            else
            {
                SharedMemory.EnemyCommand2 = c;
            }
            EnemyCommandIdx = (EnemyCommandIdx + 1) % 2;
        }
        return newStepReceived;
    }

    public static bool ReceiveCommand(NetworkStream N, TcpClient client)
    {
        string msg = "";
        byte[] buffer = new byte[client.ReceiveBufferSize];
        int bytesRead = N.Read(buffer, 0, client.ReceiveBufferSize);
        msg = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        return ParseMessage(msg);
    }

    public static void SendNewStep(NetworkStream N)
    {
        char character = (char)new_step.Length;
        string n = character + new_step;
        byte[] x = ASCIIEncoding.ASCII.GetBytes(n);
        N.Write(x, 0, x.Length);
        logger.Log("logsent.txt", new_step, LogType.Send);
    }

    public static void HostConnect(string SERVER_IP)
    {
        IPAddress localAdd = IPAddress.Parse("192.168.1.7");
        TcpListener listener = new TcpListener(localAdd, 3000);
        listener.Start();
        client = listener.AcceptTcpClient();
        nwStream = client.GetStream();
    }

    public static void ClientConnect(string SERVER_IP)
    {
        client = new TcpClient(SERVER_IP, 3000);
        nwStream = client.GetStream();
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var _ip in host.AddressList)
        {
            if (_ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return _ip.ToString();
            }
        }
        throw new Exception("Local IP Address Not Found!");
    }
}