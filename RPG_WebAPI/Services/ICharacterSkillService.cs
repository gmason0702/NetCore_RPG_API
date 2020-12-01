using RPG_WebAPI.Dtos.Character;
using RPG_WebAPI.Dtos.CharacterSkill;
using RPG_WebAPI.Models;
using System.Threading.Tasks;

namespace RPG_WebAPI.Services
{
    public interface ICharacterSkillService
    {
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto characterSkill);
    }
}