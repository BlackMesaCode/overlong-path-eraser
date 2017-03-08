using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OverlongPathEraser
{
    public class Spinner
    {
        private int _state = 0;
        private Timer _timer;

        public Spinner()
        {
            _timer = new Timer((Object o) => Turn(), null, Timeout.Infinite, 200);
        }

        public void Start()
        {
            _timer.Change(0, 200);
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, 200);
        }

        public void Turn()
        {
            switch (_state)
            {
                case 0: Console.Write("/"); _state++; break;
                case 1: Console.Write("-"); _state++; break;
                case 2: Console.Write("\\"); _state++; break;
                case 3: Console.Write("|"); _state = 0; break;
            }
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }

    }
}
