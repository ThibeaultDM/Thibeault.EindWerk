using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thibeault.EindWerk.DataLayer.Interfaces;

namespace Thibeault.EindWerk.DataLayer
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(IDataContext dataContext): base(dataContext)
        {            
        }
    }
}
