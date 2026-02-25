using System;
using System.Collections.Generic;
using System.Text;

namespace TestSecu.Domain
{
    /// <summary>
    /// Classe nécessaire pour transmettre les méta data et autres à mon helper pour mon jwt
    /// </summary>
    public class UserInfo 
    {
        public string Id { get; set; }
        public Dictionary<string, string> MetaData { get; set; }
        public List<string> Roles { get; set; }
    }
}
