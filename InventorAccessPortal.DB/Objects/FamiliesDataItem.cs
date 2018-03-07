using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InventorAccessPortal.DB.Objects
{
    public class FamiliesDataItem
    {
        public Family Family;
        public List<File_Number> FileNumbers = new List<File_Number>();
    }
}
