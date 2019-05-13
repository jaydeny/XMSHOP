using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class CommonDataDTO
    {
        public IEnumerable<RoleEntity> Roles { get; set; }
        public IEnumerable<GoodsTypeEntity> Types { get; set; }
        public IEnumerable<MenuEntity> Menus { get; set; }
        public IEnumerable<Navbar> Navbars { get; set; }
    }
}
