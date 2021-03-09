using StartWarsRestfulService.DataContext;
using StartWarsRestfulService.Models;
using System.Collections.Generic;
using System.Linq;

namespace StartWarsRestfulService.Services
{
    public class CharactersService
    {
        private ApplicationDBContext db;

        public CharactersService(ApplicationDBContext db)
        {
            this.db = db;
        }

        public List<CharactersDTO> GetAll()
        {
            List<CharactersDTO> result = new List<CharactersDTO>();
            List<CharactersClass> characters = db.Charactersobj.ToList();
            foreach (var character in characters)
            {
                int id = character.character_id;
                CharactersDTO newCharacter = new CharactersDTO(character.name, db.GetEpisodes(id), db.GetFriends(id));
                result.Add(newCharacter);
            }
            return result;
        }

        public CharactersDTO Get(int id)
        {
            var result = db.GetCharacterNameById(id);
            if (result != null)
                return new CharactersDTO(result, db.GetEpisodes(id), db.GetFriends(id));
            else
                return null;
        }

        public CharactersDTO CreateCharacter(CharactersDTO characterDTO)
        {
            string name = characterDTO.name;
            List<string> episodes = characterDTO.episodes;
            List<string> friends = characterDTO.friends;

            var additionResult = db.AddCharacterByName(name);
            if (additionResult == null)
                return null;

            int character_id = db.GetCharacterIdByName(name);

            List<int> newEpisodes = db.GetEpisodesIds(episodes);
            db.AddEpisodesToCharacter(newEpisodes, character_id);

            List<int> newFriends = db.GetFriendsIds(friends);
            db.AddFriendsToCharacter(newFriends, character_id);

            return characterDTO;
        }

        public CharactersClass DeleteById(int id)
        {
            if (!db.IsIdExists(id))
                return null;

            List<ConnectionsEpisodesClass> toRemove = db.GetConnectedEpisodes(id);
            db.RemoveEpisodesConnections(toRemove);

            List<RelationsClass> relRemove = db.GetRelationsOfCharacter(id);
            db.RemoveRelationsOfCharacter(relRemove);

            var deleted = db.RemoveCharacter(id);
            return deleted;
        }
    }
}