using ImageGallery.Services.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models
{
    public class GalleryViewModel
    {
        public List<string> ImageURLS { get; set; } = new List<string>();

        public List<SelectListItem> Marks { get; set; }

        public int Count { get; set; }

        public GalleryViewModel(GalleryLoadResponse galleryLoadResponse)
        {
            Count = galleryLoadResponse.Count;
            ImageURLS = galleryLoadResponse.ImageURLs;

            Marks = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "None"},
                new SelectListItem { Value = "1", Text = "1"},
                new SelectListItem { Value = "2", Text = "2"},
                new SelectListItem { Value = "3", Text = "3"},
                new SelectListItem { Value = "4", Text = "4"},
                new SelectListItem { Value = "5", Text = "5"},
                new SelectListItem { Value = "6", Text = "6"},
                new SelectListItem { Value = "7", Text = "7"},
                new SelectListItem { Value = "8", Text = "8"},
                new SelectListItem { Value = "9", Text = "9"},
                new SelectListItem { Value = "10", Text = "10"}
            };
        }
    }
}
