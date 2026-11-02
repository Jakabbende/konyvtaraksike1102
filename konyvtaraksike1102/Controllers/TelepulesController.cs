using konyvtaraksike1102.DTOs;
using konyvtaraksike1102.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace konyvtaraksike1102.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelepulesController : ControllerBase
    {
        //dependency injection tervezési minta
        private KonyvtarakContext _context;
        public TelepulesController(KonyvtarakContext context)
        {
            _context = context;
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_context.Telepuleseks.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong" + ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                Telepulesek? talalat = _context.Telepuleseks.FirstOrDefault(i => i.Id == id);
                if (talalat == null) return NotFound($"nincs megfelelő azonosítójú {id}");
                return Ok(talalat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong" + ex.Message);
            }
        }

        [HttpGet("GetNevreszlet/{nevreszlet}")]
        public IActionResult GetByNevReszlet(string nevreszlet)
        {
            try
            {
                var res = _context.Telepuleseks.Where(t => t.TelepNev.Contains(nevreszlet));
                _context.ChangeTracker.Clear();
                if (res.Count() > 0)
                {
                    return Ok(res);
                }
                else return NotFound("Nem talált");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba" + ex.Message);
            }
        }

        [HttpGet("TelepKonyvtar")]
        public IActionResult GetAllTelepKonyv()
        {
            try
            {
                return Ok(_context.Telepuleseks.Include(t => t.Konyvtaraks).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong" + ex.Message);
            }
        }

        //adott településhez adja vissza a település nevét és az ott lévő könyvtárak nevét, név alapján
        [HttpGet("TelepuleshezKonyvtar/{nev}")]
        public IActionResult GetTelepuleshezKonyvtar(string nev)
        {
            try
            {
                List<TelepulesKonyvtaraiDTO> res = _context.Telepuleseks.Include(t => t.Konyvtaraks).Include(t => t.Megye).Where(t => t.TelepNev == nev).Select(k => new TelepulesKonyvtaraiDTO { TelepNev = nev, MegyeNev = k.Megye.MegyeNev}).ToList();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba" + ex.Message);
            }
        }

        [HttpPost("Post")]
        public IActionResult PostTelepulesek(Telepulesek ujtelep)
        {
            try
            {
                _context.Telepuleseks.Add(ujtelep);
                _context.SaveChanges();
                return Ok("Sikeres hozzáadás");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong" + ex.Message);
            }
        }

        [HttpPut("Put/{id}")]
        public IActionResult PutTelepulesek(Telepulesek uptelep, int id)
        {
            try
            {
                var toupd = _context.Telepuleseks.FirstOrDefault(u => u.Id == id);
                _context.ChangeTracker.Clear();

                if (toupd == null) return NotFound("Hiányzó település");
                _context.Telepuleseks.Update(uptelep);
                _context.SaveChanges();
                return Ok("Sikeres mentés");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong" + ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteTelepulesek(int id)
        {
            try
            {
                var todel = _context.Telepuleseks.FirstOrDefault(u => u.Id == id);
                _context.ChangeTracker.Clear();

                if (todel == null) return NotFound("Hiányzó település");

                _context.Telepuleseks.Remove(todel);
                _context.SaveChanges();
                return Ok("Sikeres Törlés");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong" + ex.Message);
            }
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var res = await _context.Telepuleseks.ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong" + ex.Message);
            }
        }

        [HttpGet("GetByIdAsync/{id}")]
        public async Task<IActionResult> GetByIdasync(int id)
        {
            try
            {
                Telepulesek? talalat = await _context.Telepuleseks.FirstOrDefaultAsync(i => i.Id == id);
                if (talalat == null) return NotFound($"nincs megfelelő azonosítójú {id}");
                return Ok(talalat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong" + ex.Message);
            }
        }

        [HttpPost("PostAsync")]
        public async Task<IActionResult> PostTelepulesekAsync(Telepulesek ujtelep)
        {
            try
            {
                await _context.Telepuleseks.AddAsync(ujtelep);
                await _context.SaveChangesAsync();
                return Ok("Sikeres hozzáadás");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong" + ex.Message);
            }
        }

        [HttpPut("PutAsync/{id}")]
        public async Task<IActionResult> PutTelepulesekAsync(Telepulesek uptelep, int id)
        {
            try
            {
                var toupd = await _context.Telepuleseks.FirstOrDefaultAsync(u => u.Id == id);
                _context.ChangeTracker.Clear();

                if (toupd == null) return NotFound("Hiányzó település");
                _context.Telepuleseks.Update(uptelep);
                await _context.SaveChangesAsync();
                return Ok("Sikeres mentés");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong" + ex.Message);
            }
        }

        [HttpDelete("DeleteAsync/{id}")]
        public async Task<IActionResult> DeleteTelepulesekAsync(int id)
        {
            try
            {
                var todel = await _context.Telepuleseks.FirstOrDefaultAsync(u => u.Id == id);
                _context.ChangeTracker.Clear();

                if (todel == null) return NotFound("Hiányzó település");

                _context.Telepuleseks.Remove(todel);
                await _context.SaveChangesAsync();
                return Ok("Sikeres Törlés");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong" + ex.Message);
            }
        }

       
    }
}
