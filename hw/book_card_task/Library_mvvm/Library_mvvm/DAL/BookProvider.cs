using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Library_mvvm.DAL
{
    class BookProvider : IBookProvider
    {
        public Book[] LoadBooks(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Book[]));
                return (Book[])serializer.Deserialize(fs);
            }
        }

        public void SaveBooks(string path, Book[] employees)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Book[]));
                serializer.Serialize(fs, employees ?? new Book[0]);
            }
        }
    }
}
