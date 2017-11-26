﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

//c Add a project FunWithCSharpAsync to examine "async and await" feature to implement multi-threading and asynchronous programming in a simple way. This commit is before applying async and await feature to the project.
//c Invoke btnCallMethod_Click() event handler method asychronously and inside of that, invoke DoWorkAsync() method on the secondary thread asychronously. This DoWorkAsync() method makes the calling thread(the thread which called DoWorkAsync() method) sleep 4 seconds and return Task<string> to the place which called the DoWorkAsync() method. And on there, by await keyword, it extracts string which is the form to be assigned to this.Text from Task<string>.

namespace FunWithCSharpAsync
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnCallMethod_Click(object sender, EventArgs e)
        {
            this.Text = await DoWorkAsync();
        }

        // See below for code walkthrough...
        private async Task<string> DoWorkAsync()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(4000);
                return "Done with work!";
            });
        }
    }
}
