using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/05/21
    /// 游戏数据类
    /// </summary>
    public class GameResult
    {
        public int pageNum { get; set; }
        public int total { get; set; }
        public int pageSum { get; set; }
        public int pageSize { get; set; }
        public IEnumerable<GameData> data { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Integral { get; set; }
    }
}
