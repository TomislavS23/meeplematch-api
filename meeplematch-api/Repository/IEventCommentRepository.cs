using meeplematch_api.DTO;

namespace meeplematch_api.Repository;

public interface IEventCommentRepository
{
    IEnumerable<EventCommentDTO> GetEventComments(int eventId);
    void InsertEventComment(EventCommentDTO eventComment);
    void UpdateEventComment(EventCommentDTO eventComment, int eventCommentId);
    void DeleteEventComment(int eventCommentId);
}