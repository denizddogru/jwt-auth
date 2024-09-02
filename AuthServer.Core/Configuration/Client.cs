using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Configuration;

// Auth servera istek yapıcak client'a karşılık gelecek ,web app de olabilir mobil app de
public class Client
{
    public string Id { get; set; }

    public string Secret { get; set; }

    // Göndereceğim tokende hangi API'lere erişebilsin bilgileri tutulurç
    // www.myapi1.com, www.myapi2.com
    public List<string> Audiences { get; set; }

}
