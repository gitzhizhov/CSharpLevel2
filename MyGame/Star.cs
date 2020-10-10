using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGame
{
    /// <summary>
    /// Клас описывает звезды
    /// </summary>
    class Star : BaseObject
    {
        List<Bitmap> bitmapsList = new List<Bitmap>
        {
            new Bitmap("..\\..\\img/star1_1.png"),
            new Bitmap("..\\..\\img/star1_2.png"),
            new Bitmap("..\\..\\img/star1_3.png"),
            new Bitmap("..\\..\\img/star1_4.png"),
            new Bitmap("..\\..\\img/star1_5.png"),
            new Bitmap("..\\..\\img/star1_6.png")
        };

        int starIndex = 0;

        /// <summary>
        /// Инициализирует объект Star при помощи базового конструктора BaseObject
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        public override void Draw()
        {
            // делаем как бы мерцания
            starIndex++;
            if (starIndex == bitmapsList.Count)
                starIndex = 0;
            Game.Buffer.Graphics.DrawImage(bitmapsList[starIndex], Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод обновления местоположения объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
