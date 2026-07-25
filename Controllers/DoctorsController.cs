using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase // ✅ Use ControllerBase for APIs
{
    private readonly string _connectionString;

    // ✅ Remove _supabase from this controller
    public DoctorsController(string connectionString)
    {
        _connectionString = connectionString;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDoctors()
    {
        var sql = "SELECT doctor_table_id AS Doctor_Table_Id, speciality AS Speciality FROM doctors";
        
        using var connection = new NpgsqlConnection(_connectionString);
        var doctors = await connection.QueryAsync<Doctors>(sql);

        return Ok(doctors);
    }
}


