using AutoMapper;
using meeplematch_api.DTO;
using meeplematch_api.Model;
using meeplematch_api.Repository;

namespace meeplematch_api.Service;

public class EventRepository : IEventRepository
{
    private readonly IMapper _mapper;
    private readonly MeepleMatchContext _context;

    public EventRepository(IMapper mapper, MeepleMatchContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public IEnumerable<EventDTO> FindAll()
    {
        return _mapper.Map<IEnumerable<EventDTO>>(_context.Events);
    }

    public EventDTO FindById(int id)
    {
        var entity = _context.Events.FirstOrDefault(e => e.IdEvent == id);
        return _mapper.Map<EventDTO>(entity);
    }

    public void Save(EventDTO e)
    {
        _context.Events.Add(_mapper.Map<Event>(e));
        _context.SaveChanges();
    }

    public void Update(EventDTO e, int id)
    {
        var entity = _context.Events.FirstOrDefault(ev => ev.IdEvent == id);
        entity.Name = e.Name;
        entity.Location = e.Location;
        entity.Type = e.Type;
        entity.Game = e.Game;
        entity.Location = e.Location;
        entity.EventDate = e.EventDate;
        entity.Capacity = e.Capacity;
        entity.MinParticipants = e.MinParticipants;
        entity.UpdatedAt = e.UpdatedAt;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = _context.Events.FirstOrDefault(e => e.IdEvent == id);
        _context.Events.Remove(entity);
        _context.SaveChanges();
    }
}