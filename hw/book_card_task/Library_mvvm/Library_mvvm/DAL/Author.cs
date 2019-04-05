using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_mvvm.DAL
{
    public class Author
    {
        private const string InitName = "Имя Автора";

        public string Name { get; set; }

        public Author()
        {
            Name = InitName;
        }

        public Author(string name)
        {
            Name = name;
        }
    }
}
