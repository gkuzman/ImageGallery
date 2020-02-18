﻿using System.Collections.Generic;
using System.Text;

namespace ImageGallery.Shared.Extensions
{
    public static class ErrorMessagesBuilder
    {
        public static string GetErrorMessagesFormated(this List<string> messages)
        {
            var sb = new StringBuilder();

            foreach (var message in messages)
            {
                sb.AppendLine(message);
            }

            return sb.ToString();
        }
    }
}
