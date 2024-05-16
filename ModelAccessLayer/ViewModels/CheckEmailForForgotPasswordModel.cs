﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAccessLayer.ViewModels
{
    public class CheckEmailForForgotPasswordModel
    {

        [EmailAddress]
        [Required]
        public string? Email { get; set; }
    }
}
