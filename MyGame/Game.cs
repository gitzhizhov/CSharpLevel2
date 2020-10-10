using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.IO;

namespace MyGame
{
    static class Game
    {
        private static BufferedGraphicsContext context;
        public static BufferedGraphics Buffer;
        public static BaseObject[] objs;
        private static Timer timer = new Timer { Interval = 30 }; // 100
        static Random rnd;
        private static int formSize = 1000; // ограничение формы
        private static int score = 0; // счет игры



        private static int countObjs;

        // маленькиеЗвезды количество, скорость, размер
        private static int countSmallStar = 50; // 70
        private static int speedSmallStarMin = 1;
        private static int speedSmallStarMax = 2;
        private static int sizeSmallStar = 1;

        // звезды количество, скорость, размер
        private static int countStar = 15; // 30
        private static int speedStarMin = 2;
        private static int speedStarMax = 4;
        private static int sizeStarMin = 10;
        private static int sizeStarMax = 25;

        // планета количество, скорость, размер
        private static int countPlanet = 1; //
        private static int speedPlanet = 0;
        private static int sizePlanet = 250;

        // астеройды количество, скорость, размер
        private static List<Asteroid> asteroids;
        private static int speedAsteroidMin = 5;
        private static int speedAsteroidMax = 30;
        private static int sizeAsteroidMin;
        private static int sizeAsteroidMax;

        // снаряды количество, скорость, размер
        private static List<Bullet> bullet;
        private static int speedBullet = 5;
        private static int sizeBullet = 4;

        // аптечка количество, скорость, размер
        private static List<RepairKit> repairKit;
        private static int countRepairKit = 1;
        private static int speedKitMin = 2;
        private static int speedKitMax = 4;
        private static int sizeKit = 10;

        // корабль количество, скорость, размер
        private static Ship ship;
        private static int speedShip = 5;
        private static int sizeShip = 30;
        
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
            countObjs = countStar + countSmallStar + countPlanet;
            rnd = new Random();
            objs = new BaseObject[countObjs];
            bullet = new List<Bullet>();
            repairKit = new List<RepairKit>(countRepairKit);

            // размещаем маленькие звезды
            for (int i = 0; i < objs.Length - countStar - countPlanet; i++)
            {
                objs[i] = new SmallStar(
                    new Point(Convert.ToInt32(rnd.NextDouble() * Width), Convert.ToInt32(rnd.NextDouble() * Height)),
                    new Point(rnd.Next(speedSmallStarMin, speedSmallStarMax), 0),
                    new Size(sizeSmallStar, sizeSmallStar)
                    );
            }

            // размещаем звезды
            int sizeStar = rnd.Next(sizeStarMin, sizeStarMax);
            for (int i = objs.Length - countStar - countPlanet; i < objs.Length - countPlanet; i++)
            {
                objs[i] = new Star(
                    new Point(Convert.ToInt32(rnd.NextDouble() * Width), Convert.ToInt32(rnd.NextDouble() * Height)),
                    new Point(rnd.Next(speedStarMin, speedStarMax), 0),
                    new Size(sizeStar, sizeStar)
                    );
            }

            // размещаем планету
            for (int i = objs.Length - countPlanet; i < objs.Length; i++)
            {
                objs[i] = new Planet(
                    new Point(Convert.ToInt32(rnd.NextDouble() * Width), Convert.ToInt32(rnd.NextDouble() * Height)),
                    new Point(speedPlanet, 0),
                    new Size(sizePlanet, sizePlanet)
                    );
            }

            // размещаем ремкомплект
            PlaceKit();

            // размещаем астеройды
            PlaceAsteroid();

            // размещаем корабль
            ship = new Ship(new Point(10, (Height- sizeShip) / 2), new Point(0, speedShip), new Size(sizeShip, sizeShip));
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

            form.KeyDown += Form_KeyDown;

            Ship.ShipRecoveryEnergy += s => File.AppendAllText("log.txt", $"{s}\n");
            Ship.ShipReductionOfEnergy += Logging.Log;
            Ship.ShipRecoveryEnergy += Logging.Log;
            Ship.ShipDie += Logging.Log;
            Ship.MessageDie += Finish;
            Asteroid.AsteroidCreation += Logging.Log;
            Asteroid.AsteroidRecreation += Logging.Log;

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
                obj.Draw();
            foreach (Asteroid a in asteroids)
                a?.Draw();
            foreach (Bullet b in bullet)
                b?.Draw();
            foreach (RepairKit kit in repairKit)
                kit?.Draw();
            ship?.Draw();

            // вывод энергии коробля и счета    
            if (ship != null)
            {
                Buffer.Graphics.DrawString($"Enregy:{ship.Energy}", SystemFonts.DefaultFont, Brushes.White, 0, 0);
                Buffer.Graphics.DrawString($"Scoute:{score}", SystemFonts.DefaultFont, Brushes.White, 60, 0);
            }
            Buffer.Render();
        }

        /// <summary>
        /// Метод обновления объектов на форме
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
            foreach (Bullet b in bullet)
                b?.Update();

            for (int i = 0; i < asteroids.Count; i++)
            {
                if (asteroids == null) continue;
                asteroids[i].Update();
                for (int j = 0; j < bullet.Count; j++)
                {
                    try
                    {
                        // коллизия по астеройдам и пулям
                        if (bullet[j].Collision(asteroids[i]) && bullet[j] != null && asteroids[i] != null)
                        {
                            SystemSounds.Hand.Play();
                            //asteroids[i].Recreate();
                            score++;
                            bullet.RemoveAt(j);
                            asteroids.RemoveAt(i);
                            if (CheckCountAsteroid())
                                PlaceAsteroid();
                            continue;
                        }
                        // коллизия по астеройдам и ship
                        if (ship.Collision(asteroids[i]))
                        {
                            ship?.ReductionOfEnergy(asteroids[i].Power);
                            asteroids.RemoveAt(i);
                            if (CheckCountAsteroid())
                                PlaceAsteroid();
                            SystemSounds.Asterisk.Play();
                        }
                    }
                    catch (ArgumentOutOfRangeException e)
                    { }
                }
            }

            // коллизия ремкомплекта и ship
            for (int i = 0; i < repairKit.Count; i++)
            {
                repairKit[i]?.Update();
                try
                {
                    if (ship.Collision(repairKit[i]) || repairKit[i].Rest.X < 0)
                    {
                        ship?.RecoveryEnergy(repairKit[i].Energy);
                        repairKit.RemoveAt(i);
                        PlaceKit();
                    }
                }
                catch (ArgumentOutOfRangeException e)
                { }
            }
        }

        /// <summary>
        /// Метод обработки начатия клавиш UP и Down
        /// Создаем bullet "на носу" Ship
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                bullet.Add(new Bullet(
                    new Point(ship.Rest.X + sizeShip, ship.Rest.Y + sizeShip / 2),
                    new Point(speedBullet, 0),
                    new Size(sizeBullet, sizeBullet / sizeBullet)
                        )
                    );
            if (e.KeyCode == Keys.Up && ship.Rest.Y > 0)
                ship.Up();
            if (e.KeyCode == Keys.Down && ship.Rest.Y + ship.Rest.Height < Height)
                ship.Down();
        }

        /// <summary>
        /// Метод размещения списка астеройдов. Увелечение количества при уничтожении всех астеройдов.
        /// </summary>
        private static void PlaceAsteroid()
        {
            asteroids = new List<Asteroid>();
            for (int i = 0; i < Asteroid.CountAsteroid; i++)
            {
                int r = rnd.Next(speedAsteroidMin, speedAsteroidMax);
                asteroids.Add(new Asteroid(
                                new Point(Convert.ToInt32(rnd.NextDouble() * Width), rnd.Next(Game.Height)),
                                new Point(-r / 5, r),
                                new Size(r, r)
                                )       
                            );
            }
            Asteroid.CountAsteroid++;
        }

        /// <summary>
        /// Метод проверки количества элементов в списке asteroids
        /// </summary>
        /// <returns></returns>
        private static bool CheckCountAsteroid()
        {
            return asteroids.Distinct().Count() == 0;
        }

        /// <summary>
        /// Метод размещения ремкомплект
        /// </summary>
        internal static void PlaceKit()
        {
            repairKit.Add(new RepairKit(new Point(Width, rnd.Next(Game.Height)),
                                new Point(rnd.Next(speedKitMin, speedKitMax), 0),
                                new Size(sizeKit, sizeKit)
                                        )
                        );
        }

        /// <summary>
        /// Метод окончания игры
        /// </summary>
        public static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("The END",
                new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline),
                Brushes.White, 200, 100
                );
            Buffer.Render();
        }
    }
}
