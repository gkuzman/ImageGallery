using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.DAL.Entities
{
    public class UserVoteDBO
    {
        public string UserId { get; set; }

        public string ImageId { get; set; }

        public int Mark { get; set; }

        public ImageDBO Image { get; set; }
    }
}
