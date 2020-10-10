using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.CodeDom.Compiler;

namespace MyGame
{
    /// <summary>
    /// Клас описывает астеройдов
    /// </summary>
    class Asteroid : BaseObject, ICloneable, IComparable, IComparable<Asteroid>
    {
        public int Power { get; set; } = 3;
        public static int CountAsteroid { get; set; } = 1;

        public static event Action<string> AsteroidCreation;
        public static event Action<string> AsteroidRecreation;
        /// <summary>
        /// Инициализирует объект Asteroid при помощи базового конструктора BaseObject
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
            AsteroidCreation?.Invoke($"Астерой создан в координатах {Pos.X}, {Pos.Y} размером {Size.Width}");
        }
        /// <summary>
        /// Реализация обобщённого интерфейса IComparable
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns></returns>
        int IComparable.CompareTo(object obj)
        {
            if (obj is Asteroid temp)
            {
                if (Power > temp.Power)
                    return 1;
                if (Power < temp.Power)
                    return -1;
                else
                    return 0;
            }
            throw new ArgumentException("Параметр не является Астеройдом");
        }

        int IComparable<Asteroid>.CompareTo(Asteroid obj)
        {
            if (Power > obj.Power)
                return 1;
            if (Power < obj.Power)
                return -1;
            return 0;
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
            AsteroidRecreation?.Invoke("Астерой пересоздан");
        }
    }
}
