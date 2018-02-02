using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleDiContainer.Lib
{
    public class SimpleContainer
    {
        IDictionary<Type, Object> registries;
        IDictionary<string, Object> namedRegistries;

        public SimpleContainer()
        {
            this.registries = new Dictionary<Type, Object>();
            this.namedRegistries = new Dictionary<string, Object>();
        }

        public SimpleContainer Register<T, R>()
        {
            IEnumerable<object> args = GetArgs(typeof(R).GetConstructors().FirstOrDefault());

            this.registries[typeof(T)] = Activator.CreateInstance(typeof(R), args.ToArray());
            return this;
        }

        public SimpleContainer Register<T, R>(string name)
        {
            var args = GetArgs(typeof(R).GetConstructors().FirstOrDefault());

            this.namedRegistries[name] = Activator.CreateInstance(typeof(R), args.ToArray());

            return this;
        }

        public T Resolve<T>()
        {
            return (T)this.registries[typeof(T)];
        }

        public T Resolve<T>(string name)
        {
            return (T)this.namedRegistries[name];
        }

        private IEnumerable<object> GetArgs(ConstructorInfo constructorInfo)
        {
            return constructorInfo
                .GetParameters()
                .Select(arg => this.registries[arg.ParameterType]);
        }
    }
}