using System;
namespace Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute:BaseAttribute
    {

        public ColumnAttribute(string name):base(name){


        }
    }
}