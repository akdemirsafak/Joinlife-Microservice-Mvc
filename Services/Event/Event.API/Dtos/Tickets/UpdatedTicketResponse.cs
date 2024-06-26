﻿namespace Event.API.Dtos.Tickets;

public class UpdatedTicketResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Guid EventId { get; set; }
}
