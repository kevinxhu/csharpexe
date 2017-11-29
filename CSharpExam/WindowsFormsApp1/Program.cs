using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            MethodInvoker x = CreateDelegateInstance();

            x();

            x();
        }

        static MethodInvoker CreateDelegateInstance()
        {
            int counter = 5;

            MethodInvoker ret = delegate
            {
                MessageBox.Show(counter.ToString());
                Console.WriteLine(counter);

                counter++;
            };

            ret();

            return ret;
        }
    }
}
