using EternalQuest.Models;

namespace EternalQuest.Storage;

public interface IStorage
{
    void Save(IEnumerable<Goal> goals, string path);
    List<Goal> Load(string path);
}
