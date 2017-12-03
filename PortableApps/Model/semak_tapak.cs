using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableApps.Model
{
    public class semak_tapak
    {
        public int id { get; set; }
        public int makkebun_id { get; set; }
        public int appinfo_id { get; set; }
        public string kaedah { get; set; }
        public string bantuan { get; set; }
        public string jenis_tanah { get; set; }
        public string kecerunan { get; set; }
        public string jentera { get; set; }
        public string ganoderma { get; set; }
        public double peratusan_serangan { get; set; }
        public string umr_pokok_tua { get; set; }
        public string hasil { get; set; }
        public string bil_pokok_tua { get; set; }
        public string tarikh_lawat { get; set; }
        public string ptk_lawat { get; set; }
        public double luas { get; set; }
        public string catatan { get; set; }
        public DateTime created { get; set; }
        public string createdby { get; set; }
        public string lampiran { get; set; }

        public int? newid { get; set; }
        public int? newmakkebun_id { get; set; }
        public DateTime? syncdate { get; set; }
    }
}
