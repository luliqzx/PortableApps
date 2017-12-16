using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableApps.Model
{
    public class appinfo
    {

        public int id { get; set; }
        public string refno_new { get; set; }
        public string nama { get; set; }
        public int type_id { get; set; }
        public string icno { get; set; }
        public string nolesen { get; set; }
        public string bangsa { get; set; }
        public string addr1 { get; set; }
        public string addr2 { get; set; }
        public string addr3 { get; set; }
        public string bandar { get; set; }
        public string daerah { get; set; }
        public string dun { get; set; }
        public int? parlimen { get; set; }
        public string poskod { get; set; }
        public string negeri { get; set; }
        public string hometel { get; set; }
        public string officetel { get; set; }
        public string hptel { get; set; }
        public string faks { get; set; }
        public string email { get; set; }
        public string kelompok { get; set; }
        public DateTime created { get; set; }
        public string createdby { get; set; }
        public string appdate { get; set; }
        public string semak_tapak { get; set; }
        public string keputusan { get; set; }
        public int sts_bck { get; set; }
        public int status { get; set; }
        public DateTime date_approved { get; set; }
        public string approved_by { get; set; }
        public int sop { get; set; }

        public string newrefno { get; set; }
        public DateTime? syncdate { get; set; }
    }
}
