﻿using MySql.Data.MySqlClient;
using PortableApps.Model;
using PortableApps.Repo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PortableApps.Common
{
    public static class WFUtils
    {
        #region Fields / Properties
        const string passPhrase = "#1UWLyP1";        // can be any string
        const string saltValue = "s@ltValue";             // can be any string
        const string hashAlgorithm = "SHA1";                   // can be "MD5"
        const int passwordIterations = 2;                   // can be any number
        const string initVector = "@1B2c3D4e5F6g7H8";         // must be 16 bytes
        const int keySize = 256;                 // can be 192 or 128
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            //IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();

            //string passPhrase = VariableSettingRepo.GetBy("passPhrase").Value;        // can be any string
            //string saltValue = VariableSettingRepo.GetBy("saltValue").Value;             // can be any string
            //string hashAlgorithm = VariableSettingRepo.GetBy("hashAlgorithm").Value;                   // can be "MD5"
            //int passwordIterations = Convert.ToInt32(VariableSettingRepo.GetBy("passwordIterations").Value);                   // can be any number
            //string initVector = VariableSettingRepo.GetBy("initVector").Value;         // must be 16 bytes
            //int keySize = Convert.ToInt32(VariableSettingRepo.GetBy("keySize").Value);                 // can be 192 or 128

            string chiperText = string.Empty;
            chiperText = RijndaelSimple.Encrypt(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
            return chiperText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chiperText"></param>
        /// <returns></returns>
        public static string Decrypt(string chiperText)
        {
            //string passPhrase = Utils.GetAppSetting("passPhrase");        // can be any string
            //string saltValue = Utils.GetAppSetting("saltValue");             // can be any string
            //string hashAlgorithm = Utils.GetAppSetting("hashAlgorithm");                   // can be "MD5"
            //int passwordIterations = Convert.ToInt32(Utils.GetAppSetting("passwordIterations"));                   // can be any number
            //string initVector = Utils.GetAppSetting("initVector");         // must be 16 bytes
            //int keySize = Convert.ToInt32(Utils.GetAppSetting("keySize"));                 // can be 192 or 128
            //IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
            //string passPhrase = VariableSettingRepo.GetBy("passPhrase").Value;        // can be any string
            //string saltValue = VariableSettingRepo.GetBy("saltValue").Value;             // can be any string
            //string hashAlgorithm = VariableSettingRepo.GetBy("hashAlgorithm").Value;                   // can be "MD5"
            //int passwordIterations = Convert.ToInt32(VariableSettingRepo.GetBy("passwordIterations").Value);                   // can be any number
            //string initVector = VariableSettingRepo.GetBy("initVector").Value;         // must be 16 bytes
            //int keySize = Convert.ToInt32(VariableSettingRepo.GetBy("keySize").Value);                 // can be 192 or 128

            string plainText = string.Empty;
            plainText = RijndaelSimple.Decrypt(chiperText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
            return plainText;
        }

        public static bool CheckEmpty(this Control ctrl)
        {
            bool returnVal = false;
            if (ctrl is TextBox)
            {
                if (string.IsNullOrEmpty(ctrl.Text))
                {
                    MessageBox.Show(ctrl.Name.Replace("txt", "") + " tidak boleh kosong");
                    ctrl.Focus();
                    returnVal = true;
                }
            }
            return returnVal;
        }

        public static bool CheckControllCollectionEmpty(IList<Control> ctrlEmpty)
        {
            bool retVal = false;
            foreach (Control ctrl in ctrlEmpty)
            {
                if (ctrl is TextBox)
                {
                    if (string.IsNullOrEmpty(ctrl.Text))
                    {
                        retVal = true;
                        MessageBox.Show(ctrl.Name.Replace("txt", "") + " harus diisi.");
                        ctrl.Focus();
                        break;
                    }
                }
                if (ctrl is DateTimePicker)
                {
                    if (string.IsNullOrEmpty(ctrl.Text))
                    {
                        retVal = true;
                        MessageBox.Show(ctrl.Name.Replace("txt", "") + " harus diisi.");
                        ctrl.Focus();
                        break;
                    }
                }
                if (ctrl is ComboBox)
                {
                    string valx = ((ComboBox)ctrl).SelectedValue == null ? "" : ((ComboBox)ctrl).SelectedValue.ToString();
                    if (string.IsNullOrEmpty(valx))
                    {
                        retVal = true;
                        MessageBox.Show(ctrl.Name.Replace("cb", "") + " harus diisi.");
                        ctrl.Focus();
                        break;
                    }
                }
            }
            return retVal;
        }

        public static void HookFocusChangeBackColor(this Control ctrl, Color focusBackColor)
        {
            var originalColor = ctrl.BackColor;
            var gotFocusHandler = new EventHandler((sender, e) =>
            {
                (ctrl as Control).BackColor = focusBackColor;
            });
            var lostFocusHandler = new EventHandler((sender, e) =>
            {
                (ctrl as Control).BackColor = originalColor;
            });

            ctrl.GotFocus -= gotFocusHandler;
            ctrl.GotFocus += gotFocusHandler;

            ctrl.LostFocus -= lostFocusHandler;
            ctrl.LostFocus += lostFocusHandler;
        }

        /// <summary>
        /// DateTime -> Unix
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (dateTime.ToUniversalTime() - unixStart).Ticks;
            return (double)unixTimeStampInTicks / TimeSpan.TicksPerSecond;
        }

        /// <summary>
        /// Unix -> DateTime
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(double unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
        }

        public static string GenerateRefNo(string negeri, IDbTransaction mySqlTrans)
        {
            IVariablesRepo VariablesRepo = new VariablesRepo();
            IAppInfoRepo AppInfoRepo = new AppInfoRepo();

            string refno_new = "";

            variables variables = VariablesRepo.GetBy(negeri);
            refno_new = @"TBSPK/" + variables.Parent + "/";

            //int maxappinfo = AppInfoRepo.GetMaxRefNoMySQL(refno_new, mySqlTrans);
            int maxappinfo = AppInfoRepo.GetMaxRefNoMySQL(mySqlTrans);

            refno_new = refno_new + maxappinfo.ToString().PadLeft(5, '0');

            return refno_new;
        }

        public static string GetFullMessage(this Exception ex)
        {
            return ex.InnerException == null
                 ? ex.Message
                 : ex.Message + " --> " + ex.InnerException.GetFullMessage();
        }

        #region Push To MySQL

        /// <summary>
        /// Process Normal
        /// </summary>
        public static void SyncToServer()
        {
            try
            {
                IAppInfoRepo AppInfoRepo = new AppInfoRepo();
                AppInfoRepo.ResetMySQLConnection(true);
                IMakkebunRepo MakkebunRepo = new MakkebunRepo();
                ISemakTapakRepo SemakTapakRepo = new SemakTapakRepo();

                IList<appinfo> lstAppInfoToSync = AppInfoRepo.GetAllWithoutSync();
                for (int i = 0; i < lstAppInfoToSync.Count; i++)
                {
                    appinfo appinfoSqlite = lstAppInfoToSync[i];
                    AppInfoRepo.OpenMySQLDB();
                    WriteLog("MySQLBeginTransaction");
                    using (IDbTransaction sqlTrans = AppInfoRepo.MySQLBeginTransaction())
                    {
                        string newrefno = GenerateRefNo(appinfoSqlite.negeri, null);
                        appinfoSqlite.newrefno = newrefno;
                        // Insert To MySQL Server - AppInfo
                        WriteLog("AppInfoRepo.CreateMySQL");
                        try
                        {
                            if (AppInfoRepo.CreateMySQL(appinfoSqlite, sqlTrans) > 0)
                            {
                                // Update data local sqlite
                                appinfoSqlite.syncdate = DateTime.Now;
                                AppInfoRepo.UpdateSync(appinfoSqlite);
                                WriteLog(string.Format("AppInfoRepo.CreateMySQL & Update SQLite - {0} - {1} - {2}", appinfoSqlite.id, appinfoSqlite.refno_new, appinfoSqlite.newrefno));

                                IList<makkebun> lstMakkebunSqlite = MakkebunRepo.GetAllByAppInfo(appinfoSqlite.id);
                                for (int j = 0; j < lstMakkebunSqlite.Count; j++)
                                {
                                    makkebun makkebunSqlite = lstMakkebunSqlite[j];
                                    // INSERT MAKKEBUN TO MYSQL
                                    WriteLog("MakkebunRepo.CreateMySQL");
                                    try
                                    {
                                        int iSaveMakkebun = MakkebunRepo.CreateMySQL(makkebunSqlite, sqlTrans);
                                        if (iSaveMakkebun > 0)
                                        {
                                            makkebun lastmakkebun = MakkebunRepo.GetLastMakkebunBy(appinfoSqlite.id);
                                            makkebunSqlite.newid_makkebun = lastmakkebun.id_makkebun;
                                            makkebunSqlite.syncdate = DateTime.Now;
                                            // UPDATE MAKKEBUN SQLITE

                                            MakkebunRepo.UpdateSync(makkebunSqlite);
                                            WriteLog(string.Format("MakkebunRepo.CreateMySQL & Update SQLite - {0} - {1} - {2}", appinfoSqlite.id, makkebunSqlite.id_makkebun, makkebunSqlite.newid_makkebun));

                                            // GET LAWATAN SQLITE DATA
                                            semak_tapak semak_tapakSqlite = SemakTapakRepo.GetBy(appinfoSqlite.id, makkebunSqlite.id_makkebun);
                                            if (semak_tapakSqlite != null)
                                            {
                                                semak_tapakSqlite.newmakkebun_id = makkebunSqlite.newid_makkebun;
                                                // INSERT LAWATAN TO MYSQL
                                                WriteLog("SemakTapakRepo.CreateMySQL");
                                                try
                                                {
                                                    int iSaveSemakTapak = SemakTapakRepo.CreateMySQL(semak_tapakSqlite, sqlTrans);
                                                    if (iSaveSemakTapak > 0)
                                                    {
                                                        semak_tapak lastsemak_tapak = SemakTapakRepo.GetLastSemakTapakBy(appinfoSqlite.id, semak_tapakSqlite.newmakkebun_id);
                                                        semak_tapakSqlite.newid = lastsemak_tapak.id;
                                                        semak_tapakSqlite.syncdate = DateTime.Now;
                                                        // UPDATE MAKKEBUN SQLITE
                                                        int iSemakTapakUpdateSync = SemakTapakRepo.UpdateSync(semak_tapakSqlite);
                                                        WriteLog(string.Format("SemakTapakRepo.CreateMySQL & Update SQLite - {0} - {1} - {2}", appinfoSqlite.id, semak_tapakSqlite.id, semak_tapakSqlite.newid));
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    WriteLog(string.Format("ERROR-SemakTapakRepo.CreateMySQL-{0}", ex.GetFullMessage()));
                                                }
                                            }
                                            else
                                            {
                                                WriteLog(string.Format("Maklumat Kebun Belum Lawat - {0}", makkebunSqlite.id_makkebun));
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLog(string.Format("ERROR-MakkebunRepo.CreateMySQL-{0}", ex.GetFullMessage()));
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            WriteLog(string.Format("ERROR-AppInfoRepo.CreateMySQL-appinfo:{0}-refno_new:{1}-MESSAGE:{2}", appinfoSqlite.id, appinfoSqlite.refno_new, ex.GetFullMessage()));
                        }

                        sqlTrans.Commit();
                    }
                    AppInfoRepo.CloseMySQLDB();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFullMessage());
            }
        }

        private static void SetupConnection()
        {
            MySqlConnection newMySQLConn;
            IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
            VariableSetting VariableSetting = VariableSettingRepo.GetBy("Status");
            if (VariableSetting != null)
            {
                if (!string.IsNullOrEmpty(VariableSetting.Value) && VariableSetting.Value.ToUpper() == "PRODUCTION")
                {
                    VariableSetting VariableSettingConStr = VariableSettingRepo.GetBy("MySQLConn");
                    newMySQLConn = new MySqlConnection(WFUtils.Decrypt(VariableSettingConStr.Value));
                }
                else
                {
                    newMySQLConn = new MySqlConnection("Server=127.0.0.1;Database=tsspk1511;Uid=root;Pwd=;");
                }
            }
        }

        /// <summary>
        /// Process Makkebun & Lawatan
        /// </summary>
        public static void SyncToServerForMakKebun()
        {
            IAppInfoRepo AppInfoRepo = new AppInfoRepo();
            IMakkebunRepo MakkebunRepo = new MakkebunRepo();
            ISemakTapakRepo SemakTapakRepo = new SemakTapakRepo();

            IList<makkebun> lstMakKebun = MakkebunRepo.GetAllToSync();
            for (int i = 0; i < lstMakKebun.Count; i++)
            {
                makkebun makkebunsqlite = lstMakKebun[i];
                using (IDbTransaction sqlTrans = MakkebunRepo.MySQLBeginTransaction())
                {
                    // INSERT MAKKEBUN TO MYSQL
                    int iSaveMakkebun = MakkebunRepo.CreateMySQL(makkebunsqlite, sqlTrans);
                    if (iSaveMakkebun > 0)
                    {
                        makkebun lastmakkebun = MakkebunRepo.GetLastMakkebunBy(makkebunsqlite.appinfo_id);
                        makkebunsqlite.newid_makkebun = lastmakkebun.id_makkebun;
                        makkebunsqlite.syncdate = DateTime.Now;
                        // UPDATE MAKKEBUN SQLITE
                        MakkebunRepo.UpdateSync(makkebunsqlite);

                        // GET LAWATAN SQLITE DATA
                        semak_tapak semak_tapakSqlite = SemakTapakRepo.GetBy(makkebunsqlite.appinfo_id, makkebunsqlite.id_makkebun);
                        semak_tapakSqlite.newmakkebun_id = makkebunsqlite.newid_makkebun;

                        // INSERT LAWATAN TO MYSQL
                        int iSaveSemakTapak = SemakTapakRepo.CreateMySQL(semak_tapakSqlite, sqlTrans);
                        if (iSaveSemakTapak > 0)
                        {
                            semak_tapak lastsemak_tapak = SemakTapakRepo.GetLastSemakTapakBy(makkebunsqlite.appinfo_id, semak_tapakSqlite.newmakkebun_id);
                            semak_tapakSqlite.newid = lastsemak_tapak.id;
                            semak_tapakSqlite.syncdate = DateTime.Now;
                            // UPDATE MAKKEBUN SQLITE
                            int iSemakTapakUpdateSync = SemakTapakRepo.UpdateSync(semak_tapakSqlite);
                        }
                    }
                    sqlTrans.Commit();
                }
                AppInfoRepo.CloseMySQLDB();
            }
        }

        #endregion

        internal static void EnsureFolder(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if ((directoryName.Length > 0) && (!Directory.Exists(directoryName)))
            {
                Directory.CreateDirectory(directoryName);
            }
        }

        internal static void WriteLog(string strMsg)
        {
            StreamWriter sw = null;
            try
            {
                string directory = AppDomain.CurrentDomain.BaseDirectory + string.Format(@"\Logs\AppLogs\{0}\", DateTime.Now.ToString("MM.yyyy"));
                EnsureFolder(directory);
                string fileName = string.Format(@"LogFile_{1}", DateTime.Now.ToString("MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"));
                sw = new StreamWriter(Path.Combine(directory, fileName), true);
                sw.WriteLine(DateTime.Now.ToString() + " - " + strMsg);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
    }
}
