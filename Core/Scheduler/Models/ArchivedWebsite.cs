using Core.Shared;

namespace Core.Scheduler
{
    public class ArchivedWebsite
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SourceUrl { get; set; }
        public ArchiveType ArchiveType { get; set; }
    }
}
