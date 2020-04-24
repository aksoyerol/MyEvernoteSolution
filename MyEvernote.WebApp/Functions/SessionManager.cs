using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyEvernote.Entities;

namespace MyEvernote.WebApp.Functions
{
    public class SessionManager
    {
        public static EvernoteUser CurrentUser
        {
            get {    
            return Get<EvernoteUser>("login");
            }

        }

    public static void Set<T>(string key, T obj)
    {
        HttpContext.Current.Session[key] = (T)obj;
    }

    public static T Get<T>(string key)
    {
        if (HttpContext.Current.Session[key] != null)
        {
            return (T)HttpContext.Current.Session[key];
        }

        return default(T);
    }

    public static void Remove(string key)
    {
        if (HttpContext.Current.Session[key] != null)
        {
            HttpContext.Current.Session.Remove(key);
        }
    }

    public static void Cler()
    {
        HttpContext.Current.Session.Clear();
    }
}
}