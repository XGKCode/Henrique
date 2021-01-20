using AutoMapper;
using Autuas.Data;
using Autuas.Dtos;
using Autuas.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;

namespace Autuas.Controllers
{
    [ApiController]
    [Route("diarylogs")]
    public class ReflectiveDiaryLogsController : ControllerBase
    {
        private readonly IReflectiveDiaryLogRepo _diaryLogRepo;
        private readonly IMapper _mapper;

        public ReflectiveDiaryLogsController(IReflectiveDiaryLogRepo diaryLogRepo, IMapper mapper)
        {
            _diaryLogRepo = diaryLogRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ReflectiveDiaryLog>> GetAllReflectiveDiaryLogs()
        {
            var diaryLogs = _diaryLogRepo.GetAllReflectiveDiaryLogs();
            var model = _mapper.Map<IEnumerable<DiaryLogReadDto>>(diaryLogs);

            return Ok(model);
        }
        [HttpGet("{id}", Name = "GetReflectiveDiaryLogById")]
        public ActionResult<ReflectiveDiaryLog> GetReflectiveDiaryLogById(int id)
        {
            var log = _diaryLogRepo.GetReflectiveDiaryLogById(id);
            if (log != null)
            {
                return Ok(_mapper.Map<DiaryLogReadDto>(log));
            }
            return NotFound();
        }
        [HttpGet("users/{userId}")]
        public ActionResult<IEnumerable<ReflectiveDiaryLog>> GetReflectiveDiaryLogsByUserId(int userId)
        {
            var logs = _diaryLogRepo.GetReflectiveDiaryLogsByUserId(userId);
            var model = _mapper.Map<IEnumerable<DiaryLogReadDto>>(logs);

            return Ok(model);
        }
        [HttpPost]
        public ActionResult<DiaryLogReadDto> CreateDiaryLog(DiaryLogCreateDto diaryLogCreateDto)
        {
            var logModel = _mapper.Map<ReflectiveDiaryLog>(diaryLogCreateDto);
            _diaryLogRepo.CreateReflectiveDiaryLog(logModel);
            _diaryLogRepo.SaveChanges();
            var logReadModel = _mapper.Map<DiaryLogReadDto>(logModel);
            return CreatedAtRoute(nameof(GetReflectiveDiaryLogById), new { Id = logReadModel.Id }, logReadModel);
        }
        [HttpPut("{id}/{userId}")]
        public ActionResult UpdateDiaryLog(int id, int userId, DiaryLogUpdateDto diaryLogUpdateDto)
        {
            var logModelFromRepo = _diaryLogRepo.GetReflectiveDiaryLogById(id);
            if (logModelFromRepo == null)
            {
                return NotFound();
            }
            else if (logModelFromRepo.UserId == userId)
            {
                _mapper.Map(diaryLogUpdateDto, logModelFromRepo);
                _diaryLogRepo.UpdateDiaryLog(logModelFromRepo);
                _diaryLogRepo.SaveChanges();

                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}/{userId}")]
        public ActionResult DeleteDiaryLog(int id, int userId)
        {
            var log = _diaryLogRepo.GetReflectiveDiaryLogById(id);
            if (log == null)
            {
                return NotFound();
            }
            else if (log.UserId == userId)
            {
                _diaryLogRepo.DeleteDiaryLog(id);
                _diaryLogRepo.SaveChanges();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPatch("{id}")]
        public ActionResult PartialDiaryLogUpdate(int id, JsonPatchDocument<DiaryLogUpdateDto> patchDoc)
        {
            var logModelFromRepo = _diaryLogRepo.GetReflectiveDiaryLogById(id);
            if (logModelFromRepo == null)
            {
                return NotFound();
            }
            var logToPatch = _mapper.Map<DiaryLogUpdateDto>(logModelFromRepo);
            patchDoc.ApplyTo(logToPatch, ModelState);
            //validate check
            if (!TryValidateModel(logToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(logToPatch, logModelFromRepo);
            _diaryLogRepo.UpdateDiaryLog(logModelFromRepo);
            _diaryLogRepo.SaveChanges();
            return NoContent();

        }

    }
}
