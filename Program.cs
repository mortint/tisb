using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

//[assembly: Obfuscation(Exclude = false, Feature = "namespace('ISP.Engine'):-rename")]

namespace TISB {
    static class Program {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main() {
            AppDomain.CurrentDomain.AssemblyResolve += AbsenceOfDLL;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static Assembly AbsenceOfDLL(object sender, ResolveEventArgs args) {
            var folderPath = @"Library";
            var assemblyPath = Path.Combine(folderPath, new AssemblyName(args.Name).Name + ".dll");

            if (File.Exists(assemblyPath)) {
                return Assembly.LoadFrom(assemblyPath);
            }

            return null;
        }
    }
}
