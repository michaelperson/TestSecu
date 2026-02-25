using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityTools.Interfaces
{
    public  interface IUserInfo
    {
        string Id { get; set; }
        //Claims custom - Nom + la valeur
        Dictionary<string,string> MetaData { get; set; }
        // CLaims de type Role ClaimTypes.Role
        List<string> Roles { get; set; }
    }
}
