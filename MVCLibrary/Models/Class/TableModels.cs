using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLibrary.Models.Entity;

namespace MVCLibrary.Models.Class
{
    public class TableModels
    {
        public IEnumerable<Books> bookModel { get; set; }
        public IEnumerable<About> aboutModel { get; set; }
    }
}