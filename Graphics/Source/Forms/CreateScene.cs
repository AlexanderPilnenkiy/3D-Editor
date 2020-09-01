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
    public partial class CreateScene : Form
    {
        public CreateScene()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox2.Text) < 1 || Convert.ToInt32(textBox3.Text) < 800 || Convert.ToInt32(textBox4.Text) < 800 || Convert.ToInt32(textBox5.Text) < 0)
                    throw new Exception("Недопустимые значения");
               
                Scene sc = new Scene(textBox1.Text, Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox5.Text), button2.BackColor.R+"/"+ button2.BackColor.G+"/"+ button2.BackColor.B);
                bool flag = false;
                if (Program.form.sc == null)
                    flag = true;

                
                Program.form.sc = sc;
                

                if (flag==true)
                    Program.form.EnableElems();

                Program.form.installnewparams();

                Program.form.Text = "БУЛЬБУЛЯТОР1488 ЕЛЬЦИИЫЫН - " + sc.name;

                this.Dispose();
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Ошибка заполнения данными!";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Space)
                e.KeyChar = '\0';
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                button2.BackColor = Color.FromArgb(colorDialog1.Color.R, colorDialog1.Color.G, colorDialog1.Color.B);

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox3.Text) <1000 || Convert.ToInt32(textBox3.Text) >2000)
            {
                ErrorLabel.Text = "Такую ширину использовать нельзя!";
                textBox3.Text = "1500";
            }
            else
            {
                ErrorLabel.Text = "";
                button1.Enabled = true;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox4.Text) < 1000 || Convert.ToInt32(textBox4.Text) > 2000)
            {
                ErrorLabel.Text = "Такую высоту использовать нельзя!";
                textBox4.Text = "1000";
            }
            else
            {
                ErrorLabel.Text = "";
                button1.Enabled = true;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox2.Text) < 1)
            {
                ErrorLabel.Text = "Должен быть разрешён хотя бы один объект!";
                button1.Enabled = false;
            }
            else
            {
                ErrorLabel.Text = "";
                button1.Enabled = true;
            }
        }
    }
}
