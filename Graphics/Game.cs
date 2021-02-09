using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using GLTrackControl.Robots;

namespace GLTrackControl.Graphics
{
    public class Game
    {
        GameWindow window;
        private double xAxisLen, yAxisLen, zAxisLen, gridSpacing;
        private double rotateDegree;

        public Game(GameWindow window, double xAxisLen, double yAxisLen, double zAxisLen, double gridSpacing)
        {
            this.window = window;
            this.xAxisLen = xAxisLen;
            this.yAxisLen = yAxisLen;
            this.zAxisLen = zAxisLen;
            this.gridSpacing = gridSpacing;
            Start();
        }

        void Start()
        {
            window.Load += Loaded;
            window.Resize += Resize;
            window.RenderFrame += RenderF;
            //window.UpdateFrame
            window.Run(1.0 / 60.0);
        }
        void Resize(object o, EventArgs e)
        {
            GL.Viewport(0, 0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(1.5f, (float)(window.Width / window.Height), 1.0f, 200.0f);
            GL.LoadMatrix(ref matrix);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        void RenderF(object o, EventArgs e)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.PushMatrix();

            GL.Translate(5.0, 0.0, -30.0);
            GL.Rotate(9.0, 90.0, -90.0, 1.0);
            GL.Rotate(rotateDegree, 0.0, 1.0, 1.0);
            //GL.Rotate(theta, 1.0, 1.0, 0.0);

            DisplayCoordinatesAxes();
            DisplayDrone(0.5, 50, 50, 10, 10, 10);

            rotateDegree += 0.5;
            window.SwapBuffers();
        }

        void Loaded(object o, EventArgs e)
        {
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f); // black background
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Enable(EnableCap.DepthTest);
        }

        void DisplayCoordinatesAxes()
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

            DisplayXYGrid(xAxisLen, yAxisLen, zAxisLen, gridSpacing);
        }

        void DisplayXYGrid(double x, double y, double z, double gridSpace)
        {

            // draw Y grid
            int t1 = Convert.ToInt32(x / gridSpace); // iteration
            for (int i = -t1; i <= t1; i++)
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(0.5, 0.5, 0.5);
                GL.LineWidth(5);
                GL.Vertex3(i * gridSpace, -y, 0.0);
                GL.Vertex3(i * gridSpace, y, 0.0);
                GL.End();
            }

            // draw X grid
            int t2 = Convert.ToInt32(y / gridSpace); // iteration
            for (int i = -t2; i <= t2; i++)
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(0.5, 0.5, 0.5);
                GL.LineWidth(5);
                GL.Vertex3(-x, i * gridSpace, 0.0);
                GL.Vertex3(x, i * gridSpace, 0.0);
                GL.End();
            }
        }

        void DisplayDrone(double r, int lats, int longs, int X, int Y, int Z)
        {
            var s = new Drone(X, Y, Z);
            s.Color(1.0, 0.0, 0.0);
            s.Display(r, lats, longs);
        }
    }
}