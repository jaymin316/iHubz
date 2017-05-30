using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using iHubz.Domain.MainModule.CompanyEntities;
using iHubz.Infrastructure.CrossCutting.Settings;
using iHubz.Infrastructure.Data.Core;
using iHubz.Infrastructure.Data.MainModule.UnitOfWork.Mapping;

namespace iHubz.Infrastructure.Data.MainModule.UnitOfWork
{
    public class MainModuleUnitOfWork : DbContext, IQueryableUnitOfWork
    {
        public MainModuleUnitOfWork()
            : base("Name=connectionString.SqlConnection")
        {
            // don't modify existing database
            Database.SetInitializer<MainModuleUnitOfWork>(null);

            // this is required to force EntityFramework.SqlServer.dll to be included
            // ReSharper disable once UnusedVariable
            var sql = typeof(SqlProviderServices);

            // set timeout
            var timeout = SettingsHelper.DBCommandTimeout;
            if (timeout != null)
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = SettingsHelper.DBCommandTimeout;
        }

        #region DbContext Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove unused conventions
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Add entity configurations 
            modelBuilder.Configurations.Add(new CompaniesMap());
            modelBuilder.Configurations.Add(new CompanyPropertiesMap());
            modelBuilder.Configurations.Add(new StatesMap());
        }

        #endregion

        #region IDbSet Members

        public DbSet<Companies> Companies { get; set; }
        #endregion

        #region IQueryableUnitOfWork Members

        public DbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity item)
            where TEntity : class
        {
            //attach and set as unchanged
            Entry(item).State = EntityState.Unchanged;
        }

        public void Detach<TEntity>(TEntity item)
            where TEntity : class
        {
            //detach from context
            Entry(item).State = EntityState.Detached;
        }


        public void Remove<TEntity>(TEntity item)
            where TEntity : class
        {
            //Remove and set as Deleted
            Entry(item).State = EntityState.Deleted;
        }

        public void SetModified<TEntity>(TEntity item)
            where TEntity : class
        {
            //this operation also attach item in object state manager
            Entry(item).State = EntityState.Modified;
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
            where TEntity : class
        {
            //if it is not attached, attach original and set current values
            Entry(original).CurrentValues.SetValues(current);
        }

        public void ExecuteStoredProcNonQuery(string storedProcedureName, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public void Commit()
        {
            SaveChanges(this);
            //base.SaveChanges();
        }

        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb, ex
                    ); // Add the original exception as the innerException
            }
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                        .ForEach(entry => { entry.OriginalValues.SetValues(entry.GetDatabaseValues()); });
                }
            } while (saveFailed);
        }

        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            ChangeTracker.Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            DbEntityEntry entityEntry = Entry(entity);
            if (entityEntry.State != EntityState.Deleted)
            {
                entityEntry.State = EntityState.Deleted;
            }
            else
            {
                Attach(entity);
                Remove(entity);
            }

            Commit();
        }

        #endregion


    }
}
