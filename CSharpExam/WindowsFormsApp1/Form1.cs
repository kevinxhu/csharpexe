using System;
using System.Collections;
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

        //
        // Shared variables and non-shared variables
        //
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText("== Shared variables and non-shared variables ==" + Environment.NewLine);

            MethodInvoker[] delegates = new MethodInvoker[2];

            int outside = 0;

            for(int i = 0; i < 2; i++)
            {
                int inside = 0;

                delegates[i] = delegate
                {
                    richTextBox1.AppendText(outside.ToString() + "-" + inside.ToString() + Environment.NewLine);

                    outside++;
                    inside++;
                };
            }

            MethodInvoker first = delegates[0];
            MethodInvoker second = delegates[1];

            first();
            first();
            first();

            second();
            second();
        }

        //
        // IEnumerator
        //
        private void button3_Click(object sender, EventArgs e)
        {
            string Padding = new string(' ', 30);

            IEnumerable<int> CreateEnumrable()
            {
                richTextBox1.AppendText(String.Format("{0}Start of CreateEnumrable()", Padding) + Environment.NewLine);

                for(int i = 0; i < 3; i++)
                {
                    richTextBox1.AppendText(string.Format("{0}About to yield {1}", Padding, i) + Environment.NewLine);
                    yield return i;

                    richTextBox1.AppendText(string.Format("{0}After yield", Padding) + Environment.NewLine);
                }
                richTextBox1.AppendText(string.Format("{0}yielding final value", Padding) + Environment.NewLine);
                yield return -1;
                richTextBox1.AppendText(string.Format("{0}End of CreateEnumerable()", Padding) + Environment.NewLine);
            }

            IEnumerable<int> iterable = CreateEnumrable();
            IEnumerator<int> iterator = iterable.GetEnumerator();

            richTextBox1.AppendText(string.Format("Starting to iterate") + Environment.NewLine);
            
            while(true)
            {
                richTextBox1.AppendText("Calling MoveNext()" + Environment.NewLine);
                bool result = iterator.MoveNext();
                richTextBox1.AppendText(string.Format("... MoveNext result={0}", result));

                if(!result)
                {
                    break;
                }

                richTextBox1.AppendText("Fectching current..." + Environment.NewLine);
                richTextBox1.AppendText(string.Format("... Current result={0}", iterator.Current));
            }
        }
    }
}
