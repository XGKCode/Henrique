using AutoMapper;
using Autuas.Data;
using Autuas.Dtos;
using Autuas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Controllers
{
    [Route("dailycheckin")]
    [ApiController]

    public class DailyCheckInsController : ControllerBase
    {
        private readonly IDailyCheckInRepo _checkInRepo;
        private readonly IMapper _mapper;

        public DailyCheckInsController(IDailyCheckInRepo checkInRepo, IMapper mapper)
        {
            _checkInRepo = checkInRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<DailyCheckInReadDto>> GetAllCheckIns()
        {
            var checkIns = _checkInRepo.GetAllDailyCheckIns();
            var model = _mapper.Map<IEnumerable<DailyCheckInReadDto>>(checkIns);

            return Ok(model);
        }

        [HttpGet("{id}", Name = "GetDailyCheckInById")]
        public ActionResult<DailyCheckInReadDto> GetDailyCheckInById(int id)
        {
            var checkIn = _checkInRepo.GetDailyCheckInById(id);
            if (checkIn != null)
            {
                return Ok(_mapper.Map<DailyCheckInReadDto>(checkIn));
            }
            return NotFound();
        }
        [HttpGet("all/{date}")]
        public ActionResult<IEnumerable<DailyCheckInReadDto>> GetDailyCheckInsByDate(DateTime date)
        {
            var checkIns = _checkInRepo.GetDailyCheckInsByDate(date);
            var model = _mapper.Map<IEnumerable<DailyCheckInReadDto>>(checkIns);

            return Ok(model);
        }
        [HttpGet("users/{userId}")]
        public ActionResult<IEnumerable<DailyCheckInReadDto>> GetDailyCheckInsByUserId(int userId)
        {
            var checkIns = _checkInRepo.GetDailyCheckInsByUserID(userId);
            var model = _mapper.Map<IEnumerable<DailyCheckInReadDto>>(checkIns);

            return Ok(model);
        }

        //Post [controller]
        [HttpPost]
        public ActionResult<DailyCheckInReadDto> CreateDailyCheckIn(DailyCheckInCreateDto dailyCheckInCreateDto)
        {
            var checkInModel = _mapper.Map<DailyCheckIn>(dailyCheckInCreateDto);
            _checkInRepo.CreateDailyCheckIn(checkInModel);
            _checkInRepo.SaveChanges();

            var checkInReadDto = _mapper.Map<DailyCheckInReadDto>(checkInModel);

            return CreatedAtRoute(nameof(GetDailyCheckInById), new { Id = checkInReadDto.ID }, checkInReadDto);

        }
        //Delete [controller]/{id}
        [HttpDelete("{id}/{userId}")]
        public ActionResult DeleteCheckIn(int id, int userId)
        {
            var checkInModelFromRepo = _checkInRepo.GetDailyCheckInById(id);
            if (checkInModelFromRepo == null)
            {
                return NotFound();
            }
            else if (checkInModelFromRepo.UserID == userId)
            {
                _checkInRepo.DeleteDailyCheckInById(id);
                _checkInRepo.SaveChanges();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }

        }



    }
}
