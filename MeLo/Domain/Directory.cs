using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeLo.Domain
{
    public class Directory
    {

        public int id { get; set; }
        public string path { get; set; }
        public string name { get; set; }
        public Directory(string path, string name)
        {
            this.path = path;
            this.name = name;
        }
        public Directory(int id, string path, string name)
        {
            this.id = id;
            this.path = path;
            this.name = name;
        }

        public Directory()
        {
        }
    }
}
