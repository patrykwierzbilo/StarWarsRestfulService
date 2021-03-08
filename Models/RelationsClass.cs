using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartWarsRestfulService.Models
{
    [Table("relations", Schema = "public")]
    public class RelationsClass
    {
        [Key]
        public int relation_id { get; set; }
        public int character1_id { get; set; }
        public int character2_id { get; set; }
    }
}