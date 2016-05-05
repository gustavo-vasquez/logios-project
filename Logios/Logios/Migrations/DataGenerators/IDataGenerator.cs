using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logios.Migrations.DataGenerators
{
    public interface IDataGenerator
    {
        void GenerateData(ApplicationDbContext context);
    }
}
