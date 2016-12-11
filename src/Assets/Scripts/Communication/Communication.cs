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


class Communication
{

    const string create_game = "create_game";
    const string start_game = "start_game";
    const string new_step = "new_step";
    static Logger logger = new Logger();



    const int p = 3000;

    const string ip = "192.168.1.5"; ////////////////CHANGE IP HERE!!!!!!!!!

    static int flag = 0; //FLAG FOR BLOCKING
    static int arrcount = 0;


    static Queue<Command> qarr = new Queue<Command>();
    static Command[] arr = new Command[20];
    static TcpClient client;
    static NetworkStream nwStream;

    /*static void Send_command(Command c, NetworkStream N)
       {

           //ACCESSING COMMAND PARAMETERS SHOULD BE REVISED!!



           Console.WriteLine("Recieving defence action "); //for testing
           byte[] x = ASCIIEncoding.ASCII.GetBytes(defense_command);
           N.Write(x, 0, x.Length);

           //Console.WriteLine("Recieving offence action"); //for testing
           //x = ASCIIEncoding.ASCII.GetBytes(offence_command);
           //N.Write(x, 0, x.Length);


           string defense_command = "", offence_command = "";
           if (c.DefenseAction == "move")
           {

               defense_command += c.DefenseActionParameters[0];

               if (c.Player == 0)//guest
               {
                   defense_command += " 4";
               }
               else
               {
                   defense_command += " 1";
               }

           }
           else if (c.DefenseAction == "kick")
           {
               defense_command += "kick ";

               if (c.Player == 0)//guest
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
               defense_command += c.DefenseActionParameters[1];

           }


           //ATTACK COMMANDD

           if (c.OffenceAction == "move")
           {

               offence_command += c.OffenceActionParameters[0];

               if (c.Player == 0)//guest
               {
                   offence_command += " 2";
               }
               else
               {
                   offence_command += " 3";
               }

           }
           else if (c.OffenceAction == "kick")
           {
               offence_command += "kick ";

               if (c.Player == 0)//guest
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
               offence_command += c.OffenceActionParameters[1];

           }



       }*/

    static void Send_Command(Command c, NetworkStream N)
    {

        //ACCESSING COMMAND PARAMETERS SHOULD BE REVISED!!

        string defense_command = "", offence_command = "";
        if (c.DefenseAction == ActionType.MOVEROD)
        {

            defense_command += c.DefenseActionParameters[0];

            if (c.Player == 0)//guest
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

            if (c.Player == 0)//guest
            {
                defense_command += "4 ";
            }
            else
            {
                defense_command += "1 ";
            }

            defense_command += c.DefenseActionParameters[1]; //power

            defense_command += " ";
            //direction
            defense_command += c.DefenseActionParameters[2];

        }
        else
        {
            defense_command += "no_action ";
            if (c.Player == 0)//guest
            {
                defense_command += "4";
            }
            else
            {
                defense_command += "1";
            }
        }


        //ATTACK COMMANDD

        if (c.OffenceAction == ActionType.MOVEROD)
        {

            offence_command += c.OffenceActionParameters[0];

            if (c.Player == 0)//guest
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

            if (c.Player == 0)//guest
            {
                offence_command += "2 ";
            }
            else
            {
                offence_command += "3 ";
            }

            offence_command += c.OffenceActionParameters[1]; //power

            offence_command += " ";
            //direction
            offence_command += c.OffenceActionParameters[2];

        }
        else
        {
            offence_command += "no_action ";
            if (c.Player == 0)//guest
            {
                offence_command += "2";
            }
            else
            {
                offence_command += "3";
            }
        }


        //  defense_command = defense_command + ", " + offence_command; //COMBINED COMMAND

        Console.WriteLine("Sending defense action "); //for testing
        byte[] x = ASCIIEncoding.ASCII.GetBytes(defense_command);
        N.Write(x, 0, x.Length);


        logger.Log("log.txt", defense_command, LogType.Send);

        //            DateTime today = DateTime.Today;

        //          StreamWriter sw = new StreamWriter(path,true);
        //        sw.WriteLine(DateTime.Now.ToString("hh:mm:ss ")+ "sent >>> " + defense_command);

        Console.WriteLine("Sending offence action "); //for testing
        x = ASCIIEncoding.ASCII.GetBytes(offence_command);
        N.Write(x, 0, x.Length);

        logger.Log("log.txt", offence_command, LogType.Send);

        // sw.WriteLine(DateTime.Now.ToString("hh:mm:ss ") + "sent >>> " + offence_command);
        //      sw.Close();


    }

    static Command Parse_Msg(string msg)
    {
        Command c = new Command();
        Console.WriteLine(msg);
        while (msg.Length > 0)
        {



            if (msg[0] == 'k')//first action k;
            {

                if (msg[5] == '1' || msg[5] == '4') //defense rod
                {
                    c.DefenseAction = ActionType.KICK;
                    // c.DefenseActionParameters.Add((int)msg[5] - 48);

                    c.DefenseActionParameters.Add((int)msg[7] - 48);
                    if (msg[9] == '-')
                    {
                        c.DefenseActionParameters.Add(-1);


                        logger.Log("log.txt", msg.Substring(0, 11), LogType.Receive);



                        msg = msg.Substring(11);
                    }
                    else
                    {
                        c.DefenseActionParameters.Add((int)msg[9] - 48);


                        logger.Log("log.txt", msg.Substring(0, 10), LogType.Receive);


                        msg = msg.Substring(10);
                    }
                }
                else
                {
                    c.OffenceAction = ActionType.KICK;
                    //            c.OffenceActionParameters.Add((int)msg[5] - 48);

                    c.OffenceActionParameters.Add((int)msg[7] - 48);
                    if (msg[9] == '-')
                    {
                        c.OffenceActionParameters.Add(-1);

                        logger.Log("log.txt", msg.Substring(0, 11), LogType.Receive);


                        msg = msg.Substring(11);
                    }
                    else
                    {
                        c.OffenceActionParameters.Add((int)msg[9] - 48);

                        logger.Log("log.txt", msg.Substring(0, 10), LogType.Receive);


                        msg = msg.Substring(10);
                    }
                }
            }

            else if (msg[0] == 'n' && msg[1] == 'o') //NO ACTION
            {
                if (msg[10] == '1' || msg[10] == '4')
                {
                    c.DefenseAction = ActionType.NO_ACTION;
                    // c.DefenseActionParameters.Add((int)msg[10] - 48);

                }
                else
                {
                    c.OffenceAction = ActionType.NO_ACTION;
                    //c.OffenceActionParameters.Add((int)msg[10] - 48);
                }

            }


            else if (msg[0] != 'n') //not new time step move only
            {
                if (msg[2] == '1' || msg[2] == '4') //defense rod
                {
                    c.DefenseAction = ActionType.MOVEROD;
                    c.DefenseActionParameters.Add((int)msg[0] - 48);
                    //                     c.DefenseActionParameters.Add((int)msg[2] - 48);


                    logger.Log("log.txt", msg.Substring(0, 3), LogType.Receive);



                    msg = msg.Substring(3);
                }
                else if (msg[2] == ' ' && (msg[3] == '1' || msg[3] == '4'))
                {
                    c.DefenseAction = ActionType.MOVEROD;
                    c.DefenseActionParameters.Add(-1);
                    // c.DefenseActionParameters.Add((int)msg[3] - 48);

                    logger.Log("log.txt", msg.Substring(0, 4), LogType.Receive);


                    msg = msg.Substring(4);
                }
                else if (msg[2] != ' ')
                {
                    c.OffenceAction = ActionType.MOVEROD;
                    c.OffenceActionParameters.Add((int)msg[0] - 48);
                    //                   c.OffenceActionParameters.Add((int)msg[2] - 48);

                    logger.Log("log.txt", msg.Substring(0, 3), LogType.Receive);


                    msg = msg.Substring(3);
                }
                else
                {
                    c.OffenceAction = ActionType.MOVEROD;
                    c.OffenceActionParameters.Add(-1);
                    //                 c.OffenceActionParameters.Add((int)msg[3] - 48);

                    logger.Log("log.txt", msg.Substring(0, 4), LogType.Receive);


                    msg = msg.Substring(4);
                }

            }

            if (msg.Length > 0 && msg[0] == 'n' && msg[1] == 'e') //newstep...unblock!
            {
                flag = 1;
                msg = msg.Substring(8);
            }


            qarr.Enqueue(c);
            arr[arrcount] = c;
            arrcount++;



        }
        return c;
    }

    static void Receive_Command(NetworkStream N, TcpClient client)//client from host//stream should be of 3000
    {

        while (true)
        {
            Console.WriteLine("Waiting for msgs"); //for testing
            string msg;

            byte[] buffer = new byte[client.ReceiveBufferSize];
            //   string dataReceived;

            //---read first client's stream---



            int bytesRead = N.Read(buffer, 0, client.ReceiveBufferSize);

            //---convert the data received into a string---
            msg = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            DateTime today = DateTime.Today;


            if (msg == new_step)
            {
                flag = 1;
                Console.WriteLine("Received new step!"); //for testing
                logger.Log("log.txt", new_step, LogType.Receive);
            }
            else
            {
                Console.WriteLine("Received msgs!"); //for testing


                Command received = new Command();
                received = (Parse_Msg(msg));
                //Console.Write("Host received: defense:" + received.DefenseAction + " " + received.DefenseActionParameters[0] + " " + received.DefenseActionParameters[1] + "\n");

                Console.Write("Host received: defense:" + arr[0].DefenseAction + " " + arr[0].DefenseActionParameters[0] + "\n");
                if (arrcount > 1)
                {
                    Console.Write("Host received: defense:" + arr[1].OffenceAction + " " + arr[1].OffenceActionParameters[0] + "\n");
                    arrcount = 0;
                }

            }




        }


    }

    static void Send_New_Step(NetworkStream N)//host to client//stream should be of 3005
    {
        Console.WriteLine("Sending new time step"); //for testing
        byte[] x = ASCIIEncoding.ASCII.GetBytes(new_step);
        N.Write(x, 0, x.Length);


        logger.Log("log.txt", new_step, LogType.Send);

    }

    static void Rec_New_Step(NetworkStream N, TcpClient client)//client from host//stream should be of 3000
    {
        Console.WriteLine("Waiting for a new time step"); //for testing


        byte[] buffer = new byte[client.ReceiveBufferSize];
        string dataReceived;

        //---read first client's stream---
        flag = 0;

        do //blocks till receive a new time step
        {
            int bytesRead = N.Read(buffer, 0, client.ReceiveBufferSize);


            //---convert the data received into a string---
            dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        } while (dataReceived != new_step);
        flag = 1;
        Console.WriteLine("Received a new time step!"); //for testing


    }


    static void Host_Connect(string SERVER_IP)
    {
        //server waits for client
        IPAddress localAdd = IPAddress.Parse(SERVER_IP);
        TcpListener listener = new TcpListener(localAdd, 3000);
        Console.WriteLine("Listening...");
        listener.Start();

        //---incoming client connected---

        client = listener.AcceptTcpClient();

        nwStream = client.GetStream();


    }


    static void Client_Connect(string SERVER_IP)
    {

        client = new TcpClient(SERVER_IP, 3000);
        nwStream = client.GetStream();


    }



    /* static void Connect_As_Host(int PORT_NO,string SERVER_IP,int type)
     {
         try
         {
             //---listen at the specified IP and port no.---
             if (type == 1)
             {

                 //server waits for client
                 IPAddress localAdd = IPAddress.Parse(SERVER_IP);
                 TcpListener listener = new TcpListener(localAdd, 3000);
                 Console.WriteLine("Listening...");
                 listener.Start();

                 //---incoming client connected---


                 TcpClient client = listener.AcceptTcpClient();
                 TcpClient client2 = new TcpClient("192.168.1.6", 3005);

                 NetworkStream nwStream = client.GetStream();
                 NetworkStream nwStream2 = client2.GetStream();

                //Connection complete!

                 byte[] x = ASCIIEncoding.ASCII.GetBytes("3ash yabo el seminar awl mara");

                 //---write back the text to the client---
             //    Console.WriteLine("Sending back : " + ASCIIEncoding.ASCII.GetString(x));

               //  nwStream2.Write(x, 0, x.Length);

                     //---get the incoming data through a network stream---
                     nwStream = client.GetStream();
                     nwStream2 = client2.GetStream();
                     //NetworkStream nwStream2 = client2.GetStream();
                     byte[] buffer = new byte[client.ReceiveBufferSize];


                     //---read first client's stream---
                     int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);


                     //---convert the data received into a string---
                     string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                     if (dataReceived == create_game)
                     {
                         Console.WriteLine("Received a game request! enter y for accept");
                         char choice;
                         choice = (char)Console.Read();
                         if (choice == 'y')
                         {

                            // Send_command(start_game, nwStream2);


                             //Console.WriteLine("Accepting game.. ");
                             //x = ASCIIEncoding.ASCII.GetBytes(start_game);


                             //nwStream2.Write(x, 0, x.Length);
                         }



                         ///START OF GAME
                         Console.WriteLine("GAME STARTED!");
                         while (true) //game started
                         {
                             //---write back the text to the client---
//                                Console.WriteLine("Sending back a new step : " + ASCIIEncoding.ASCII.GetString(x));
                             Console.WriteLine("Sending back a new step : ");
                             x = ASCIIEncoding.ASCII.GetBytes(new_step);
                             nwStream2.Write(x, 0, x.Length);
                             //UPDATE GUI HERE!!!

                             //wait for client's command  then send host's
                             bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
                             dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                             Console.WriteLine("Received: " + dataReceived + " from client");



                             string command;
                             Console.WriteLine("Enter Command to send:");
                             command = Console.ReadLine();
                             x = ASCIIEncoding.ASCII.GetBytes(command);





                         }
                     }
                 client.Close();

                 //}
                 //client2.Close();
                 listener.Stop();
             }
             else
             {
                 // client2 3000 recieve client 3005 send
                 //---data to send to the server---
                 //string textToSend = DateTime.Now.ToString();
                 TcpClient client2 = new TcpClient(SERVER_IP, 3000);

                 byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(create_game);

                 NetworkStream nwStream2 = client2.GetStream();
                 //nwStream2.Write(bytesToSend, 0, bytesToSend.Length);

                 IPAddress localAdd = IPAddress.Parse("192.168.1.6");
                 TcpListener listener = new TcpListener(localAdd, 3005);
                 Console.WriteLine("Listening for server...");
                 listener.Start();

                 //---incoming client connected---
                 //  TcpClient client2 = new TcpClient("192.168.1.10", 5000);

                 TcpClient client = listener.AcceptTcpClient();
                 NetworkStream nwStream = client.GetStream();

                 byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                 int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                 string messangerec = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                 Console.WriteLine("Received : " + messangerec);

                 if (messangerec == start_game)
                 {



                     while (true) //game starts
                     {
                         //---create a TCPClient object at the IP and port no.---


                         //---read back the text---
                         //yereed mel port el tany..WAITING FOR A NEW TIMESTEP
                         bytesToRead = new byte[client.ReceiveBufferSize];
                         bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                         messangerec = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                         Console.WriteLine("Received : " + messangerec);


                         if (messangerec == new_step)
                         {
                                 //PROCESS DATA


                             string command2; //send client's command!
                             Console.WriteLine("Enter Command to send:");
                             command2 = Console.ReadLine();


                             bytesToSend = ASCIIEncoding.ASCII.GetBytes(command2);

                             //---send the text---
                             Console.WriteLine("Sending : " + "client sends!!!");
                             nwStream2.Write(bytesToSend, 0, bytesToSend.Length);

                             //GET HOST'S DATA
                             bytesToRead = new byte[client.ReceiveBufferSize];
                             bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                             messangerec = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                             Console.WriteLine("Received : " + messangerec);

                         }
                     }
                     client.Close();
                 }
                 }



         }
         catch (ArgumentNullException e)
         {
             Console.WriteLine("ArgumentNullException: {0}", e);
         }
         catch (SocketException e)
         {
             Console.WriteLine("SocketException: {0}", e);
         }

         Console.WriteLine("\n Press Enter to continue...");


     }

     */


    /*   static void Main(string[] args)
        {
            //   Connect_As_Host(p,ip,t);  

            //host

            int type = 1;
            if (type == 1)
            {

                Host_Connect(ip);
                while (true)
                {
                    Thread Rec = new Thread(() => Receive_Command(nwStream, client));



                    Command c = new Command();
                    c.Player = 1;
                    Command received = new Command();


                    Send_New_Step(nwStream);

                    Console.WriteLine("Enter defense command to send: ");

                    c.DefenseAction = Console.ReadLine();
                    c.DefenseActionParameters.Add(Int32.Parse(Console.ReadLine()));
                    c.DefenseActionParameters.Add(Int32.Parse(Console.ReadLine()));

                    if (c.DefenseAction == "kick")
                    {
                        c.DefenseActionParameters.Add(Int32.Parse(Console.ReadLine()));

                    }


                    Console.WriteLine("Enter Offence command to send: ");

                    c.OffenceAction = Console.ReadLine();
                    c.OffenceActionParameters.Add(Int32.Parse(Console.ReadLine()));
                    c.OffenceActionParameters.Add(Int32.Parse(Console.ReadLine()));

                    if (c.OffenceAction == "kick")
                    {
                        c.OffenceActionParameters.Add(Int32.Parse(Console.ReadLine()));

                    }

                    Send_Command(c, nwStream);
                    Rec.Start();

                    //received = Receive_Command(nwStream, client);



                }

            }
            else
            {
                flag = 0;
                Client_Connect(ip);
                while (true)
                {


                    Thread Rec = new Thread(() => Receive_Command(nwStream, client));

                    Rec.Start();


                    Command c = new Command();
                    Command received = new Command();

                    // Rec_New_Step(nwStream,client);

                    while (flag == 0) ;
                    flag = 0;
                    Console.WriteLine("Enter defense command to send: ");

                    c.DefenseAction = Console.ReadLine();
                    c.DefenseActionParameters.Add(Int32.Parse(Console.ReadLine()));
                    c.DefenseActionParameters.Add(Int32.Parse(Console.ReadLine()));

                    if (c.DefenseAction == "kick")
                    {
                        c.DefenseActionParameters.Add(Int32.Parse(Console.ReadLine()));

                    }


                    Console.WriteLine("Enter Offence command to send: ");

                    c.OffenceAction = Console.ReadLine();
                    c.OffenceActionParameters.Add(Int32.Parse(Console.ReadLine()));
                    c.OffenceActionParameters.Add(Int32.Parse(Console.ReadLine()));

                    if (c.OffenceAction == "kick")
                    {
                        c.OffenceActionParameters.Add(Int32.Parse(Console.ReadLine()));

                    }

                    Send_Command(c, nwStream);


                    //  received = Receive_Command(nwStream, client);

                    //Console.Write("Client received: defense:" + received.DefenseAction + " " + received.DefenseActionParameters[0] + " " + received.DefenseActionParameters[1] + "\n");

                    //                    Console.Write("Client received: offense" + received.OffenceAction + " " + received.OffenceActionParameters[0] + " " + received.OffenceActionParameters[1] + "\n");
                }

            }
        }
        */
}