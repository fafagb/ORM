using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Infrastructure;
using Infrastructure.Mapping;
using Infrastructure.Model;
namespace ORMClient {
    class Program {

        static void Main (string[] args) {

            var dto = ExpressionGenericMapper<UserModel, UserModelDTO>.Trans (new UserModel () { Id = 1, Name = "123" });

            SqlHelper sqlHelper = new SqlHelper ();
            //  sqlHelper.Insert<UserModel> (new UserModel(){ Name="123", Pwd="123"  });
            //参数表达式
            ParameterExpression parameterExpression = Expression.Parameter (typeof (UserModel), "c");
            //反射获取属性
            var propertyId = typeof (UserModel).GetProperty ("Id");
            //通过parameterExpression来获取调用Id
            MemberExpression idExp = Expression.Property (parameterExpression, propertyId);
            //常量表达式
            ConstantExpression constant10 = Expression.Constant (1, typeof (int));
            //二元表达式
            BinaryExpression expressionExp = Expression.Equal (idExp, constant10);
            Expression<Func<UserModel, bool>> predicate1 = Expression.Lambda<Func<UserModel, bool>> (expressionExp, new ParameterExpression[1] {
                parameterExpression
            });

         //   var model = sqlHelper.FindCondition<UserModel> (x=>x.Id==1);
           UserModel user=new UserModel();
          user.Id=1;
          int id=user.Id;
        // var l=  sqlHelper.FindCondition<UserModel>(x=>x.Id==id);
          var l2=  sqlHelper.FindCondition<UserModel>(x=>x.Id==user.Id);


            Func<UserModel, bool> func1 = c => c.Id == 10;
            Func<UserModel, bool> func2 = c => { return c.Id == 10; };
            Expression<Func<UserModel, bool>> predicate = c => c.Id == 10;
            // Expression<Func<UserModel, bool>> predicate2 = c => {return c.Id == 10;};//无法将具有语句体的 lambda 表达式转换为表达式树 [ORMClient]
            Func<UserModel, bool> func3 = predicate.Compile ();

            List<int> list = new List<int> ();
            list.Add (1);
            list.Add (2);
            list.Find (x => x.Equals (1)); //不是linq
            list.Where (x => x == 1); //是linq,linq扩展了IE

            var query = list.AsQueryable ();
            query.Where (x => x == 1);
            var x = query.Where (x => x == 1).Select (x => x);
            var select1 = from item in query select item;
            var select2 = from p in query where p == 1 select p;

            //Console.WriteLine ($"{model[0].Name}");
        }
    }
}