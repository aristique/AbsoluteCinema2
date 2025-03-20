using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_logic
{
    public interface Icomment
    {
        void Addcomment (string comment );
        void Removecomment (int commentId);
        void Modifycomment(int commentId,string comment);



    }

}
