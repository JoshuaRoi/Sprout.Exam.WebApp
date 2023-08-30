using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sprout.Exam.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.UnitTests
{
    public static class ContextGenerator
    {
        public static ApplicationDbContext Generate()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            OperationalStoreOptions storeOptions = new();
            IOptions<OperationalStoreOptions> operationalStoreOptions = Options.Create(storeOptions);

            return new ApplicationDbContext(optionsBuilder.Options, operationalStoreOptions);
        }
    }
}

