using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Visit
{
    public class RegisterExitDto
    {
        public Guid? VisitId { get; set; }
        public string? Code { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
