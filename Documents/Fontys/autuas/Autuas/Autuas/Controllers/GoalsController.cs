using AutoMapper;
using Autuas.Data;
using Autuas.Dtos;
using Autuas.Helpers;
using Autuas.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Autuas.Controllers
{
    [Route("goals")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly IGoalRepo _goalRepo;
        private readonly IMapper _mapper;

        public GoalsController(IGoalRepo goalRepo, IMapper mapper)
        {
            _goalRepo = goalRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GoalReadDto>> GetAllGoals()
        {
            var goals = _goalRepo.GetAllGoals();
            var model = _mapper.Map<IEnumerable<GoalReadDto>>(goals);

            return Ok(model);
        }
        [HttpGet("{id}", Name = "GetGoalsById")]
        public ActionResult<GoalReadDto> GetGoalById(int id)
        {
            var goal = _goalRepo.GetGoalById(id);
            if (goal != null)
            {
                return Ok(_mapper.Map<GoalReadDto>(goal));
            }
            return NotFound();
        }
        [HttpGet("completed/{completed}")]
        public ActionResult<IEnumerable<GoalReadDto>> GetGoalsByCompletion(bool completed)
        {
            var goals = _goalRepo.GetAllGoalsByCompletion(completed);
            var model = _mapper.Map<IEnumerable<GoalReadDto>>(goals);

            return Ok(model);
        }
        [HttpGet("goaltype/{goalType}")]
        public ActionResult<IEnumerable<GoalReadDto>> GetGoalsByGoalType(string goalType)
        {
            var goals = _goalRepo.GetAllGoalsByType(goalType);
            var model = _mapper.Map<IEnumerable<GoalReadDto>>(goals);

            return Ok(model);
        }
        [HttpGet("users/{userId}", Name = "GetGoalsByUserId")]
        public ActionResult<IEnumerable<GoalReadDto>> GetGoalsByUserId(int userId)
        {
            var goals = _goalRepo.GetAllGoalsById(userId);
            var model = _mapper.Map<IEnumerable<GoalReadDto>>(goals);

            return Ok(model);
        }
        [HttpPost]
        public ActionResult<GoalReadDto> CreateGoal(GoalCreateDto goalCreateDto)
        {
            var goalModel = _mapper.Map<Goal>(goalCreateDto);
            _goalRepo.CreateGoal(goalModel);
            _goalRepo.SaveChanges();

            var goalReadDto = _mapper.Map<GoalReadDto>(goalModel);

            return CreatedAtAction(nameof(GetGoalById), new { id = goalReadDto.Id }, goalReadDto);
        }


        [HttpDelete("{id}/{userId}")]
        public ActionResult DeleteGoal(int id, int userId)
        {
            var goalModelFromRepo = _goalRepo.GetGoalById(id);
            if (goalModelFromRepo == null)
            {
                return NotFound();
            }
            else if (goalModelFromRepo.UserId == userId)
            {
                _goalRepo.DeleteGoal(id);
                _goalRepo.SaveChanges();
                return NoContent();
            }
            return BadRequest();
        }
        [HttpPut("{id}/{userId}")]
        public ActionResult<GoalReadDto> UpdateGoal(int id, int userId, GoalUpdateDto goalUpdateDto)
        {
            var goalModelFromRepo = _goalRepo.GetGoalById(id);
            if (goalModelFromRepo == null)
            {
                return NotFound();
            }
            else if (goalModelFromRepo.UserId == userId)
            {
                // maps the goal objects with the new values to the goal object that has been created, and updates the new changes
                _mapper.Map(goalUpdateDto, goalModelFromRepo);
                _goalRepo.UpdateGoal(goalModelFromRepo);
                _goalRepo.SaveChanges();
                var goalReadDto = _mapper.Map<GoalReadDto>(goalModelFromRepo);

                return CreatedAtAction(nameof(GetGoalById), new { id = goalReadDto.Id }, goalReadDto);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPatch("{id}")]
        public ActionResult PartialGoalUpdate(int id, JsonPatchDocument<GoalUpdateDto> patchDoc)
        {
            var goalModelFromRepo = _goalRepo.GetGoalById(id);
            if (goalModelFromRepo == null)
            {
                return NotFound();
            }
            var goalToPatch = _mapper.Map<GoalUpdateDto>(goalModelFromRepo);
            patchDoc.ApplyTo(goalToPatch, ModelState);

            //validate check
            if (!TryValidateModel(goalToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(goalToPatch, goalModelFromRepo);
            _goalRepo.UpdateGoal(goalModelFromRepo);
            _goalRepo.SaveChanges();
            //204 - for successful changes
            return NoContent();
        }


    }
}
