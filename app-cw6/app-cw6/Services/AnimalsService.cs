namespace app_cw6.Services;
using app_cw6.Models;
using app_cw6.Repositories;

public class AnimalsService : IAnimalsService
{
    private IAnimalsRepository _animalsRepository;

    public AnimalsService(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }

    public IEnumerable<Animal> GetAnimals(string query)
    {
        return _animalsRepository.GetAnimals(query);
    }

    public int CreateAnimal(Animal animal)
    {
        return _animalsRepository.CreateAnimal(animal);
    }

    public int UpdateAnimal(int idAnimal, Animal animal)
    {
        return _animalsRepository.UpdateAnimal(idAnimal, animal);
    }
    public int DeleteAnimal(int idAnimal)
    {
        return _animalsRepository.DeleteAnimal(idAnimal);
    } 
}