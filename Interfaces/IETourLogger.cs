using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IETourLogger
    {
        public Task LogAsync(Log.LogType type, string content);
    }
}
