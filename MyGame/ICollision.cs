using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGame
{
    /// <summary>
    /// Интерфейс определяет столкновение обьектов
    /// </summary>
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rest { get; }
    }
}
