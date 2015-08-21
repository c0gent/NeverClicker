using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Alib.Interop;


namespace NeverClicker
{
    public partial class FormMain : Form
    {
        AutomationEngine aEng;

        public FormMain()
        {
            InitializeComponent();
            aEng = new AutomationEngine(this);
        }

        public void WriteTextBox(string message)
        {
            textBox1.AppendText(message + "\r\n");
        }

        private void buttonAutoInvoke_Click(object sender, EventArgs e)
        {
            aEng.AutoInvoke();
        }

        private void buttonMoveMouse_Click(object sender, EventArgs e)
        {
            aEng.MoveMouse();
        }

        private void buttonCheckVar_Click(object sender, EventArgs e)
        {
            WriteTextBox(textBox_var.Text + ": " + aEng.GetVar(textBox_var.Text) + "\r\n");
        }

        private void buttonExecuteFunction_Click(object sender, EventArgs e)
        {
            WriteTextBox(
                textBoxExecuteFunction.Text
                + textBoxExecuteFunctionP1.Text + ", "
                + textBoxExecuteFunctionP2.Text + ", "
                + textBoxExecuteFunctionP3.Text + ", "
                + textBoxExecuteFunctionP4.Text
                + ": "
                + aEng.ExecuteFunctionTest(
                    textBoxExecuteFunction.Text, 
                    textBoxExecuteFunctionP1.Text, 
                    textBoxExecuteFunctionP2.Text,
                    textBoxExecuteFunctionP3.Text,
                    textBoxExecuteFunctionP4.Text
                )
            );
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            //albEng.Terminate();
            //albEng = new Alib.Interop.AlibEngine();
        }

        private void buttonSuspend_Click(object sender, EventArgs e)
        {
            //if (aEng.ToggleInputThread(TextBoxProgress()) == ThreadState.Suspended)
            //{
            //    buttonSuspend.Text = "Resume";
            //}
            //else
            //{
            //    buttonSuspend.Text = "Suspend";
            //}
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            aEng.Stop();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void groupTests_Enter(object sender, EventArgs e)
        {

        }
    }
}
