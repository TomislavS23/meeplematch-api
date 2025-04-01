using meeplematch_api.DTO;
using meeplematch_api.Model;

namespace meeplematch_api.Repository;

public interface IEventRepository
{
    IEnumerable<EventDTO> FindAll();
    EventDTO FindById(int id);
    void Save(EventDTO e);
    void Update(EventDTO e, int id);
    void Delete(int id);
}