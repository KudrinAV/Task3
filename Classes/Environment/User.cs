using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Environment
{
    public class User : IUser
    {
        public string Name { get; private set; }
        public ITerminal Telephone { get; private set; }

        public User(string name, ITerminal terminal)
        {
            Name = name;
            Telephone = terminal;
        }
    }
}
