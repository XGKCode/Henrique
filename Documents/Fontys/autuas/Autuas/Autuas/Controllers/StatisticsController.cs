using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Autuas.Data;
using Autuas.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autuas.Controllers
{
    [Route("statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IGoalRepo _goalRepo;
        private readonly IReflectiveDiaryLogRepo _diaryRepo;
        private readonly IDailyCheckInRepo _dailyCheckInRepo;
        private readonly IMapper _mapper;

        public StatisticsController(IGoalRepo goalRepo, IReflectiveDiaryLogRepo diaryRepo, IDailyCheckInRepo dailyCheckInRepo, IMapper mapper)
        {
            _goalRepo = goalRepo;
            _diaryRepo = diaryRepo;
            _dailyCheckInRepo = dailyCheckInRepo;
            _mapper = mapper;
        }
        [HttpGet("allgoals/{userId}")]
        public IActionResult GetNumberOfAllGoals(int userId)
        {
            var numOfAllGoals = _goalRepo.GetCountOfAllGoals(userId);
            return Ok(numOfAllGoals);
        }
        [HttpGet("completedgoals/{userId}")]
        public IActionResult GetNumberOfCompletedGoals(int userId)
        {
            var numOfCompletedGoals = _goalRepo.GetCompletedGoalsCount(userId);
            return Ok(numOfCompletedGoals);
        }
        [HttpGet("incompletedgoals/{userId}")]
        public IActionResult GetNumberOfIncompletedGoals(int userId)
        {
            var numOfIncompletedGoals = _goalRepo.GetIncompletedGoalsCount(userId);
            return Ok(numOfIncompletedGoals);
        }
        [HttpGet("positivefeelings/{userId}")]
        public IActionResult GetPositiveFeelingsCount(int userId)
        {
            var numOfPositiveFeelings = _dailyCheckInRepo.GetPositiveFeelingsCount(userId);
            return Ok(numOfPositiveFeelings);
        }
        [HttpGet("negativefeelings/{userId}")]
        public IActionResult GetNegativeFeelingsCount(int userId)
        {
            var numOfNegativeFeelings = _dailyCheckInRepo.GetNegativeFeelingsCount(userId);
            return Ok(numOfNegativeFeelings);
        }

    }
}
