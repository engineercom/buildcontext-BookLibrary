namespace BookLibrary.API.Models;

public class BookResponse
{
    public string Kind { get; set; }
    public List<BookItem> Items { get; set; }
}
public class BookItem {

    public string Id { get; set; }
    public  string SelfLink { get; set; }

    public VolumeInfo VolumeInfo  { get; set; }

}
public class VolumeInfo {

    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Description { get; set; }
    public List<string> Authors { get; set; }


}

