using PortableApps.Repo;
using PortableApps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PortableApps
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

            ProcessFirst();

            Application.Run(new TestingForm());
        }

        private static void ProcessFirst()
        {
            ISqLiteBaseRepository sqlRepo = new SqLiteBaseRepository();
            using (var cnn = sqlRepo.MySQLiteConnection())
            {
                cnn.Open();
                //sqlRepo.ResetDefault(cnn);
                //sqlRepo.SetupAppInfo(cnn);
                //sqlRepo.SetupDun(cnn);
                //sqlRepo.SetupMakkebun(cnn);
                //sqlRepo.SetupParlimen(cnn);
                //sqlRepo.SetupVariables(cnn);
                //sqlRepo.SetupDaerah(cnn);
                //sqlRepo.SetupVariableSetting(cnn);
                //sqlRepo.SetupTBangsa(cnn);
            }

            //IBangsaRepo BangsaRepo = new BangsaRepo();
            //BangsaRepo.SyncBangsaFromAppInfoMySQL();
        }
    }
}
