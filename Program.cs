using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Expression.MyTestedASPNET;

namespace Exprsn
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Func<MyClass, string> func =
                c => c.MyMethod(42, "Bottles");

            Expression<Func<MyClass, int, string>> lambdaEpxression =
                (x,i) => x.MyMethod(i, "bottles");


            // Lambda :enum ExpressionType
            lambdaEpxression.NodeType.Print();

            // Call :enum ExpressionTppe
            lambdaEpxression.Body.NodeType.Print();

            // x.MyMethod(i, "bottles");
            lambdaEpxression.Body.Print();

            // Expression object
            var expression = lambdaEpxression.Body;

            //x.MyMethod(i, "bottles");
            expression.Print();

            // Call
            expression.NodeType.Print(); 

            Expression<Func<int, int, int>> sumLamdaExpression =
                (x, y) => x + y;

            ParseExpression(lambdaEpxression);

            Console.Read();
        }

        private static void ParseExpression(System.Linq.Expressions.Expression expression)
        {
            if (expression.NodeType == ExpressionType.Lambda)
            {
                var lambda = expression as LambdaExpression;
                Console.WriteLine("ParseExpression");
                Console.WriteLine("Lambda");

                foreach (var parameter in lambda.Parameters)
                        parameter.Name.Print();

                // Recursion
                ParseExpression(lambda.Body); // only LamdaExpression has body

            }

            if(expression.NodeType == ExpressionType.Call)
            {               
                var methodCallExpression = expression as MethodCallExpression;
                // MethodInfo
                methodCallExpression.Method.Name.Print();

                foreach (var argument in methodCallExpression.Arguments)
                {
                    // In our case ParameterExpression or ConstantEpxression
                    argument.NodeType.Print();
                    ParseExpression(argument);
                }
            }

            if(expression.NodeType == ExpressionType.MemberAccess)
            {
                (expression as MemberExpression)
                .Member.Name.Print(); // MemberInfo
            }

            if(expression.NodeType == ExpressionType.Constant)
            {
                (expression as ConstantExpression)
                 .Value
                 .Print();
            }
        }
    }


    public static class Extension
    {
        public static void Print(this object obj) => Console.WriteLine(obj);
    }

}
