using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

public class Logger
{
    public void Log(string filename, string command, LogType type)
    {
        string s = "";
        s += DateTime.Now.ToString("HH:mm:ss");

        if(type == LogType.Receive)
        {
            s += " rec >>> ";
        }
        else
        {
            s += " sent >>> ";
        }

        s += command + '\n';


        string path = /*Directory.GetCurrentDirectory() + */filename;
        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(s);
            }
        }
        else
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(s);
            }
        }
    }

}
