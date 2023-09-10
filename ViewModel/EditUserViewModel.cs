using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingStore.ViewModel
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }
        [Required]
        public String? UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        public List<string>? Roles { get; set; }
    }
}