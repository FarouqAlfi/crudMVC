using System;

using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CrudMVC.Models
{
    public class DB :DbContext

    {
        public DB() : base("DefaultConnection")
        {
            Database.Log = m => System.Diagnostics.Debug.WriteLine(m);
        }
        public virtual IDbSet<Product> Products { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Types()
            .Configure(c => c.ToTable(GetTableName(c.ClrType.Name), "public"));
            modelBuilder.Conventions.Add<CustomKeyConvention>();
        }

        public static string GetTableName(String typeName)
        {
            var pluralizationService = (IPluralizationService)
                DbConfiguration.DependencyResolver.GetService(typeof(IPluralizationService), "plural");

            var result = pluralizationService.Pluralize(typeName);

            result = Regex.Replace(result, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);

            return result.ToLower();
        }

        public static string GetColumnName(String typeName)
        {
            var result = typeName;

            result = Regex.Replace(result, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);

            return result.ToLower();
        }

        public class CustomKeyConvention : Convention
        {
            public CustomKeyConvention()
            {
                Properties()
                    .Configure(config => config.HasColumnName(DB.GetColumnName(config.ClrPropertyInfo.Name)));
            }
        }
    }
}