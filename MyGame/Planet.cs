using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace MyGame
{
    /// <summary>
    /// Клас описывает планеты
    /// </summary>
    class Planet : BaseObject
    {
        Bitmap image = new Bitmap("..\\..\\img/planet.png");

        /// <summary>
        /// Инициализирует объект Planet при помощи базового конструктора BaseObject
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Planet(Point pos, Point dir, Size size) : base(pos, dir, size) { }

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
            if (Pos.X < 0)
            {
                Pos.X = Game.Width + Size.Width;
                Pos.Y = Convert.ToInt32(rnd.NextDouble() * Game.Height);
            }
        }
    }
}
