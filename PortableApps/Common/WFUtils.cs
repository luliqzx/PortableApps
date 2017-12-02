using PortableApps.Repo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PortableApps.Common
{
    public static class WFUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();

            string passPhrase = VariableSettingRepo.GetBy("passPhrase").Value;        // can be any string
            string saltValue = VariableSettingRepo.GetBy("saltValue").Value;             // can be any string
            string hashAlgorithm = VariableSettingRepo.GetBy("hashAlgorithm").Value;                   // can be "MD5"
            int passwordIterations = Convert.ToInt32(VariableSettingRepo.GetBy("passwordIterations").Value);                   // can be any number
            string initVector = VariableSettingRepo.GetBy("initVector").Value;         // must be 16 bytes
            int keySize = Convert.ToInt32(VariableSettingRepo.GetBy("keySize").Value);                 // can be 192 or 128

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
            IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
            string passPhrase = VariableSettingRepo.GetBy("passPhrase").Value;        // can be any string
            string saltValue = VariableSettingRepo.GetBy("saltValue").Value;             // can be any string
            string hashAlgorithm = VariableSettingRepo.GetBy("hashAlgorithm").Value;                   // can be "MD5"
            int passwordIterations = Convert.ToInt32(VariableSettingRepo.GetBy("passwordIterations").Value);                   // can be any number
            string initVector = VariableSettingRepo.GetBy("initVector").Value;         // must be 16 bytes
            int keySize = Convert.ToInt32(VariableSettingRepo.GetBy("keySize").Value);                 // can be 192 or 128

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
    }
}
