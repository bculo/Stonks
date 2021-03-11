using Stonk.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stonk.Application.Contracts.Services
{
    public interface IHtmlLookupService
    {
        /// <summary>
        /// Find HTML element by given ID
        /// </summary>
        /// <param name="html">HTML content</param>
        /// <param name="elementId">ID name</param>
        /// <param name="innerHtml">return element with or without html tags</param>
        /// <exception cref="System.Exception">If multiple elements with same ID are found, method throws exception</exception>
        /// <returns></returns>
        string FindHtmlElementById(string html, string elementId, bool innerHtml = false);

        /// <summary>
        /// Find HTML element by given CLASS name
        /// </summary>
        /// <param name="html">HTML content</param>
        /// <param name="elementClass">CLASS name</param>
        /// <param name="innerHtml">return element with or without html tags</param>
        /// <exception cref="System.Exception">If multiple elements with same CLASS are found, method throws exception</exception>
        /// <returns></returns>
        string FindHtmlElementByClass(string html, string elementClass, bool innerHtml = false);

        /// <summary>
        /// Find HTML element by given html tag
        /// </summary>
        /// <param name="html">HTML content</param>
        /// <param name="elementName">TAG name</param>
        /// <param name="innerHtml">return element with or without html tags</param>
        /// <exception cref="System.Exception">If multiple elements with same TAG are found, method throws exception</exception>
        /// <returns></returns>
        string FindHtmlElementByTag(string html, string elementName, bool innerHtml = false);

        /// <summary>
        /// Get attribute value
        /// </summary>
        /// <param name="htmlElement">HTML element</param>
        /// <param name="attributeName">Get value for given attribute</param>
        /// <returns></returns>
        T GetHtmlElementDataAttributeValue<T>(string htmlElement, string attributeName);
    }
}
