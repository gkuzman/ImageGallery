using System.Collections.Generic;

namespace ImageGallery.DAL.Entities
{
    public class ImageDBO
    {
        public string Id { get; set; }

        public virtual IEnumerable<UserVoteDBO> UserVotes { get; set; }
    }
}
