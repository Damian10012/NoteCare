using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            //to be edited with your local obsidian path
            string path = @"D:\Git\Obsidian\";


            List<string> dir = Directory.GetDirectories(path).ToList();
            for (int i = 0; i < dir.Count; i++)
            {
                List<string> toWrite = new List<string>();
                toWrite.Add("Links: [[Ordering]]");
                toWrite.Add("");
                toWrite.Add("---");
                string temp = dir[i];
                var split = temp.Split('\\');
                dir[i] = split[split.Length - 1];
                //check for usual generated files
                if (dir[i] == ".obsidian" || dir[i] == "Templates" || dir[i] == ".git")
                {
                    dir.RemoveAt(i);
                    i--;
                    continue;
                }
                var files = Directory.GetFiles($"{path}{dir[i]}");
                for (int j = 0; j < files.Length; j++)
                {
                    //get rid of whole path
                    temp = files[j];
                    var split2 = temp.Split('\\');
                    files[j] = split2[split2.Length - 1];
                }
                //file named dir[i].md is always the root of the directory with all the links
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
                    //creating the root file
                    File.Create($@"{path}{dir[i]}\{dir[i]}.md");
                }

                try
                {
                    //there is a probability that it can drop while you have the file open or mostly while opening a graph view - we don't want that
                    var text = File.ReadAllLines($@"{path}{dir[i]}\{dir[i]}.md");
                    //we don't have to check each file in "toWrite" because Obsidian automatically updates the links upon renaming
                    if (text.Length == toWrite.Count) continue;
                    //write everything to the file
                    File.WriteAllLines($@"{path}{dir[i]}\{dir[i]}.md", toWrite);
                }
                catch (Exception)
                {

                }

            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ShowInTaskbar = false;
            Hide();
            while (true)
            {
                Checking();
                //could be lower/higher depending your needs, I think 1000 is okay
                Thread.Sleep(1000);
            }

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
