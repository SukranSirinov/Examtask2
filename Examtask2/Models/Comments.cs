using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examtask2.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Raiting { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageUrl { get; set; }
    }
}
