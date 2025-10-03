using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenSaver
{
    public partial class MainForm : Form
    {
        private List<Snowflake> snowflakes;
        private Random random;
        private System.Windows.Forms.Timer timer;
        private Image snowflakeImage;

        public MainForm()
        {
            InitializeComponent();
            InitializeSnowfall();
        }

        private void InitializeSnowfall()
        {
            this.TopMost = true;
            this.DoubleBuffered = true;

            snowflakeImage = Properties.Resources.SnowflakeImage;
            random = new Random();
            snowflakes = new List<Snowflake>();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 16;
            timer.Tick += AnimationTimer_Tick;
            timer.Start();

            for (int i = 0; i < 100; i++)
            {
                snowflakes.Add(CreateRandomSnowflake());
            }

            this.KeyDown += (s, e) => this.Close();
            this.MouseClick += (s, e) => this.Close();
        }

        private Snowflake CreateRandomSnowflake()
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            float sizeMultiplier = 0.02f + (float)random.NextDouble() * 0.08f;
            float fallSpeed = 1 + sizeMultiplier * 40;
            float swayAmount = random.Next(1, 4);

            return new Snowflake
            {
                X = random.Next(-50, screenWidth + 50),
                Y = random.Next(-screenHeight, -20),
                SizeMultiplier = sizeMultiplier,
                FallSpeed = fallSpeed,
                SwaySpeed = (float)(random.NextDouble() * 0.05f + 0.01f),
                SwayAmount = swayAmount,
                SwayOffset = (float)(random.NextDouble() * Math.PI * 2)
            };
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            for (int i = 0; i < snowflakes.Count; i++)
            {
                Snowflake snowflake = snowflakes[i];
                snowflake.Y += snowflake.FallSpeed;
                snowflake.X += (float)(Math.Sin(snowflake.SwayOffset) * snowflake.SwayAmount);
                snowflake.SwayOffset += snowflake.SwaySpeed;

                if (snowflake.Y > screenHeight + 50) snowflake.Y = -50;
                if (snowflake.X < -100) snowflake.X = screenWidth + 50;
                if (snowflake.X > screenWidth + 100) snowflake.X = -50;
            }

            if (snowflakes.Count < 150 && random.Next(100) < 3)
            {
                snowflakes.Add(CreateRandomSnowflake());
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (Snowflake snowflake in snowflakes)
            {
                int width = (int)(snowflakeImage.Width * snowflake.SizeMultiplier);
                int height = (int)(snowflakeImage.Height * snowflake.SizeMultiplier);
                e.Graphics.DrawImage(snowflakeImage, snowflake.X - width / 2, snowflake.Y - height / 2, width, height);
            }
        }
    }
}