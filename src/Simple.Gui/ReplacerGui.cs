﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Microsoft.Build.Utilities;

namespace Simple.Gui
{
    public partial class ReplacerGui : Form
    {
        public ReplacerGui()
        {
            InitializeComponent();
            txtNamespace_TextChanged(this, null);

            AdvancedGroup.Visible = false;
            AutoResize();
        }


        private void AutoResize()
        {
            if (AdvancedGroup.Visible)
            {
                this.Height = AdvancedGroup.Top + AdvancedGroup.ClientSize.Height + 2 * AdvancedGroup.Left + btnOk.Height;
                txtCatalog.Focus();
                btnMore.Text = "<< Less";
            }
            else
            {
                this.Height = AdvancedGroup.Top + AdvancedGroup.Left + btnOk.Height;
                txtNamespace.Focus();
                btnMore.Text = "More >>";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"data\");
            string message = "Temporary data is on:\n" + path + "\n\nAnd will be installed on:\n" + txtDirectory.Text + "\n\nAre you sure?";

            if (MessageBox.Show(message, "Simple.Net", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();

                var progress = new ProgressGui();

                var t = new Thread(() =>
                {
                    var logic = new ProgramLogic()
                    {
                        Catalog = txtCatalog.Text.Trim(),
                        IISUrl = txtIISUrl.Text.Trim(),
                        InstallPath = txtDirectory.Text.Trim(),
                        Namespace = txtNamespace.Text.Trim(),
                        ServiceName = txtSvcName.Text.Trim(),
                        SetupEnv = chkSetup.Checked,
                        ReplacePath = path,
                    };
                    logic.OnProgress += (text, subText) => progress.InvokeControlAction(x => x.SetProgress(text, subText));
                    logic.OnFinish += (success, url) => progress.InvokeControlAction(x => x.ShowFinished(success, url));

                    logic.Execute();
                });
                t.Start();

                progress.ShowDialog(this);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void txtNamespace_TextChanged(object sender, EventArgs e)
        {
            txtCatalog.Text = txtNamespace.Text.Replace(".", "");
            txtIISUrl.Text = txtNamespace.Text.Replace(".", "-").ToLower();
            txtSvcName.Text = txtNamespace.Text.Replace(".", "").ToLower() + "svc";

            var instDir = GetInstDir();

            if (e == null)
                txtDirectory.Text = Path.Combine(Environment.CurrentDirectory, instDir);
            else
                txtDirectory.Text = Path.Combine(Path.GetDirectoryName(txtDirectory.Text) ?? txtDirectory.Text, instDir);
        }

        private string GetInstDir()
        {
            var instDir = string.IsNullOrEmpty(txtIISUrl.Text) ? "project" : txtIISUrl.Text;
            return instDir;
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            AdvancedGroup.Visible = !AdvancedGroup.Visible;
            AutoResize();
        }

        

        private void btnDirectory_Click(object sender, EventArgs e)
        {
            var instDir = GetInstDir();

            folderBrowser.SelectedPath = txtDirectory.Text;
            folderBrowser.ShowDialog(this);
            txtDirectory.Text = (Path.GetFileName(folderBrowser.SelectedPath.ToLower()) == instDir.ToLower())
                ? folderBrowser.SelectedPath
                : Path.Combine(folderBrowser.SelectedPath, instDir);
        }





    }
}

