using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Core.Services
{
    public interface IMessageService
    {
        void CloseApplication();

        void MinimalizeApplication();
    }
}
