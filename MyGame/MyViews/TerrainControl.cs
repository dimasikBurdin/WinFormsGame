using MyGameModelNew.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Point = MyGameModel.Domain.Point;

namespace MyViews
{
    public partial class TerrainControl : UserControl
    {
        private readonly ScenePainter painter;
        private float zoomScale = 1;
        private int tickCount;
        private int EnemyTickCount;
        public Size TerrainClientSize { get; private set; }
        public Timer Timer { get; private set; }
        public static Enemy RemoveEnemyFromList { get; set; }
        private List<Enemy> deadEnemy = new List<Enemy>();
        private List<GameObject> pickedHealers = new List<GameObject>();

        public TerrainControl(ScenePainter scenePainter)
        {
            TerrainClientSize = new Size(800, 800);
            painter = scenePainter;
            DoubleBuffered = true;

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

            if (player != null && tickCount == 0)
            {
                player.Act(ScenePainter.currentMap);
                if (player.PikedHealer != null)
                    pickedHealers.Add(player.PikedHealer);
            }

            RemovePickedHealers();
            if (EnemyTickCount == 0)
                foreach (var enemy in map.Enemies)
                {
                    enemy.Act(ScenePainter.currentMap);
                    if (enemy.IsDeadEnemy)
                        deadEnemy.Add(enemy);                    
                }
            RemoveEnemy();

            if (player.Health <= 0)
                MainForm.Over();

            tickCount++;
            EnemyTickCount++;
            if (tickCount == 5) tickCount = 0;
            if (EnemyTickCount == 8) EnemyTickCount = 0;

            Invalidate();
        }

        private void RemovePickedHealers()
        {
            foreach(var healer in pickedHealers)
            {
                ScenePainter.currentMap.Objects.Remove(healer);
            }
        }
        private void RemoveEnemy()
        {
            //if(RemoveEnemyFromList != null)
            //    ScenePainter.currentMap.Enemies.Remove(RemoveEnemyFromList);
            //if(enemy.IsDeadEnemy != default)
            //    ScenePainter.currentMap.Enemies.Remove(enemy);
            foreach (var enemy in deadEnemy)
                ScenePainter.currentMap.Enemies.Remove(enemy);

        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            var player = ScenePainter.currentMap.Player;
            switch (e.KeyCode)
            {
                case Keys.W:
                    //player.Delta.Y = 0;
                    player.Delta = new Point() { X = player.Delta.X, Y = 0 };
                    break;
                case Keys.S:
                    //player.Delta.Y = 0;
                    player.Delta = new Point() { X = player.Delta.X, Y = 0 };
                    break;
                case Keys.A:
                    //player.Delta.X = 0;
                    player.Delta = new Point() { X = 0, Y = player.Delta.Y };
                    break;
                case Keys.D:
                    //player.Delta.X = 0;
                    player.Delta = new Point() { X = 0, Y = player.Delta.Y };                    
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
                case Keys.Escape:
                    MainForm.TerrainControl.Hide();
                    MainForm.MainMenu.Show();                        
                    Timer.Stop();
                    break;
                case Keys.Space:
                    player.CanHit = true;
                    break;
                case Keys.H:
                    player.UseHealer();
                    break;
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
