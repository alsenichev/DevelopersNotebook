using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces
{
  public interface IMainRepository
  {
    IList<Note> LoadNotes();

    void SaveNotes(IEnumerable<Note> notes);
  }
}