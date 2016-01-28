using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL.SignUp
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Physician
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class Wrapper
    {
        public List<Physician> Rows { get; set; }
    }
}
