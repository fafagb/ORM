using System;
namespace Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute:BaseAttribute
    {
        public TableAttribute(string name):base(name){


        }
    }
}