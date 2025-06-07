using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StarCraftShooter
{
    //try to figure out how to utlize a central class for managing the cursor w/multi threads...current methods work but not great
    //maybe a queue system for threads using cursor?
    public static class CursorManager
    {
        public static string TextToPrint { get; set; }
        public static int[] CursorPosition { get; set; }
        public static Semaphore MyProperty { get; set; } = new Semaphore(1, 1);
        static object _lockObj = new object();

        //this doesn't really work with how player/enemy images are currently set up. They print on multi lines and reference objects positions
        public static void Print(string textToPrint, int leftPosition, int topPosition)
        {
            lock(_lockObj)
            {
                Console.SetCursorPosition(leftPosition, topPosition);
                Console.Write(textToPrint);
            }
        }
    }
}
