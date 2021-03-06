﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DataAccess.Mappings;
using DataAccess.Models;
using Domain.Interfaces;
using log4net;
using Newtonsoft.Json;
using DataAccessNote = DataAccess.Models.Note;
using DomainNote = Domain.Models.Note;

namespace DataAccess.Services
{
  public class MainRepository : IMainRepository
  {
    private const string DestinationFolder = @"c:\Users\Aleksey\Dropbox\Documents\Notes";
    private const string FileName = "Notes.json";

    private static readonly ILog logger =
      LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    private readonly string filePath = Path.Combine(DestinationFolder, FileName);
    private readonly ITimeProvider timeProvider;

    public MainRepository(ITimeProvider timeProvider)
    {
      this.timeProvider = timeProvider;
    }

    public IList<DomainNote> LoadNotes()
    {
      if (File.Exists(filePath))
      {
        string json = File.ReadAllText(filePath);
        var notes = JsonConvert.DeserializeObject<NotebookData>(json).Notes;
        return NoteMapper.Convert(notes);
      }
      return new List<DomainNote>();
    }

    public void SaveNotes(IEnumerable<DomainNote> notes)
    {
      var data = new NotebookData {Notes =  NoteMapper.Convert(notes.ToList()), TimeStamp = timeProvider.Now};
      string json = JsonConvert.SerializeObject(data);
      // for now, possible exceptions will be handled on the application-level
      File.WriteAllText(filePath, json);
    }
  }
}