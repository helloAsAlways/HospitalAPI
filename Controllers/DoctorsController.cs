using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebApplication2.Models;



namespace WebApplication2.Controllers;

public class CreateDoctorRequest
{
    public string Name { get; set; } = "";
    public DateTime? Date_Of_Birth { get; set; }
    public string Contact_Number { get; set; } = "";
    public string speciality { get; set; } = "";
}
[ApiController]
[Route("api/[controller]")]

public class DoctorsController : ControllerBase // ✅ Use ControllerBase for APIs
{
    private readonly string _connectionString;
    private readonly Supabase.Client _supabase;
    // ✅ Remove _supabase from this controller
    public DoctorsController( Supabase.Client supabase, string connectionString)
    {
        _connectionString = connectionString;
        _supabase = supabase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorRequest req)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        // Step 1: Insert into persons and RETURN the generated ID
        var personSql = @"
        INSERT INTO persons (name, date_of_birth, contact_number) 
        VALUES (@Name, @Date_Of_Birth, @Contact_Number)
        RETURNING id";

        var personId = await conn.ExecuteScalarAsync<long>(personSql, req);

        // Step 2: Insert into doctors using the generated ID
        var doctorSql = @"
        INSERT INTO doctors (doctor_table_id, speciality) 
        VALUES (@PersonId, @speciality)
        ";

        await conn.ExecuteAsync(
            doctorSql, 
            new { PersonId = personId, req.speciality }
        );

        return Created(
            $"/api/doctors/{personId}",
            new { Id = personId, req.Name, req.speciality }
        );
    }
}


