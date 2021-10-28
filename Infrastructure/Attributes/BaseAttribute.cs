using System;

namespace Infrastructure.Attributes {
    public class BaseAttribute : Attribute {
        private string _name;
        public BaseAttribute (string name) {
            _name = name;
        }

        public string GetMappingName(){
            
            return  _name;
        }
    }
}