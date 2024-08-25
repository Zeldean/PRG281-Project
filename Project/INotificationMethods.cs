using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal interface INotificationMethods
    {
        int GeneratingNote(string fileP);

        void Respons(int unitlimit);

        void message();
    }
}
