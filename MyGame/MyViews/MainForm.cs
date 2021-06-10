using MyGameModelNew.Domain;
//using MyGameModel.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyViews
{
    public partial class MainForm : Form
    {
        //size +
        //index / zIndex +
        //в форме есть событие, принимающее нажатие клавиш. Для управления написать методы в моделе (в игроке например), которые будут реагировать на эти нажатия +
        public static Label LabelHp { get; set; }
        public static Label LabelHealerText { get; set; }
        public static Label LabelHealerImage { get; set; }
        public static Label LabelRedKeyText { get; set; }
        public static Label LabelRedKeyImage { get; set; }
        public static Label LabelGreenKeyText { get; set; }
        public static Label LabelGreenKeyImage { get; set; }
        public static Label LabelBlueKeyText { get; set; }
        public static Label LabelBlueKeyImage { get; set; }
        public static Label LabelHandText { get; set; }
        public static Label LabelHandImage { get; set; }
        public static Label LabelWoodSwoardText { get; set; }
        public static Label LabelWoodSwoardImage { get; set; }
        public static Label LabelSteelSwoardText { get; set; }
        public static Label LabelSteelSwoardImage { get; set; }
        public static TerrainControl TerrainControl { get; set; }
        public static MenuControl MainMenu { get; set; }
        public static MessageBoxControl MessageBox { get; set; }
        public static NpcMessage NpcMessage { get; set; }
        public static Game Game { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;

            MessageBox = new MessageBoxControl(); //пригодится для диалогов с npc и не только
            MessageBox.Location = new Point(TerrainControl.ClientSize.Width / 2 - MessageBox.ClientSize.Width / 2,
                TerrainControl.ClientSize.Height / 2 - MessageBox.ClientSize.Height / 2);
            Controls.Add(MessageBox);
            MessageBox.Hide();

            NpcMessage = new NpcMessage();
            NpcMessage.Location = new Point(TerrainControl.ClientSize.Width / 2 - NpcMessage.ClientSize.Width / 2,
                TerrainControl.Bottom - NpcMessage.ClientSize.Height);
            NpcMessage.Hide();
            Controls.Add(NpcMessage);


            MainMenu = new MenuControl();
            MainMenu.ClientSize = TerrainControl.ClientSize;
            MainMenu.Show();
            Controls.Add(MainMenu);

            ClientSize = new Size(TerrainControl.ClientSize.Width + 100, TerrainControl.ClientSize.Height);
            TerrainControl.Hide();
            TerrainControl.Timer.Stop();
            
        }

        public MainForm()
        {
            Game = new Game();
            Game.CurrentGameStage = GameStage.Game;

            var levels = LoadLevels().ToArray();
            var scenePainter = new ScenePainter(levels);
            TerrainControl = new TerrainControl(scenePainter);

            BackColor = Color.LightGray;

            LabelHp = new Label()
            {
                Size = new Size(110, 50),
                Top = 0,
                Left = TerrainControl.TerrainClientSize.Width + 10,
                Font = new Font(FontFamily.GenericSerif, 23, FontStyle.Bold),
                ForeColor = Color.Purple
            };

            LabelHealerText = new Label()
            {
                Size = new Size(23, 40),
                Top = 60,
                Left = TerrainControl.TerrainClientSize.Width + 10,
                Font = new Font(FontFamily.GenericSerif, 23, FontStyle.Bold),
                ForeColor = Color.Purple
            };

            LabelHealerImage = new Label()
            {
                Size = new Size(30, 30),
                Top = 65,
                Left = TerrainControl.TerrainClientSize.Width + 40,
            };

            LabelRedKeyText = new Label()
            {
                Size = new Size(23, 40),
                Top = 120,
                Left = TerrainControl.TerrainClientSize.Width + 20,
                Font = new Font(FontFamily.GenericSerif, 23, FontStyle.Bold),
                ForeColor = Color.Purple
            };

            LabelRedKeyImage = new Label()
            {
                Size = new Size(30, 30),
                Top = 125,
                Left = TerrainControl.TerrainClientSize.Width + 50,
            };

            LabelGreenKeyText = new Label()
            {
                Size = new Size(23, 40),
                Top = 180,
                Left = TerrainControl.TerrainClientSize.Width + 20,
                Font = new Font(FontFamily.GenericSerif, 23, FontStyle.Bold),
                ForeColor = Color.Purple
            };

            LabelGreenKeyImage = new Label()
            {
                Size = new Size(30, 30),
                Top = 185,
                Left = TerrainControl.TerrainClientSize.Width + 50,
            };

            LabelBlueKeyText = new Label()
            {
                Size = new Size(23, 40),
                Top = 240,
                Left = TerrainControl.TerrainClientSize.Width + 20,
                Font = new Font(FontFamily.GenericSerif, 23, FontStyle.Bold),
                ForeColor = Color.Purple
            };

            LabelBlueKeyImage = new Label()
            {
                Size = new Size(30, 30),
                Top = 245,
                Left = TerrainControl.TerrainClientSize.Width + 50,
            };

            LabelHandText = new Label()
            {
                Size = new Size(23, 40),
                Left = TerrainControl.TerrainClientSize.Width + 10,
                Top = 600,
                Font = new Font(FontFamily.GenericSerif, 23, FontStyle.Bold),
                ForeColor = Color.Green
            };

            LabelHandImage = new Label()
            {
                Size = new Size(30, 30),
                Top = 550,
                Left = TerrainControl.TerrainClientSize.Width + 10
            };

            LabelWoodSwoardText = new Label()
            {
                Size = new Size(23, 40),
                Left = TerrainControl.TerrainClientSize.Width + 40,
                Top = 600,
                Font = new Font(FontFamily.GenericSerif, 23, FontStyle.Bold),
                ForeColor = Color.Green
            };

            LabelWoodSwoardImage = new Label()
            {
                Size = new Size(30, 30),
                Top = 550,
                Left = TerrainControl.TerrainClientSize.Width + 40
            };

            LabelSteelSwoardText = new Label()
            {
                Size = new Size(23, 40),
                Left = TerrainControl.TerrainClientSize.Width + 70,
                Top = 600,
                Font = new Font(FontFamily.GenericSerif, 23, FontStyle.Bold),
                ForeColor = Color.Green
            };

            LabelSteelSwoardImage = new Label()
            {
                Size = new Size(30, 30),
                Top = 550,
                Left = TerrainControl.TerrainClientSize.Width + 70
            };

            Controls.Add(TerrainControl);

            Controls.Add(LabelHp);

            Controls.Add(LabelHealerText);
            Controls.Add(LabelHealerImage);

            Controls.Add(LabelRedKeyText);
            Controls.Add(LabelRedKeyImage);

            Controls.Add(LabelGreenKeyText);
            Controls.Add(LabelGreenKeyImage);

            Controls.Add(LabelBlueKeyText);
            Controls.Add(LabelBlueKeyImage);

            Controls.Add(LabelHandText);
            Controls.Add(LabelHandImage);

            Controls.Add(LabelWoodSwoardText);
            Controls.Add(LabelWoodSwoardImage);

            Controls.Add(LabelSteelSwoardText);
            Controls.Add(LabelSteelSwoardImage);

        }

        public static void ShowNpcMessages(List<string> messages)
        {
            TerrainControl.Timer.Stop();
            NpcMessage.BringToFront();
            NpcMessage.Show();
            if (messages.Count != 0)
            {
                NpcMessage.Messages = messages;
                NpcMessage.Label.Text = messages[0];
            }
            else
            {
                NpcMessage.Hide();
                TerrainControl.Timer.Start();
            }
        }

        public static void Over()
        {
            TerrainControl.Timer.Stop();
            MessageBox.Show();
            MessageBox.Focus();
            TerrainControl.SendToBack();//-> в mainForm
        }

        private static IEnumerable<Map> LoadLevels()
        {
            //yield return Map.FromText(Properties.Resources.EnemyMoveTestMap1);
            //yield return Map.FromText(Properties.Resources.TestMap);
            //yield return Map.FromText(Properties.Resources.TestSizeMap_20x20);
            yield return Map.FromText(Properties.Resources.Level1);
            yield return Map.FromText(Properties.Resources.Level2);
            yield return Map.FromText(Properties.Resources.Level3);
            yield return Map.FromText(Properties.Resources.Level4);
            yield return Map.FromText(Properties.Resources.Level5);
            //yield return Map.FromText(Properties.Resources.TestMap3);
            //yield return Map.FromText(Properties.Resources.TestMap_2);
        }
    }
}
