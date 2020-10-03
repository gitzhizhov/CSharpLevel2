using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGame
{
    /// <summary>
    /// Клас описывает астеройдов
    /// </summary>
    class Asteroid : BaseObject
    {
        public int Power { get; set; }

        /// <summary>
        /// Инициализирует объект Asteroid при помощи базового конструктора BaseObject
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }

        /// <summary>
        /// Клонирование объекта Asteroid
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            // Создаем копию нашего Asteriod
            Asteroid asteroid = new Asteroid(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y), new Size(Size.Width, Size.Height));
            // Не забываем скопировать новому астероиду Power нашего астероида
            asteroid.Power = Power;
            return asteroid;
        }

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод обновления местоположения объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width - Size.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height - Size.Height) Dir.Y = -Dir.Y;
        }

        /// <summary>
        /// Прересоздаёт объект при столкновении
        /// </summary>
        public void Recreate()
        {
            Pos.X = Game.Width - Size.Width;
            Pos.Y = Convert.ToInt32(rnd.NextDouble() * (Game.Height - Size.Height));
        }
    }
}
