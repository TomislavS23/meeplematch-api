using AutoMapper;
using meeplematch_api.DTO;
using meeplematch_api.Model;
using meeplematch_api.Repository;

namespace meeplematch_api.Service;

public class EventParticipantRepository : IEventParticipantRepository
{
    private readonly IMapper _mapper;
    private readonly MeepleMatchContext _context;

    public EventParticipantRepository(IMapper mapper, MeepleMatchContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public IEnumerable<EventParticipantDTO> FindEventParticipantsByEventId(int eventId)
    {
        var result = _context.EventParticipants.FirstOrDefault(ep => ep.IdEvent == eventId);
        
        return _mapper.Map<IEnumerable<EventParticipantDTO>>(result);
    }

    public void InsertEventParticipant(EventParticipantDTO participant)
    {
        _context.EventParticipants.Add(_mapper.Map<EventParticipant>(participant));
        _context.SaveChanges();
    }

    public void RemoveEventParticipant(int eventId, int userId)
    {
        var result = _context.EventParticipants
            .FirstOrDefault(ep => ep.IdEvent == eventId && ep.IdUser == userId);
        
        _context.EventParticipants.Remove(result);
        _context.SaveChanges();
    }

    public void RemoveEventParticipants(int eventId)
    {
        _context.EventParticipants.RemoveRange(_context.EventParticipants.Where(ep => ep.IdEvent == eventId));
        _context.SaveChanges();
    }
}