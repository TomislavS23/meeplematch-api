﻿namespace meeplematch_api.DTO
{
    public class CreateUserDTO
    {
        public string? Username { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public int? RoleId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImagePath { get; set; }
        public bool? IsMale { get; set; }
    }
}
