using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uxsoft.Share.Models.HomeViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<string> Files { get; set; } = Enumerable.Empty<string>();
    }
}
