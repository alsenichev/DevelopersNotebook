using System;

namespace Domain.Interfaces
{
  public interface ITimeProvider
  {
    DateTime Now { get; }

    DateTime ThisFriday { get; }

    DateTime Tomorrow { get; }

    DateTime NextMonday { get; }
  }
}