using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GLTrackControl.Graphics
{
    public class Game
    {
        GameWindow window;
        double xAxis, yAxis, zAxis;
        double theta;

        public Game(GameWindow window, double xAxis, double yAxis, double zAxis)
        {
            this.window = window;
            this.xAxis = xAxis;
            this.yAxis = yAxis;
            this.zAxis = zAxis;
            Start();
        }

        void Start()
        {
            window.Load += loaded;
            window.Resize += resize;
            window.RenderFrame += renderF;
            //window.UpdateFrame
            window.Run(1.0 / 60.0);
        }
        void resize(object o, EventArgs e)
        {
            GL.Viewport(0, 0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(1.5f, (float)(window.Width / window.Height), 1.0f, 200.0f);
            GL.LoadMatrix(ref matrix);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        void renderF(object o, EventArgs e)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.PushMatrix();

            GL.Translate(5.0, 0.0, -30.0);
            GL.Rotate(9.0, 90.0, -90.0, 1.0);
            GL.Rotate(theta, 0.0, 1.0, 1.0);
            //GL.Rotate(theta, 1.0, 1.0, 0.0);

            // draw 3 Coordinates Axes
            drawCoordinatesAxes();

            DrawSphere(0.5, 50, 50, 10, 10, 10);

            // increment degree to rotate
            theta += 0.5;


            window.SwapBuffers();
        }

        void loaded(object o, EventArgs e)
        {
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f); // black background
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Enable(EnableCap.DepthTest);
        }

        void drawCoordinatesAxes()
        {
            // X axis: red
            GL.Begin(PrimitiveType.Lines);
            Colors.Red();
            GL.Vertex3(-20.0, 0, 0.0f); // origin of the line
            GL.Vertex3(20.0, 0.0, 0.0f); // ending point of the line
            GL.End();

            // Y axis: green
            GL.Begin(PrimitiveType.Lines);
            Colors.Green();
            GL.Vertex3(0.0, -20.0, 0.0); // origin of the line
            GL.Vertex3(0.0, 20.0, 0.0); // ending point of the line
            GL.End();

            // Z axis: blue
            GL.Begin(PrimitiveType.Lines);
            Colors.Blue();
            GL.Vertex3(0.0, 0.0, -20.0); // origin of the line
            GL.Vertex3(0.0, 0.0, 20.0); // ending point of the line
            GL.End();

            // (45.0) point
            //DrawSphere(0.3, 100, 100, 0, 0, 0);

            // draw XY grid
            drawGrid(20, 20, 20, 4);
        }

        /**
         * <param name="s">space between 2 grids </param>
         */
        void drawGrid(double x, double y, double z, double s)
        {

            // draw Y grid
            int t1 = Convert.ToInt32(x / s); // iteration
            for (int i = -t1; i <= t1; i++)
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(0.5, 0.5, 0.5);
                GL.LineWidth(5);
                GL.Vertex3(i * s, -y, 0.0);
                GL.Vertex3(i * s, y, 0.0);
                GL.End();
            }

            // draw X grid
            int t2 = Convert.ToInt32(y / s); // iteration
            for (int i = -t2; i <= t2; i++)
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(0.5, 0.5, 0.5);
                GL.LineWidth(5);
                GL.Vertex3(-x, i * s, 0.0);
                GL.Vertex3(x, i * s, 0.0);
                GL.End();
            }

        }

        void DrawSphere(double r, int lats, int longs, int X, int Y, int Z)
        {
            int i, j;
            double M_PI = 3.14;
            for (i = 0; i <= lats; i++)
            {
                double lat0 = M_PI * (-0.5 + (double)(i - 1) / lats);
                double z0 = Math.Sin(lat0);
                double zr0 = Math.Cos(lat0);

                double lat1 = M_PI * (-0.5 + (double)i / lats);
                double z1 = Math.Sin(lat1);
                double zr1 = Math.Cos(lat1);

                GL.Begin(PrimitiveType.QuadStrip);
                GL.Color3(1.0, 0.0, 0.0);
                for (j = 0; j <= longs; j++)
                {
                    double lng = 2 * M_PI * (double)(j - 1) / longs;
                    double x = Math.Cos(lng);
                    double y = Math.Sin(lng);

                    GL.Normal3(x * zr0, y * zr0, z0);
                    GL.Vertex3(r * x * zr0 + X, r * y * zr0 + Y, r * z0 + Z);
                    GL.Normal3(x * zr1, y * zr1, z1);
                    GL.Vertex3(r * x * zr1 + X, r * y * zr1 + Y, r * z1 + Z);
                }
                GL.End();
            }
        }

        void drawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(1.0, 1.0, 0.0);
            GL.Vertex3(-10.0, 10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            GL.Color3(1.0, 0.0, 1.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(10.0, -10.0, 10.0);

            GL.Color3(0.0, 1.0, 1.0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            GL.Color3(1.0, 0.0, 0.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);

            GL.Color3(0.0, 1.0, 0.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);

            GL.Color3(0.0, 0.0, 1.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);

            GL.End();
        }

        void drawText()
        {
        }
    }
}
