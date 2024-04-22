using Microsoft.Data.SqlClient;

namespace app_cw6.Repositories;
using app_cw6.Models;


public class AnimalsRepository : IAnimalsRepository
{
    private IConfiguration _configuration;
    private readonly string _connectionString;
    
    public AnimalsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(_connectionString))
        {
            throw new InvalidOperationException("Failed to load the database connection string.");
        }
    }

    public IEnumerable<Animal> GetAnimals(string query)
    {
        using var con = new SqlConnection(_connectionString);
        con.Open();
        
        using var cmd = new SqlCommand($"SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY {query} ASC", con);
        
        var reader = cmd.ExecuteReader();
        var animals = new List<Animal>();
        while (reader.Read())
        {
            var data = new Animal
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                Category = reader["Category"].ToString(),
                Area = reader["Area"].ToString()
            };
            animals.Add(data);
        }
        
        return animals;
    }

    public int CreateAnimal(Animal animal)
    {
        using var con = new SqlConnection(_connectionString);
        con.Open();
        
        using var cmd = new SqlCommand("INSERT INTO Animal(IdAnimal, Name, Description, Category, Area) VALUES(@IdAnimal, @Name, @Description, @Category, @Area)", con);
        cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);

        var affectedCount = cmd.ExecuteNonQuery();
        
        return affectedCount; 
    }
    
    public int UpdateAnimal(int idAnimal, Animal animal)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        
        using var cmd = new SqlCommand("UPDATE Animal SET Name=@Name, Description=@Description, Category=@Category, Area=@Area WHERE IdAnimal = @IdAnimal", con);
        cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }
    
    public int DeleteAnimal(int idAnimal)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand($"DELETE FROM Animal WHERE IdAnimal = {idAnimal}", con);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }
}