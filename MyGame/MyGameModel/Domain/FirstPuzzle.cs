using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameModel.Domain
{
    class FirstPuzzle : Puzzle
    {
        public FirstPuzzle(Point position)
        {
            Position = position;
            IsFinished = false;
        }
        //надо свой собственный controll...
    }
}
