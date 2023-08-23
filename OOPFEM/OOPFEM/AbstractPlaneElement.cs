using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    abstract class AbstractPlaneElement : AbstractELement
    {
        protected double nu { get; set; }
    }
}
