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

        public TerrainControl(ScenePainter scenePainter)
        {
           // InitializeComponent();
            painter = scenePainter;
            DoubleBuffered = true;
            //Click += TerrainControl_Click;
            KeyPress += TerrainControl_KeyPress;
        }

        private void TerrainControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            var delta = Point.Empty;
            switch(e.KeyChar)
            {
                case 'd':
                    ScenePainter.Player.Position = new Point() { X = ScenePainter.PlayerPosition.X + 1, Y = ScenePainter.PlayerPosition.Y };
                    break;
                case 'a':
                    ScenePainter.Player.Position = new Point() { X = ScenePainter.PlayerPosition.X - 1, Y = ScenePainter.PlayerPosition.Y };
                    break;
                case 'w':
                    ScenePainter.Player.Position = new Point() { X = ScenePainter.PlayerPosition.X, Y = ScenePainter.PlayerPosition.Y - 1 };
                    break;
                case 's':
                    ScenePainter.Player.Position = new Point() { X = ScenePainter.PlayerPosition.X, Y = ScenePainter.PlayerPosition.Y + 1 };
                    break;
            }
            
            Invalidate();
        }

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
            e.Graphics.Clear(Color.White);
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
