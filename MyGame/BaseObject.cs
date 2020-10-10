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
        public delegate void Message();

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
        /// Абстрактный метод обновления местоположения объекта
        /// </summary>
        public abstract void Update();

        // Так как переданный объект тоже должен будет реализовывать интерфейс ICollision, мы 
        // можем использовать его свойство Rect и метод IntersectsWith для обнаружения пересечения с
        // нашим объектом (а можно наоборот)
        public bool Collision(ICollision o) => o.Rest.IntersectsWith(this.Rest);
        public Rectangle Rest => new Rectangle(Pos, Size);
    }
}
