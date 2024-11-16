using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace DrawingVisualApp
{
    // Based on https://editor.p5js.org/Shayan-To/sketches/bwwPOY8L_

    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer;
        public Random rnd = new Random();
        public int w, h;
        WriteableBitmap wb;

        int d = 2;
        int colorMax = 500;
        int stepsPerFrame = 20;
        int x, y, step;
        bool[,] arr;
        int rx, ry;


        public MainWindow()
        {
            InitializeComponent();

            w = (int)g.Width;
            h = (int)g.Height;

            wb = new WriteableBitmap(w, h, 96, 96, PixelFormats.Bgr32, null);
            g.Source = wb;
            wb.Clear(Colors.White);

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);

            Init();
        }

        private void timerTick(object sender, EventArgs e) => Drawing();

        private void Init()
        {
            arr = new bool[h, w];
            x = w / 2;
            y = h / 2;
            step = 0;

            timer.Start();
        }

        private void Drawing()
        {

            
            for (int iii = 0; iii < stepsPerFrame; iii += 1)
            {
                if (step >= w * h)
                {
                    return;
                }

                while (true)
                {
                    rx = rnd.Next(-1, 2);
                    ry = rnd.Next(-1, 2);
                    var maxSteps = rnd.Next(w + h);
                    for (int i = 0; i < maxSteps; i += 1)
                    {
                        x = (x + rx + w) % w;
                        y = (y + ry + h) % h;
                        if (arr[y, x] != true)
                        {
                            break;
                        }
                    }
                    if (arr[y, x] != true)
                    {
                        break;
                    }
                }

                arr[y, x] = true;
                step += 1;
                byte r = 0;
                byte g = 0;
                byte b = 0;
                Helper.HsvToRgb(step % colorMax, 1, 1, out r, out g, out b);
                wb.SetPixel(x, y, r, g, b);
            }
        }
    }
}
