using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Application.Dtos.BookDtos;

public class ResultBookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string UserName { get; set; }
    public string Authors { get; set; }
    public string GoogleId { get; set; }
    public string SubTitle { get; set; }
}
