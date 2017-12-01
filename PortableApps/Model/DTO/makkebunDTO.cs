using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableApps.Model.DTO
{
    public class makkebunDTO
    {
        public int id_makkebun { get; set; }
        public int appinfo_id { get; set; }
        public string addr1 { get; set; }
        public string addr2 { get; set; }
        public string addr3 { get; set; }
        public string nolot { get; set; }
        public string hakmiliktanah { get; set; }
        public string tncr { get; set; }
        public double luasmatang { get; set; }
        public string tebang { get; set; }
        public string tarikhtebang { get; set; }
        public string nolesen { get; set; }
        public string syarattanah { get; set; }
        public string pemilikan { get; set; }
        public string pengurusan { get; set; }
        public double luaslesen { get; set; }
        public string catatan { get; set; }
        public DateTime created { get; set; }
        public string createdby { get; set; }
        #region semak_tapak
        public string tarikh_lawat { get; set; }
        public int? semak_tapak_id { get; set; }
        #endregion semak_tapak
    }
}
