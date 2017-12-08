using MySql.Data.MySqlClient;
using PortableApps.Model;
using PortableApps.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PortableApps
{
    public partial class MDIParent2 : Form
    {
        #region Fields / Properties
        IAppInfoRepo AppInfoRepo = new AppInfoRepo();
        IVariablesRepo VariablesRepo = new VariablesRepo();
        IMakkebunRepo MakkebunRepo = new MakkebunRepo();
        ISemakTapakRepo SemakTapakRepo = new SemakTapakRepo();

        #endregion

        #region Constructor
        public MDIParent2()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void timer1_Tick(object sender, EventArgs e)
        {
            string Nama = new VariableSettingRepo().GetBy("UserKeyIn").Value + " | " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            tssUserKeyIn.Text = Nama;

        }

        private void rbMaklumatPemohon_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(appinfosaveform))
                {
                    f.Close();
                    //f.Activate();
                    //return;
                }
            }
            Form form = new appinfosaveform();
            form.MdiParent = this;
            form.Show();
        }

        private void rbmakkebun_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(makkebunform))
                {
                    f.Close();
                    //f.Activate();
                    //return;
                }
            }
            Form form = new makkebunform();
            form.MdiParent = this;
            form.Show();
        }

        private void rbSetting_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(VariableSettingForm))
                {
                    f.Close();
                    //f.Activate();
                    //return;
                }
            }
            Form form = new VariableSettingForm();
            form.MdiParent = this;
            form.Show();
        }

        private void rbDaftarMaklumatPermohonan_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(appinfolistform))
                {
                    f.Close();
                    //f.Activate();
                    //return;
                }
            }
            Form form = new appinfolistform();
            form.MdiParent = this;
            form.Show();
        }

        private void MDIParent2_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            BringToFront();


        }

        private void roobClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ribbonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbLawatanPengesahanKebun_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(lawatanpengesahankebunform))
                {
                    f.Close();
                    //f.Activate();
                    //return;
                }
            }
            Form form = new lawatanpengesahankebunform();
            form.MdiParent = this;
            form.Show();
        }

        private void rbInitData_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(InitializeForm))
                {
                    f.Close();
                    //f.Activate();
                    //return;
                }
            }
            Form form = new InitializeForm();
            form.MdiParent = this;
            form.Show();
        }

        private void rbUpdateServer_Click(object sender, EventArgs e)
        //{

        //    MessageBox.Show("Under Construction");
        //    return;
        //    //foreach (Form f in this.MdiChildren)
        //    //{
        //    //    if (f.GetType() == typeof(InitializeForm))
        //    //    {
        //    //        f.Close();
        //    //        //f.Activate();
        //    //        //return;
        //    //    }
        //    //}
        //    //Form form = new InitializeForm();
        //    //form.MdiParent = this;
        //    //form.Show();

        //    SyncToServer();

        //}
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(SecurityForm))
                {
                    f.Close();
                    //f.Activate();
                    //return;
                }
            }
            Form form = new SecurityForm();
            //form.MdiParent = this;
            form.ShowDialog();
            //form.Show();
        }

        #endregion

        #region Functions

        public void SyncToServer()
        {
            IList<appinfo> lstAppInfoToSync = AppInfoRepo.GetAllWithoutSync();
            for (int i = 0; i < lstAppInfoToSync.Count; i++)
            {
                appinfo appinfoSqlite = lstAppInfoToSync[i];
                AppInfoRepo.OpenMySQLDB();

                using (IDbTransaction sqlTrans = AppInfoRepo.MySQLBeginTransaction())
                {
                    string newrefno = GenerateRefNo(appinfoSqlite.negeri, null);
                    appinfoSqlite.newrefno = newrefno;
                    // Insert To MySQL Server - AppInfo
                    if (AppInfoRepo.CreateMySQL(appinfoSqlite, sqlTrans) > 0)
                    {
                        // Update data local sqlite
                        appinfoSqlite.syncdate = DateTime.Now;
                        AppInfoRepo.UpdateSync(appinfoSqlite);

                        IList<makkebun> lstMakkebunSqlite = MakkebunRepo.GetAllByAppInfo(appinfoSqlite.id);
                        for (int j = 0; j < lstMakkebunSqlite.Count; j++)
                        {
                            makkebun makkebunSqlite = lstMakkebunSqlite[j];
                            // INSERT MAKKEBUN TO MYSQL
                            int iSaveMakkebun = MakkebunRepo.CreateMySQL(makkebunSqlite, sqlTrans);
                            if (iSaveMakkebun > 0)
                            {
                                makkebun lastmakkebun = MakkebunRepo.GetLastMakkebunBy(appinfoSqlite.id);
                                makkebunSqlite.newid_makkebun = lastmakkebun.id_makkebun;
                                makkebunSqlite.syncdate = DateTime.Now;
                                // UPDATE MAKKEBUN SQLITE
                                MakkebunRepo.UpdateSync(makkebunSqlite);

                                // GET LAWATAN SQLITE DATA
                                semak_tapak semak_tapakSqlite = SemakTapakRepo.GetBy(appinfoSqlite.id, makkebunSqlite.id_makkebun);
                                semak_tapakSqlite.newmakkebun_id = makkebunSqlite.newid_makkebun;

                                // INSERT LAWATAN TO MYSQL
                                int iSaveSemakTapak = SemakTapakRepo.CreateMySQL(semak_tapakSqlite, sqlTrans);
                                if (iSaveSemakTapak > 0)
                                {
                                    semak_tapak lastsemak_tapak = SemakTapakRepo.GetLastSemakTapakBy(appinfoSqlite.id, semak_tapakSqlite.newmakkebun_id);
                                    semak_tapakSqlite.newid = lastsemak_tapak.id;
                                    semak_tapakSqlite.syncdate = DateTime.Now;
                                    // UPDATE MAKKEBUN SQLITE
                                    int iSemakTapakUpdateSync = SemakTapakRepo.UpdateSync(semak_tapakSqlite);
                                }
                            }
                        }
                    }
                    sqlTrans.Commit();
                }
                AppInfoRepo.CloseMySQLDB();
            }
        }

        private string GenerateRefNo(string negeri, IDbTransaction mySqlTrans)
        {
            string refno = "";

            variables variables = VariablesRepo.GetBy(negeri);
            refno = @"TSSPK/" + variables.Parent + "/";

            int maxappinfo = AppInfoRepo.GetMaxRefNoMySQL(refno, mySqlTrans);

            refno = refno + maxappinfo.ToString().PadLeft(5, '0');

            return refno;
        }

        #endregion

    }
}
