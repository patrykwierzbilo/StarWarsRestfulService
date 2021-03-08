using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StartWarsRestfulService.Models
{
    [Table("characters", Schema = "public")]
    public class CharactersClass
    {
        [Key]
        public int character_id { get; set; }
        public string name { get; set; }
    }
}