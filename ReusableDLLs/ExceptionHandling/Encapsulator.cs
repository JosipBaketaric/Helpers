using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    public class Encapsulator
    {

        public T TryCatch<T>(Func<T> serviceMethod)
        {
            T result = default(T);
            try
            {
                result = serviceMethod();
            }
            catch (Exception exception)
            {
                //ErrorLogRepository.LogException(exception);
            }
            return result;
        }

        private Exception GetLastException(Exception exception)
        {
            if (exception.InnerException == null)
                return exception;

            exception = GetLastException(exception.InnerException);

            return exception;
        }

        
    }


}
