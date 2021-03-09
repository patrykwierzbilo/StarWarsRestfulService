using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StartWarsRestfulService.Models
{
    [DataContract]
    public class CharactersDTO
    {
        public CharactersDTO(string name, List<string> episodes, List<string> friends)
        {
            this.name = name;
            this.episodes = episodes;
            this.friends = friends;
        }

        [DataMember]
        public string name { get; set; }
        [DataMember]
        public List<string> episodes { get; set; }
        [DataMember]
        public List<string> friends { get; set; }
    }
}