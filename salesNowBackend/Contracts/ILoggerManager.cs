using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.Contracts
{
    public interface ILoggerManager
    {
        void LogError(string message);
        void LogDebug(string message);
        void LogWarn(string message);
        void LogInfo(string message);
    }
}
