﻿using SharedLib.Common;

namespace Event.API.Entities;

public class Eventy
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public EventTypeEnum Type { get; set; }
    public Guid VenueId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public EventStatusEnum Statu { get; set; }
    public virtual ICollection<Tickety> Tickets { get; set; }


    public Eventy()
    {
        Id = Guid.NewGuid();
        Tickets = new List<Tickety>();
    }
}

