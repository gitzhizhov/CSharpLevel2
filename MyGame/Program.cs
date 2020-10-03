using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Form MF = new Form();
            MF.Width = 800;
            MF.Height = 600;
            Game.Init(MF);
            MF.Show();
            Game.Draw();
            Application.Run(MF);
        }
    }
}
