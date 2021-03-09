using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using StartWarsRestfulService.DataContext;
using StartWarsRestfulService.Models;
using StartWarsRestfulService.Services;

namespace StartWarsRestfulService.Controllers
{
    public class CharactersDTOController : ApiController
    {
        private CharactersService charactersService;

        public CharactersDTOController()
        {
            charactersService = new CharactersService(new ApplicationDBContext());
        }

        // GET: api/Characters
        public IQueryable<CharactersDTO> GetCharactersobj()
        {
            return charactersService.GetAll().AsQueryable();
        }
        
        // GET: api/Characters/5
        [ResponseType(typeof(CharactersClass))]
        public IHttpActionResult GetCharactersClass(int id)
        {
            CharactersDTO character = charactersService.Get(id);
            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);
        }

        // PUT: api/Characters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCharactersClass(int id, CharactersDTO characterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            charactersService.DeleteById(id);
            charactersService.CreateCharacter(characterDTO);
            return StatusCode(HttpStatusCode.NoContent);
        }
        
       // POST: api/Characters
       [ResponseType(typeof(CharactersDTO))]
        public IHttpActionResult PostCharactersClass(CharactersDTO charactersClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdCharacter = charactersService.CreateCharacter(charactersClass);
            return CreatedAtRoute("DefaultApi", new { id = createdCharacter.name }, charactersClass);
        }

        // DELETE: api/Characters/5
        [ResponseType(typeof(CharactersClass))]
        public IHttpActionResult DeleteCharactersClass(int id)
        {
            CharactersClass deleted = charactersService.DeleteById(id);
            if (deleted == null)
            {
                return NotFound();
            }
            return Ok(deleted);
        }
    }
}