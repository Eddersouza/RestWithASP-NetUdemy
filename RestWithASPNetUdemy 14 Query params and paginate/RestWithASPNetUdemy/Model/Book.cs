using RestWithASPNetUdemy.Model.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace RestWithASPNetUdemy.Model
{
    public class Book : BaseEntity
    {
        public string Author { get; set; }
        public DateTime LaunchDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
    }
}