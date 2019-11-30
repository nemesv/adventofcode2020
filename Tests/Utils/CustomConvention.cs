using Fixie;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tests.Utils
{
    public class CustomConvention : Fixie.Discovery
    {
        public CustomConvention()
        {
            Methods
                .Where(method => method.IsVoid());

            Parameters
                .Add<FromInputAttributes>();
        }

        class FromInputAttributes : ParameterSource
        {
            public IEnumerable<object[]> GetParameters(MethodInfo method)
            {
                return method.GetCustomAttributes<InputAttribute>(true).Select(input => input.Parameters);
            }
        }
    }
}
