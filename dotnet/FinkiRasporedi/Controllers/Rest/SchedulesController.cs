﻿using FinkiRasporedi.Data;
using FinkiRasporedi.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinkiRasporedi.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedule()
        {
            if (_context.Schedule == null)
            {
                return NotFound();
            }
            return await _context.Schedule.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Schedule>> AddLectureToSchedule(int scheduleId, [FromBody] int lectureId)
        {
            var schedule = await _context.Schedule.FindAsync(scheduleId);

            if (schedule == null)
            {
                return NotFound("Schedule not found");
            }

            var lecture = await _context.Lectures.FindAsync(lectureId);

            if (lecture == null)
            {
                return NotFound("Lecture not found");
            }

            schedule.Lectures.Add(lecture);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchedule", new { id = schedule.Id }, schedule);
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            if (_context.Schedule == null)
            {
                return NotFound();
            }
            var schedule = await _context.Schedule.FindAsync(id);

            if (schedule == null)
            {
                return NotFound();
            }

            return schedule;
        }

        // PUT: api/Schedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Schedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
            if (_context.Schedule == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Schedule'  is null.");
            }
            _context.Schedule.Add(schedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchedule", new { id = schedule.Id }, schedule);
        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            if (_context.Schedule == null)
            {
                return NotFound();
            }
            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedule.Remove(schedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScheduleExists(int id)
        {
            return (_context.Schedule?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}