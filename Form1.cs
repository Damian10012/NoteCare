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

namespace NoteCare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Checking()
        {

            List<string> dir = Directory.GetDirectories(@"D:\Git\Obsidian\").ToList();
            for (int i = 0; i < dir.Count; i++)
            {
                List<string> toWrite = new List<string>();
                toWrite.Add("Links: [[Ordering]]");
                toWrite.Add("");
                toWrite.Add("---");
                string temp = dir[i];
                var split = temp.Split('\\');
                dir[i] = split[split.Length - 1];
                if (dir[i] == ".obsidian" || dir[i] == "Templates" || dir[i] == ".git")
                {
                    dir.RemoveAt(i);
                    i--;
                    continue;
                }
                var files = Directory.GetFiles($@"D:\Git\Obsidian\{dir[i]}");
                for (int j = 0; j < files.Length; j++)
                {
                    temp = files[j];
                    var split2 = temp.Split('\\');
                    files[j] = split2[split2.Length - 1];
                }

                if (files.Contains($"{dir[i]}.md"))
                {
                    for (int j = 0; j < files.Length; j++)
                    {
                        if (files[j] == $"{dir[i]}.md") continue;
                        toWrite.Add($"[[{files[j]}]]");

                    }
                }
                else
                {
                    File.Create($@"D:\Git\Obsidian\{dir[i]}\{dir[i]}.md");
                }
                    
                try
                {
                    var text = File.ReadAllLines($@"D:\Git\Obsidian\{dir[i]}\{dir[i]}.md");
                    if (text.Length == toWrite.Count)
                    {
                        continue;
                    }
                }
                catch (Exception)
                {

                }
                File.WriteAllLines($@"D:\Git\Obsidian\{dir[i]}\{dir[i]}.md", toWrite);
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ShowInTaskbar = false;
            Hide();
            Checking();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {

            if (Visible)
            {
                ShowInTaskbar = false;
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            ShowInTaskbar = false;
            Hide();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
