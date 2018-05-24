using System;

namespace DataAccessLayer
{
    public class ModelFactory<T>
    {
        private static T _dbContext;
        public static T GetContext()
        {
            if (_dbContext == null)
                _dbContext = (T)Activator.CreateInstance(typeof(T));
            return _dbContext;
        }
    }
}
