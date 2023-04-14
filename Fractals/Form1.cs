namespace Fractals
{
    public partial class Form1 : Form
    {
        int zoom = 200;
        double zoomStep = 1.5f;
        int pbWidth;
        int pbHeight;
        double visibleWidth;
        double visibleHeight;
        double offsetX;
        double offsetY;

        Brush mainBrush = Brushes.Gray;
        Font tipFont = new Font("Arial", 20);
        Graphics pbG;

        public Form1()
        {
            InitializeComponent();
            pbWidth = pictureBox.Width;
            pbHeight = pictureBox.Height;
            pbG = pictureBox.CreateGraphics();
            Draw(zoom, 0, 0);
        }

        private void Draw(double zoom, double offsetX, double offsetY)
        {
            Bitmap bmp = new Bitmap(pbWidth, pbHeight);
            visibleWidth = (pbWidth / zoom);
            visibleHeight = (pbHeight / zoom);
            double halfWidth = visibleWidth / 2;
            double halfHeight = visibleHeight / 2;
            for (int x = 0; x < pictureBox.Width; x++)
            {
                for (int y = 0; y < pictureBox.Height; y++)
                {
                    double magic = DoMagic(x * visibleWidth / pbWidth - halfWidth - offsetX, y * visibleHeight / pbHeight - halfHeight - offsetY);
                    int alpha = 255 - (int)magic;
                    bmp.SetPixel(x, y, Color.FromArgb(alpha, Color.Black));
                }
                pbG.FillRectangle(mainBrush, x, pbHeight - 10, 1, 8);
            }
            pictureBox.Image = bmp;
        }

        private double DoMagicLazy(double x, double y)
        {
            return DoMagicLazy(new PointD(x, y));
        }

        private double DoMagic(double x, double y)
        {
            return DoMagic(new PointD(x, y));
        }

        private double DoMagicLazy(PointD p)
        {
            if (p.X > -0.1 && p.X < 0.1 && p.Y > -0.1 && p.Y < 0.1)
            {
                ;
            }
            PointD pNext = new PointD(p.X * p.X - p.Y * p.Y + p.X, 2 * p.X * p.Y +p.Y);
            double difX = pNext.X * pNext.X - p.X * p.X; ;
            double difY = pNext.Y * pNext.Y - p.Y * p.Y;
            double dif = difX + difY;
            if (dif <= 0) return 0;
            return 100 - 100 / (1 + Math.Sqrt(dif));
        }

        private double DoMagic(PointD p)
        {
            PointD result = p;
            int i = 0;
            int iMax = 255;
            for (; i < iMax; i++)
            {
                PointD buf = result;
                result.X = + buf.X * buf.X - buf.Y * buf.Y + p.X;
                result.Y = + 2 * buf.X * buf.Y + p.Y;
                if (result.X == double.PositiveInfinity || 
                    result.X == double.NegativeInfinity ||
                    result.Y == double.PositiveInfinity ||
                    result.Y == double.NegativeInfinity)
                {
                    break;
                }
                if (result.X == 0 && result.Y == 0)
                {
                    i = iMax;
                    break;
                }
            }
            if (iMax != 255) i = i / iMax * 255;
            return 255 - i;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            /*int ms = DateTime.Now.Millisecond;
            if (ms - lastDraw >= screenDelay)
            {
                zoom += zoomStep;
                Draw(zoom);
                lastDraw = ms;
            }*/
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs clickevent = ((MouseEventArgs)e);
            if (clickevent.Button == MouseButtons.Left)
            {
                offsetX += ((double)pbWidth - clickevent.X) / zoom - visibleWidth / 2;
                offsetY += ((double)pbHeight - clickevent.Y) / zoom - visibleHeight / 2;
                zoom = (int)(zoom * zoomStep);
            }
            else if (clickevent.Button == MouseButtons.Right)
            {
                offsetX -= ((double)pbWidth - clickevent.X) / zoom - visibleWidth / 2;
                offsetY -= ((double)pbHeight - clickevent.Y) / zoom - visibleHeight / 2;
                zoom = (int)(zoom / zoomStep);
            }
            Draw(zoom, offsetX, offsetY);
        }
    }
}