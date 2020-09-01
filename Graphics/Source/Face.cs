using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphics
{
    public class Face : IType
    {
        public const int MinimumDataLength = 4;
        public const string Prefix = "f";
        public int[] VertexIndexList { get; set; }

        public int color;

        public Face()
        {

        }

        public Face(int ix, int iy, int iz, int color)
        {
            VertexIndexList =new int[3];
            VertexIndexList[0] = ix;
            VertexIndexList[1] = iy;
            VertexIndexList[2] = iz;
            this.color = color;
        }

        public void LoadFromStringArray(string[] data)
        {
            if (data.Length < MinimumDataLength)
                throw new ArgumentException("Должно быть " + MinimumDataLength, "data");

            if (!data[0].ToLower().Equals(Prefix))
                throw new ArgumentException("Префикс для граней: '" + Prefix + "'","data");

            int vcount = data.Count() - 2;
            VertexIndexList = new int[vcount];

            bool success;

            for (int i = 0; i < vcount; i++)
            {
                string[] parts = data[i + 1].Split('/');

                int vindex;
                success = int.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out vindex);
                if (!success) throw new ArgumentException("Параметр не был int");
                VertexIndexList[i] = vindex;

            }
            color = int.Parse(data[4]);
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            b.Append("f");

            for (int i = 0; i < VertexIndexList.Count(); i++)
            {
                    b.AppendFormat(" {0}", VertexIndexList[i]);
            }

            return b.ToString();
        }
    }
}
