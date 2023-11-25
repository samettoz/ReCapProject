using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(string message) : base(default, true, message)
        {

        }

        public SuccessDataResult() : base(default, false)
        {

        }

        public SuccessDataResult(T data, string message) : base(data,true,message)
        {
            
        }
        public SuccessDataResult(T data) : base(data,true)
        {
            
        }

    }
}
