using DemoProject.Models;
using System.Collections.Generic;

namespace DemoProject.IRepository
{
    public interface IConfigurationRepository
    {
        Configuration Save(Configuration configuration);

        Configuration Get(string configurationId);

        List<Configuration> Gets();

        bool Delete(string configurationId);
    }
}
