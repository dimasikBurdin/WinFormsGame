using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MyGameModelNew.Domain
{
    public class Fire
    {
        public Point Position { get; private set; }
        public int CurrentAnimation { get; set; }

        public Fire(Point position)
        {
            Position = position;
        }

        public void Act()
        {
            Animation();
        }

        private void Animation()
        {
            CurrentAnimation += 32;
            if (CurrentAnimation > 96) CurrentAnimation = 0;
        }
    }
}
