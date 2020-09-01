using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class Camera
    {
        public Vertex eye;
        public Vertex center;
        public Vertex up;
        public string Name="Камера";
        public double focus = 1000;
        public int scale = 1;

        public double pitch = 0;
        public double yaw = -90;
        public double roll = 0;
        public double CamSpeed = 5f;

        public Camera()
        {
            eye = new Vertex(0, 0, 1);
            center = new Vertex(0, 0, 0);
            up = new Vertex(0, 1, 0);
        }

    }
}
