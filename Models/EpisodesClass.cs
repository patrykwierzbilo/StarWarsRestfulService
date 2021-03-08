using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartWarsRestfulService.Models
{
    [Table("episodes", Schema = "public")]
    public class EpisodesClass
    {
        [Key]
        public int episode_id { get; set; }
        public string name { get; set; }
    }
}