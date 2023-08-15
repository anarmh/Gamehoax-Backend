using Gamehoax_backend.Models;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Gamehoax_backend.Viewmodel
{
    public class ContactVM
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Don`t be empty")]
        public string Subject { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Don`t be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
      
        public string Message { get; set; }
        public IEnumerable<ServiceIcon> ServiceIcons { get; set; }
    }
}
