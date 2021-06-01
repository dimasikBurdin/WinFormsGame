using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MyGameModelNew.Domain
{
    public class Gate
    {
        public Point Position { get; private set; }
        public int CurrentAnimation { get; private set; }
        public KeyAndGateType Type { get; private set; }
        public GateState State { get; private set; }
        private bool isOpening;

        public Gate(Point position, KeyAndGateType gateType)
        {
            Position = position;
            Type = gateType;
            State = GateState.Lock;
        }

        public void Act()
        {
            Animation();
        }

        public void Open()
        {
            State = GateState.Open;
            isOpening = true;
        }

        private void Animation()
        {
            if(State == GateState.Open && isOpening)
            {
                CurrentAnimation += 64;
                if (CurrentAnimation > 192)
                {
                    CurrentAnimation = 192;
                    isOpening = false;
                }
            }
        }


    }
}
