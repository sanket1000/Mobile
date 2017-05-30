using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.ConfigSvc;

namespace Core.API.ConfigSvc.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlConfig = new ConfigSvc.Config().configuration();

            string xmlHeader = new ConfigSvc.Config().headerconfiguration();

            string xmlDistribution = new ConfigSvc.Config().distributionconfiguration();
        }
    }
}
