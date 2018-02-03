using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleIoCContainer.Lib
{
    public class SimpleContainer
    {
        IDictionary<Type, Type> registries;
        IDictionary<string, object> namedRegistries;
        IDictionary<Type, object> singletonRegistries;

        public SimpleContainer()
        {
            this.registries = new Dictionary<Type, Type>();
            this.namedRegistries = new Dictionary<string, object>();
            this.singletonRegistries = new Dictionary<Type, object>();
        }

        public SimpleContainer Register<T, R>()
        {
            this.registries[typeof(T)] = typeof(R);
            return this;
        }

        public SimpleContainer Register<T, R>(string name)
        {
            var args = GetArgs(typeof(R).GetConstructors().FirstOrDefault());
            this.namedRegistries[name] = Activator.CreateInstance(typeof(R), args.ToArray());
            return this;
        }

        public SimpleContainer RegisterSingleton<T, R>()
        {
            this.singletonRegistries[typeof(T)] = null;
            this.Register<T, R>();
            return this;
        }

        public T Resolve<T>()
        {
            return (T)this.Resolve(typeof(T));
        }

        public T Resolve<T>(string name)
        {
            return (T)this.namedRegistries[name];
        }

        object Resolve(Type type)
        {
            var instance =
                this.singletonRegistries.ContainsKey(type)
                    ? this.LoadInstance(type)
                    : this.CreateInstance(type);
            return instance;
        }

        object CreateInstance(Type type)
        {
            var instanceType = this.registries[type];

            // try all constructors, untill a suitable one is found
            foreach (var constructor in instanceType.GetConstructors())
            {
                IEnumerable<object> args = GetArgs(constructor);
                try
                {
                    var instance = Activator.CreateInstance(instanceType, args.ToArray());
                    return instance;
                }
                catch
                {
                    Console.WriteLine("Ups... It did it again...");
                }
            }

            throw new MissingMethodException("Instance cannot be created");
        }

        object LoadInstance(Type type)
        {
            if (this.singletonRegistries[type] == null)
            {
                this.singletonRegistries[type] = this.CreateInstance(type);
            }

            return this.singletonRegistries[type];
        }

        IEnumerable<object> GetArgs(ConstructorInfo constructorInfo)
        {
            return constructorInfo.GetParameters()
                                  .Select(arg => arg.ParameterType)
                                  .Select(this.Resolve);
        }
    }
}