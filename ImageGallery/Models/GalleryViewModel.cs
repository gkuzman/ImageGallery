using ImageGallery.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models
{
    public class GalleryViewModel
    {
        public List<string> ImageURLS { get; set; } = new List<string>();

        public int Count { get; set; }

        public GalleryViewModel(GalleryLoadResponse galleryLoadResponse)
        {
            Count = galleryLoadResponse.Count;
            //ImageURLS = galleryLoadResponse.ImageIds;

            foreach (var img in galleryLoadResponse.ImageIds)
            {
                ImageURLS.Add("https://businessagency.thehague.com/app/uploads/2019/04/iStock-466866544-1920x490.jpg");
                ImageURLS.Add("https://img.huffingtonpost.com/asset/5dcc613f1f00009304dee539.jpeg?cache=QaTFuOj2IM&ops=crop_834_777_4651_2994%2Cscalefit_720_noupscale");
            }
        }
    }
}
