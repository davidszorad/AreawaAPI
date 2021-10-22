using Core.Shared;
using System;

namespace Core.Reader
{
    public class ArchivedWebsite : BaseItemResult
    {
        public Guid PublicId { get; set; }
        public string ShortId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Uri SourceUrl { get; set; }
        public Uri ArchiveUrl { get; set; }
        public ArchiveType ArchiveType { get; set; }
    }
}
