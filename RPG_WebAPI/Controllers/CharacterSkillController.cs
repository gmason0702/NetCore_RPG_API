using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPG_WebAPI.Dtos.CharacterSkill;
using RPG_WebAPI.Services;

namespace RPG_WebAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CharacterSkillController : ControllerBase
    {
        private readonly ICharacterSkillService _characterSkillService;

        public CharacterSkillController(ICharacterSkillService characterSkillService)
        {
            _characterSkillService = characterSkillService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacterSkill(AddCharacterSkillDto newCharacterskill)
        {
            return Ok(await _characterSkillService.AddCharacterSkill(newCharacterskill));
        }
    }
}
