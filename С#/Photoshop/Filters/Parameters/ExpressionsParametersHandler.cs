using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.PropertyGridInternal;
using System.Linq.Expressions;

namespace MyPhotoshop
{
    public class ExpressionsParametersHandler<TParameters> : IParametersHandler<TParameters>
        where TParameters : IParameters, new()
    {
        static ParameterInfo[] descriptions;
        static Func<double[], TParameters> parser;


        static ExpressionsParametersHandler()
        {
            descriptions = typeof(TParameters)
                    .GetProperties()
                    .Select(z => z.GetCustomAttributes(typeof(ParameterInfo), false))
                    .Where(z => z.Length > 0)
                    .Select(z => z[0])
                    .Cast<ParameterInfo>()
                    .ToArray();

            var properties = typeof(TParameters)
                            .GetProperties()
                            .Where(z => z.GetCustomAttributes(typeof(ParameterInfo), false).Length > 0)
                            .ToArray();
            var arg = Expression.Parameter(typeof(double[]), "values");
            var bindings = new List<MemberBinding>();
            for(int i = 0; i < properties.Length; i++)
            {
                var binding = Expression.Bind(properties[i], Expression.ArrayIndex(arg, Expression.Constant(i)));
                bindings.Add(binding);
            }

            var body = Expression.MemberInit(Expression.New(typeof(TParameters).GetConstructor(new Type[0])),
                bindings);

            var lambda = Expression.Lambda<Func<double[], TParameters>>(body, arg);
            parser = lambda.Compile();
        }
        public TParameters CreateParameters(double[] values) => parser(values);
        public ParameterInfo[] GetDescription() => descriptions;
    }
}
