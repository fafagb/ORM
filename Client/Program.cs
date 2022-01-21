using System;
using Infrastructure;
using Infrastructure.Model;

namespace ORMClient {
    class Program {
        static void Main (string[] args) {

            SqlHelper sqlHelper = new SqlHelper ();
           //  sqlHelper.Insert<UserModel> (new UserModel(){ Name="123", Pwd="123"  });
          var model=  sqlHelper.FindCondition<UserModel> (x => x.Id == 1);
            Console.WriteLine ($"{model[0].Name}");
        }
    }
}