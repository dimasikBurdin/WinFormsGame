using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MyGameModelNew.Domain
{
    public class Key
    {
        public Point Position { get; set; }
        public KeyAndGateType Type { get; private set; }

        public Key(Point position, KeyAndGateType type)
        {
            Position = position;
            Type = type;
        }
    }
}
