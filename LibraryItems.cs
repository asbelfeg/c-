using System;
using System.Collections.Generic;

namespace LibraryApp
{
    public abstract class LibraryItem
    {
        public string Title { get; set; }

        public LibraryItem(string title)
        {
            Title = title;
        }
    }

    public interface ISearchable
    {
        string Search();
    }

    public class Book : LibraryItem, ISearchable
    {
        public Book(string title) : base(title) { }

        public string Search()
        {
            return $"Книга {Title} — нашлась";
        }
    }

    public class Magazine : LibraryItem, ISearchable
    {
        public Magazine(string title) : base(title) { }

        public string Search()
        {
            return $"Журнал {Title} — нашёлся";
        }
    }

    public class Dvd : LibraryItem, ISearchable
    {
        public Dvd(string title) : base(title) { }

        public string Search()
        {
            return $"DVD {Title} — нашёлся";
        }
    }
}