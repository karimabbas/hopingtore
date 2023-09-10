using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingStore.Models
{
    public class BlogTag
    {
        public int Id { get; set; }

        [ForeignKey("Blog")]
        public int Blog_Id { get; set; }
        public Blog? Blog { get; set; }

        [ForeignKey("Tag")]
        public int Tag_Id { get; set; }
        public Tag? Tag { get; set; }


    }
}