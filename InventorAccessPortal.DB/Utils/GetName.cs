using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorAccessPortal.DB.Utils
{
    public class FirstAndLastName
    {
        public String FirstName;
        public String LastName;
    }
    public static class GetName
    {
        public static FirstAndLastName FirstAndLast(String InvestigatorName)
        {
            var nameArray = InvestigatorName.Split(',').ToArray();
            for (var i = 0; i < nameArray.Length; i++)
            {
                nameArray[i] = nameArray[i].Trim(); // trims the strings
                nameArray[i] = nameArray[i].First().ToString().ToUpper() + nameArray[i].Substring(1); // capalizes the first letter
            }
            return new FirstAndLastName
            {
                FirstName = nameArray[1],
                LastName = nameArray[0]
            };
        }
    }
}
