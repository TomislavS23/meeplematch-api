using meeplematch_api.DTO;
using meeplematch_api.Model;

namespace meeplematch_api.Repository;

public interface IEventParticipantRepository
{
    IEnumerable<EventParticipantDTO> FindEventParticipantsByEventId(int eventId);
    IEnumerable<EventParticipantDTO> FindEventParticipantsByUserId(int userId);
    void InsertEventParticipant(EventParticipantDTO participant);
    void RemoveEventParticipant(int eventId, int userId);
    void RemoveEventParticipants(int eventId);
}