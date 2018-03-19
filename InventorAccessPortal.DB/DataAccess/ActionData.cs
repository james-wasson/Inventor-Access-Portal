using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using InventorAccessPortal.DB.Enums;
using InventorAccessPortal.DB.Objects;
using Newtonsoft.Json;
using System.Data.Entity.Core;

namespace InventorAccessPortal.DB.DataAccess
{ 
    public class ActionData
    {
        public static Guid? SetNewAction(NewActionData data, EntityContext e = null)
        {
            e.CheckInit();
            // if no login data return null, can't store
            if (data.InvestigatorName == null) return null;
            // try to searlize the object
            String jsonData = null;
            try
            {
                jsonData = JsonConvert.SerializeObject(data.Model);
            }
            catch (JsonException ex) // there was some serilazation
            {
                return null;
            }
            var guid = Guid.NewGuid();
            // try to build the row and insert
            try
            {
                var action = new Web_Action_datum()
                {
                    Action_Number = data.ActionNumber,
                    Guid = guid.ToString(),
                    Investigator_Name = data.InvestigatorName,
                    Expires = data.Expires,
                    Json_Data = jsonData
                };
                e.Web_Action_Data.Add(action);
                e.SaveChanges();
            }
            // if error, return null
            catch (EntityException ex)
            {
                return null;
            }
            return guid;
        }

        public static Tuple<type, Web_Action_datum> GetAction<type>(Guid guid, EntityContext e = null)
        {
            e.CheckInit();
            var sGuid = guid.ToString();
            var data = e.Web_Action_Data.FirstOrDefault(p => p.Guid == sGuid);
            if (data == null) return Tuple.Create(default(type), default(Web_Action_datum));
            if (data.Expires == null || DateTime.Now <= data.Expires)
            {
                var json = data.Json_Data;
                type rv;
                try
                {
                    rv = JsonConvert.DeserializeObject<type>(json);
                }
                catch (Exception ex)
                {
                    return Tuple.Create(default(type), default(Web_Action_datum));
                }
                return Tuple.Create(rv, data);
            }
            return Tuple.Create(default(type), default(Web_Action_datum));
        }

    }
}
