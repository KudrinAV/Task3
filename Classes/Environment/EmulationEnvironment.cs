using Classes.TariffPlans;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Environment
{
    public class EmulationEnvironment : IEmulationEnvironment
    {
        public IAPS Aps { get; private set; }
        public List<IUser> Users { get; private set; }
        public List<ITerminal> Telephones { get; private set; }
        public ITariffPlan Cheap { get; private set; }
        public ITariffPlan Expensive { get; private set; }

        public EmulationEnvironment()
        {
            Aps = new APS();
            Users = new List<IUser>();
            Telephones = new List<ITerminal>();
            Cheap = new Cheap();
            Expensive = new Expensive();
        }

        public void CreateTerminals(int number)
        {
            for (int i = 0; i < number; i++)
            {
                Telephones.Add(new Terminal(Telephones.Count + 1));
            }
        }

        public void CreateUsers()
        {
            for (int i = 0; i < Telephones.Count; i++)
            {
                Users.Add(new User(_randomString(5), Telephones.ElementAt(i)));
            }
        }

        private string _randomString(int length)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

