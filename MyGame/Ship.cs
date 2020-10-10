using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public delegate void Status(object sender, string msg);
    /// <summary>
    /// Класс описывает Корабль
    /// </summary>
    class Ship : BaseObject
    {
        Bitmap image = new Bitmap("..\\..\\img/rocket.png");
        private static int maxEnergy = 100;
        private int energy = maxEnergy;
        public int Energy => energy;

        public static event Action<string> ShipReductionOfEnergy;
        public static event Action<string> ShipRecoveryEnergy;
        public static event Action<string> ShipDie;
        public static Message MessageDie; // событий гибель корабля

        /// <summary>
        /// Перменная типа "Делегат"
        /// </summary>
        private Action<object, string> EventStatus;

        /// <summary>
        /// Метод уменьшения энергии
        /// </summary>
        /// <param name="n"></param>
        public void ReductionOfEnergy(int n)
        {
            energy -= n;
            if (energy < 0)
                Die();
            ShipReductionOfEnergy?.Invoke("Повреждения коробля");
        }

        /// <summary>
        /// Метод востановления энергии
        /// </summary>
        /// <param name="n"></param>
        public void RecoveryEnergy(int n)
        {
            energy += n;
            if (energy > maxEnergy)
                energy = maxEnergy;
            ShipRecoveryEnergy?.Invoke($"Ремонт коробля на {n}");
        }

        /// <summary>
        /// Инициализирует объект Ship при помощи базового конструктора BaseObject
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update() { }

        /// <summary>
        /// Метод движения вверх
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0)
                Pos.Y -= Dir.Y;
        }

        /// <summary>
        /// Метод движения вниз
        /// </summary>
        public void Down()
        {
            if (Pos.Y < Game.Height)
                Pos.Y += Dir.Y;
        }

        public void Die()
        {
            ShipDie?.Invoke("Корабль унечтожен");
        }
    }
}
