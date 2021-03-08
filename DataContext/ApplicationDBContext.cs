using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using StartWarsRestfulService.Models;

namespace StartWarsRestfulService.DataContext
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext():base(nameOrConnectionString: "MyConnection")
        {

        }

        public virtual DbSet<CharactersClass> Charactersobj { get; set; }
        public virtual DbSet<EpisodesClass> Episodessobj { get; set; }
        public virtual DbSet<RelationsClass> Relationssobj { get; set; }
        public virtual DbSet<ConnectionsEpisodesClass> ConnectionsEpisodessobj { get; set; }

        public List<CharactersDTO> GetAllCharacters()
        {
            List<CharactersDTO> result = new List<CharactersDTO>();
            List<CharactersClass> characters = Charactersobj.ToList();
            foreach (var character in characters)
            {
                int id = character.character_id;
                CharactersDTO newCharacter = new CharactersDTO(character.name, GetEpisodes(id), GetFriends(id));
                result.Add(newCharacter);
            }
            return result;
        }

        public CharactersDTO Get(int id)
        {
            var result = GetCharacterNameById(id);
            if (result != null)
                return new CharactersDTO(result, GetEpisodes(id), GetFriends(id));
            else
                return null;
        }

        string GetCharacterNameById(int id)
        {
            var result = Charactersobj.First(x => x.character_id == id);
            if(result != null)
            {
                return result.name;
            }
            else
            {
                return null;
            }
        }

        List<string> GetEpisodes(int id)
        {
            List<int> episodesIds = ConnectionsEpisodessobj.
                    Where(x => x.character_id == id).
                    Select(x => x.episode_id).
                    ToList();
            List<string> episodes = Episodessobj.
                Where(x => episodesIds.Contains(x.episode_id)).
                Select(x => x.name).
                ToList();
            return episodes;
        }

        List<string> GetFriends(int id)
        {
            List<int> friendsIds = Relationssobj.
                    Where(x => x.character1_id == id).
                    Select(x => x.character2_id).
                    ToList();
            List<string> friends = Charactersobj.
                Where(x => friendsIds.Contains(x.character_id)).
                Select(x => x.name).
                ToList();
            return friends;
        }

        public CharactersDTO CreateCharacter(CharactersDTO characterDTO)
        {
            string name = characterDTO.name;
            List<string> episodes = characterDTO.episodes;
            List<string> friends = characterDTO.friends;
            if (IsNameNotExists(name))
                return null;
            CharactersClass character = new CharactersClass() { name = name };
            
            Charactersobj.Add(character);
            SaveChanges();
            int character_id = Charactersobj.Where(x => x.name.Equals(name)).Select(x => x.character_id).ToList().First();
            List<int> newEpisodes = Episodessobj.
                Where(x => episodes.Contains(x.name)).
                Select(x => x.episode_id).
                ToList();
            foreach(var episodeId in newEpisodes)
            {
                ConnectionsEpisodesClass conn = new ConnectionsEpisodesClass();
                conn.character_id = character_id;
                conn.episode_id = episodeId;
                ConnectionsEpisodessobj.Add(conn);
            }
            SaveChanges();
            List<int> newFriends = Charactersobj.
                Where(x => friends.Contains(x.name)).
                Select(x => x.character_id).
                ToList();
            foreach (var frinedId in newFriends)
            {
                RelationsClass relation = new RelationsClass();
                relation.character1_id = character_id;
                relation.character2_id = frinedId;
                Relationssobj.Add(relation);
            }
            SaveChanges();
            return characterDTO;
        }

        public void DeleteById(int id)
        {
            if (IsIdNotExists(id))
                return;
            List<ConnectionsEpisodesClass> toRemove = ConnectionsEpisodessobj.
                Where(x => x.character_id == id).
                Select(x => x).
                ToList();
            foreach(var elem in toRemove)
            {
                ConnectionsEpisodessobj.Remove(elem);
            }
            //lack of check of free episodes
            SaveChanges();
            List<RelationsClass> relRemove = Relationssobj.
                Where(x => x.character1_id == id || x.character2_id == id).
                Select(x => x).
                ToList();
            foreach (var elem in relRemove)
            {
                Relationssobj.Remove(elem);
            }
            SaveChanges();
            var characterRem = Charactersobj.Where(x => x.character_id == id).Select(x => x).ToList().First();
            Charactersobj.Remove(characterRem);
            SaveChanges();
        }

        bool IsIdNotExists(int id)
        {
            return Charactersobj.Where(x => x.character_id == id).ToList() == null;
        }

        bool IsNameNotExists(string name)
        {
            return Charactersobj.Where(x => x.name.Equals(name)).ToList() == null;
        }
    }
}