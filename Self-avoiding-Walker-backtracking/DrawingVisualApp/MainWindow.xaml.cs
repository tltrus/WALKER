using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace DrawingVisualApp
{
    /// <summary>
    /// Based on #162 — Self Avoiding Walk  https://thecodingtrain.com/challenges/162-self-avoiding-walk
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer;
        public static Random rnd = new Random();
        public int width, height;

        DrawingVisual visual;
        DrawingContext dc;

        Spot[,] grid;
        public static int spacing = 10;
        public static int cols, rows;
        List<Spot> path;
        Spot spot;

        public MainWindow()
        {
            InitializeComponent();

            width = (int)g.Width;
            height = (int)g.Height;

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 20);

            Init();
        }

        private void timerTick(object sender, EventArgs e) => Drawing();

        private void Init()
        {
            path = new List<Spot>();

            cols = width / spacing;
            rows = height / spacing;

            grid = new Spot[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    grid[i, j] = new Spot(i, j);

            spot = grid[rows / 2, cols / 2];
            path.Add(spot);
            spot.visited = true;

            visual = new DrawingVisual();

            timer.Start();
        }

        private void Drawing()
        {
            g.RemoveVisual(visual);

            using (dc = visual.RenderOpen())
            {
                spot = spot.NextSpot(grid);
                if (spot is null)
                {
                    path.RemoveAt(path.Count - 1);
                    spot = path[path.Count - 1];
                }
                else
                {
                    path.Add(spot);
                    spot.visited = true;
                }

                if (path.Count == cols * rows)
                {
                    timer.Stop();
                }

                DrawLines(dc);

                var point = new Point(spot.x, spot.y);
                dc.DrawEllipse(Brushes.White, null, point, 4, 4);

                dc.Close();
                g.AddVisual(visual);
            }
        }

        private void DrawLines(DrawingContext dc)
        {
            for (int i = 0; i < path.Count-1; i++)
            {
                var p0 = new Point(path[i].x, path[i].y);
                var p1 = new Point(path[i+1].x, path[i+1].y);
                dc.DrawLine(new Pen(Brushes.White, 2), p0, p1);
            }
        }
    }
}
