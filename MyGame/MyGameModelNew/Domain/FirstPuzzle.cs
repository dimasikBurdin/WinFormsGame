using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameModelNew.Domain
{
    class FirstPuzzle : Puzzle//TODO
    {
        public FirstPuzzle(Point position)
        {
            Position = position;
            IsFinished = false;
        }
    }
}
