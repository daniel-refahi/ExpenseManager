using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpenseManger.Repository
{
    public interface IDisposedTracker
    {
        bool IsDisposed { get; set; }
    }

    public abstract class RepositoryBase<TContext> : IDisposable 
        where TContext : DbContext, IDisposedTracker, new()
    {
        private TContext _DataContext;

        protected virtual TContext DataContext 
        {
            get
            {
                if (_DataContext == null || _DataContext.IsDisposed)
                {
                    _DataContext = new TContext();
                    //See http://msdn.microsoft.com/en-us/library/dd456853.aspx for details on this property and what it does
                    //Disable proxy creation to allow serialization and prevent 
                    //the "In order to serialize the parameter, add the type to the known types collection for the operation using ServiceKnownTypeAttribute" error
                    AllowSerialization = true;
                }
                return _DataContext;
            }
        }

        protected virtual bool AllowSerialization
        {
            get
            {
                return _DataContext.Configuration.ProxyCreationEnabled;
            }
            set
            {
                _DataContext.Configuration.ProxyCreationEnabled = !value;
            }
        }

        protected virtual T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (predicate != null)
            {
                return DataContext.Set<T>().Single(predicate);
            }
            else
            {
                throw new ApplicationException("Predicate value must be passed to Get<T>.");
            }
        }

        protected virtual List<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            try
            {

                if (predicate != null)
                {
                    return DataContext.Set<T>().Where(predicate).ToList();
                }
                else
                {
                    DataContext.Set<T>().ToList();
                }

            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        protected virtual List<T> GetList<T, TKey>(Expression<Func<T, bool>> predicate,Expression<Func<T, TKey>> orderBy) where T : class
        {
            try
            {
                if (predicate != null)
                {
                    return DataContext.Set<T>().Where(predicate).OrderBy(orderBy).ToList();
                }
                else
                {
                    DataContext.Set<T>().ToList();
                }
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        protected virtual OperationStatus Add<T>(T record) where T : class
        {
            OperationStatus opStatus = new OperationStatus { Status = true };
            try
            {
                DataContext.Set<T>().Add(record);
            }
            catch (Exception ex)
            {
                opStatus = OperationStatus.CreateFromSystemException($"Error adding {typeof(T)}.", ex);
            }
            return opStatus;
        }

        protected virtual OperationStatus Add<T>(List<T> records) where T : class
        {
            OperationStatus opStatus = new OperationStatus { Status = true };
            try
            {
                foreach (var record in records)
                {
                    DataContext.Set<T>().Add(record);
                }
            }
            catch (Exception ex)
            {
                opStatus = OperationStatus.CreateFromSystemException($"Error adding {typeof(T)}.", ex);
            }
            return opStatus;
        }

        protected virtual OperationStatus Delete<T>(T record) where T : class
        {
            OperationStatus opStatus = new OperationStatus { Status = true };
            try
            {
                DataContext.Set<T>().Remove(record);
                DataContext.Entry(record).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                opStatus = OperationStatus.CreateFromSystemException($"Error Delete {typeof(T)}.", ex);
            }
            return opStatus;
        }

        protected virtual OperationStatus Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            OperationStatus opStatus = new OperationStatus { Status = true };
            try
            {
                DataContext.Set<T>().RemoveRange(DataContext.Set<T>().Where(predicate));
                DataContext.Entry(DataContext.Set<T>().Where(predicate).ToList()).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                opStatus = OperationStatus.CreateFromSystemException($"Error Delete {typeof(T)}.", ex);
            }
            return opStatus;
        }

        protected virtual OperationStatus DeleteRange<T>(List<T> records) where T : class
        {
            OperationStatus opStatus = new OperationStatus { Status = true };
            try
            {
                DataContext.Set<T>().RemoveRange(records);
                DataContext.Entry(records).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                opStatus = OperationStatus.CreateFromSystemException($"Error Delete {typeof(T)}.", ex);
            }
            return opStatus;
        }

        protected virtual OperationStatus Update<T>(T oldRecord, T newRecord) where T : class
        {
            OperationStatus opStatus = new OperationStatus { Status = true };
            try
            {
                foreach (PropertyInfo propertyInfo in oldRecord.GetType().GetProperties())
                {
                    oldRecord.GetType().GetProperty(propertyInfo.Name).SetValue(oldRecord, propertyInfo.GetValue(newRecord));
                }
                DataContext.Entry(oldRecord).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                opStatus = OperationStatus.CreateFromSystemException($"Error adding {typeof(T)}.", ex);
            }
            return opStatus;
        }

        protected virtual OperationStatus Save()
        {
            OperationStatus opStatus = new OperationStatus { Status = true };

            try
            {
                opStatus.Status = DataContext.SaveChanges() > 0;
            }
            catch (Exception exp)
            {
                opStatus = OperationStatus.CreateFromSystemException(exp.ToString(), exp);
            }

            return opStatus;
        }

        protected OperationStatus ExecuteStoreCommand(string cmdText, params object[] parameters)
        {
            var opStatus = new OperationStatus { Status = true };

            try
            {
                opStatus.RecordsAffected = DataContext.Database.ExecuteSqlCommand(cmdText, parameters);
            }
            catch (Exception exp)
            {
                OperationStatus.CreateFromSystemException("Error executing store command: ", exp);
            }
            return opStatus;
        }

        public virtual void Dispose()
        {
            if (DataContext != null) DataContext.Dispose();
        }
    }
}
