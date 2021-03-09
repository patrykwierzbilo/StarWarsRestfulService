using System;
using System.Collections.Generic;
using System.Linq;
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

        public string GetCharacterNameById(int id)
        {
            var result = Charactersobj.Where(x => x.character_id == id).Select(x => x).ToList();
            if(result.Count != 0)
            {
                return result.First().name;
            }
            else
            {
                return null;
            }
        }

        public List<string> GetEpisodes(int id)
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

        public List<string> GetFriends(int id)
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

        public bool IsIdExists(int id)
        {
            return Charactersobj.Where(x => x.character_id == id).ToList().Count != 0;
        }

        public bool IsNameExists(string name)
        {
            return Charactersobj.Where(x => x.name.Equals(name)).ToList().Count != 0;
        }

        public CharactersClass AddCharacterByName(string name)
        {
            if (IsNameExists(name))
                return null;
            CharactersClass character = new CharactersClass() { name = name };
            Charactersobj.Add(character);
            SaveChanges();
            return character;
        }

        public int GetCharacterIdByName(string name)
        {
            var result = Charactersobj.Where(x => x.name.Equals(name)).Select(x => x.character_id).ToList();
            if(result.Count != 0)
            {
                return result.First();
            }
            else
            {
                return -1;
            }
        }

        public List<int> GetEpisodesIds(List<string> episodes)
        {
            return Episodessobj.
                Where(x => episodes.Contains(x.name)).
                Select(x => x.episode_id).
                ToList();
        }

        public void AddEpisodesToCharacter(List<int> episodesIds, int character_id)
        {
            foreach (var episodeId in episodesIds)
            {
                ConnectionsEpisodesClass conn = new ConnectionsEpisodesClass();
                conn.character_id = character_id;
                conn.episode_id = episodeId;
                ConnectionsEpisodessobj.Add(conn);
            }
            SaveChanges();
        }

        public List<int> GetFriendsIds(List<string> friends)
        {
            return Charactersobj.
                Where(x => friends.Contains(x.name)).
                Select(x => x.character_id).
                ToList();
        }

        public void AddFriendsToCharacter(List<int> friendsIds, int character_id)
        {
            foreach (var frinedId in friendsIds)
            {
                RelationsClass relation = new RelationsClass();
                relation.character1_id = character_id;
                relation.character2_id = frinedId;
                Relationssobj.Add(relation);
            }
            SaveChanges();
        }

        public List<ConnectionsEpisodesClass> GetConnectedEpisodes(int id)
        {
            return ConnectionsEpisodessobj.
                Where(x => x.character_id == id).
                Select(x => x).
                ToList();
        }

        public void RemoveEpisodesConnections(List<ConnectionsEpisodesClass> toRemove)
        {
            foreach (var elem in toRemove)
            {
                ConnectionsEpisodessobj.Remove(elem);
            }
            SaveChanges();
        }

        public List<RelationsClass> GetRelationsOfCharacter(int id)
        {
            return Relationssobj.
                Where(x => x.character1_id == id || x.character2_id == id).
                Select(x => x).
                ToList();
        }

        public void RemoveRelationsOfCharacter(List<RelationsClass> toRemove)
        {
            foreach (var elem in toRemove)
            {
                Relationssobj.Remove(elem);
            }
            SaveChanges();
        }

        public CharactersClass RemoveCharacter(int id)
        {
            var characterRem = GetCharacterById(id);
            if(characterRem.Count == 0)
                return null;
            var deleted = characterRem.First();
            Charactersobj.Remove(deleted);
            SaveChanges();
            return deleted;
        }

        public List<CharactersClass> GetCharacterById(int id)
        {
            return Charactersobj.Where(x => x.character_id == id).Select(x => x).ToList();
        }
    }
}