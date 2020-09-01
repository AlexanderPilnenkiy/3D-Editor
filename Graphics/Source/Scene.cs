using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class Scene
    {
        public string name="Без названия";
        public int width = 1000;
        public int height = 800;
        public Bitmap bitmap;
        public List<Engine> Engines = new List<Engine>();
        public List<Camera> cameras = new List<Camera>();
        public int count_m = 1;
        public int count_c = 1;
        public Color color;
        public byte projection = 0;
        public int coloring = 0;
        public bool forallobjs = false;
        public bool offcameras = false;
        public bool panorama = false;

        public int curr_m=-1;
        public int curr_c=-1;

        public Scene(string name,int width,int height, int count_m,int count_c, string color)
        {
            string [] objs = color.Split('/');
            this.name = name;
            this.width = width;
            this.height = height;
            this.color= Color.FromArgb(255,Convert.ToByte(objs[0]), Convert.ToByte(objs[1]), Convert.ToByte(objs[2]));
            this.count_m = count_m;
            this.count_c = count_c;
        }

        public Scene()
        {

        }

        public void Load(string path)
        {
            LoadString(File.ReadAllLines(path));
        }

        public void LoadString(IEnumerable<string> data)
        {
            foreach (var line in data)
            {
                processLine(line);
            }
        }

        public void Save(string fileName)
        {
            FileStream aFile = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(aFile);
            aFile.Seek(0, SeekOrigin.End);
            sw.WriteLine("namescene " + name);
            sw.WriteLine("width " +width);
            sw.WriteLine("height " + height);
            sw.WriteLine("colorscene " + color.R+"/"+ color.G + "/" + color.B);
            sw.WriteLine("coloringscene " + coloring);
            sw.WriteLine("forallobjs " + forallobjs);
            sw.WriteLine("offcameras " + offcameras);
            sw.WriteLine("panorama " + panorama);
            sw.WriteLine("countobjects " + count_m);
            sw.WriteLine("countcameras " + count_c);
            sw.WriteLine("currentobject " + curr_m);
            sw.WriteLine("currentcamera " + curr_c);

            sw.WriteLine("projection " + projection);
            sw.WriteLine(" ");

            for (int j = 0; j < cameras.Count; j++)
            {
                sw.WriteLine("namecamera " + cameras[j].Name);
                sw.WriteLine("eye " + Convert.ToString(cameras[j].eye.X) + " " + Convert.ToString(cameras[j].eye.Y) + " " + Convert.ToString(cameras[j].eye.Z));
                sw.WriteLine("center " + Convert.ToString(cameras[j].center.X) + " " + Convert.ToString(cameras[j].center.Y) + " " + Convert.ToString(cameras[j].center.Z));
                sw.WriteLine("up " + Convert.ToString(cameras[j].up.X) + " " + Convert.ToString(cameras[j].up.Y) + " " + Convert.ToString(cameras[j].up.Z));
                sw.WriteLine("focus " + Convert.ToString(cameras[j].focus));
                sw.WriteLine("speed " + Convert.ToString(cameras[j].CamSpeed));
                sw.WriteLine("camparams " + Convert.ToString(cameras[j].pitch)+" " + Convert.ToString(cameras[j].yaw)+" " + Convert.ToString(cameras[j].roll));
                sw.WriteLine("scale " + cameras[j].scale);
                sw.WriteLine(" ");
            }

            for (int j=0;j<Engines.Count;j++)
            {
                sw.WriteLine("nameobject " + Engines[j].NameEngine);
                sw.Write("params ");
                for (int i = 0; i < Engines[j].parameters.Length; i++)
                    sw.Write(Engines[j].parameters[i] + " ");
                sw.WriteLine(" ");

                sw.Write("colors ");
                for (int i = 0; i < Engines[j].colors.Length; i++)
                    sw.Write(Engines[j].colors[i].R + "/" + Engines[j].colors[i].G + "/" + Engines[j].colors[i].B + " ");
                sw.WriteLine(" ");

                for (int i = 0; i < Engines[j].VertexList.Count; i++)
                    sw.WriteLine("v " + Convert.ToString(Engines[j].VertexList[i].X, CultureInfo.InvariantCulture) + " " + Convert.ToString(Engines[j].VertexList[i].Y, CultureInfo.InvariantCulture) + " " + Convert.ToString(Engines[j].VertexList[i].Z, CultureInfo.InvariantCulture));
                sw.WriteLine(" ");
                for (int i = 0; i < Engines[j].FaceList.Count; i++)
                    sw.WriteLine("f " + Engines[j].FaceList[i].VertexIndexList[0] + " " + Engines[j].FaceList[i].VertexIndexList[1] + " " + Engines[j].FaceList[i].VertexIndexList[2] + " " + Engines[j].FaceList[i].color);
                sw.WriteLine(" ");
            }
           
            sw.Close();
        }

        public void readColor(string color)
        {
            var objs = color.Split('/').Select(s => int.Parse(s)).ToList();
            this.color = Color.FromArgb(objs[0], objs[1], objs[2]);
        }

        public void readparams(string[] mass)
        {
            for (int i = 0; i < mass.Length - 1; i++)
                Engines[Engines.Count-1].parameters[i] = mass[i + 1];
        }

        public Vertex readparamscamCenter(string[] mass)
        {
            cameras[cameras.Count - 1].center.X = Double.Parse(mass[1]);
            cameras[cameras.Count - 1].center.Y = Double.Parse(mass[2]);
            cameras[cameras.Count - 1].center.Z = Double.Parse(mass[3]);
            return cameras[cameras.Count - 1].center;
        }

        public Vertex readparamscamEye(string[] mass)
        {
            cameras[cameras.Count - 1].eye.X = Double.Parse(mass[1]);
            cameras[cameras.Count - 1].eye.Y = Double.Parse(mass[2]);
            cameras[cameras.Count - 1].eye.Z = Double.Parse(mass[3]);
            return cameras[cameras.Count - 1].eye;
        }

        public Vertex readparamscamUp(string[] mass)
        {
            cameras[cameras.Count - 1].up.X = Double.Parse(mass[1]);
            cameras[cameras.Count - 1].up.Y = Double.Parse(mass[2]);
            cameras[cameras.Count - 1].up.Z = Double.Parse(mass[3]);
            return cameras[cameras.Count - 1].up;
        }

        public void readparamscamAng(string[] mass)
        {
            cameras[cameras.Count - 1].pitch = Double.Parse(mass[1]);
            cameras[cameras.Count - 1].yaw = Double.Parse(mass[2]);
            cameras[cameras.Count - 1].roll = Double.Parse(mass[3]);
        }

        public void readcolors(string[] mass)
        {
            for (int i = 1; i < mass.Length; i++)
            {
                string[] c = mass[i].Split('/');
                Engines[Engines.Count - 1].colors[i - 1] = Color.FromArgb(Byte.Parse(c[0]), Byte.Parse(c[1]), Byte.Parse(c[2]));
            }

        }

        public void processLine(string line)
        {
            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 0)
            {
                switch (parts[0])
                {
                    case "namescene":
                        name = parts[1];
                        break;
                    case "width":
                        width = Convert.ToInt32(parts[1]);
                        break;
                    case "height":
                        height = Convert.ToInt32(parts[1]);
                        break;
                    case "colorscene":
                        readColor(parts[1]);
                        break;
                    case "coloringscene":
                        coloring = Convert.ToInt32(parts[1]);
                        break;
                    case "forallobjs":
                        forallobjs = Convert.ToBoolean(parts[1]);
                        break;
                    case "offcameras":
                        offcameras = Convert.ToBoolean(parts[1]);
                        break;
                    case "panorama":
                        panorama = Convert.ToBoolean(parts[1]);
                        break;
                    case "countobjects":
                        count_m = Convert.ToInt32(parts[1]);
                        break;
                    case "countcameras":
                        count_c = Convert.ToInt32(parts[1]);
                        break;
                    case "currentobject":
                        curr_m = Convert.ToInt32(parts[1]);
                        break;
                    case "currentcamera":
                        curr_c = Convert.ToInt32(parts[1]);
                        break;
                    case "projection":
                        projection = Convert.ToByte(parts[1]);
                        break;
                    case "namecamera":
                        cameras.Add(new Camera());
                        cameras[cameras.Count - 1].Name = parts[1];
                        break;
                    case "focus":
                        cameras[cameras.Count - 1].focus= Convert.ToDouble(parts[1]);
                        break;
                    case "camparams":
                       readparamscamAng(parts);
                        break;
                    case "scale":
                        cameras[cameras.Count - 1].scale = Convert.ToInt32(parts[1]);
                        break;
                    case "center":
                        cameras[cameras.Count - 1].center= readparamscamCenter(parts);
                        break;
                    case "eye":
                        cameras[cameras.Count - 1].eye = readparamscamEye(parts);
                        break;
                    case "up":
                        cameras[cameras.Count - 1].up = readparamscamUp(parts);
                        break;
                    case "nameobject":
                        Engines.Add(new Engine());
                        Engines[Engines.Count - 1].NameEngine = parts[1];
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
                        Engines[Engines.Count-1].VertexList.Add(v);
                        break;
                    case "f":
                        Face f = new Face();
                        f.LoadFromStringArray(parts);
                        Engines[Engines.Count-1].FaceList.Add(f);
                        break;

                }
            }
        }

    }
}
