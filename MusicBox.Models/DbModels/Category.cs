using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicBox.Models.DbModels
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Category name is required")]
        [StringLength(250, MinimumLength =3, ErrorMessage = "Category name length must be correct")]
        public string CategoryName { get; set; }
    }
}
