using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
  public class NotebookData
  {
    public DateTime TimeStamp { get; set; }

    public IList<Note> Notes { get; set; }
  }
}