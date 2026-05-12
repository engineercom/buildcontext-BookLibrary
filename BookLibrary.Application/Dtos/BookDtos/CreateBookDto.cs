using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Application.Dtos.BookDtos;

public class CreateBookDto
{
    public string Title { get; set; }
    public string Description { get; set; }

    public string SubTitle { get; set; }
    public string GoogleId { get; set; }
    public string Authors { get; set; }

}
