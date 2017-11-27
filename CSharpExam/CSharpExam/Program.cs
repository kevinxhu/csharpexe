using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExec
{
    using System;
    using System.Reflection;

    public class Base<T, U> { }

    public class Derived<V> : Base<string, V>
    {
        public G<Derived<V>> F;

        public class Nested { }
    }

    public class G<T> { }


    //Type.IsGenericType Property
    //https://msdn.microsoft.com/en-us/library/system.type.isgenerictype(v=vs.110).aspx

    class Example
    {
        public static void Main()
        {
            // Get the generic type definition for Derived, and the base
            // type for Derived.
            //
            Type tDerived = typeof(Derived<>);
            Type tDerivedBase = tDerived.BaseType;

            // Declare an array of Derived<int>, and get its type.
            //
            Derived<int>[] d = new Derived<int>[0];
            Type tDerivedArray = d.GetType();

            // Get a generic type parameter, the type of a field, and a
            // type that is nested in Derived. Notice that in order to
            // get the nested type it is necessary to either (1) specify
            // the generic type definition Derived<>, as shown here,
            // or (2) specify a type parameter for Derived.
            //
            Type tT = typeof(Base<,>).GetGenericArguments()[0];
            Type tF = tDerived.GetField("F").FieldType;
            Type tNested = typeof(Derived<>.Nested);

            DisplayGenericType(tDerived, "Derived<V>");
            DisplayGenericType(tDerivedBase, "Base type of Derived<V>");
            DisplayGenericType(tDerivedArray, "Array of Derived<int>");
            DisplayGenericType(tT, "Type parameter T from Base<T>");
            DisplayGenericType(tF, "Field type, G<Derived<V>>");
            DisplayGenericType(tNested, "Nested type in Derived<V>");
        }

        public static void DisplayGenericType(Type t, string caption)
        {
            Console.WriteLine("\n{0}", caption);
            Console.WriteLine("    Type: {0}", t);

            Console.WriteLine("\t            IsGenericType: {0}",
                t.IsGenericType);
            Console.WriteLine("\t  IsGenericTypeDefinition: {0}",
                t.IsGenericTypeDefinition);
            Console.WriteLine("\tContainsGenericParameters: {0}",
                t.ContainsGenericParameters);
            Console.WriteLine("\t       IsGenericParameter: {0}",
                t.IsGenericParameter);
        }
    }
}
