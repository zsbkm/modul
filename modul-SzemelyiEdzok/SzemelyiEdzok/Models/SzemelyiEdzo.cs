using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using System;
using System.Web.Caching;

namespace SzemelyiEdzokSzemelyiEdzok.Models
{
    [TableName("Szemelyi_edzok")]
    [PrimaryKey("ID", AutoIncrement = true)]
    [Cacheable("Szemelyi_edzok", CacheItemPriority.Default, 20)]
    [Scope("ModuleId")]
    public class SzemelyiEdzo
    {
        public int ID { get; set; }
        public string Nev { get; set; }
        public DateTime Szul_ido { get; set; }
        public string Telefonszam { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Bio { get; set; }
        public string Motto { get; set; }
        public string Sportok { get; set; }
        public string Napszak { get; set; }
    }
}