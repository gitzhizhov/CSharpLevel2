using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGame
{
    abstract class BaseObject : ICollision
    {
        protected Point Pos; // позиция
        protected Point Dir; // направление
        protected Size Size; // размер

        protected Random rnd = new Random();

        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        /// <summary>
        /// Абстрактный метод отрисовки объекта
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        //public virtual void Draw()
        //{
        //    Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        //}

        /// <summary>
        /// Абстрактный метод обновления местоположения объекта
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Метод обновления местоположения объекта
        /// </summary>
        //public virtual void Update()
        //{
        //    Pos.X = Pos.X + Dir.X;
        //    Pos.Y = Pos.Y + Dir.Y;

        //    if (Pos.X < 0) Dir.X = -Dir.X;
        //    if (Pos.X > Game.Width) Dir.X = -Dir.X;
        //    if (Pos.Y < 0) Dir.Y = -Dir.Y;
        //    if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        //}

        // Так как переданный объект тоже должен будет реализовывать интерфейс ICollision, мы 
        // можем использовать его свойство Rect и метод IntersectsWith для обнаружения пересечения с
        // нашим объектом (а можно наоборот)
        public bool Collision(ICollision o) => o.Rest.IntersectsWith(this.Rest);
        public Rectangle Rest => new Rectangle(Pos, Size);
    }
}
