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
    public partial class CreateCamera : Form
    {
        public CreateCamera()
        {
            InitializeComponent();
        }

        bool flag = false;
        int i = 0;

        public CreateCamera(Camera camera, int i)
        {
            InitializeComponent();
            flag = true;
            textBox1.Text = camera.eye.X.ToString();
            textBox2.Text = camera.eye.Y.ToString();
            textBox3.Text = camera.eye.Z.ToString();
            textBox4.Text = camera.center.X.ToString();
            textBox5.Text = camera.center.Y.ToString();
            textBox6.Text = camera.center.Z.ToString();
            textBox10.Text = camera.CamSpeed.ToString();
            textBox7.Text = camera.Name;
            textBox8.Text = camera.focus.ToString();
            textBox9.Text = camera.scale.ToString();
            this.i = i;
            Text = "Изменение камеры";
            button1.Text = "Изменить";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                 if (Math.Abs(Convert.ToDouble(textBox1.Text)) > 2000 || Math.Abs(Convert.ToDouble(textBox2.Text)) > 2000 || Math.Abs(Convert.ToDouble(textBox3.Text)) > 2000
                     || Math.Abs(Convert.ToDouble(textBox4.Text)) > 2000 || Math.Abs(Convert.ToDouble(textBox5.Text)) > 2000 || Math.Abs(Convert.ToDouble(textBox6.Text)) > 2000
                     || Convert.ToInt32(textBox8.Text) > 2000 || Convert.ToInt32(textBox8.Text) < 1000
                     || Convert.ToInt32(textBox9.Text) > 5 || Convert.ToInt32(textBox9.Text) < 1
                     || Convert.ToInt32(textBox10.Text) > 10 || Convert.ToInt32(textBox10.Text) < 1)
                     throw new Exception();

                Camera c = new Camera();
                c.eye.X = Double.Parse(textBox1.Text);
                c.eye.Y = Double.Parse(textBox2.Text);
                c.eye.Z = Double.Parse(textBox3.Text);
                c.center.X = Double.Parse(textBox4.Text);
                c.center.Y = Double.Parse(textBox5.Text);
                c.center.Z = Double.Parse(textBox6.Text);

                c.CamSpeed = Double.Parse(textBox10.Text);

               // c.up.X = Double.Parse(textBox1.Text);
              //  c.up.Z = Double.Parse(textBox3.Text);
                //c.up.Y = Double.Parse(textBox2.Text)+1;

                c.Name = textBox7.Text;
                c.focus = double.Parse(textBox8.Text);
                c.scale = int.Parse(textBox9.Text);
                Program.form.sc.cameras.Add(c);
                Program.form.sc.curr_c = Program.form.sc.cameras.Count - 1;

               

                if (flag == true)
                {
                    Program.form.sc.cameras.RemoveAt(i);
                    Program.form.sc.curr_c--;
                }

                Program.form.installnewparams();

                this.Dispose();
            }
            catch(Exception)
            {
                ErrorLabel.Text = "Произошла ошибка!";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {

        }

        private void textBox4_Leave(object sender, EventArgs e)
        {

        }

        private void textBox5_Leave(object sender, EventArgs e)
        {

        }

        private void textBox6_Leave(object sender, EventArgs e)
        {

        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            try {
            if (Convert.ToInt32(textBox8.Text) > 2500 || Convert.ToInt32(textBox8.Text) < 800)
            {
                    throw new Exception();
                }
            else
            {
                ErrorLabel.Text = "";
                button1.Enabled = true;
            }
            }
            catch (Exception) {
                ErrorLabel.Text = "Такой фокус использовать нельзя!";
                textBox8.Text = "1000";
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            try {
            if (Convert.ToInt32(textBox9.Text) > 5 || Convert.ToInt32(textBox9.Text) < 1)
            {
                    throw new Exception();
                }
            else
            {
                ErrorLabel.Text = "";
                button1.Enabled = true;
            }
            }
            catch (Exception) {
                ErrorLabel.Text = "Такой масштаб использовать нельзя!";
                button1.Enabled = false;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
