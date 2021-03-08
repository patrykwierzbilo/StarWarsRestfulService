using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartWarsRestfulService.Models
{
    [Table("connections_episodes", Schema = "public")]
    public class ConnectionsEpisodesClass
    {
        [Key]
        public int connection_id { get; set; }
        public int character_id { get; set; }
        public int episode_id { get; set; }
    }
}