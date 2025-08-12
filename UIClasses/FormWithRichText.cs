using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpAppPlayground.UIClasses
{
    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // DOCUMENT THIS: Invoke vs BeginInvoke
    // invoke can create cyclic dependencies, causing deadlocks
    // beginInvoke is asynchronous and does not block the calling thread
    // document why this happens and when to use each

    public partial class FormWithRichText : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public FormWithRichText()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        protected void InitializeComponent()
        {
            lblMain = new Label();
            richTBoxMain = new RichTextBox();
            btnClose = new Button();
            SuspendLayout();
            // 
            // lblMain
            // 
            lblMain.AutoSize = true;
            lblMain.Location = new Point(540, 9);
            lblMain.Name = "lblMain";
            lblMain.Size = new Size(65, 15);
            lblMain.TabIndex = 0;
            lblMain.Text = "Main Label";
            // 
            // richTBoxMain
            // 
            richTBoxMain.Location = new Point(540, 62);
            richTBoxMain.Name = "richTBoxMain";
            richTBoxMain.Size = new Size(248, 321);
            richTBoxMain.TabIndex = 1;
            richTBoxMain.Text = "";
            // 
            // btnClose
            // 
            btnClose.Location = new Point(589, 403);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(199, 35);
            btnClose.TabIndex = 2;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // FormWithRichText
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnClose);
            Controls.Add(richTBoxMain);
            Controls.Add(lblMain);
            Name = "FormWithRichText";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblMain;
        private RichTextBox richTBoxMain;
        private Button btnClose;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // invoking the main UI thread to do it if it is called from another thread
        // This is a workaround to avoid the error:
        // "Cross-thread operation not valid: Control 'richTBoxMain' accessed from a thread other than the thread it was created on."
        public void updateRichTextBoxMain(string msg, Color lineColor = default)
        {
            if (this.IsDisposed || this.Disposing)
            {
                // Form is disposed or disposing, do not attempt to update UI
                Debug.Print("Form is disposed or disposing, skipping updateRichTextBoxMain.");
                return;
            }
            if (InvokeRequired)
            {
                try
                {
                    Debug.Print("InvokeRequired for updateRichTextBoxMain().");
                    BeginInvoke(new Action<string, Color>(updateRichTextBoxMain), msg, lineColor);
                }
                catch (ObjectDisposedException)
                {
                    Debug.Print("Invoke failed: Form is disposed.");
                }
                catch (InvalidOperationException)
                {
                    Debug.Print("Invoke failed: Form is disposed or handle is invalid.");
                }
            }
            else
            {
                if (richTBoxMain != null && !richTBoxMain.IsDisposed)
                {
                    richTBoxMain.SelectionColor = lineColor;
                    richTBoxMain.AppendText(msg + Environment.NewLine);
                    richTBoxMain.Refresh(); // Force the rich text box to refresh immediately
                }
                else
                {
                    Debug.Print($"text: {msg}");
                }
            }
        }

        public void updateLabelMain(string msg)
        {
            if (this.IsDisposed || this.Disposing)
            {
                // Form is disposed or disposing, do not attempt to update UI
                Debug.Print("Form is disposed or disposing, skipping updateLabelMain.");
                return;
            }
            if (InvokeRequired)
            {
                try
                {
                    Debug.Print("InvokeRequired for updateLabelMain().");
                    BeginInvoke(new Action<string>(updateLabelMain), msg);
                }
                catch (ObjectDisposedException)
                {
                    Debug.Print("Invoke failed: Form is disposed.");
                }
                catch (InvalidOperationException)
                {
                    Debug.Print("Invoke failed: Form is disposed or handle is invalid.");
                }
            }
            else
            {
                if (lblMain != null && !lblMain.IsDisposed)
                {
                    lblMain.Text = msg;
                    lblMain.Refresh(); // Force the label to refresh immediately
                                       // WITHOUT refresh the label might not redraw immediately
                                       // GUI.Label doesn't update/redraw as aggressively as GUI.TextBox
                }
                else
                {
                    Debug.Print($"label: {msg}");
                }
            }
        }
    }
}
