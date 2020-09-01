using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Graphics
{
    public class Vertex:IType
    {
        public const int MinimumDataLength = 4;
        public const string Prefix = "v";

        public Vertex(double x,double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vertex(Matrix3D m)
        {
                X = m.M11 ;
                Y = m.M21 ;
                Z = m.M31 ;
          
        }

        public Vertex(Vertex a)
        {
            X = a.X;
            Y = a.Y;
            Z = a.Z;
        }

        public Vertex()
        {

        }

       

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

      //  public int Index { get; set; }

        public void LoadFromStringArray(string[] data)
        {
            if (data.Length < MinimumDataLength)
                throw new ArgumentException("Минимальная длина строки дожна быть: " + MinimumDataLength, "data");

            if (!data[0].ToLower().Equals(Prefix))
                throw new ArgumentException("Префикс должен быть: '" + Prefix + "'", "data");

            bool success;

            double x, y, z;

            success = double.TryParse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture, out x);
            if (!success) throw new ArgumentException("Невозможно парсить X как double");

            success = double.TryParse(data[2], NumberStyles.Any, CultureInfo.InvariantCulture, out y);
            if (!success) throw new ArgumentException("Невозможно парсить Y как double");

            success = double.TryParse(data[3], NumberStyles.Any, CultureInfo.InvariantCulture, out z);
            if (!success) throw new ArgumentException("Невозможно парсить Z как double");

            X = x;
            Y = y;
            Z = z;
        }


        public override string ToString()
        {
            return string.Format("v {0} {1} {2}", X, Y, Z);
        }

        public static Vertex difference_vertex(Vertex a, Vertex b)
        {
            Vertex A = new Vertex();
            A.X = a.X - b.X;
            A.Y = a.Y - b.Y;
            A.Z = a.Z - b.Z;
            return A;
        }

        public static Vertex summa_vertex(Vertex a, Vertex b)
        {
            Vertex A = new Vertex();
            A.X = a.X + b.X;
            A.Y = a.Y + b.Y;
            A.Z = a.Z + b.Z;
            return A;
        }

        public static Vertex multiplication_vertex(Vertex a, Vertex b)
        {
            Vertex A = new Vertex();
            A.X = a.X * b.X;
            A.Y = a.Y * b.Y;
            A.Z = a.Z * b.Z;
            return A;
        }

        public static Vertex multiplication_vertex(Vertex a, double b)
        {
            Vertex A = new Vertex();
            A.X = a.X * b;
            A.Y = a.Y * b;
            A.Z = a.Z * b;
            return A;
        }

        public static Vertex cross_vertex(Vertex a, Vertex b)
        {
            Vertex A = new Vertex();

            A.X = a.Y * b.Z - a.Z * b.Y;
            A.Y = a.Z * b.X - a.X * b.Z;
            A.Z = a.X * b.Y - a.Y * b.X;

            //A.X = a.Z * b.Y - a.X * b.Z;
            //A.Y = a.X * b.Z - a.Z * b.X;
            //A.Z = a.Y * b.X - a.X * b.Y;

            return A;
        }


        public static double norm(Vertex a)
        {
            return Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);
        }

        public static Vertex normalize(Vertex a)
        {
            Vertex A = new Vertex();
          
            if (a.X == 0 && a.Y == 0 && a.Z == 0)
            {
                A.X = 0;
                A.Y = 0;
                A.Z = 0;
            }
            else
            {
                A.X = a.X * (1 / norm(a));
                A.Y = a.Y * (1 / norm(a));
                A.Z = a.Z * (1 / norm(a));
            }
            return A;
        }

    }
}
