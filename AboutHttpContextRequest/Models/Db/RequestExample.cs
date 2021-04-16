using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AboutHttpContextRequest.Models.Db
{
    public class RequestExample:DbContext
    {
        public RequestExample() : base("RequestExample") { }
        public DbSet<User> Users { get; set; }


    }
}