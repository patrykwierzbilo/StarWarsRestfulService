using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StartWarsRestfulService.Models
{
    [Table("characters", Schema ="public")]
    public class CharactersClass
    {
        [Key]
        public int id { get; set; }
        public string episodes { get; set; }
        public string friends { get; set; }
        public string planet { get; set; }
    }
}