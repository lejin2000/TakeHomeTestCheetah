using System;
using System.Collections.Generic;
using System.Text;

namespace TakeHomeTestCheetah
{
    class Recepient
    {
        public class Recipients
        {
            public IList<string> tags { get; set; }
            public string name { get; set; }
            public int id { get; set; }
        }

        public class RecipientsItemList
        {
            public IList<Recipients> recipients { get; set; }
        }
    }
}
