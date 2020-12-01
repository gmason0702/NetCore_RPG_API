﻿using Microsoft.EntityFrameworkCore;
using RPG_WebAPI.Data;
using RPG_WebAPI.Dtos.Fight;
using RPG_WebAPI.Dtos.Skill;
using RPG_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_WebAPI.Services
{
    public class FightService : IFightService
    {
        private readonly ApplicationDbContext _context;

        public FightService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            ServiceResponse<AttackResultDto> response = new ServiceResponse<AttackResultDto>();
            try
            {
                Character attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                Character opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
                damage -= new Random().Next(opponent.Defense);
                if (damage>0)
                {
                    opponent.HitPoints -= damage;
                }
                if (opponent.HitPoints<=0)
                {
                    response.Message = $"{opponent.Name} has been defeated!";
                }
                _context.Characters.Update(opponent);
                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }
        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            ServiceResponse<AttackResultDto> response = new ServiceResponse<AttackResultDto>();
            try
            {
                Character attacker = await _context.Characters
                    .Include(c => c.CharacterSkills).ThenInclude(cs=>cs.Skill)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                Character opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                CharacterSkill characterSkill = attacker.CharacterSkills.FirstOrDefault(cs => cs.Skill.Id == request.SkillId);
                if (characterSkill==null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesn't have that skill.";
                    return response;
                }

                int damage = characterSkill.Skill.Damage + (new Random().Next(attacker.Intelligence));
                damage -= new Random().Next(opponent.Defense);
                if (damage > 0)
                {
                    opponent.HitPoints -= damage;
                }
                if (opponent.HitPoints <= 0)
                {
                    opponent.Defeats++;
                    response.Message = $"{opponent.Name} has been defeated!";
                }
                _context.Characters.Update(opponent);
                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }
    }
}
