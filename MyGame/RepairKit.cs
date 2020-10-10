using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    /// <summary>
    /// Класс описывает Ремкомплект
    /// </summary>
    class RepairKit : BaseObject
    {
        Bitmap image = new Bitmap("..\\..\\img/repairkit.png");
        private static int maxEnergy = 50; // задел на будущие, если руки дойдут (
        private int energy = maxEnergy;
        public int Energy => energy;

        /// <summary>
        /// Инициализирует объект RepairKit при помощи базового конструктора BaseObject
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public RepairKit(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод обновления местоположения объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            //if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
