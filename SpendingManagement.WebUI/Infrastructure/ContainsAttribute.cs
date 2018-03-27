using System;

namespace SpendingManagement.WebUI.Models
{
    public class ContainsAttribute : Attribute
    {
        private string sign = ":";
        public ContainsAttribute(string name)
        {

        }
    }
}