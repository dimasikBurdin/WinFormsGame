using MyGameModel.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = MyGameModel.Domain.Point;

namespace MyGameModel.Views
{
    public partial class TerrainControl : UserControl
    {
        private readonly ScenePainter painter;
        private float zoomScale = 1;
        private int tickCount;
        private int EnemyTickCount;
        public Size TerrainClientSize { get; private set; }
        public Timer Timer { get; private set; }

        public TerrainControl(ScenePainter scenePainter)
        {
            TerrainClientSize = new Size(800, 800);
            painter = scenePainter;
            DoubleBuffered = true;

            //var timer = new Timer();
            Timer = new Timer();
            Timer.Interval = 15;
            Timer.Tick += TimerTick;
            Timer.Start();

            KeyDown += TerrainControl_KeyDown;
            KeyUp += OnKeyUp;
        }
                

        private void TimerTick(object sender, EventArgs args)
        {
            var map = ScenePainter.currentMap;
            var player = map.Player;

            //if (map.Enemies[0].CanMove) 
            //    map.Enemies[0].Move();//test smart

            if (EnemyTickCount == 0)
                foreach (var enemy in map.Enemies)
                    //if (enemy.CanMove)
                        enemy.Move();//test
            if (player != null && player.IsMoving && tickCount == 3)
                player.Act();
            
            tickCount++;
            EnemyTickCount++;
            if (tickCount == 4) tickCount = 0;
            if (EnemyTickCount == 8) EnemyTickCount = 0;
            Invalidate();
        }

        private bool IsPlayerKeys(KeyEventArgs e)
        {
            return e.KeyCode == Keys.A || e.KeyCode == Keys.S
                || e.KeyCode == Keys.D || e.KeyCode == Keys.W;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            var player = ScenePainter.currentMap.Player;
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.Delta.Y = 0;
                    break;
                case Keys.S:
                    player.Delta.Y = 0;
                    break;
                case Keys.A:
                    player.Delta.X = 0;
                    break;
                case Keys.D:
                    player.Delta.X = 0;
                    break;
            }
            if(player.Delta == Point.Empty)
            {
                player.IsMoving = false;
            }
        }

        private void TerrainControl_KeyDown(object sender, KeyEventArgs e)
        {
            var player = ScenePainter.currentMap.Player;
            if (IsPlayerKeys(e))
            {
                switch (e.KeyCode)
                {
                    case Keys.D:
                        player.Delta = new Point { X = 1, Y = 0 };
                        //player.Delta.X = 1;
                        player.IsMoving = true;
                        break;
                    case Keys.A:
                        player.Delta = new Point { X = -1, Y = 0 };
                        //player.Delta.X = -1;
                        player.IsMoving = true;
                        break;
                    case Keys.W:
                        player.Delta = new Point { X = 0, Y = -1 };
                        //player.Delta.Y = -1;
                        player.IsMoving = true;
                        break;
                    case Keys.S:
                        player.Delta = new Point { X = 0, Y = 1 };
                        //player.Delta.Y = 1;
                        player.IsMoving = true;
                        break;
                }
            }
        }

        private void TerrainControl_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private float ZoomScale
        {
            get { return zoomScale; }
            set
            {
                zoomScale = Math.Min(1000f, Math.Max(0.001f, value));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ClientSize = TerrainClientSize;

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(Color.AliceBlue);
            if (painter == null) return;
            var sceneSize = painter.Size;
            var vMargin = sceneSize.Height * ClientSize.Width < ClientSize.Height * sceneSize.Width;
            zoomScale = vMargin
                ? ClientSize.Width / sceneSize.Width
                : ClientSize.Height / sceneSize.Height;
            e.Graphics.ResetTransform();
            e.Graphics.ScaleTransform(ZoomScale, ZoomScale);
            painter.Paint(e.Graphics);
        }
    }
}
