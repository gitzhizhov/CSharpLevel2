using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGame
{
    /// <summary>
    /// Клас описывает снарядов
    /// </summary>
    class Bullet : BaseObject
    {
        /// <summary>
        /// Инициализирует объект Bullet при помощи базового конструктора BaseObject
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод обновления местоположения объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
        }
    }
}
