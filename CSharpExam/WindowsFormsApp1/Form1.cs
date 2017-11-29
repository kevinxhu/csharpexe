using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public MethodInvoker CreateDelegateInstance()
        {
            int counter = 5;

            MethodInvoker ret = delegate
            {
                richTextBox1.AppendText(counter.ToString() + Environment.NewLine); 
                Console.WriteLine(counter);

                counter++;
            };

            ret();

            return ret;
        }

        //
        // Demo how local variables are instanced
        //
        public void DemoLocalVarInst()
        {
            richTextBox1.AppendText("== local variables are instanced==" + Environment.NewLine);

            List<MethodInvoker> list = new List<MethodInvoker>();

            for(int index = 0; index < 5; index++)
            {
                int counter = index * 10;
                list.Add(delegate
                {
                    richTextBox1.AppendText(counter.ToString() + Environment.NewLine);
                    Console.WriteLine(counter);
                    counter++;
                });
            }

            foreach(MethodInvoker t in list)
            {
                t();
            }

            list[0]();
            list[0]();
            list[0]();

            list[1]();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MethodInvoker x = CreateDelegateInstance();

            x();

            x();

            DemoLocalVarInst();
        }
    }
}
