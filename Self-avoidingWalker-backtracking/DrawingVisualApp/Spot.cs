using System.Collections.Generic;


namespace DrawingVisualApp
{
    class Spot
    {
        int i, j; // i - rows; j - cols
        public int x, y;
        public bool visited;
        List<Step> options;

        public Spot(int i, int j)
        {
            this.i = i; // i - rows; j - cols
            this.j = j;
            x = j * MainWindow.spacing;
            y = i * MainWindow.spacing;
            options = Options();
        }

        void Clear()
        {
            visited = false;
            options = Options();
        }

        List<Step> Options()
        {
            return new List<Step>
                {
                new Step(1, 0),
                new Step(-1, 0),
                new Step(0, 1),
                new Step(0, -1)
                };
        }

        bool isValid(int x, int y, Spot[,] grid)
        {
            if (x < 0 || x >= MainWindow.cols || y < 0 || y >= MainWindow.rows)
            {
                return false;
            }
            return !grid[y, x].visited;
        }


        public Spot NextSpot(Spot[,] grid)
        {
            var validOptions = new List<Step>();

            foreach (var option in options)
            {
                var newX = j + option.dx;
                var newY = i + option.dy;
                if (isValid(newX, newY, grid) && !option.tried)
                {
                    validOptions.Add(option);
                }
            }

            var len = validOptions.Count;
            if (len > 0)
            {
                var step = validOptions[MainWindow.rnd.Next(len)];
                step.tried = true;

                return grid[i + step.dy, j + step.dx];
            }
            else
            {
                return null;
            }
        }
    }
}
