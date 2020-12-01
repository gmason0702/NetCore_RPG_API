using RPG_WebAPI.Dtos.Fight;
using RPG_WebAPI.Dtos.Skill;
using RPG_WebAPI.Models;
using System.Threading.Tasks;

namespace RPG_WebAPI.Services
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);
    }
}