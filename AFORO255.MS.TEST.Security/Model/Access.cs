using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Security.Model
{
    [Table("Access")]
    public class Access
    {
        [Key]
        [Column("id_user")]
        public int Id { get; set; }
        public string Username  { get; set; }
        public string Password { get; set; }
    }
}
