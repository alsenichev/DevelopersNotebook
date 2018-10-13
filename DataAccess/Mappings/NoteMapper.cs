using System.Collections.Generic;
using System.Linq;
using DomainNote = Domain.Models.Note;
using DataAccessNote = DataAccess.Models.Note;

namespace DataAccess.Mappings
{
  public static class NoteMapper
  {
    private static DataAccessNote Convert(DomainNote domainNote)
    {
      return new DataAccessNote
      {
        Header = domainNote.Header,
        Text = domainNote.Text,
        Duration = domainNote.Duration,
        StartedAt = domainNote.StartedAt,
        State = domainNote.State
      };
    }

    private static DomainNote Convert(DataAccessNote dataAccessNote)
    {
      return new DomainNote(
        dataAccessNote.Text,
        dataAccessNote.Header,
        dataAccessNote.State,
        dataAccessNote.StartedAt,
        dataAccessNote.Duration);
    }

    public static IList<DataAccessNote> Convert(IList<DomainNote> domainNotes)
    {
      return domainNotes.Select(n => Convert(n)).ToList();
    }

    public static IList<DomainNote> Convert(IList<DataAccessNote> dataAccessNote)
    {
      return dataAccessNote.Select(n => Convert(n)).ToList();
    }
  }
}