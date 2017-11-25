using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableApps.Repo
{
    public class VariableSetting
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public int? CanModify { get; set; }
        public bool IsEncrypt { get; set; }
    }
}
