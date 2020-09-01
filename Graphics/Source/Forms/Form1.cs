using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Graphics
{
    public partial class Form1 : Form
    {
        public Scene sc;
        static int[] buff;

        public Form1()
        {
            InitializeComponent();
        }
        
        public Matrix3D lookat(Vertex eye, Vertex center, Vertex up)
        {
            Vertex CameraDirection = Vertex.normalize(Vertex.difference_vertex(eye, center));
            Vertex cameraRight = Vertex.normalize(Vertex.cross_vertex(up, CameraDirection));
            Vertex cameraUp = Vertex.normalize(Vertex.cross_vertex(CameraDirection, cameraRight));
            Matrix3D Minv = Matrix3D.Identity;
            Matrix3D Tr = Matrix3D.Identity;

            Minv.M11 = cameraRight.X;
            Minv.M21 = cameraUp.X;
            Minv.M31 = CameraDirection.X;
            Tr.M14 = -eye.X;

            Minv.M12 = cameraRight.Y;
            Minv.M22 = cameraUp.Y;
            Minv.M32 = CameraDirection.Y;
            Tr.M24 = -eye.Y;

            Minv.M13 = cameraRight.Z;
            Minv.M23 = cameraUp.Z;
            Minv.M33 = CameraDirection.Z;
            Tr.M34 = -eye.Z;

            return Minv * Tr;
        }



        //обмен
        static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }

        public void create_paral(Engine Engine, double width_st, double width_fn, double length_st, double length_fn, double height_down, double height_up, int c1)
        {

            Engine.VertexList.Add(new Vertex(length_st, height_down + 50, width_st));
            Engine.VertexList.Add(new Vertex(length_st, height_down + 50, width_fn));
            Engine.VertexList.Add(new Vertex(length_fn, height_down + 50, width_fn));
            Engine.VertexList.Add(new Vertex(length_fn, height_down + 50, width_st));
            Engine.VertexList.Add(new Vertex(length_st, height_up + 50, width_st));
            Engine.VertexList.Add(new Vertex(length_fn, height_up + 50, width_st));
            Engine.VertexList.Add(new Vertex(length_fn, height_up + 50, width_fn));
            Engine.VertexList.Add(new Vertex(length_st, height_up + 50, width_fn));

            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 8, Engine.VertexList.Count - 7, Engine.VertexList.Count - 6, c1));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 6, Engine.VertexList.Count - 5, Engine.VertexList.Count - 8, c1));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 4, Engine.VertexList.Count - 3, Engine.VertexList.Count - 2, c1));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 2, Engine.VertexList.Count - 1, Engine.VertexList.Count - 4, c1));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 8, Engine.VertexList.Count - 5, Engine.VertexList.Count - 3, c1));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 3, Engine.VertexList.Count - 4, Engine.VertexList.Count - 8, c1));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 5, Engine.VertexList.Count - 6, Engine.VertexList.Count - 2, c1));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 2, Engine.VertexList.Count - 3, Engine.VertexList.Count - 5, c1));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 6, Engine.VertexList.Count - 7, Engine.VertexList.Count - 1, c1));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 2, Engine.VertexList.Count - 6, c1));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 7, Engine.VertexList.Count - 8, Engine.VertexList.Count - 4, c1));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 4, Engine.VertexList.Count - 1, Engine.VertexList.Count - 7, c1));
        }

        public void create_cyl(Engine Engine, double lenght_st, double lenght_fn, double width, double height, double radius, int c)
        {

            double dop = radius * Math.Sin(0.7854);

            Engine.VertexList.Add(new Vertex(width - radius, height, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius + dop, height + dop, lenght_st));
            Engine.VertexList.Add(new Vertex(width, height, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius, height + radius, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius - dop, height + dop, lenght_st));
            Engine.VertexList.Add(new Vertex(width - 2 * radius, height, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius - dop, height - dop, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius, height - radius, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius + dop, height - dop, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius + dop, height + dop, lenght_fn));
            Engine.VertexList.Add(new Vertex(width, height, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius, height + radius, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius - dop, height + dop, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius - radius, height, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius - dop, height - dop, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius, height - radius, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius + dop, height - dop, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius, height, lenght_fn));


            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 17, Engine.VertexList.Count - 16, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 15, Engine.VertexList.Count - 17, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 14, Engine.VertexList.Count - 15, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 13, Engine.VertexList.Count - 14, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 12, Engine.VertexList.Count - 13, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 11, Engine.VertexList.Count - 12, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 10, Engine.VertexList.Count - 11, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 16, Engine.VertexList.Count - 10, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 16, Engine.VertexList.Count - 9, Engine.VertexList.Count - 8, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 16, Engine.VertexList.Count - 17, Engine.VertexList.Count - 9, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 17, Engine.VertexList.Count - 7, Engine.VertexList.Count - 9, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 17, Engine.VertexList.Count - 15, Engine.VertexList.Count - 7, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 15, Engine.VertexList.Count - 6, Engine.VertexList.Count - 7, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 15, Engine.VertexList.Count - 14, Engine.VertexList.Count - 6, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 14, Engine.VertexList.Count - 5, Engine.VertexList.Count - 6, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 14, Engine.VertexList.Count - 13, Engine.VertexList.Count - 5, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 13, Engine.VertexList.Count - 4, Engine.VertexList.Count - 5, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 13, Engine.VertexList.Count - 12, Engine.VertexList.Count - 4, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 12, Engine.VertexList.Count - 3, Engine.VertexList.Count - 4, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 12, Engine.VertexList.Count - 11, Engine.VertexList.Count - 3, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 11, Engine.VertexList.Count - 2, Engine.VertexList.Count - 3, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 11, Engine.VertexList.Count - 10, Engine.VertexList.Count - 2, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 10, Engine.VertexList.Count - 8, Engine.VertexList.Count - 2, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 10, Engine.VertexList.Count - 16, Engine.VertexList.Count - 8, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 8, Engine.VertexList.Count - 9, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 9, Engine.VertexList.Count - 7, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 7, Engine.VertexList.Count - 6, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 6, Engine.VertexList.Count - 5, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 5, Engine.VertexList.Count - 4, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 4, Engine.VertexList.Count - 3, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 3, Engine.VertexList.Count - 2, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 2, Engine.VertexList.Count - 8, c));
        }

        public void little_mufta(Engine Engine, double lenght_st, double lenght_fn, double width, double height, double radius, int c)
        {

            double dop = radius * Math.Sin(0.7854);

            Engine.VertexList.Add(new Vertex(width - radius, height, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius + dop, height + dop, lenght_st));
            Engine.VertexList.Add(new Vertex(width, height, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius, height + radius, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius - dop, height + dop, lenght_st));
            Engine.VertexList.Add(new Vertex(width - 2 * radius, height, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius - dop, height - dop, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius, height - radius, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius + dop, height - dop, lenght_st));
            Engine.VertexList.Add(new Vertex(width - radius + dop, height + dop, lenght_fn));
            Engine.VertexList.Add(new Vertex(width, height, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius, height + radius, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius - dop, height + dop, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius - radius, height, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius - dop, height - dop, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius, height - radius, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius + dop, height - dop, lenght_fn));
            Engine.VertexList.Add(new Vertex(width - radius, height, lenght_fn));


            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 17, Engine.VertexList.Count - 16, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 15, Engine.VertexList.Count - 17, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 14, Engine.VertexList.Count - 15, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 13, Engine.VertexList.Count - 14, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 12, Engine.VertexList.Count - 13, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 11, Engine.VertexList.Count - 12, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 10, Engine.VertexList.Count - 11, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 16, Engine.VertexList.Count - 10, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 16, Engine.VertexList.Count - 9, Engine.VertexList.Count - 8, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 16, Engine.VertexList.Count - 17, Engine.VertexList.Count - 9, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 17, Engine.VertexList.Count - 7, Engine.VertexList.Count - 9, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 17, Engine.VertexList.Count - 15, Engine.VertexList.Count - 7, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 15, Engine.VertexList.Count - 6, Engine.VertexList.Count - 7, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 15, Engine.VertexList.Count - 14, Engine.VertexList.Count - 6, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 14, Engine.VertexList.Count - 5, Engine.VertexList.Count - 6, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 14, Engine.VertexList.Count - 13, Engine.VertexList.Count - 5, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 13, Engine.VertexList.Count - 4, Engine.VertexList.Count - 5, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 13, Engine.VertexList.Count - 12, Engine.VertexList.Count - 4, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 12, Engine.VertexList.Count - 3, Engine.VertexList.Count - 4, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 12, Engine.VertexList.Count - 11, Engine.VertexList.Count - 3, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 11, Engine.VertexList.Count - 2, Engine.VertexList.Count - 3, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 11, Engine.VertexList.Count - 10, Engine.VertexList.Count - 2, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 10, Engine.VertexList.Count - 8, Engine.VertexList.Count - 2, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 10, Engine.VertexList.Count - 16, Engine.VertexList.Count - 8, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 8, Engine.VertexList.Count - 9, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 9, Engine.VertexList.Count - 7, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 7, Engine.VertexList.Count - 6, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 6, Engine.VertexList.Count - 5, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 5, Engine.VertexList.Count - 4, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 4, Engine.VertexList.Count - 3, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 3, Engine.VertexList.Count - 2, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 2, Engine.VertexList.Count - 8, c));
        }



        public void create_legs(Engine Engine, double[,] coord_legs, double heightLeg, int i, int c)
        {
            double radius_leg = 15;
            double dop = radius_leg * Math.Sin(0.7854);
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0], -heightLeg, -coord_legs[i, 1]));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0], -heightLeg, -coord_legs[i, 1] - radius_leg));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0] + dop, -heightLeg, -(coord_legs[i, 1] + dop)));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0] + radius_leg, -heightLeg, -coord_legs[i, 1]));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0] - radius_leg, -heightLeg, -(coord_legs[i, 1])));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0] - dop, -heightLeg, -(coord_legs[i, 1] + dop)));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0], -heightLeg, -(coord_legs[i, 1] - radius_leg)));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0] - dop, -heightLeg, -(coord_legs[i, 1] - dop)));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0] + dop, -heightLeg, -(coord_legs[i, 1] - dop)));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0] + dop, 0, -(coord_legs[i, 1] + dop)));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0] + radius_leg, 0, -(coord_legs[i, 1])));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0], 0, -(coord_legs[i, 1] + radius_leg)));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0] - dop, 0, -(coord_legs[i, 1] + dop)));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0] - radius_leg, 0, -(coord_legs[i, 1])));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0] - dop, 0, -(coord_legs[i, 1] - dop)));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0], 0, -(coord_legs[i, 1] - radius_leg)));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0] + dop, 0, -(coord_legs[i, 1] - dop)));
            Engine.VertexList.Add(new Vertex(coord_legs[i, 0], 0, -coord_legs[i, 1]));

            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 17, Engine.VertexList.Count - 16, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 14, Engine.VertexList.Count - 13, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 12, Engine.VertexList.Count - 11, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 15, Engine.VertexList.Count - 10, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 17, Engine.VertexList.Count - 13, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 14, Engine.VertexList.Count - 11, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 12, Engine.VertexList.Count - 10, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 18, Engine.VertexList.Count - 15, Engine.VertexList.Count - 16, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 15, Engine.VertexList.Count - 16, Engine.VertexList.Count - 9, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 16, Engine.VertexList.Count - 17, Engine.VertexList.Count - 7, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 17, Engine.VertexList.Count - 13, Engine.VertexList.Count - 6, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 13, Engine.VertexList.Count - 14, Engine.VertexList.Count - 5, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 9, Engine.VertexList.Count - 8, Engine.VertexList.Count - 15, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 9, Engine.VertexList.Count - 7, Engine.VertexList.Count - 16, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 7, Engine.VertexList.Count - 6, Engine.VertexList.Count - 17, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 6, Engine.VertexList.Count - 5, Engine.VertexList.Count - 13, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 14, Engine.VertexList.Count - 11, Engine.VertexList.Count - 4, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 11, Engine.VertexList.Count - 12, Engine.VertexList.Count - 3, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 12, Engine.VertexList.Count - 10, Engine.VertexList.Count - 2, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 10, Engine.VertexList.Count - 15, Engine.VertexList.Count - 8, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 5, Engine.VertexList.Count - 4, Engine.VertexList.Count - 14, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 4, Engine.VertexList.Count - 3, Engine.VertexList.Count - 11, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 3, Engine.VertexList.Count - 2, Engine.VertexList.Count - 12, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 2, Engine.VertexList.Count - 8, Engine.VertexList.Count - 10, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 2, Engine.VertexList.Count - 8, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 9, Engine.VertexList.Count - 7, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 6, Engine.VertexList.Count - 5, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 4, Engine.VertexList.Count - 3, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 2, Engine.VertexList.Count - 3, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 9, Engine.VertexList.Count - 8, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 6, Engine.VertexList.Count - 7, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 4, Engine.VertexList.Count - 5, c));
        }

        public void create_cone(Engine Engine, double width, double width_dop, double height, double diamAnt, double heightAnt, int c)
        {
            double dop1 = (double)diamAnt / 2 * Math.Sin(0.7854);

            Engine.VertexList.Add(new Vertex(width_dop - 5, (double)height / 2 + 5, -(width + 5)));
            Engine.VertexList.Add(new Vertex(width_dop - 5, (double)height / 2 + 5, -(width + 5 + (double)diamAnt / 2)));
            Engine.VertexList.Add(new Vertex(width_dop - 5 + dop1, (double)height / 2 + 5, -(width + 5 + dop1)));
            Engine.VertexList.Add(new Vertex(width_dop - 5 + (double)diamAnt / 2, (double)height / 2 + 5, -(width + 5)));
            Engine.VertexList.Add(new Vertex(width_dop - 5 - (double)diamAnt / 2, (double)height / 2 + 5, -(width + 5)));
            Engine.VertexList.Add(new Vertex(width_dop - 5 - dop1, height / 2 + 5, -(width + 5 + dop1)));
            Engine.VertexList.Add(new Vertex(width_dop - 5, (double)height / 2 + 5, -(width + 5 - (double)diamAnt / 2)));
            Engine.VertexList.Add(new Vertex(width_dop - 5 - dop1, (double)height / 2 + 5, -(width + 5 - dop1)));
            Engine.VertexList.Add(new Vertex(width_dop - 5 + dop1, (double)height / 2 + 5, -(width + 5 - dop1)));
            Engine.VertexList.Add(new Vertex(width_dop - 5, (double)height / 2 + 5 + heightAnt, -(width + 5)));

            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 10, Engine.VertexList.Count - 9, Engine.VertexList.Count - 8, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 10, Engine.VertexList.Count - 6, Engine.VertexList.Count - 5, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 10, Engine.VertexList.Count - 4, Engine.VertexList.Count - 3, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 10, Engine.VertexList.Count - 7, Engine.VertexList.Count - 2, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 10, Engine.VertexList.Count - 7, Engine.VertexList.Count - 8, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 10, Engine.VertexList.Count - 9, Engine.VertexList.Count - 5, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 10, Engine.VertexList.Count - 6, Engine.VertexList.Count - 3, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 10, Engine.VertexList.Count - 4, Engine.VertexList.Count - 2, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 7, Engine.VertexList.Count - 8, Engine.VertexList.Count - 1, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 8, Engine.VertexList.Count - 9, Engine.VertexList.Count - 1, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 9, Engine.VertexList.Count - 5, Engine.VertexList.Count - 1, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 5, Engine.VertexList.Count - 6, Engine.VertexList.Count - 1, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 6, Engine.VertexList.Count - 3, Engine.VertexList.Count - 1, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 3, Engine.VertexList.Count - 4, Engine.VertexList.Count - 1, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 4, Engine.VertexList.Count - 2, Engine.VertexList.Count - 1, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 2, Engine.VertexList.Count - 7, Engine.VertexList.Count - 1, c));
            Engine.FaceList.Add(new Face(Engine.VertexList.Count - 1, Engine.VertexList.Count - 1, Engine.VertexList.Count - 1, c));

        }

        double ser;

        //метод создание вновь смоделированнового объекта
        public void Create(string Name, int width, int length, int height, int countInd, int lipInd, int countAnt, int heightAnt, int diamAnt, int countBut, int lipBut, int heightLeg, int countLeg, int lengthLipCap, int heightCap, string[] mass, Color c1, Color c2, Color c3, Color c4, Color c5, Color c6)
        {
            Engine Engine = new Engine();
            Engine.NameEngine = Name;

            Engine.VertexList.Add(new Vertex(0, 0, 0));

            Engine.parameters[0] = width.ToString();
            Engine.parameters[1] = length.ToString();
            Engine.parameters[2] = height.ToString();
            Engine.parameters[3] = countInd.ToString();
            Engine.parameters[4] = lipInd.ToString();
            Engine.parameters[5] = countAnt.ToString();
            Engine.parameters[6] = heightAnt.ToString();
            Engine.parameters[7] = diamAnt.ToString();
            Engine.parameters[8] = countBut.ToString();
            Engine.parameters[9] = lipBut.ToString();
            Engine.parameters[10] = heightLeg.ToString();
            Engine.parameters[11] = countLeg.ToString();
            Engine.parameters[12] = lengthLipCap.ToString();
            Engine.parameters[13] = heightCap.ToString();

            for (int i = 0; i < mass.Length - 4; i++)
            {
                Engine.parameters[14 + i] = 0.ToString();
            }

            Engine.parameters[20] = 1.ToString();
            Engine.parameters[21] = 1.ToString();
            Engine.parameters[22] = 1.ToString();
            Engine.parameters[23] = 1.ToString();

            Engine.colors[0] = Color.FromArgb(c1.R, c1.G, c1.B);
            Engine.colors[1] = Color.FromArgb(c2.R, c2.G, c2.B);
            Engine.colors[2] = Color.FromArgb(c3.R, c3.G, c3.B);
            Engine.colors[3] = Color.FromArgb(c4.R, c4.G, c4.B);
            Engine.colors[4] = Color.FromArgb(c5.R, c5.G, c5.B);
            Engine.colors[5] = Color.FromArgb(c6.R, c6.G, c6.B);
            
            create_paral(Engine, -80, 100, 0, -80, 0, height, 0);
            
            double width_dop = length - 5;
            create_cone(Engine, -50, -80/2 + 4, height - 100, heightAnt, diamAnt, 0);
            
            #region Не трогать, пусть будет
            //create_paral(Engine, -195/*не трогать*/, -200, -5, 5 /*не трогать*/, 0, -100, 0);
            //create_cyl(Engine, -60 /*божье чудо*/, 30, 200, -length, width/5, 0);
            #endregion
            create_cone(Engine, 195, -80 / 2 + 2, height - 100, countAnt, 100, 0);
            double[,] coord_legs = new double[4, 2];
                coord_legs = new double[4, 4];
                coord_legs[0, 0] = -80 / 2 - 4; coord_legs[0, 1] = 200;
                create_legs(Engine, coord_legs, -100, 0, 0);
            
            int width_but = length;
            double radius_but = -width;
            little_mufta(Engine, 60, -80, ser, 100, length, 0);
            
            create_cyl(Engine, -80, -200, 20, 100, width/1.25, 0);
            
            create_paral(Engine, -180, -100, 0, -80, 115, 90, 0);
            create_paral(Engine, -175, -130, 0, -80, 125, 90, 0);
            
            sc.Engines.Add(Engine);
            MoveForX(Convert.ToInt32(mass[0]), sc.Engines.Count - 1);
            MoveForY(Convert.ToInt32(mass[1]), sc.Engines.Count - 1);
            MoveForZ(Convert.ToInt32(mass[2]), sc.Engines.Count - 1);
            RotateAroundX(Convert.ToInt32(mass[3]), sc.Engines.Count - 1);
            RotateAroundY(Convert.ToInt32(mass[4]), sc.Engines.Count - 1);
            RotateAroundZ(Convert.ToInt32(mass[5]), sc.Engines.Count - 1);
            Zoom(Convert.ToInt32(mass[6]), sc.Engines.Count - 1);
            MirrorAroundX(Convert.ToInt32(mass[7]), sc.Engines.Count - 1);
            MirrorAroundY(Convert.ToInt32(mass[8]), sc.Engines.Count - 1);
            MirrorAroundZ(Convert.ToInt32(mass[9]), sc.Engines.Count - 1);

            LoadObjects();
        }

        public void MirrorAroundX(int a, int number)
        {
            sc.Engines[number].parameters[21] = a.ToString();
        }

        public void MirrorAroundY(int a, int number)
        {
            sc.Engines[number].parameters[22] = a.ToString();
        }

        public void MirrorAroundZ(int a, int number)
        {
            sc.Engines[number].parameters[23] = a.ToString();
        }

        //загрузка объекта
        public void LoadObject()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                var Engine = new Engine();
                Engine.LoadObj(OFD.FileName);
                sc.Engines.Add(Engine);
                LoadObjects();
            }

        }
        
        public void LoadObjects()
        {
            try
            {
                sc.bitmap = new Bitmap(sc.width, sc.height);
                buff = new int[sc.width * sc.height];
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                ErrorLabel.Text = "";

                Matrix3D ModelView = Matrix3D.Identity;

                if (!checkBox12.Checked && sc.cameras.Count >= 1)
                {
                    //Vertex front = new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye));

                    ModelView = lookat(sc.cameras[sc.curr_c].eye, sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].up);
                }


                for (int i = 0; i < sc.width * sc.height; i++)
                    buff[i] = int.MinValue;

                for (int i = 0; i < sc.Engines.Count; i++)
                    comboBox1.Items.Add(sc.Engines[i].NameEngine);

                for (int i = 0; i < sc.cameras.Count; i++)
                    comboBox2.Items.Add(sc.cameras[i].Name);

                if (comboBox2.Items.Count == 0 || sc.curr_c == -1)
                    comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
                else
                    comboBox2.SelectedIndex = sc.curr_c;

                if (comboBox1.Items.Count == 0 || sc.curr_m == -1)
                    comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
                else
                    comboBox1.SelectedIndex = sc.curr_m;

                int[] x = new int[3];
                int[] y = new int[3];
                int[] z = new int[3];

                for (int i = 0; i < sc.Engines.Count; i++)
                {
                   

                    for (int k = 0; k < sc.Engines[i].FaceList.Count; k++)
                    {
                        int[] face = sc.Engines[i].FaceList[k].VertexIndexList;
                        List<Matrix3D> ms = new List<Matrix3D>(3);

                        for (int j = 0; j < 3; j++)
                        {

                        x[j] = (int)Engine.parse(sc.Engines[i].VertexList[face[j]].X, sc.Engines[i].VertexList[face[j]].Y, sc.Engines[i].VertexList[face[j]].Z, sc.Engines[i]).X*2;
                        y[j] = (int)Engine.parse(sc.Engines[i].VertexList[face[j]].X, sc.Engines[i].VertexList[face[j]].Y, sc.Engines[i].VertexList[face[j]].Z, sc.Engines[i]).Y*2;
                        z[j] = (int)Engine.parse(sc.Engines[i].VertexList[face[j]].X, sc.Engines[i].VertexList[face[j]].Y, sc.Engines[i].VertexList[face[j]].Z, sc.Engines[i]).Z*2;

                         Matrix3D coords1 = new Matrix3D();
                         coords1.M11 = x[j];
                         coords1.M12 = 1;
                         coords1.M13 = 1;
                         coords1.M14 = 1;


                         coords1.M21 = y[j];
                         coords1.M22 = 1;
                         coords1.M23 = 1;
                         coords1.M24 = 1;


                         coords1.M31 = z[j];
                         coords1.M32 = 1;
                         coords1.M33 = 1;
                         coords1.M34 = 1;


                         coords1.OffsetX = 1;
                         coords1.OffsetY = 1;
                         coords1.OffsetZ = 1;
                         coords1.M44 = 1;

                         ms.Add(coords1);

                     }


                        Vertex a1 = new Vertex(ModelView * ms[0]);
                        Vertex a2 = new Vertex(ModelView * ms[1]);
                        Vertex a3 = new Vertex(ModelView * ms[2]);

                        //Vertex a1, a2, a3;
                        //a1 = new Vertex();
                        //a2 = new Vertex();
                        //a3 = new Vertex();


                        //for (int h = 0; h < 3; h++)
                        //{
                        //    x[h] = (int)(x[h] * ModelView.M11 + y[h] * ModelView.M12 + z[h] * ModelView.M13 + ModelView.M14);
                        //    y[h] = (int)(x[h] * ModelView.M21 + y[h] * ModelView.M22 + z[h] * ModelView.M23 + ModelView.M24);
                        //    z[h] = (int)(x[h] * ModelView.M31 + y[h] * ModelView.M32 + z[h] * ModelView.M33 + ModelView.M34);
                        //}
                        //a1 = new Vertex(x[0], y[0], z[0]);
                        //a2 = new Vertex(x[1], y[1], z[1]);
                        //a3 = new Vertex(x[2], y[2], z[2]);


                     double rast = -1000;
                     double scale = 1;

                     if (!checkBox12.Checked && sc.cameras.Count >= 1)
                     {
                         rast = -sc.cameras[sc.curr_c].focus;
                         scale = sc.cameras[sc.curr_c].scale;
                         scale = (double)(scale * 0.4);
                     }

                        a1.X *= scale;
                        a1.Y *= scale;
                        a1.Z *= scale;
                        a2.X *= scale;
                        a2.Y *= scale;
                        a2.Z *= scale;
                        a3.X *= scale;
                        a3.Y *= scale;
                        a3.Z *= scale;

                        if (sc.projection == 0)
                     {
                         a1.X /= (a1.Z / (rast) + 1);
                         a1.Y /= (a1.Z / (rast) + 1);
                         a2.X /= (a2.Z / (rast) + 1);
                         a2.Y /= (a2.Z / (rast) + 1);
                         a3.X /= (a3.Z / (rast) + 1);
                         a3.Y /= (a3.Z / (rast) + 1);
                     }

                        Vertex light_dir = new Vertex(0,0,-1);
                        Vertex n = Vertex.cross_vertex(Vertex.difference_vertex(a3, a1), Vertex.difference_vertex(a2, a1));
                        n=Vertex.normalize(n);
                        double intensity = Math.Round((n.X * light_dir.X + n.Y * light_dir.Y + n.Z * light_dir.Z), 2);

                        a1.X +=  sc.width / 2;
                        a2.X +=  sc.width / 2;
                        a3.X +=  sc.width / 2;
                        a1.Y +=  sc.height/ 2;
                        a2.Y +=  sc.height / 2;
                        a3.Y +=  sc.height / 2;


                        if (sc.coloring == 1 && Math.Abs(intensity)>0)
                        {
                            triangle(a1, a2, a3, sc.bitmap, Color.FromArgb((int)(sc.Engines[i].colors[sc.Engines[i].FaceList[k].color].R * Math.Abs(intensity)),
                                (int)(sc.Engines[i].colors[sc.Engines[i].FaceList[k].color].G * Math.Abs(intensity)),
                                (int)(sc.Engines[i].colors[sc.Engines[i].FaceList[k].color].B * Math.Abs(intensity))));
                            //triangle(a1, a2, a3, sc.bitmap, sc.Engines[i].colors[sc.Engines[i].FaceList[k].color]);
                        }
                       
                        else if (sc.coloring==0)
                        {
                            line((int)a1.X, (int)a1.Y, (int)a2.X, (int)a2.Y, sc.bitmap, sc.Engines[i].colors[5]);
                            line((int)a1.X, (int)a1.Y, (int)a3.X, (int)a3.Y, sc.bitmap, sc.Engines[i].colors[5]);
                            line((int)a2.X, (int)a2.Y, (int)a3.X, (int)a3.Y, sc.bitmap, sc.Engines[i].colors[5]);
                        }
                 }

             }
            
              pictureBox1.Image = sc.bitmap;
              pictureBox1.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
              pictureBox1.Refresh();
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Предупреждение! Невозможно выполнить отрисовку сцены корректно!";
            }

        }

        //растеризация и удаление невидимых граней
        public void triangle(Vertex a0, Vertex a1, Vertex a2, Bitmap image, Color color)
        {
            try
            {
                if (a0.Y == a1.Y && a0.Y == a2.Y) return;
                if (a0.Y > a1.Y)
                    Swap(ref a0, ref a1);
                if (a0.Y > a2.Y)
                    Swap(ref a0, ref a2);
                if (a1.Y > a2.Y)
                    Swap(ref a1, ref a2);
                double total_height = a2.Y - a0.Y;
                for (int i = 0; i < total_height; i++)
                {
                    bool second_half = i > a1.Y - a0.Y || a1.Y == a0.Y;
                    double segment_height = second_half ? a2.Y - a1.Y : a1.Y - a0.Y;
                    double alpha = (double)i / total_height;

                    double beta = (double)(i - (second_half ? a1.Y - a0.Y : 0)) / segment_height;

                    Vertex A = ( Vertex.summa_vertex(a0,Vertex.multiplication_vertex((Vertex.difference_vertex(a2, a0)),alpha)));

                    Vertex B = (second_half ? Vertex.summa_vertex(a1, Vertex.multiplication_vertex(Vertex.difference_vertex(a2, a1), beta)) : 
                        Vertex.summa_vertex(a0, Vertex.multiplication_vertex(Vertex.difference_vertex(a1,a0),beta)));

                    if (A.X >B.X)
                    {
                        Swap(ref A, ref B);
                    }
                    for (double j = A.X; j <= B.X; j++)
                    {
                        double phi = B.X == A.X ? 1 : (double)(j - A.X) / (double)(B.X - A.X);
                        Vertex P = new Vertex();

                        P = (Vertex.summa_vertex(A,Vertex.multiplication_vertex(Vertex.difference_vertex(B,A),phi)));

                        int idx = (int)((int)P.X + (int)P.Y * sc.width);

                        if ((int)P.X >= sc.width || (int)P.Y >= sc.height || (int)P.X < 0) continue;
                        if (buff[idx]< (int)P.Z)
                        {
                            buff[idx] = (int)P.Z;
                            image.SetPixel((int)P.X, (int)P.Y, color);

                        }
                    }
                }
            }
            catch(Exception)
            {
                ErrorLabel.Text = "Предупреждение! Произошла ошибка во время отрисовки!";
            }
        }

        //Алгоритм Брезенхема
        public void line(int x0, int y0, int x1, int y1, Bitmap image, Color color)
        {
            try
            {
                bool steep = false;
                if (Math.Abs(x0 - x1) < Math.Abs(y0 - y1))
                {
                    Swap(ref x0, ref y0);
                    Swap(ref x1, ref y1);
                    steep = true;
                }
                if (x0 > x1)
                {
                    Swap(ref x0, ref x1);
                    Swap(ref y0, ref y1);
                }
                int dx = x1 - x0;
                int dy = y1 - y0;
                int derror = Math.Abs(dy) * 2;
                int error = 0;
                int y = y0;
                for (int x = x0; x <= x1; x++)
                {
                    if (steep)
                        image.SetPixel(y, x, color);
                    else
                        image.SetPixel(x, y, color);
                    error += derror;

                    if (error > dx)
                    {
                        y += (y1 > y0 ? 1 : -1);
                        error -= dx * 2;
                    }
                }
            }
            catch(Exception)
            {
                ErrorLabel.Text = "Ошибка";
            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeleteObject();
        }

        private void DeleteObject()
        {
            try
            {
                sc.Engines.RemoveAt(sc.curr_m);
                sc.curr_m = -1;
                LoadObjects();
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Ошибка! Невозможно удалить данный объект!";
            }
        }

        private void FMoveLeftX()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        MoveForX(-8 + Convert.ToInt32(sc.Engines[i].parameters[14]), i);
                }
                else
                    MoveForX(-8 + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[14]), sc.curr_m);
                LoadObjects();
            }
            catch (Exception)
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FMoveLeftX();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sc.curr_m = comboBox1.SelectedIndex;
            MoveX.Text = sc.Engines[sc.curr_m].parameters[14];
            MoveY.Text = sc.Engines[sc.curr_m].parameters[15];
            MoveZ.Text = sc.Engines[sc.curr_m].parameters[16];
            textBox10.Text ="";
            textBox11.Text ="";
            textBox12.Text="";
            RotateX.Text = sc.Engines[sc.curr_m].parameters[17];
            RotateY.Text = sc.Engines[sc.curr_m].parameters[18];
            RotateZ.Text = sc.Engines[sc.curr_m].parameters[19];
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            trackBar1.Value = int.Parse(sc.Engines[sc.curr_m].parameters[20]);

            if(Convert.ToInt32(sc.Engines[sc.curr_m].parameters[21])==-1)
            checkBox2.Checked= true;
            else checkBox2.Checked = false;
            if (Convert.ToInt32(sc.Engines[sc.curr_m].parameters[22]) == -1)
                checkBox3.Checked = true;
            else checkBox3.Checked = false;
            if (Convert.ToInt32(sc.Engines[sc.curr_m].parameters[23]) == -1)
                checkBox4.Checked = true;
            else checkBox4.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FMoveRightX();
        }

        private void FMoveRightX()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        MoveForX(8 + Convert.ToInt32(sc.Engines[i].parameters[14]), i);
                }
                else
                    MoveForX(8 + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[14]), sc.curr_m);
                LoadObjects();
            }
            catch (Exception)
            {

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FMoveLeftY();
        }

        private void FMoveLeftY()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        MoveForY(-8 + Convert.ToInt32(sc.Engines[i].parameters[15]), i);
                }
                else
                    MoveForY(-8 + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[15]), sc.curr_m);
                LoadObjects();
            }
            catch (Exception)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FMoveRightY();
        }

        private void FMoveRightY()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        MoveForY(8 + Convert.ToInt32(sc.Engines[i].parameters[15]), i);
                }
                else
                    MoveForY(8 + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[15]), sc.curr_m);
                LoadObjects();
            }
            catch(Exception)
            {

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FMoveLeftZ();
        }

        private void FMoveLeftZ()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        MoveForZ(8 + Convert.ToInt32(sc.Engines[i].parameters[16]), i);
                }
                else
                    MoveForZ(8 + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[16]), sc.curr_m);
                LoadObjects();
            }
            catch (Exception)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FMoveRightZ();
        }

        private void FMoveRightZ()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        MoveForZ(-8 + Convert.ToInt32(sc.Engines[i].parameters[16]), i);
                }
                else
                    MoveForZ(-8 + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[16]), sc.curr_m);
                LoadObjects();
            }
            catch (Exception)
            {

            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        MoveForX(Convert.ToInt32(MoveX.Text), i);
                }
                else
                    MoveForX(Convert.ToInt32(MoveX.Text), sc.curr_m);

                LoadObjects();
            }
            catch (Exception) { }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        MoveForY(Convert.ToInt32(MoveY.Text), i);
                }
                else
                    MoveForY(Convert.ToInt32(MoveY.Text), sc.curr_m);

                LoadObjects();
            }
            catch (Exception) { }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        MoveForZ(Convert.ToInt32(MoveZ.Text), i);
                }
                else
                    MoveForZ(Convert.ToInt32(MoveZ.Text), sc.curr_m);

                LoadObjects();
            }
            catch (Exception) { }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveObject();

        }

        private void SaveObject()
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = saveFileDialog1.FileName;
                sc.Engines[sc.curr_m].WriteObject(filename);
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Ошибка! Невозможно сохранить данный объект!";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveObject();
        }

        private void RotateLeftX_Click(object sender, EventArgs e)
        {
            FRotateLeftX();
        }

        private void FRotateLeftX()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        RotateAroundX(1 + Convert.ToInt32(sc.Engines[i].parameters[17]), i);
                }
                else
                    RotateAroundX(1 + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[17]), sc.curr_m);
                LoadObjects();
            }
            catch (Exception) { }
        }

        private void RotateRightX_Click(object sender, EventArgs e)
        {
            FRotateRightX();
        }

        private void FRotateRightX()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        RotateAroundX(-1 + Convert.ToInt32(sc.Engines[i].parameters[17]), i);
                }
                else
                    RotateAroundX(-1 + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[17]), sc.curr_m);
                LoadObjects();
            }
            catch (Exception) { }
        }

        private void RotateLeftY_Click(object sender, EventArgs e)
        {
            FRotateLeftY();
        }

        private void FRotateLeftY()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        RotateAroundY(1 + Convert.ToInt32(sc.Engines[i].parameters[18]), i);
                }
                else
                    RotateAroundY(1 + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[18]), sc.curr_m);
                LoadObjects();
            }
            catch (Exception) { }
        }

        private void RotateRightY_Click(object sender, EventArgs e)
        {
            FRotateRightY();
        }

        private void FRotateRightY()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        RotateAroundY(-1 + Convert.ToInt32(sc.Engines[i].parameters[18]), i);
                }
                else
                    RotateAroundY(-1 + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[18]), sc.curr_m);
                LoadObjects();
            }
            catch (Exception) { }
        }

        private void RotateLeftZ_Click(object sender, EventArgs e)
        {
            FRotateLeftZ();
        }

        private void FRotateLeftZ()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        RotateAroundZ(1 + Convert.ToInt32(sc.Engines[i].parameters[19]), i);
                }
                else
                    RotateAroundZ(1 + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[19]), sc.curr_m);
                LoadObjects();
            }
            catch (Exception) { }
        }

        private void RotateRightZ_Click(object sender, EventArgs e)
        {
            FRotateRightZ();
        }

        private void FRotateRightZ()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        RotateAroundZ(-1 + Convert.ToInt32(sc.Engines[i].parameters[19]), i);
                }
                else
                    RotateAroundZ(-1 + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[19]), sc.curr_m);
                LoadObjects();
            }
            catch (Exception) { }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
          


        }

        private void MoveForX(int value, int number)
        {
            sc.Engines[number].parameters[14] = Convert.ToString(value);

        }

        private void MoveForY(int value, int number)
        {
            sc.Engines[number].parameters[15] = Convert.ToString(value);
        }

        private void MoveForZ(int value, int number)
        {
            sc.Engines[number].parameters[16] = Convert.ToString(value);
        }

        private void RotateAroundX(int degree, int number)
        {
            double rad = Math.PI * (degree) / 180;
            sc.Engines[number].parameters[17] = Convert.ToString(degree);
 
        }

        private void RotateAroundY(int degree, int number)
        {
            double rad = Math.PI * (degree) / 180;
            sc.Engines[number].parameters[18] = Convert.ToString(degree);
        }

        private void RotateAroundZ(int degree, int number)
        {
            double rad = Math.PI * (degree) / 180;
            sc.Engines[number].parameters[19] = Convert.ToString(degree);
           
        }

        private void Zoom(int value, int number)
        {
            double val = ((double)value / 20);
            sc.Engines[number].parameters[20] = (value).ToString();
        }

        private void FZoom()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        Zoom(trackBar1.Value, i);
                }
                else
                    Zoom(trackBar1.Value, sc.curr_m);

                textBox16.Text = trackBar1.Value.ToString();
                LoadObjects();
            }
            catch (Exception)
            {

            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            FZoom();
        }

        private void FOffCameras()
        {
            comboBox2.Enabled = !comboBox2.Enabled;
            button16.Enabled = !button16.Enabled;
            button17.Enabled = !button17.Enabled;
            groupBox6.Enabled = !groupBox6.Enabled;
            sc.offcameras = !sc.offcameras;
            LoadObjects();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            FOffCameras();
        }

        private void CreateObject()
        {
            if (sc.Engines.Count < sc.count_m)
            {
                CreateForm cf = new CreateForm();
                cf.Show();
            }
            else
            {
                ErrorLabel.Text = "Нельзя добавить больше объектов";
            }
        }

        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateObject();  
        }

        private void OpenObject()
        {
            if (sc.Engines.Count < sc.count_m)
            {
                LoadObject();
            }
            else
            {
                ErrorLabel.Text = "Нельзя добавить больше объектов";
            }
        }

        private void загрузитьОбъектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenObject();
        }

        private void сохранитьОбъектToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ChangeObject();
        }

        private void ChangeObject()
        {
            try
            {
                CreateForm cf = new CreateForm(comboBox1.SelectedIndex, sc.Engines[comboBox1.SelectedIndex].NameEngine, sc.Engines[comboBox1.SelectedIndex].parameters, sc.Engines[comboBox1.SelectedIndex].colors);
                cf.Show();
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Ошибка! Невозможно изменить данный объект!";
            }
        }

        private void создатьСценуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateScene();
        }

        private void CreateScene()
        {
            CreateScene cs = new CreateScene();
            cs.Show();
        }

        public void installnewparams()
        {
            pictureBox1.BackColor = sc.color;
            pictureBox1.Width = sc.width;
            pictureBox1.Height = sc.height;

            if(sc.projection==0)
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;

            if (sc.coloring == 0)
                radioButton3.Checked = true;
            else
                radioButton4.Checked = true;

            checkBox1.Checked = sc.forallobjs;
            checkBox12.Checked = sc.offcameras;
            checkBox5.Checked = sc.panorama;

            LoadObjects();
        }

        public void EnableElems()
        {
            tabControl2.Enabled = true;
            объектToolStripMenuItem.Enabled = true;
            камераToolStripMenuItem.Enabled = true;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
            groupBox5.Enabled = true;
            groupBox6.Enabled = true;
        }

        private void OpenScene()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                if (sc == null)
                    EnableElems();
                sc = new Scene();
                sc.Load(OFD.FileName);

                Program.form.Text = "Build Engine Scene - " + sc.name;
                installnewparams();
            }
            
        }

        private void загрузитьСценуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenScene();
        }

        private void SaveScene()
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = saveFileDialog1.FileName;
                sc.Save(filename);
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Ошибка!";
            }
        }

        private void сохранитьСценуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveScene();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (checkBox5.Checked)
                    MoveCameraXPan(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                else
                    MoveCameraX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                LoadObjects();
            }
            catch (Exception) { }
         
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try {
            if(checkBox5.Checked)
                MoveCameraXPan(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)),false);
            else
            MoveCameraX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
        }
            catch (Exception) { }
            LoadObjects();
        }

        private void MoveCameraXPan(Vertex cameraFront,bool R)
        {
            if (R == true)
            {
                sc.cameras[sc.curr_c].eye.X -= Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).X * sc.cameras[sc.curr_c].CamSpeed;
                sc.cameras[sc.curr_c].eye.Y -= Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).Y * sc.cameras[sc.curr_c].CamSpeed;
                sc.cameras[sc.curr_c].eye.Z -= Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).Z * sc.cameras[sc.curr_c].CamSpeed;
            }
            else
            {
                sc.cameras[sc.curr_c].eye.X+= Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).X * sc.cameras[sc.curr_c].CamSpeed;
                sc.cameras[sc.curr_c].eye.Y += Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).Y * sc.cameras[sc.curr_c].CamSpeed;
                sc.cameras[sc.curr_c].eye.Z += Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).Z * sc.cameras[sc.curr_c].CamSpeed;

            }
        }

        private void MoveCameraYPan(bool W)
        {
            if (W == true)
            {
                sc.cameras[sc.curr_c].eye.X += sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.X;
                sc.cameras[sc.curr_c].eye.Y += sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.Y;
                sc.cameras[sc.curr_c].eye.Z += sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.Z;
            }
            else
            {
                sc.cameras[sc.curr_c].eye.X -= sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.X;
                sc.cameras[sc.curr_c].eye.Y -= sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.Y;
                sc.cameras[sc.curr_c].eye.Z -= sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.Z;
            }
        }


        private void MoveCameraZPan(Vertex cameraFront, bool Plus)
        {
            if (Plus == true)
            {
                sc.cameras[sc.curr_c].eye.X += sc.cameras[sc.curr_c].CamSpeed * cameraFront.X;
                sc.cameras[sc.curr_c].eye.Y += sc.cameras[sc.curr_c].CamSpeed * cameraFront.Y;
                sc.cameras[sc.curr_c].eye.Z += sc.cameras[sc.curr_c].CamSpeed * cameraFront.Z;
            }
            else
            {
                sc.cameras[sc.curr_c].eye.X -= sc.cameras[sc.curr_c].CamSpeed * cameraFront.X;
                sc.cameras[sc.curr_c].eye.Y -= sc.cameras[sc.curr_c].CamSpeed * cameraFront.Y;
                sc.cameras[sc.curr_c].eye.Z -= sc.cameras[sc.curr_c].CamSpeed * cameraFront.Z;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (checkBox5.Checked)
                    MoveCameraYPan(false);
                else
                    MoveCameraY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                LoadObjects();

             }
            catch (Exception) { }
            
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (checkBox5.Checked)
                    MoveCameraYPan(true);
                else
                    MoveCameraY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                LoadObjects();
             }
            catch (Exception) { }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (checkBox5.Checked)
                    MoveCameraZPan(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                else
                    MoveCameraZ(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);


                LoadObjects();
            }
            catch (Exception) { }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox5.Checked)
                    MoveCameraZPan(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                else
                    MoveCameraZ(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                LoadObjects();
            }
            catch (Exception) { }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try {
            RotateCamAroundX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
            LoadObjects();
            }
            catch (Exception) { }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                RotateCamAroundX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                LoadObjects();
            }
            catch (Exception) { }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                RotateCamAroundY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                LoadObjects();
            }
            catch (Exception) { }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try {
            RotateCamAroundY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
            LoadObjects();
            }
            catch (Exception) { }
        }

        private void RotateCamAroundZ(Vertex cameraFront, bool flag)
        {
            double rad = Math.PI * (1) / 180;
            if (flag)
            {
                sc.cameras[sc.curr_c].roll++;
                sc.cameras[sc.curr_c].up.X+= Math.Cos(rad);
                sc.cameras[sc.curr_c].up.Z+= Math.Sin(rad);
            }
           else
            {
                sc.cameras[sc.curr_c].roll--;
                sc.cameras[sc.curr_c].up.X -= Math.Cos(rad);
                sc.cameras[sc.curr_c].up.Z -= Math.Sin(rad);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                RotateCamAroundZ(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye), true);
                LoadObjects();
            }
            catch (Exception) { }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                Vertex u = Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye);
                double radius = Math.Round(Math.Sqrt(u.X * u.X + u.Y * u.Y + u.Z * u.Z), 0);
                RotateCamAroundZ(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye), false);
                LoadObjects();
            }
            catch (Exception) { }
        }

        private void CreateCamera()
        {
            if (sc.cameras.Count < sc.count_c)
            {
                CreateCamera cc = new CreateCamera();
                cc.Show();
            }
            else
                ErrorLabel.Text = "Нельзя добавить больше камер";
        }

        private void добавитьКамеруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateCamera();
        }

        private void FAcceptAll()
        {
            comboBox1.Enabled = !comboBox1.Enabled;
            groupBox3.Enabled = !groupBox3.Enabled;
            menuStrip1.Enabled = !menuStrip1.Enabled;
            trackBar1.Enabled = !trackBar1.Enabled;
            sc.forallobjs = !sc.forallobjs;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            FAcceptAll();
        }

        private void ChangeCamera()
        {
            try
            {

                CreateCamera cc = new CreateCamera(sc.cameras[sc.curr_c], sc.curr_c);
                cc.Show();
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Ошибка! Невозможно изменить данную камеру!";
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ChangeCamera();
        }

        private void DeleteCamera()
        {
            try
            {
                sc.cameras.RemoveAt(sc.curr_c);
                sc.curr_c = -1;
                LoadObjects();
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Ошибка! Невозможно удалить данную камеру!";
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            DeleteCamera();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            sc.curr_c = comboBox2.SelectedIndex;
            textBox1.Text= sc.cameras[sc.curr_c].eye.X.ToString();
            textBox2.Text= sc.cameras[sc.curr_c].eye.Y.ToString();
            textBox3.Text=sc.cameras[sc.curr_c].eye.Z.ToString();
            textBox4.Text=sc.cameras[sc.curr_c].center.X.ToString();
            textBox5.Text=sc.cameras[sc.curr_c].center.Y.ToString();
            textBox6.Text=sc.cameras[sc.curr_c].center.Z.ToString();
            textBox8.Text = sc.cameras[sc.curr_c].CamSpeed.ToString();


            textBox7.Text=sc.cameras[sc.curr_c].focus.ToString();
            textBox9.Text = sc.cameras[sc.curr_c].scale.ToString();

            FRotateRightY();
            FRotateLeftY();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            sc.projection = 1;
            LoadObjects();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            sc.projection = 0;
            LoadObjects();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            CopyObject();
        }

        private void CopyObject()
        {
            try
            {
                if (sc.Engines.Count < sc.count_m)
                {
                    Engine m = new Engine(sc.Engines[sc.curr_m]);

                    m.NameEngine += "-копия";
                    sc.Engines.Add(m);

                    LoadObjects();
                }
                else
                {
                    ErrorLabel.Text = "Ошибка! Невозможно копировать объект (возможно предел объектов)!";
                }
            }
            catch (Exception)
            {

            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            InitializeObject();
        }

        private void InitializeObject()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int j = 0; j < sc.Engines.Count; j++)
                    {
                        for (int i = 14; i < sc.Engines[j].parameters.Length - 1; i++)
                            sc.Engines[j].parameters[i] = 0.ToString();
                        sc.Engines[j].parameters[20] = 1.ToString();
                        sc.Engines[j].parameters[21] = 1.ToString();
                        sc.Engines[j].parameters[22] = 1.ToString();
                        sc.Engines[j].parameters[23] = 1.ToString();
                    }
                }
                else
                {
                    for (int i = 14; i < sc.Engines[sc.curr_m].parameters.Length - 1; i++)
                        sc.Engines[sc.curr_m].parameters[i] = 0.ToString();
                    sc.Engines[sc.curr_m].parameters[20] = 1.ToString();
                    sc.Engines[sc.curr_m].parameters[21] = 1.ToString();
                    sc.Engines[sc.curr_m].parameters[22] = 1.ToString();
                    sc.Engines[sc.curr_m].parameters[23] = 1.ToString();
                }



                LoadObjects();
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Ошибка! Невозможно установить начальные значения данному объекту!";
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int j = 0; j < sc.Engines.Count; j++)
                {
                        sc.Engines[j].parameters[21] = (-Convert.ToInt32(sc.Engines[j].parameters[21])).ToString();
                }
            }
            else
            {
                sc.Engines[sc.curr_m].parameters[21] = (-Convert.ToInt32(sc.Engines[sc.curr_m].parameters[21])).ToString();
            }

          

            LoadObjects();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int j = 0; j < sc.Engines.Count; j++)
                {
                    sc.Engines[j].parameters[22] = (-Convert.ToInt32(sc.Engines[j].parameters[22])).ToString();
                }
            }
            else
            {
                sc.Engines[sc.curr_m].parameters[22] = (-Convert.ToInt32(sc.Engines[sc.curr_m].parameters[22])).ToString();
            }


           
            LoadObjects();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int j = 0; j < sc.Engines.Count; j++)
                {
                    sc.Engines[j].parameters[23] = (-Convert.ToInt32(sc.Engines[j].parameters[23])).ToString();
                }
            }
            else
            {
                sc.Engines[sc.curr_m].parameters[23] = (-Convert.ToInt32(sc.Engines[sc.curr_m].parameters[23])).ToString();
            }

            LoadObjects();
        }

        private void RotateX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        RotateAroundX(Convert.ToInt32(RotateX.Text), i);
                }
                else
                    RotateAroundX(Convert.ToInt32(RotateX.Text), sc.curr_m);

                LoadObjects();
            }
            catch (Exception) { }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            sc.coloring = 0;
            LoadObjects();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            sc.coloring = 1;
            LoadObjects();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        MoveForX(Convert.ToInt32(textBox10.Text) + Convert.ToInt32(sc.Engines[i].parameters[14]), i);
                }
                else
                MoveForX(Convert.ToInt32(textBox10.Text)+ Convert.ToInt32(sc.Engines[sc.curr_m].parameters[14]), sc.curr_m);

                LoadObjects();
            }
            catch (Exception) { }

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        MoveForY(Convert.ToInt32(textBox11.Text) + Convert.ToInt32(sc.Engines[i].parameters[15]), i);
                }
                else
                    MoveForY(Convert.ToInt32(textBox11.Text) + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[15]), sc.curr_m);

                LoadObjects();
            }
            catch (Exception) { }
        }

        private void textBox12_Leave(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        MoveForZ(Convert.ToInt32(textBox12.Text) + Convert.ToInt32(sc.Engines[i].parameters[16]), i);
                }
                else
                    MoveForZ(Convert.ToInt32(textBox12.Text) + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[16]), sc.curr_m);

                LoadObjects();
            }
            catch (Exception) { }
        }

        private void MoveX_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void RotateY_Leave(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        RotateAroundY(Convert.ToInt32(RotateY.Text), i);
                }
                else
                    RotateAroundY(Convert.ToInt32(RotateY.Text), sc.curr_m);

                LoadObjects();
            }
            catch (Exception) { }
        }

        private void RotateZ_Leave(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        RotateAroundZ(Convert.ToInt32(RotateZ.Text), i);
                }
                else
                    RotateAroundZ(Convert.ToInt32(RotateZ.Text), sc.curr_m);

                LoadObjects();
            }
            catch (Exception) { }
        }

        private void textBox13_Leave(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        RotateAroundX(Convert.ToInt32(textBox13.Text)+ Convert.ToInt32(sc.Engines[i].parameters[17]), i);
                }
                else
                    RotateAroundX(Convert.ToInt32(textBox13.Text)+ Convert.ToInt32(sc.Engines[sc.curr_m].parameters[17]), sc.curr_m);

                LoadObjects();
            }
            catch (Exception) { }
        }

        private void textBox14_Leave(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        RotateAroundY(Convert.ToInt32(textBox14.Text) + Convert.ToInt32(sc.Engines[i].parameters[18]), i);
                }
                else
                    RotateAroundY(Convert.ToInt32(textBox14.Text) + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[18]), sc.curr_m);

                LoadObjects();
            }
            catch (Exception) { }
        }

        private void textBox15_Leave(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < sc.Engines.Count; i++)
                        RotateAroundZ(Convert.ToInt32(textBox15.Text) + Convert.ToInt32(sc.Engines[i].parameters[19]), i);
                }
                else
                    RotateAroundZ(Convert.ToInt32(textBox15.Text) + Convert.ToInt32(sc.Engines[sc.curr_m].parameters[19]), sc.curr_m);

                LoadObjects();
            }
            catch (Exception) { }
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
           
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void FMirrorX()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int j = 0; j < sc.Engines.Count; j++)
                    {
                        sc.Engines[j].parameters[21] = (-Convert.ToInt32(sc.Engines[j].parameters[21])).ToString();
                    }
                }
                else
                {
                    sc.Engines[sc.curr_m].parameters[21] = (-Convert.ToInt32(sc.Engines[sc.curr_m].parameters[21])).ToString();
                }
                checkBox2.Checked = !checkBox2.Checked;
                LoadObjects();
            }
            catch (Exception)
            {

            }
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            FMirrorX();
        }

        private void FMirrorY()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int j = 0; j < sc.Engines.Count; j++)
                    {
                        sc.Engines[j].parameters[22] = (-Convert.ToInt32(sc.Engines[j].parameters[22])).ToString();
                    }
                }
                else
                {
                    sc.Engines[sc.curr_m].parameters[22] = (-Convert.ToInt32(sc.Engines[sc.curr_m].parameters[22])).ToString();
                }

                checkBox3.Checked = !checkBox3.Checked;

                LoadObjects();
            }
            catch (Exception)
            {

            }
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            FMirrorY();
        }

        private void FMirrorZ()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int j = 0; j < sc.Engines.Count; j++)
                    {
                        sc.Engines[j].parameters[23] = (-Convert.ToInt32(sc.Engines[j].parameters[23])).ToString();
                    }
                }
                else
                {
                    sc.Engines[sc.curr_m].parameters[23] = (-Convert.ToInt32(sc.Engines[sc.curr_m].parameters[23])).ToString();
                }

                checkBox4.Checked = !checkBox4.Checked;

                LoadObjects();
            }
            catch (Exception)
            {

            }
        }

        private void button22_Click_1(object sender, EventArgs e)
        {
            FMirrorZ();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
          
       } 

        private void ChangeProj()
        {
            if (radioButton1.Checked)
                radioButton2.Checked = true;
            else
                radioButton1.Checked = true;
            LoadObjects();
        }

        private void ChangeColoring()
        {
            if (radioButton3.Checked)
                radioButton4.Checked = true;
            else
                radioButton3.Checked = true;
            LoadObjects();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void MoveCameraX(Vertex cameraFront, bool R)
        {
            if (R == true)
            {
                sc.cameras[sc.curr_c].eye.X -= Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).X * sc.cameras[sc.curr_c].CamSpeed;
                sc.cameras[sc.curr_c].eye.Y -= Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).Y * sc.cameras[sc.curr_c].CamSpeed;
                sc.cameras[sc.curr_c].eye.Z -= Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).Z * sc.cameras[sc.curr_c].CamSpeed;

                sc.cameras[sc.curr_c].center.X -= Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).X * sc.cameras[sc.curr_c].CamSpeed;
                sc.cameras[sc.curr_c].center.Y -= Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).Y * sc.cameras[sc.curr_c].CamSpeed;
                sc.cameras[sc.curr_c].center.Z -= Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).Z * sc.cameras[sc.curr_c].CamSpeed;
            }
            else
            {
                sc.cameras[sc.curr_c].eye.X += Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).X * sc.cameras[sc.curr_c].CamSpeed;
                sc.cameras[sc.curr_c].eye.Y += Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).Y * sc.cameras[sc.curr_c].CamSpeed;
                sc.cameras[sc.curr_c].eye.Z += Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).Z * sc.cameras[sc.curr_c].CamSpeed;

                sc.cameras[sc.curr_c].center.X += Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).X * sc.cameras[sc.curr_c].CamSpeed;
                sc.cameras[sc.curr_c].center.Y += Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).Y * sc.cameras[sc.curr_c].CamSpeed;
                sc.cameras[sc.curr_c].center.Z += Vertex.normalize(Vertex.cross_vertex(cameraFront, sc.cameras[sc.curr_c].up)).Z * sc.cameras[sc.curr_c].CamSpeed;
            }
         }

        private void MoveCameraY(Vertex cameraFront, bool W)
        {
            if (W == true)
            {
                sc.cameras[sc.curr_c].eye.X += sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.X;
                sc.cameras[sc.curr_c].eye.Y += sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.Y;
                sc.cameras[sc.curr_c].eye.Z += sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.Z;

                sc.cameras[sc.curr_c].center.X += sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.X;
                sc.cameras[sc.curr_c].center.Y += sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.Y;
                sc.cameras[sc.curr_c].center.Z += sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.Z;
            }
            else
            {
                sc.cameras[sc.curr_c].eye.X -= sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.X;
                sc.cameras[sc.curr_c].eye.Y -= sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.Y;
                sc.cameras[sc.curr_c].eye.Z -= sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.Z;

                sc.cameras[sc.curr_c].center.X -= sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.X;
                sc.cameras[sc.curr_c].center.Y -= sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.Y;
                sc.cameras[sc.curr_c].center.Z -= sc.cameras[sc.curr_c].CamSpeed * sc.cameras[sc.curr_c].up.Z;
            }
        }

        private void MoveCameraZ(Vertex cameraFront, bool Plus)
        {
            if (Plus == true)
            {
                sc.cameras[sc.curr_c].eye.X += sc.cameras[sc.curr_c].CamSpeed * cameraFront.X;
                sc.cameras[sc.curr_c].eye.Y += sc.cameras[sc.curr_c].CamSpeed * cameraFront.Y;
                sc.cameras[sc.curr_c].eye.Z += sc.cameras[sc.curr_c].CamSpeed * cameraFront.Z;

                sc.cameras[sc.curr_c].center.X += sc.cameras[sc.curr_c].CamSpeed * cameraFront.X;
                sc.cameras[sc.curr_c].center.Y += sc.cameras[sc.curr_c].CamSpeed * cameraFront.Y;
                sc.cameras[sc.curr_c].center.Z += sc.cameras[sc.curr_c].CamSpeed * cameraFront.Z;
            }
            else
            {
                sc.cameras[sc.curr_c].eye.X -= sc.cameras[sc.curr_c].CamSpeed * cameraFront.X;
                sc.cameras[sc.curr_c].eye.Y -= sc.cameras[sc.curr_c].CamSpeed * cameraFront.Y;
                sc.cameras[sc.curr_c].eye.Z -= sc.cameras[sc.curr_c].CamSpeed * cameraFront.Z;

                sc.cameras[sc.curr_c].center.X -= sc.cameras[sc.curr_c].CamSpeed * cameraFront.X;
                sc.cameras[sc.curr_c].center.Y -= sc.cameras[sc.curr_c].CamSpeed * cameraFront.Y;
                sc.cameras[sc.curr_c].center.Z -= sc.cameras[sc.curr_c].CamSpeed * cameraFront.Z;
            }
        }

        public void RotateCam(Vertex cameraFront)
        {
            double rad = Math.PI * (sc.cameras[sc.curr_c].pitch) / 180;
            double rad2 = Math.PI * (sc.cameras[sc.curr_c].yaw) / 180;
            double rad3 = Math.PI * (sc.cameras[sc.curr_c].roll) / 180;

            cameraFront.X = Math.Cos(rad) * Math.Cos(rad2);
            cameraFront.Y = Math.Sin(rad);
            cameraFront.Z = Math.Cos(rad) * Math.Sin(rad2);
        }



        public void RotateCamAroundX(Vertex cameraFront, bool R)
        {
            if(R==true)
            sc.cameras[sc.curr_c].yaw += 10;
            else
            sc.cameras[sc.curr_c].yaw -= 10;
            RotateCam(cameraFront);

            //if(R==true)
            //sc.cameras[sc.curr_c].center =Vertex.summa_vertex(sc.cameras[sc.curr_c].center, cameraFront) ;
            //else
            //    sc.cameras[sc.curr_c].center = Vertex.difference_vertex(sc.cameras[sc.curr_c].center, cameraFront);

            sc.cameras[sc.curr_c].center = (Vertex.summa_vertex(Vertex.normalize(cameraFront), sc.cameras[sc.curr_c].eye));

            //    sc.cameras[sc.curr_c].center.X += sc.cameras[sc.curr_c].eye.X;
            //   sc.cameras[sc.curr_c].center.Y += sc.cameras[sc.curr_c].eye.Y;
            //  sc.cameras[sc.curr_c].center.Z += sc.cameras[sc.curr_c].eye.Z;
        }

        public void RotateCamAroundY(Vertex cameraFront, bool W)
        {
            if (W == true)
                sc.cameras[sc.curr_c].pitch += 10;
            else
                sc.cameras[sc.curr_c].pitch-= 10;

            if (sc.cameras[sc.curr_c].pitch > 89.0f)
                sc.cameras[sc.curr_c].pitch = 89.0f;
            if (sc.cameras[sc.curr_c].pitch < -89.0f)
                sc.cameras[sc.curr_c].pitch = -89.0f;

            RotateCam(cameraFront);

            //if (W == true)
            //    sc.cameras[sc.curr_c].center = Vertex.summa_vertex(sc.cameras[sc.curr_c].center, cameraFront);
            //else
            //    sc.cameras[sc.curr_c].center = Vertex.difference_vertex(sc.cameras[sc.curr_c].center, cameraFront);

            sc.cameras[sc.curr_c].center = (Vertex.summa_vertex(Vertex.normalize(cameraFront), sc.cameras[sc.curr_c].eye));
            //sc.cameras[sc.curr_c].center.X += sc.cameras[sc.curr_c].eye.X;
            //sc.cameras[sc.curr_c].center.Y += sc.cameras[sc.curr_c].eye.Y;
            //sc.cameras[sc.curr_c].center.Z += sc.cameras[sc.curr_c].eye.Z;
        }

        private void tabControl2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (checkBox6.Checked == true)
                {
                    if (page == 0)
                    {
                        if (e.Shift && e.KeyCode == Keys.A)
                            ChangeObject();
                        else if (e.Shift && e.KeyCode == Keys.S)
                            SaveObject();
                        else if (e.Shift && e.KeyCode == Keys.D)
                            DeleteObject();
                        else if (e.Shift && e.KeyCode == Keys.C)
                            CopyObject();
                        else if (e.Shift && e.KeyCode == Keys.I)
                            InitializeObject();
                        else if (e.Shift && e.KeyCode == Keys.Z)
                        {
                            if (trackBar1.Value != 5)
                                trackBar1.Value++;
                            FZoom();
                        }
                        else if (e.Control && e.KeyCode == Keys.Z)
                        {
                            if (trackBar1.Value != 1)
                                trackBar1.Value--;
                            FZoom();
                        }
                        else if (e.Shift && e.KeyCode == Keys.Up)
                        {
                            if (sc.curr_m != comboBox1.Items.Count - 1)
                                sc.curr_m++;
                        }
                        else if (e.Shift && e.KeyCode == Keys.Down)
                        {
                            if (sc.curr_m > 0)
                                sc.curr_m--;
                        }

                        else if (e.Shift && e.KeyCode == Keys.Y)
                            FMoveLeftX();
                        else if (e.Shift && e.KeyCode == Keys.U)
                            FMoveRightX();
                        else if (e.Shift && e.KeyCode == Keys.H)
                            FMoveLeftY();
                        else if (e.Shift && e.KeyCode == Keys.J)
                            FMoveRightY();
                        else if (e.Shift && e.KeyCode == Keys.B)
                            FMoveLeftZ();
                        else if (e.Shift && e.KeyCode == Keys.N)
                            FMoveRightZ();
                        else if (e.Shift && e.KeyCode == Keys.O)
                            FMirrorX();
                        else if (e.Shift && e.KeyCode == Keys.K)
                            FMirrorY();
                        else if (e.Shift && e.KeyCode == Keys.M)
                            FMirrorZ();

                        else if (e.Control && e.KeyCode == Keys.Y)
                            FRotateLeftX();
                        else if (e.Control && e.KeyCode == Keys.U)
                            FRotateRightX();
                        else if (e.Control && e.KeyCode == Keys.H)
                            FRotateLeftY();
                        else if (e.Control && e.KeyCode == Keys.J)
                            FRotateRightY();
                        else if (e.Control && e.KeyCode == Keys.B)
                            FRotateLeftZ();
                        else if (e.Control && e.KeyCode == Keys.N)
                            FRotateRightZ();
                    }
                    else if (page == 1)
                    {

                        if (e.Control && e.KeyCode == Keys.Up)
                        {
                            if (sc.curr_m != comboBox1.Items.Count - 1)
                                sc.curr_c++;
                        }
                        else if (e.Control && e.KeyCode == Keys.Down)
                        {
                            if (sc.curr_c > 0)
                                sc.curr_c--;
                        }
                        else if (e.Control && e.KeyCode == Keys.R)
                            DeleteCamera();
                        else if (e.Control && e.KeyCode == Keys.C)
                            ChangeCamera();

                        if (e.Control && e.KeyCode == Keys.A)
                        {
                            RotateCamAroundX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Control && e.KeyCode == Keys.D)
                        {
                            RotateCamAroundX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }
                        else if (e.Control && e.KeyCode == Keys.PageUp)
                        {
                            RotateCamAroundZ(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye), true);
                        }
                        else if (e.Control && e.KeyCode == Keys.PageDown)
                        {
                            RotateCamAroundZ(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye), false);
                        }
                        else if (e.Control && e.KeyCode == Keys.W)
                        {
                            RotateCamAroundY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }
                        else if (e.Control && e.KeyCode == Keys.S)
                        {
                            RotateCamAroundY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Shift && e.KeyCode == Keys.W)
                        {
                            MoveCameraY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }
                        else if (e.Shift && e.KeyCode == Keys.S)
                        {
                            MoveCameraY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Shift && e.KeyCode == Keys.PageUp)
                        {
                            MoveCameraZ(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Shift && e.KeyCode == Keys.PageDown)
                        {
                            MoveCameraZ(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }

                        else if (e.Shift && e.KeyCode == Keys.D)
                        {
                            MoveCameraX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Shift && e.KeyCode == Keys.A)
                        {
                            MoveCameraX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }
                    }

                        if (e.Shift && e.KeyCode == Keys.F1)
                            OpenScene();
                        else if (e.Control && e.KeyCode == Keys.F1)
                            SaveScene();
                        else if (e.KeyCode == Keys.F1)
                            CreateScene();

                        else if (e.Shift && e.KeyCode == Keys.F2)
                            OpenObject();
                        else if (e.Control && e.KeyCode == Keys.F2)
                            SaveObject();
                        else if (e.KeyCode == Keys.F2)
                            CreateObject();
                        else if (e.KeyCode == Keys.F3)
                            CreateCamera();
                        else if (e.KeyCode == Keys.F4)
                            ChangeColoring();
                        else if (e.KeyCode == Keys.F5)
                            ChangeProj();
                        else if (e.KeyCode == Keys.F6)
                        {
                            FAcceptAll();
                            checkBox1.Checked = !checkBox1.Checked;
                        }
                        else if (e.KeyCode == Keys.F7)
                        {
                            FOffCameras();
                            checkBox12.Checked = !checkBox12.Checked;
                        }
                        else if (e.KeyCode == Keys.F8)
                        {
                            checkBox5.Checked = !checkBox5.Checked;
                        }
                        LoadObjects();
                }
            }
            catch(Exception)
            {
                ErrorLabel.Text = "Произошла ошибка! Возможно функция недоступна!";
            }
            
        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }



        byte page = 0;

        private void button23_Click(object sender, EventArgs e)
        {
        }

        private void tabControl2_Enter(object sender, EventArgs e)
        {
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            page = 0;
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            page = 1;
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            button15.Enabled =!button15.Enabled;
            button7.Enabled = !button7.Enabled;
            label30.Enabled = !label30.Enabled;
            sc.panorama = !sc.panorama;
        }

        private void RotateX_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            MoveX.ReadOnly = checkBox6.Checked;
            MoveY.ReadOnly = checkBox6.Checked;
            MoveZ.ReadOnly = checkBox6.Checked;
            textBox10.ReadOnly = checkBox6.Checked;
            textBox11.ReadOnly = checkBox6.Checked;
            textBox12.ReadOnly = checkBox6.Checked;
            RotateX.ReadOnly = checkBox6.Checked;
            RotateY.ReadOnly = checkBox6.Checked;
            RotateZ.ReadOnly = checkBox6.Checked;
            textBox13.ReadOnly = checkBox6.Checked;
            textBox14.ReadOnly = checkBox6.Checked;
            textBox15.ReadOnly = checkBox6.Checked;
        }

        bool firstMouse=true;
        int lastX;
        int lastY;

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

           
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (firstMouse)
                {
                    lastX = Cursor.Position.X;
                    lastY = Cursor.Position.Y;
                    firstMouse = false;
                }

                float offsetX = Cursor.Position.X - lastX;
                float offsetY = Cursor.Position.Y - lastY;
                lastX = Cursor.Position.X;
                lastY = Cursor.Position.Y;

                sc.cameras[sc.curr_c].yaw += offsetX;
                sc.cameras[sc.curr_c].pitch += offsetY;

                float sens = 0.000005f;
                offsetX *= sens;
                offsetY *= sens;

                if (sc.cameras[sc.curr_c].pitch > 89.0f)
                    sc.cameras[sc.curr_c].pitch = 89.0f;
                if (sc.cameras[sc.curr_c].pitch < -89.0f)
                    sc.cameras[sc.curr_c].pitch = -89.0f;

                Vertex a = new Vertex(0, 0, 0);
                RotateCam(a);
                sc.cameras[sc.curr_c].center = (Vertex.summa_vertex(Vertex.normalize(a), sc.cameras[sc.curr_c].eye));
                LoadObjects();
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Не выбрана камера!";
            }
        }

        private void сценаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ChangeObject();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MoveCameraX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabControl1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (checkBox6.Checked == true)
                {
                    if (page == 0)
                    {
                        if (e.Shift && e.KeyCode == Keys.A)
                            ChangeObject();
                        else if (e.Shift && e.KeyCode == Keys.S)
                            SaveObject();
                        else if (e.Shift && e.KeyCode == Keys.D)
                            DeleteObject();
                        else if (e.Shift && e.KeyCode == Keys.C)
                            CopyObject();
                        else if (e.Shift && e.KeyCode == Keys.I)
                            InitializeObject();
                        else if (e.Shift && e.KeyCode == Keys.Z)
                        {
                            if (trackBar1.Value != 5)
                                trackBar1.Value++;
                            FZoom();
                        }
                        else if (e.Control && e.KeyCode == Keys.Z)
                        {
                            if (trackBar1.Value != 1)
                                trackBar1.Value--;
                            FZoom();
                        }
                        else if (e.Shift && e.KeyCode == Keys.Up)
                        {
                            if (sc.curr_m != comboBox1.Items.Count - 1)
                                sc.curr_m++;
                        }
                        else if (e.Shift && e.KeyCode == Keys.Down)
                        {
                            if (sc.curr_m > 0)
                                sc.curr_m--;
                        }

                        else if (e.Shift && e.KeyCode == Keys.Y)
                            FMoveLeftX();
                        else if (e.Shift && e.KeyCode == Keys.U)
                            FMoveRightX();
                        else if (e.Shift && e.KeyCode == Keys.H)
                            FMoveLeftY();
                        else if (e.Shift && e.KeyCode == Keys.J)
                            FMoveRightY();
                        else if (e.Shift && e.KeyCode == Keys.B)
                            FMoveLeftZ();
                        else if (e.Shift && e.KeyCode == Keys.N)
                            FMoveRightZ();
                        else if (e.Shift && e.KeyCode == Keys.O)
                            FMirrorX();
                        else if (e.Shift && e.KeyCode == Keys.K)
                            FMirrorY();
                        else if (e.Shift && e.KeyCode == Keys.M)
                            FMirrorZ();

                        else if (e.Control && e.KeyCode == Keys.Y)
                            FRotateLeftX();
                        else if (e.Control && e.KeyCode == Keys.U)
                            FRotateRightX();
                        else if (e.Control && e.KeyCode == Keys.H)
                            FRotateLeftY();
                        else if (e.Control && e.KeyCode == Keys.J)
                            FRotateRightY();
                        else if (e.Control && e.KeyCode == Keys.B)
                            FRotateLeftZ();
                        else if (e.Control && e.KeyCode == Keys.N)
                            FRotateRightZ();
                    }
                    else if (page == 1)
                    {

                        if (e.Control && e.KeyCode == Keys.Up)
                        {
                            if (sc.curr_m != comboBox1.Items.Count - 1)
                                sc.curr_c++;
                        }
                        else if (e.Control && e.KeyCode == Keys.Down)
                        {
                            if (sc.curr_c > 0)
                                sc.curr_c--;
                        }
                        else if (e.Control && e.KeyCode == Keys.R)
                            DeleteCamera();
                        else if (e.Control && e.KeyCode == Keys.C)
                            ChangeCamera();

                        if (e.Control && e.KeyCode == Keys.A)
                        {
                            RotateCamAroundX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Control && e.KeyCode == Keys.D)
                        {
                            RotateCamAroundX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }
                        else if (e.Control && e.KeyCode == Keys.PageUp)
                        {
                            RotateCamAroundZ(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye), true);
                        }
                        else if (e.Control && e.KeyCode == Keys.PageDown)
                        {
                            RotateCamAroundZ(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye), false);
                        }
                        else if (e.Control && e.KeyCode == Keys.W)
                        {
                            RotateCamAroundY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }
                        else if (e.Control && e.KeyCode == Keys.S)
                        {
                            RotateCamAroundY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Shift && e.KeyCode == Keys.W)
                        {
                            MoveCameraY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }
                        else if (e.Shift && e.KeyCode == Keys.S)
                        {
                            MoveCameraY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Shift && e.KeyCode == Keys.PageUp)
                        {
                            MoveCameraZ(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Shift && e.KeyCode == Keys.PageDown)
                        {
                            MoveCameraZ(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }

                        else if (e.Shift && e.KeyCode == Keys.D)
                        {
                            MoveCameraX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Shift && e.KeyCode == Keys.A)
                        {
                            MoveCameraX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }
                    }

                    if (e.Shift && e.KeyCode == Keys.F1)
                        OpenScene();
                    else if (e.Control && e.KeyCode == Keys.F1)
                        SaveScene();
                    else if (e.KeyCode == Keys.F1)
                        CreateScene();

                    else if (e.Shift && e.KeyCode == Keys.F2)
                        OpenObject();
                    else if (e.Control && e.KeyCode == Keys.F2)
                        SaveObject();
                    else if (e.KeyCode == Keys.F2)
                        CreateObject();
                    else if (e.KeyCode == Keys.F3)
                        CreateCamera();
                    else if (e.KeyCode == Keys.F4)
                        ChangeColoring();
                    else if (e.KeyCode == Keys.F5)
                        ChangeProj();
                    else if (e.KeyCode == Keys.F6)
                    {
                        FAcceptAll();
                        checkBox1.Checked = !checkBox1.Checked;
                    }
                    else if (e.KeyCode == Keys.F7)
                    {
                        FOffCameras();
                        checkBox12.Checked = !checkBox12.Checked;
                    }
                    else if (e.KeyCode == Keys.F8)
                    {
                        checkBox5.Checked = !checkBox5.Checked;
                    }
                    LoadObjects();
                }
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Произошла ошибка! Возможно функция недоступна!";
            }
        }

        private void tabControl3_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (checkBox6.Checked == true)
                {
                    if (page == 0)
                    {
                        if (e.Shift && e.KeyCode == Keys.A)
                            ChangeObject();
                        else if (e.Shift && e.KeyCode == Keys.S)
                            SaveObject();
                        else if (e.Shift && e.KeyCode == Keys.D)
                            DeleteObject();
                        else if (e.Shift && e.KeyCode == Keys.C)
                            CopyObject();
                        else if (e.Shift && e.KeyCode == Keys.I)
                            InitializeObject();
                        else if (e.Shift && e.KeyCode == Keys.Z)
                        {
                            if (trackBar1.Value != 5)
                                trackBar1.Value++;
                            FZoom();
                        }
                        else if (e.Control && e.KeyCode == Keys.Z)
                        {
                            if (trackBar1.Value != 1)
                                trackBar1.Value--;
                            FZoom();
                        }
                        else if (e.Shift && e.KeyCode == Keys.Up)
                        {
                            if (sc.curr_m != comboBox1.Items.Count - 1)
                                sc.curr_m++;
                        }
                        else if (e.Shift && e.KeyCode == Keys.Down)
                        {
                            if (sc.curr_m > 0)
                                sc.curr_m--;
                        }

                        else if (e.Shift && e.KeyCode == Keys.Y)
                            FMoveLeftX();
                        else if (e.Shift && e.KeyCode == Keys.U)
                            FMoveRightX();
                        else if (e.Shift && e.KeyCode == Keys.H)
                            FMoveLeftY();
                        else if (e.Shift && e.KeyCode == Keys.J)
                            FMoveRightY();
                        else if (e.Shift && e.KeyCode == Keys.B)
                            FMoveLeftZ();
                        else if (e.Shift && e.KeyCode == Keys.N)
                            FMoveRightZ();
                        else if (e.Shift && e.KeyCode == Keys.O)
                            FMirrorX();
                        else if (e.Shift && e.KeyCode == Keys.K)
                            FMirrorY();
                        else if (e.Shift && e.KeyCode == Keys.M)
                            FMirrorZ();

                        else if (e.Control && e.KeyCode == Keys.Y)
                            FRotateLeftX();
                        else if (e.Control && e.KeyCode == Keys.U)
                            FRotateRightX();
                        else if (e.Control && e.KeyCode == Keys.H)
                            FRotateLeftY();
                        else if (e.Control && e.KeyCode == Keys.J)
                            FRotateRightY();
                        else if (e.Control && e.KeyCode == Keys.B)
                            FRotateLeftZ();
                        else if (e.Control && e.KeyCode == Keys.N)
                            FRotateRightZ();
                    }
                    else if (page == 1)
                    {

                        if (e.Control && e.KeyCode == Keys.Up)
                        {
                            if (sc.curr_m != comboBox1.Items.Count - 1)
                                sc.curr_c++;
                        }
                        else if (e.Control && e.KeyCode == Keys.Down)
                        {
                            if (sc.curr_c > 0)
                                sc.curr_c--;
                        }
                        else if (e.Control && e.KeyCode == Keys.R)
                            DeleteCamera();
                        else if (e.Control && e.KeyCode == Keys.C)
                            ChangeCamera();

                        if (e.Control && e.KeyCode == Keys.A)
                        {
                            RotateCamAroundX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Control && e.KeyCode == Keys.D)
                        {
                            RotateCamAroundX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }
                        else if (e.Control && e.KeyCode == Keys.PageUp)
                        {
                            RotateCamAroundZ(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye), true);
                        }
                        else if (e.Control && e.KeyCode == Keys.PageDown)
                        {
                            RotateCamAroundZ(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye), false);
                        }
                        else if (e.Control && e.KeyCode == Keys.W)
                        {
                            RotateCamAroundY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }
                        else if (e.Control && e.KeyCode == Keys.S)
                        {
                            RotateCamAroundY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Shift && e.KeyCode == Keys.W)
                        {
                            MoveCameraY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }
                        else if (e.Shift && e.KeyCode == Keys.S)
                        {
                            MoveCameraY(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Shift && e.KeyCode == Keys.PageUp)
                        {
                            MoveCameraZ(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Shift && e.KeyCode == Keys.PageDown)
                        {
                            MoveCameraZ(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }

                        else if (e.Shift && e.KeyCode == Keys.D)
                        {
                            MoveCameraX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), true);
                        }
                        else if (e.Shift && e.KeyCode == Keys.A)
                        {
                            MoveCameraX(new Vertex(Vertex.difference_vertex(sc.cameras[sc.curr_c].center, sc.cameras[sc.curr_c].eye)), false);
                        }
                    }

                    if (e.Shift && e.KeyCode == Keys.F1)
                        OpenScene();
                    else if (e.Control && e.KeyCode == Keys.F1)
                        SaveScene();
                    else if (e.KeyCode == Keys.F1)
                        CreateScene();

                    else if (e.Shift && e.KeyCode == Keys.F2)
                        OpenObject();
                    else if (e.Control && e.KeyCode == Keys.F2)
                        SaveObject();
                    else if (e.KeyCode == Keys.F2)
                        CreateObject();
                    else if (e.KeyCode == Keys.F3)
                        CreateCamera();
                    else if (e.KeyCode == Keys.F4)
                        ChangeColoring();
                    else if (e.KeyCode == Keys.F5)
                        ChangeProj();
                    else if (e.KeyCode == Keys.F6)
                    {
                        FAcceptAll();
                        checkBox1.Checked = !checkBox1.Checked;
                    }
                    else if (e.KeyCode == Keys.F7)
                    {
                        FOffCameras();
                        checkBox12.Checked = !checkBox12.Checked;
                    }
                    else if (e.KeyCode == Keys.F8)
                    {
                        checkBox5.Checked = !checkBox5.Checked;
                    }
                    LoadObjects();
                }
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Произошла ошибка! Возможно функция недоступна!";
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            DeleteObject();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            SaveObject();
        }

        private void button23_Click_1(object sender, EventArgs e)
        {
            FRotateLeftX();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            FMoveLeftX();
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            FMoveRightZ();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            FMoveLeftY();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            FMoveRightX();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            FMoveLeftX();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            FMoveRightY();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            FMoveLeftZ();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            FRotateLeftX();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            FRotateRightX();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            FRotateLeftY();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            FRotateRightY();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            FRotateLeftZ();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            FRotateRightZ();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DeleteCamera();
        }
    }
}
