using Fusillade;
using System;
using System.Collections.Generic;
using System.Text;

namespace CakeMaker.Services.Apis
{
    public interface IApiService<T>
    {
        T GetApi(Priority priority);
    }
}
