using RPG_WebAPI.Dtos.Character;
using RPG_WebAPI.Dtos.Weapon;
using RPG_WebAPI.Models;
using System.Threading.Tasks;

namespace RPG_WebAPI.Services
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}