using app_cw6.Models;

namespace app_cw6.Services;

public interface IAnimalsService
{
    IEnumerable<Animal> GetAnimals(string query);
    int CreateAnimal(Animal animals);
    int UpdateAnimal(int idAnimal, Animal animal);
    int DeleteAnimal(int idAnimal);
}