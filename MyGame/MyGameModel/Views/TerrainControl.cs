using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGameModel.Views
{
    public partial class TerrainControl : UserControl
    {
        private readonly ScenePainter painter;
        private float zoomScale = 1;
        private KeyEventArgs keyEvent;
        private HashSet<KeyEventArgs> keyEvents = new HashSet<KeyEventArgs>();

        public TerrainControl(ScenePainter scenePainter)
        {
           // InitializeComponent();
            painter = scenePainter;
            DoubleBuffered = true;
            var timer = new Timer();
            timer.Interval = 30;
            timer.Tick += TimerTick;
            timer.Start();
            //Click += TerrainControl_Click;
            KeyDown += TerrainControl_KeyDown;
        }

        private void TimerTick(object sender, EventArgs args)
        {
            var map = ScenePainter.currentMap;
            if (keyEvent != null && IsPlayerKeys(keyEvent))
                map.Player.MovePlayer(keyEvent);
            map.Enemies[0].Move();//test
            Invalidate();
        }

        private bool IsPlayerKeys(KeyEventArgs e)
        {
            return e.KeyCode == Keys.A || e.KeyCode == Keys.S
                || e.KeyCode == Keys.D || e.KeyCode == Keys.W;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            keyEvent = null;
            //keyEvents.Remove(e);
        }

        private void TerrainControl_KeyDown(object sender, KeyEventArgs e)
        {
            keyEvents.Add(e);
            keyEvent = e;
            //if(e.KeyCode == Keys.A || e.KeyCode == Keys.S || e.KeyCode == Keys.D || e.KeyCode == Keys.W)
            //    ScenePainter.currentMap.Player.MovePlayer(e);      
            //Invalidate();
        }

        //digger_window -> timer (4 str)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //private void TerrainControl_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        public float ZoomScale
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
            ClientSize = new Size(700, 700);

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
