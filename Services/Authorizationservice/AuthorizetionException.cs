using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Authorizationservice;
public class AuthorizetionException : Exception    
{
    public AuthorizetionException(string message = "invalid Email Or Password") : base(message) 
    {
        
    }




}
