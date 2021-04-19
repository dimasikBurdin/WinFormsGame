using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGameModel
{
    public partial class GameControl : UserControl
    {
        private static Pen cellBorderPen = new Pen(Color.FromArgb(0x77, 0xAA, 0xEE), 2);
        public GameControl()
        {
            InitializeComponent();
        }

        //public void DrawEmptyCell(Graphics graphics, Bitmap rectangle)
        //{
        //    graphics.DrawRectangle(cellBorderPen, rectangle);
        //}

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    var cellWidth = Properties.Resources.Grass1.Width;
        //    var cellHeight = Properties.Resources.Grass1.Height;


        //    base.OnPaint(e);
        //    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //    BackColor = Color.DimGray;
        //    //var pointToPicture = GeneratePointToPicture(Width, Height);
        //    for(var x = 0; x < 40; x++)
        //    {
        //        for(var y = 0; y < 40; y++)
        //        {
        //            var image = Properties.Resources.Grass1;
        //            e.Graphics.DrawImage(image, new Rectangle(x * cellWidth, y * cellHeight, cellWidth, cellHeight));
        //        }
        //    }
        //    //foreach (var pair in pointToPicture)
        //    //   e.Graphics.DrawImage(pair.Value, pair.Key);
        //}

        //private static Dictionary<Point, Bitmap> GeneratePointToPicture(int width, int height)
        //{
        //    var result = new Dictionary<Point, Bitmap>();
        //    for (var x = 0; x < width; x++)
        //    {
        //        for (int y = 0; y < height; y++)
        //        {
        //            if (y % 2 == 0)
        //                result.Add(new Point(x, y), Properties.Resources.Grass1);
        //            else result.Add(new Point(x, y), Properties.Resources.Image1);
        //        }
        //    }
        //    return result;
        //}

        private void GameControl_Load(object sender, EventArgs e)
        {

        }
    }
}
