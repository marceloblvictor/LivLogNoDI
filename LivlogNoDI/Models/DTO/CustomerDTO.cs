﻿using LivlogNoDI.Enums;

namespace LivlogNoDI.Models.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public CustomerCategory Category { get; set; }
    }
}
