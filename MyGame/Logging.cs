using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyGame
{
    /// <summary>
    /// Класс логирования
    /// </summary>
    class Logging
    {
        public static void Log(string msg)
        {
            using (StreamWriter sw = new StreamWriter("event.log", true))
            {
                string text = string.Format($"[{DateTime.Now:dd.MM.yyy HH:mm:ss}] - {msg}" + Environment.NewLine);
                sw.Write(text);
            }
        }
    }
}
