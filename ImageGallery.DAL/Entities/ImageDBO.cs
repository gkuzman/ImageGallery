using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.DAL.Entities
{
    public class ImageDBO
    {
        public string Id { get; set; }

        public virtual IEnumerable<UserVoteDBO> UserVotes { get; set; }
    }
}
