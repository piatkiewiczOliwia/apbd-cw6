using app_cw6.Models;

namespace app_cw6.Repositories;

public interface IAnimalsRepository
{
    IEnumerable<Animal> GetAnimals(string query);
    int CreateAnimal(Animal animal);
    int UpdateAnimal(int idAnimal, Animal animal);
    int DeleteAnimal(int idAnimal);
}