using System;
using System.Collections.Generic;
using System.Text;

namespace RestApiLeseTests.WebObjects
{
    public class En
    {
        public string ItemName { get; set; }
        public string Id { get; set; }
        public string UrlName { get; set; }
    }

    public class Items
    {
        public IList<En> En { get; set; }
    }

    public class Payload
    {
        public Items Items { get; set; }
    }

    public class ItemBase
    {
        public Payload Payload { get; set; }
    }
}
