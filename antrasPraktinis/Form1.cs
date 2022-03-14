using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace antrasPraktinis
{
    public partial class Form1 : Form
    {
        AEScipher AesCipher;
        public Form1()
        {
            InitializeComponent();
            AesCipher = new AEScipher();
            comboBox1.Items.AddRange(new string[] { "CBC", "CFB", "ECB", "OFB" });
            comboBox1.SelectedIndex = 0;
            textBox1.Text = "1234567812345678";
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            int keyL = textBox1.TextLength;
            if (keyL == 16 || keyL == 24 || keyL == 32)
            {
                AesCipher.setKey(textBox1.Text);
                AesCipher.setCipherMode(CipherMode.CBC);
                richTextBox2.Text = AesCipher.Encrypt(richTextBox1.Text);
            }
            else
                MessageBox.Show("Please enter a 128bit (16char), 192bit (24char) or 256bit (32char) key");
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            int keyL = textBox1.TextLength;
            if (keyL == 16 || keyL == 24 || keyL == 32)
            {
                AesCipher.setKey(textBox1.Text);
                AesCipher.setCipherMode(CipherMode.CBC);
                richTextBox2.Text = AesCipher.Decrypt(richTextBox1.Text);
            }
            else
                MessageBox.Show("Please enter a 128bit (16char), 192bit (24char) or 256bit (32char) key");
        }

        private void buttonFileRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Choose text file";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void buttonFileSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.Title = "Save text to file";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, richTextBox2.Text);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;
            string selection = comboBox1.SelectedItem.ToString();
            switch (selection)
            {
                case "CBC":
                    AesCipher.setCipherMode(CipherMode.CBC);
                    break;
                case "CFB":
                    AesCipher.setCipherMode(CipherMode.CFB);
                    break;
                case "ECB":
                    AesCipher.setCipherMode(CipherMode.ECB);
                    break;
                case "OFB":
                    AesCipher.setCipherMode(CipherMode.OFB);
                    break;
            }
        }
    }
}
