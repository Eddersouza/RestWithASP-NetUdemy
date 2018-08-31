using RestWithASPNetUdemy.Data.Converter;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNetUdemy.Data.Converters
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public BookVO Parse(Book origin)
        {
            if (origin == null) return new BookVO();

            return new BookVO
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public List<BookVO> Parse(List<Book> origin)
        {
            if (origin == null) return new List<BookVO>();

            return origin.Select(item => Parse(item)).ToList();
        }

        public Book Parse(BookVO origin)
        {
            if (origin == null) return new Book();

            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public List<Book> Parse(List<BookVO> origin)
        {
            if (origin == null) return new List<Book>();

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}