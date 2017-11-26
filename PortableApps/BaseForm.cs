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
    public partial class BaseForm : Form
    {
        IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
        public int PageSize { get; set; }

        public BaseForm()
        {
            InitializeComponent();

            VariableSetting varPageSize = VariableSettingRepo.GetBy("PageSize");
            if (varPageSize == null)
            {
                PageSize = 20;
            }
            else
            {
                PageSize = Convert.ToInt32(varPageSize.Value);
            }
        }
    }
}
