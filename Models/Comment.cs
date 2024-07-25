﻿using api.Models;
using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Comment
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int? StockId { get; set; }
    public Stock? Stock { get; set; }
    public string AppUserId { get; set; }
   // public AppUser AppUser { get; set; }
}
