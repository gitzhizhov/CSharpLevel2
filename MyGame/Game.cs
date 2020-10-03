using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

namespace MyGame
{
    static class Game
    {
        private static BufferedGraphicsContext context;
        public static BufferedGraphics Buffer;
        public static BaseObject[] objs;
        private static Timer timer = new Timer { Interval = 100 };
        static Random rnd;
        private static int formSize = 1000;

        private static int countStar = 15; // 30
        private static int countSmallStar = 50; // 70
        private static int countPlanet = 1; //
        private static Asteroid[] asteroids;
        private static int countAsteroid = 3;
        private static Bullet bullet;
        private static int countObjs = countStar + countSmallStar;
        private static int speed = 5;
        private static int minSize = 10;
        private static int maxSize = 25;

        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game() { }

        /// <summary>
        /// Метод создания обьектов в окне
        /// </summary>
        public static void Load()
        {
            rnd = new Random();
            objs = new BaseObject[countObjs];

            // размещаем маленькие звезды
            for (int i = 0; i < objs.Length - countStar - countPlanet; i++)
            {
                objs[i] = new SmallStar(
                    new Point(Convert.ToInt32(rnd.NextDouble() * Width), Convert.ToInt32(rnd.NextDouble() * Height)),
                    new Point(rnd.Next(speed / 6, 2), 0),
                    new Size(1, 1)
                    );
            }

            // размещаем звезды
            int sizeStar = rnd.Next(minSize, maxSize);
            for (int i = objs.Length - countStar - countPlanet; i < objs.Length - countPlanet; i++)
            {
                objs[i] = new Star(
                    new Point(Convert.ToInt32(rnd.NextDouble() * Width), Convert.ToInt32(rnd.NextDouble() * Height)),
                    new Point(rnd.Next(speed / 3, 4), 0),
                    new Size(sizeStar, sizeStar)
                    );
            }

            // размещаем планету
            for (int i = objs.Length - countPlanet; i < objs.Length; i++)
            {
                objs[i] = new Planet(
                    new Point(Convert.ToInt32(rnd.NextDouble() * Width), Convert.ToInt32(rnd.NextDouble() * Height)),
                    new Point(rnd.Next(0, 1), 0),
                    new Size(maxSize * 10, maxSize * 10)
                    );
            }

            // размещаем пулю
            bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));


            // размещаем астеройды
            asteroids = new Asteroid[countAsteroid];

            for (int i = 0; i < asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                asteroids[i] = new Asteroid(
                    new Point(Convert.ToInt32(rnd.NextDouble() * Width), rnd.Next(Game.Height)),
                    new Point(-r / 5, r),
                    new Size(r, r)
                    );
            }

            //


            //for (int i = 0; i < objs.Length; i++)
            //{
            //    objs[i] = new BaseObject(new Point(600, i * 20), new Point(-i, - i), new Size(5, 5));
            //}
        }
        /// <summary>
        /// Метод создания графики в форме
        /// </summary>
        /// <param name="form">Форма</param>
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            // Создаем объект (поверхность рисования) и связываем его с формой
            g = form.CreateGraphics();
            try
            {
                // Запоминаем размеры формы
                Width = form.ClientSize.Width;
                Height = form.ClientSize.Height;
                if (Width > formSize || Width < 0 || Height > formSize || Height < 0)
                    throw new ArgumentOutOfRangeException("Высота или ширина (Width, Height) больше 1000 или принимает отрицательное значение");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"Ошибка {e.Message}");
            }
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Обработчик таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(Object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Метод отрисовки объектов
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
            {
                obj.Draw();
            }
            foreach (Asteroid a in asteroids)
            {
                a.Draw();
            }
            bullet.Draw();
            Buffer.Render();
        }

        /// <summary>
        /// Метод обновления объектов на форме
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in objs)
            {
                obj.Update();
            }

            for (int i = 0; i < asteroids.Length; i++)
            {
                asteroids[i].Update();
                if (bullet.Collision(asteroids[i]))
                {
                    SystemSounds.Hand.Play();
                    asteroids[i].Recreate();
                }

            }
            //foreach (Asteroid a in asteroids)
            //{
            //    a.Update();
            //    if (a.Collision(bullet))
            //    {
            //        SystemSounds.Hand.Play();
            //        //asteroids.Remove(a);
            //        a.Clone();
            //    }
            //}
            bullet.Update();
        }
    }
}
