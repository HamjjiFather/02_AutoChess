using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework.DataBind.Extension;


namespace KKSFramework.DataBind.Methods
{
    public class MethodDelegates
    {
        protected readonly object[] Methods;


        public void Invoke ()
        {
            Methods.ForEach (x => ((Action) x).Invoke ());
        }


        public MethodDelegates (params object[] methods)
        {
            Methods = methods;
        }
    }


    public class MethodDelegates<T> : MethodDelegates
    {
        public MethodDelegates (params object[] methods) : base (methods)
        {
        }


        public void Invoke (T value)
        {
            Methods.ForEach (x => ((Action<T>) x).Invoke (value));
        }


        public void Invoke (IReadOnlyList<T> value)
        {
            Methods.ForEach ((x, i) => ((Action<T>) x).Invoke (value[i]));
        }
    }


    public class MethodDelegates<T, T1> : MethodDelegates
    {
        public MethodDelegates (params object[] methods) : base (methods)
        {
        }


        public void Invoke (T value1, T1 value2)
        {
            Methods.ForEach (x => ((Action<T, T1>) x).Invoke (value1, value2));
        }


        public void Invoke (IReadOnlyList<T> value1, IReadOnlyList<T1> value2)
        {
            Methods.ForEach ((x, i) => ((Action<T, T1>) x).Invoke (value1[i], value2[i]));
        }
    }


    public class MethodDelegates<T, T1, T2> : MethodDelegates
    {
        public MethodDelegates (params object[] methods) : base (methods)
        {
        }


        public void Invoke (T value1, T1 value2, T2 value3)
        {
            Methods.ForEach (x => ((Action<T, T1, T2>) x).Invoke (value1, value2, value3));
        }


        public void Invoke (IReadOnlyList<T> value1, IReadOnlyList<T1> value2, IReadOnlyList<T2> value3)
        {
            Methods.ForEach ((x, i) => ((Action<T, T1, T2>) x).Invoke (value1[i], value2[i], value3[i]));
        }
    }


    public class MethodDelegates<T, T1, T2, T3> : MethodDelegates
    {
        public MethodDelegates (params object[] methods) : base (methods)
        {
        }


        public void Invoke (T value1, T1 value2, T2 value3, T3 value4)
        {
            Methods.ForEach (x => ((Action<T, T1, T2, T3>) x).Invoke (value1, value2, value3, value4));
        }


        public void Invoke (IReadOnlyList<T> value1, IReadOnlyList<T1> value2, IReadOnlyList<T2> value3,
            IReadOnlyList<T3> value4)
        {
            Methods.ForEach ((x, i) =>
                ((Action<T, T1, T2, T3>) x).Invoke (value1[i], value2[i], value3[i], value4[i]));
        }
    }


    public class MethodDelegates<T, T1, T2, T3, T4> : MethodDelegates
    {
        public MethodDelegates (params object[] methods) : base (methods)
        {
        }


        public void Invoke (T value1, T1 value2, T2 value3, T3 value4, T4 values5)
        {
            Methods.ForEach (x =>
                ((Action<T, T1, T2, T3, T4>) x).Invoke (value1, value2, value3, value4, values5));
        }


        public void Invoke (IReadOnlyList<T> value1, IReadOnlyList<T1> value2, IReadOnlyList<T2> value3,
            IReadOnlyList<T3> value4, IReadOnlyList<T4> values5)
        {
            Methods.ForEach ((x, i) =>
                ((Action<T, T1, T2, T3, T4>) x).Invoke (value1[i], value2[i], value3[i], value4[i],
                    values5[i]));
        }
    }


    public class FuncDelegates<T>
    {
        protected readonly object[] Methods;


        public IEnumerable<T> Invoke ()
        {
            return Methods.Select (x => ((Func<T>) x).Invoke ());
        }


        public FuncDelegates (params object[] methods)
        {
            Methods = methods;
        }
    }


    public class FuncDelegates<T, T1> : FuncDelegates<T>
    {
        public FuncDelegates (params object[] methods) : base (methods)
        {
        }


        public IEnumerable<T1> Invoke (T value2)
        {
            return Methods.Select (x => ((Func<T, T1>) x).Invoke (value2));
        }


        public IEnumerable<T1> Invoke (IReadOnlyList<T> value)
        {
            return Methods.Select ((x, i) => ((Func<T, T1>) x).Invoke (value[i]));
        }
    }


    public class FuncDelegates<T, T1, T2> : FuncDelegates<T>
    {
        public FuncDelegates (params object[] methods) : base (methods)
        {
        }


        public IEnumerable<T2> Invoke (T value, T1 value2)
        {
            return Methods.Select (x => ((Func<T, T1, T2>) x).Invoke (value, value2));
        }


        public IEnumerable<T2> Invoke (IReadOnlyList<T> value, IReadOnlyList<T1> value1)
        {
            return Methods.Select ((x, i) => ((Func<T, T1, T2>) x).Invoke (value[i], value1[i]));
        }
    }


    public class FuncDelegates<T, T1, T2, T3> : FuncDelegates<T>
    {
        public FuncDelegates (params object[] methods) : base (methods)
        {
        }


        public IEnumerable<T3> Invoke (T value, T1 value2, T2 value3)
        {
            return Methods.Select (x => ((Func<T, T1, T2, T3>) x).Invoke (value, value2, value3));
        }


        public IEnumerable<T3> Invoke (IReadOnlyList<T> value, IReadOnlyList<T1> value2, IReadOnlyList<T2> value3)
        {
            return Methods.Select ((x, i) =>
                ((Func<T, T1, T2, T3>) x).Invoke (value[i], value2[i], value3[i]));
        }
    }


    public class FuncDelegates<T, T1, T2, T3, T4> : FuncDelegates<T>
    {
        public FuncDelegates (params object[] methods) : base (methods)
        {
        }


        public IEnumerable<T4> Invoke (T value, T1 value2, T2 value3, T3 value4)
        {
            return Methods.Select (x => ((Func<T, T1, T2, T3, T4>) x).Invoke (value, value2, value3, value4));
        }


        public IEnumerable<T4> Invoke (IReadOnlyList<T> values, IReadOnlyList<T1> value2, IReadOnlyList<T2> value3,
            IReadOnlyList<T3> value4)
        {
            return Methods.Select ((x, i) =>
                ((Func<T, T1, T2, T3, T4>) x).Invoke (values[i], value2[i], value3[i], value4[i]));
        }
    }
}