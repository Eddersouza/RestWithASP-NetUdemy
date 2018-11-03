using System.Collections.Generic;
using Tapioca.HATEOAS;

namespace RestWithASPNetUdemy.Data.VO
{
    public class PersonVO : ISupportsHyperMedia
    {
        public string Address { get; set; }

        public string FirstName { get; set; }

        public string Gender { get; set; }

        public long Id { get; set; }

        public string LastName { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}