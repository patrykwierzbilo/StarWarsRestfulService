using StartWarsRestfulService.DataContext;
using StartWarsRestfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            return db.GetAllCharacters();
        }

        public CharactersDTO Get(int id)
        {
            var character = db.Get(id);
            if (character != null)
                return character;
            else
                return null;
        }

        public void CreateCharacter(CharactersDTO characterDTO)
        {
            db.CreateCharacter(characterDTO);
        }

        public void DeleteCharacter(int id)
        {
            db.DeleteById(id);
        }
    }
}