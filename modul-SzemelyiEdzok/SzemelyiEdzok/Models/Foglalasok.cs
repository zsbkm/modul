
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using System;
using System.Web.Caching;

namespace SzemelyiEdzokSzemelyiEdzok.Models
{
    [TableName("Foglalasok")]
    [PrimaryKey("ID", AutoIncrement = true)]
    [Cacheable("Foglalasok", CacheItemPriority.Default, 20)]
    [Scope("ModuleId")]
    public class Foglalasok
    {
        public int ID { get; set; }
        public int SzemelyiEdzoID { get; set; }
        public string Nev { get; set; }
        public int DNN_azonosito { get; set; }
        public string Sport { get; set; }
        public DateTime Idopont { get; set; }
        public string Megjegyzes { get; set; }
    }
}