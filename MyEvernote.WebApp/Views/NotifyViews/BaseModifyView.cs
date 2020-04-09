using System.Collections.Generic;

namespace MyEvernote.WebApp.Views.NotifyViews
{
    public class BaseModifyView<T>
    {
        public BaseModifyView()
        {
            Header = "Yönlendiriliyorsunuz...";
            Title = "Geçersiz İşlem";
            IsRedirect = true;
            RedirectingUrl = "/Home/Index/";
            RedirectingTime = 1000;

        }

        public List<string> Items;
        public string Header;
        public string Title;
        public bool IsRedirect;
        public string RedirectingUrl;
        public int RedirectingTime;

    }
}