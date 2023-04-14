namespace Fractals
{
    public partial class Form1 : Form
    {
        int zoom = 200;
        float zoomStep = 1.5f;
        int pbWidth;
        int pbHeight;
        float visibleWidth;
        float visibleHeight;
        float offsetX;
        float offsetY;

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

        private void Draw(float zoom, float offsetX, float offsetY)
        {
            Bitmap bmp = new Bitmap(pbWidth, pbHeight);
            visibleWidth = ((float)pbWidth / zoom);
            visibleHeight = ((float)pbHeight / zoom);
            float halfWidth = visibleWidth / 2;
            float halfHeight = visibleHeight / 2;
            for (int x = 0; x < pictureBox.Width; x++)
            {
                for (int y = 0; y < pictureBox.Height; y++)
                {
                    float magic = DoMagic((float)x * visibleWidth / pbWidth - halfWidth - offsetX, (float)y * visibleHeight / pbHeight - halfHeight - offsetY);
                    int alpha = 255 - (int)magic;
                    bmp.SetPixel(x, y, Color.FromArgb(alpha, Color.Black));
                }
                pbG.FillRectangle(mainBrush, x, pbHeight - 10, 1, 8);
            }
            pictureBox.Image = bmp;
        }

        private float DoMagicLazy(float x, float y)
        {
            return DoMagicLazy(new PointF(x, y));
        }

        private float DoMagic(float x, float y)
        {
            return DoMagic(new PointF(x, y));
        }

        private float DoMagicLazy(PointF p)
        {
            if (p.X > -0.1 && p.X < 0.1 && p.Y > -0.1 && p.Y < 0.1)
            {
                ;
            }
            PointF pNext = new PointF(p.X * p.X - p.Y * p.Y + p.X, 2 * p.X * p.Y +p.Y);
            float difX = pNext.X * pNext.X - p.X * p.X; ;
            float difY = pNext.Y * pNext.Y - p.Y * p.Y;
            float dif = difX + difY;
            if (dif <= 0) return 0;
            return 100 - 100 / (1 + MathF.Sqrt(dif));
        }

        private float DoMagic(PointF p)
        {
            PointF result = p;
            int i = 0;
            int iMax = 255;
            for (; i < iMax; i++)
            {
                PointF buf = result;
                result.X = + buf.X * buf.X - buf.Y * buf.Y + p.X;
                result.Y = + 2 * buf.X * buf.Y + p.Y;
                if (result.X == float.PositiveInfinity || 
                    result.X == float.NegativeInfinity ||
                    result.Y == float.PositiveInfinity ||
                    result.Y == float.NegativeInfinity)
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
                offsetX += ((float)pbWidth - clickevent.X) / zoom - visibleWidth / 2;
                offsetY += ((float)pbHeight - clickevent.Y) / zoom - visibleHeight / 2;
                zoom = (int)(zoom * zoomStep);
            }
            else if (clickevent.Button == MouseButtons.Right)
            {
                offsetX -= ((float)pbWidth - clickevent.X) / zoom - visibleWidth / 2;
                offsetY -= ((float)pbHeight - clickevent.Y) / zoom - visibleHeight / 2;
                zoom = (int)(zoom / zoomStep);
            }
            Draw(zoom, offsetX, offsetY);
        }
    }
}