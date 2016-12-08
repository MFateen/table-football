using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

public class Logger {
    public void Log(string filename, Command command) {

        string s = command.name + " ";
        for (int i = 0; i < command.parameters.Count; i++) {
            s += command.parameters[i] + " ";
        }
        s += command.player;

        string path = Directory.GetCurrentDirectory() + filename;
        if (!File.Exists(path)) {
            using (StreamWriter sw = File.CreateText(path)) {
                sw.WriteLine(s);
            }
        } else {
            using (StreamWriter sw = File.AppendText(path)) {
                sw.WriteLine(s);
            }
        }
    }
}