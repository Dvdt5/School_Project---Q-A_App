﻿using School_Project___Q_A_App.Models;
using System.ComponentModel.DataAnnotations;

namespace School_Project___Q_A_App.DTOs
{
    public class PostDto : BaseDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
