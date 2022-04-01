using Microsoft.AspNetCore.Mvc;
using WDIPaladins.Application;
using WDIPaladins.Domain;


namespace WDIPaladins.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private IPaladinsRepository _paladinsRepository;

        public ApiController(IPaladinsRepository p)
        {
            _paladinsRepository = p;
        }





        [HttpGet("/getPaladins",Name = "GetAllPaladin")]
        public async Task<ActionResult<List<Paladin>>> GetAllPaladin()
        {
            var list = await _paladinsRepository.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("/getPaladin/{id}", Name = "GetPaladin")]
        public async Task<ActionResult<Paladin>> GetPaladin(int id)
        {
            var paladin = await _paladinsRepository.GetByIdAsync(id);
            return Ok(paladin);
        }

        [HttpPost("/addPaladin",Name = "AddPaladin")]

        public async Task<ActionResult<Paladin>> AddPaladin([FromBody]
        PaladynDto pal)
        {
            Paladin p = new Paladin()
            {
                Monastery = new Monastery() { Id = pal.MonasteryId },
                Name = pal.Name,
                Title = pal.Title,
                UniqueId = Guid.NewGuid(),
            };

            foreach (var id in pal.ItemIds)
            {
                p.Items.Add(new PaladinsItems()
                {
                    ItemId = id
                }
                );
            }

            foreach (var id in pal.SkillIds)
            {
                p.Skills.Add(new PaladinsSkills()
                {
                    SkillId = id
                }
                );
            }

            await _paladinsRepository.AddAsync(p);
            return Ok();
        }



        [HttpPost("/updatedPaladin", Name = "UpdatedPaladin")]

        public async Task<ActionResult<Paladin>> UpdatedPaladin([FromBody]
        Paladin paladin)
        {


            await _paladinsRepository.UpdateAsync(paladin);
            return Ok();
        }


        [HttpPost("/deletePaladin", Name = "DeletedPaladin")]

        public async Task<ActionResult<Paladin>> DeletedPaladin(int id)
        {
            await _paladinsRepository.DeleteAsync(id);
            return Ok();
        }


        //[HttpGet("/test1", Name = "Test1")]
        //public string Test1()
        //{
        //    return "Test1";
        //}

        //[HttpGet("/test2", Name = "Test2")]
        //public string Test2()
        //{
        //    return "Test2";
        //}
    }
}
