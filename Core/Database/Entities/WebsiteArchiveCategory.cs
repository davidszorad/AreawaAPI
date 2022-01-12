namespace Core.Database.Entities;

public class WebsiteArchiveCategory
{
    public long WebsiteArchiveId { get; set; }
    public WebsiteArchive WebsiteArchive { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
}