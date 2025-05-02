using AutoMapper;
using meeplematch_api.DTO;
using meeplematch_api.Model;
using meeplematch_api.Repository;
using Microsoft.EntityFrameworkCore;

namespace meeplematch_api.Service;

public class EventCommentRepository : IEventCommentRepository
{
    private readonly IMapper _mapper;
    private readonly MeepleMatchContext _context;

    public EventCommentRepository(IMapper mapper, MeepleMatchContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public IEnumerable<EventCommentDTO> GetEventComments(int eventId)
    {
        var result = _context.EventComments.Where(ec => ec.EventId == eventId).Include(ec => ec.User).ToList();
        return _mapper.Map<IEnumerable<EventCommentDTO>>(result);
    }

    public void InsertEventComment(EventCommentDTO eventComment)
    {
        _context.EventComments.Add(_mapper.Map<EventComment>(eventComment));
        _context.SaveChanges();
    }

    public void UpdateEventComment(EventCommentDTO eventComment, int eventCommentId)
    {
        var entity = _context.EventComments.FirstOrDefault(ec => ec.IdEventComment == eventCommentId);
        var updatedEntity = _mapper.Map(eventComment, entity);
        _context.EventComments.Update(updatedEntity);
        _context.SaveChanges();
    }

    public void DeleteEventComment(int eventCommentId)
    {
        var entity = _context.EventComments.FirstOrDefault(ec => ec.IdEventComment == eventCommentId);
        _context.EventComments.Remove(entity);
        _context.SaveChanges();
    }
}