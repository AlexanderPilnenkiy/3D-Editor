using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Media.Media3D;

namespace Graphics
{
    public class Engine
    {
        public List<Vertex> VertexList;
        public List<Face> FaceList;

        public string NameEngine { get; set; }
        public string [] parameters=new string [24];
        public Color[] colors = new Color[6];

	    public Engine()
        {
            VertexList = new List<Vertex>();
            FaceList = new List<Face>();
            NameEngine = "Безымянный";
            parameters[20] = 0.ToString();
            parameters[20] = 0.ToString();
            parameters[16] = 0.ToString();
            parameters[17] = 0.ToString();
            parameters[18] = 0.ToString();
            parameters[19] = 0.ToString();
            parameters[20] = 1.ToString();
            parameters[21] = 1.ToString();
            parameters[22] = 1.ToString();
            parameters[23] = 1.ToString();
        }

        public Engine(Engine m)
        {
            VertexList = new List<Vertex>();
            FaceList = new List<Face>();
            NameEngine =m.NameEngine;
            for(int i=0;i<m.VertexList.Count;i++)
            {
                double x = m.VertexList[i].X;
                double y = m.VertexList[i].Y;
                double z = m.VertexList[i].Z;
                Vertex V = new Vertex(x,y , z);
                VertexList.Add(V);
            }
            for (int i = 0; i < m.colors.Length; i++)
            {
                colors[i] = m.colors[i];
            }
            for (int i = 0; i < m.FaceList.Count; i++)
            {
                Face F = new Face(m.FaceList[i].VertexIndexList[0], m.FaceList[i].VertexIndexList[1], m.FaceList[i].VertexIndexList[2], m.FaceList[i].color);
                FaceList.Add(F);
                
            }
            for (int i = 0; i <m.parameters.Length; i++)
            {
                parameters[i] = m.parameters[i];
            }

        }

        public void LoadObj(string path)
        {
            LoadObj(File.ReadAllLines(path));
        }

	    public void LoadObj(Stream data)
        {
            using (var reader = new StreamReader(data))
            {
                LoadObj(reader.ReadToEnd().Split(Environment.NewLine.ToCharArray()));
            }
        }

	    public void LoadObj(IEnumerable<string> data)
        {
            foreach (var line in data)
            {
                processLine(line);
            }

            //updateSize();
        }

        //запись объекта в файл
        public void WriteObject(string fileName)
        {
            FileStream aFile = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(aFile);
            aFile.Seek(0, SeekOrigin.End);
            sw.WriteLine("nameobject "+NameEngine);
            sw.Write("params ");
            for(int i=0;i<parameters.Length;i++)
                sw.Write(parameters[i]+" ");
            sw.WriteLine(" ");

            sw.Write("colors ");
            for (int i = 0; i < colors.Length; i++)
                sw.Write(colors[i].R + "/"+ colors[i].G+ "/" +colors[i].B+" ");
            sw.WriteLine(" ");

            for (int i=0;i<VertexList.Count;i++)
                sw.WriteLine("v " + Convert.ToString(VertexList[i].X, CultureInfo.InvariantCulture) + " "+ Convert.ToString(VertexList[i].Y, CultureInfo.InvariantCulture) +" "+ Convert.ToString(VertexList[i].Z, CultureInfo.InvariantCulture));
            sw.WriteLine(" ");
            for (int i = 0; i < FaceList.Count; i++)
                sw.WriteLine("f " + FaceList[i].VertexIndexList[0] + " " + FaceList[i].VertexIndexList[1] + " " + FaceList[i].VertexIndexList[2]+" "+FaceList[i].color);
            sw.WriteLine(" ");
            sw.Close();
        }


      
        public  void readparams(string [] mass)
        {
            for(int i=0;i<mass.Length-1;i++)
                parameters[i] = mass[i+1];
        }

        public void readcolors(string[] mass)
        {
            for (int i = 1; i < mass.Length; i++)
            {
                string[] c = mass[i].Split('/');
                colors[i-1] = Color.FromArgb(Byte.Parse(c[0]),Byte.Parse(c[1]),Byte.Parse(c[2]));
            }
                
        }

        public static Vertex parse(double x,double y,double z, Engine m)
        {
            Vertex b = new Vertex(0, 0, 0);
            double val = (Double.Parse(m.parameters[20]) / 20);
            val += 1;

            double rad1 = Math.PI * (Double.Parse(m.parameters[17])) / 180;
            double rad2 = Math.PI * (Double.Parse(m.parameters[18])) / 180;
            double rad3 = Math.PI * (Double.Parse(m.parameters[19])) / 180;

            //Matrix3D local = Matrix3D.Identity;
            //local.M11 = x;
            //local.M22 = y;
            //local.M33 = z;
            Matrix3D zoom = Matrix3D.Identity;
            zoom.M11 = val;
            zoom.M22 = val;
            zoom.M33 = val;
            Matrix3D rotateX = Matrix3D.Identity;
            rotateX.M22 = Math.Cos(rad1);
            rotateX.M23 = Math.Sin(rad1);
            rotateX.M32 = -Math.Sin(rad1);
            rotateX.M33 = Math.Cos(rad1);
            Matrix3D rotateY = Matrix3D.Identity;
            rotateY.M11 = Math.Cos(rad2);
            rotateY.M13 = -Math.Sin(rad2);
            rotateY.M31 = Math.Sin(rad2);
            rotateY.M33 = Math.Cos(rad2);
            Matrix3D rotateZ = Matrix3D.Identity;
            rotateZ.M11 = Math.Cos(rad3);
            rotateZ.M12 = Math.Sin(rad3);
            rotateZ.M21 = -Math.Sin(rad3);
            rotateZ.M22 = Math.Cos(rad3);
            Matrix3D move = Matrix3D.Identity;
            move.M14 = Double.Parse(m.parameters[14]);
            move.M24 = Double.Parse(m.parameters[15]);
            move.M34 = Double.Parse(m.parameters[16]);
            Matrix3D sym = Matrix3D.Identity;
            sym.M11 = Double.Parse(m.parameters[21]);
            sym.M22 = Double.Parse(m.parameters[22]);
            sym.M33 = Double.Parse(m.parameters[23]);
            Matrix3D world = zoom*rotateX*rotateY*rotateZ*move*sym;
            b.X = world.M11 * x + world.M12 * y + world.M13 * z + world.M14;
            b.Y = world.M21 * x + world.M22 * y + world.M23 * z + world.M24;
            b.Z = world.M31 * x + world.M32 * y + world.M33 * z + world.M34;
            return b;
           
        }

        private void processLine(string line)
        {
            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 0)
            {
                switch (parts[0])
                {
                    case "nameobject":
                        NameEngine = parts[1];
                        break;
                    case "params":
                        readparams(parts);
                        break;
                    case "colors":
                        readcolors(parts);
                        break;
                    case "v":
                        Vertex v = new Vertex();
                        v.LoadFromStringArray(parts);
                        VertexList.Add(v);
                      //  v.Index = VertexList.Count();
                        break;
                    case "f":
                        Face f = new Face();
                        f.LoadFromStringArray(parts);
                        FaceList.Add(f);
                        break;
                }
            }
        }
    }
}
