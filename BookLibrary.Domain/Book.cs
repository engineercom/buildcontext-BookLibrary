using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Domain;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    [ForeignKey("AppUser")]
    public string? UserId { get; set; }
    public AppUser? AppUser { get; set; }

    //google book api için genişletiyourz
    public string? GoogleId { get; set; }
    public string? SubTitle { get; set; }
    public string? Authors { get; set; }
}
