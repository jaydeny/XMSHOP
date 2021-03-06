﻿using System.Collections.Generic;

namespace XM.Model
{
    public class CommonDataDTO
    {
        public IEnumerable<RoleEntity> Roles { get; set; }
        public IEnumerable<DicEntity> Types { get; set; }
        public IEnumerable<MenuEntity> Menus { get; set; }
        public IEnumerable<Navbar> Navbars { get; set; }
        public IEnumerable<AgentEntity> Agents { get; set; }
    }
}
