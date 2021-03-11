using Stonk.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stonk.Application.Contracts.Services
{
    public interface IHtmlLookupService
    {
        string FindHtmlElementById(string html, string elementId, bool innerHtml = false);

        string FindHtmlElementByClass(string html, string elementClass, bool innerHtml = false);
    }
}
