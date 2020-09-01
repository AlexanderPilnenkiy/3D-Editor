using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics
{
    public partial class CreateForm : Form
    {
        public CreateForm()
        {
            InitializeComponent();
        }

        int index = 0;

        public CreateForm(int index,String name, string[] mass, Color [] colors)
        {
            InitializeComponent();
            NameBox.Text = name;
            WidthtextBox.Text = mass[0];
            LenghttextBox.Text = mass[1];
            HeighttextBox.Text = mass[2];
            CountIndtextBox.Text = mass[3];
            LipIndtextBox.Text = mass[4];
            CountAnttextBox.Text = mass[5];
            HeightAnttextBox.Text=mass[6];
            DiamAnttextBox.Text = mass[7];
            CountButtontextBox.Text = mass[8];
            LipButtontextBox.Text = mass[9];
            HeightLegtextBox.Text = mass[10];
            CountLegtextBox.Text = mass[11];
            LeghtLipCaptextBox.Text = mass[12];
            HeightCaptextBox.Text=mass[13];
            this.index = index;
            button1.Text = "Изменить";
            Text = "Изменить объект";

            button2.BackColor = colors[0];
            button3.BackColor = colors[1];
            button4.BackColor = colors[2];
            button5.BackColor = colors[3];
            button6.BackColor = colors[4];
            button7.BackColor = colors[5];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string [] mass= new string[10];
            if (button1.Text == "Изменить")
            {
                for(int i=0;i<mass.Length;i++)
                {
                    mass[i]=Program.form.sc.Engines[index].parameters[14 + i];
                }
                Program.form.sc.Engines.RemoveAt(index);
            }
            else
            {
                for (int i = 0; i < mass.Length-4; i++)
                {
                    mass[i] = 0.ToString();
                }
                mass[mass.Length-1] = 1.ToString();
                mass[mass.Length - 2] = 1.ToString();
                mass[mass.Length - 3] = 1.ToString();
                mass[mass.Length - 4] = 1.ToString();
                Program.form.sc.curr_m = Program.form.sc.Engines.Count;
            }

            int s;
            if (LenghttextBox.Text == "50") { s = 47; } else { s = Convert.ToInt32(LenghttextBox.Text); }
            Program.form.Create(NameBox.Text, Convert.ToInt32(WidthtextBox.Text), s, Convert.ToInt32(HeighttextBox.Text), 
                Convert.ToInt32(CountIndtextBox.Text), Convert.ToInt32(LipIndtextBox.Text), Convert.ToInt32(CountAnttextBox.Text), 
                Convert.ToInt32(HeightAnttextBox.Text), Convert.ToInt32(DiamAnttextBox.Text),Convert.ToInt32(CountButtontextBox.Text),
                Convert.ToInt32(LipButtontextBox.Text), Convert.ToInt32(HeightLegtextBox.Text), Convert.ToInt32(CountLegtextBox.Text), 
                Convert.ToInt32(LeghtLipCaptextBox.Text), Convert.ToInt32(HeightCaptextBox.Text), mass, button2.BackColor, button3.BackColor, 
                button4.BackColor, button5.BackColor, button6.BackColor, button7.BackColor);

          
            this.Dispose();

        }


        private void NameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Space)
                e.KeyChar = '\0';
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                button2.BackColor = Color.FromArgb(colorDialog1.Color.R, colorDialog1.Color.G, colorDialog1.Color.B);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
                button3.BackColor = Color.FromArgb(colorDialog2.Color.R, colorDialog2.Color.G, colorDialog2.Color.B);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (colorDialog3.ShowDialog() == DialogResult.OK)
                button4.BackColor = Color.FromArgb(colorDialog3.Color.R, colorDialog3.Color.G, colorDialog3.Color.B);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (colorDialog4.ShowDialog() == DialogResult.OK)
                button5.BackColor = Color.FromArgb(colorDialog4.Color.R, colorDialog4.Color.G, colorDialog4.Color.B);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (colorDialog5.ShowDialog() == DialogResult.OK)
                button6.BackColor = Color.FromArgb(colorDialog5.Color.R, colorDialog5.Color.G, colorDialog5.Color.B);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (colorDialog6.ShowDialog() == DialogResult.OK)
                button7.BackColor = Color.FromArgb(colorDialog6.Color.R, colorDialog6.Color.G, colorDialog6.Color.B);
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            WidthtextBox.Text = "80";
            LenghttextBox.Text = "30";
            HeighttextBox.Text = "20";
            DiamAnttextBox.Text = "100";
            CountAnttextBox.Text = "50";
            HeightAnttextBox.Text = "50";
        }
    }
}
