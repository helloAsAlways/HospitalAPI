using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Supabase;
using WebApplication2.Models;

[ApiController]
[Route("api/[controller]")]

namespace WebApplication2.Controllers;

public class DoctorsController: Controller
{
    private readonly Supabase.Client _supabase;
    private readonly string _connectionString;

    public DoctorsController(Supabase.Client supabase, string connectionString)
    {
        _supabase = supabase;
        _connectionString = connectionString;
    }
}