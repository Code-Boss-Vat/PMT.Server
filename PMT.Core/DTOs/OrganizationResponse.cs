﻿
namespace PMT.Core.DTOs;

public class OrganizationResponse
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }
    public Guid? AdminId { get; set; }
}
