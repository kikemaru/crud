using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace OAIP6
{
    public partial class Form1 : Form
    {
        UserContext db;
        public Form1()
        {
            InitializeComponent();
            db = new UserContext();
            db.Izdatelstvo.Load();
            dataGridView1.DataSource = db.Izdatelstvo.Local.ToBindingList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 plForm = new Form2();
            DialogResult result = plForm.ShowDialog(this);
            if(result == DialogResult.Cancel)
                return;

            User user = new User();
            user.Exemplar = plForm.textBox3.Text;
            user.BookName = plForm.textBox1.Text;
            user.Avtor = plForm.textBox2.Text;
            user.Genre = plForm.comboBox1.SelectedItem.ToString();
            db.Izdatelstvo.Add(user);
            db.SaveChanges();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                User user = db.Izdatelstvo.Find(id);
                Form2 form2 = new Form2();

                form2.textBox1.Text = user.BookName;
                form2.textBox2.Text = user.Avtor;
                form2.textBox3.Text = user.Exemplar;
                form2.comboBox1.SelectedItem = user.Genre;

                DialogResult result = form2.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;

                user.Exemplar = form2.textBox3.Text;
                user.BookName = form2.textBox1.Text;
                user.Avtor = form2.textBox2.Text;
                user.Genre = form2.comboBox1.SelectedItem.ToString();

                db.SaveChanges();
                dataGridView1.Refresh();
                MessageBox.Show("Объект обновлен");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
            if (converted == false)
                return;
            User users = db.Izdatelstvo.Find(id);
            db.Izdatelstvo.Remove(users);
            db.SaveChanges();
            MessageBox.Show("Объект Удален");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}