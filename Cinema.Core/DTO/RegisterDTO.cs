﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Username can't be blank")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Address can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in proper email address format")]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already in use")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Repeat Password can't be blank")]
        [Compare(nameof(Password), ErrorMessage = "Password and Repeat Password do not match")]
        public string RepeatPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number can't be blank")]
        [Phone(ErrorMessage = "Phone Number should be in proper phone number format")]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already in use")]
        public string PhoneNumber { get; set; } = string.Empty;

    }
}