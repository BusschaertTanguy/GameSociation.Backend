using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Domain.Enumerations
{
    public class Enumeration : IComparable
    {
        protected Enumeration(int id, string display)
        {
            Id = id;
            Display = display;
        }

        public int Id { get; }

        public string Display { get; }

        public int CompareTo(object other)
        {
            return Id.CompareTo(((Enumeration) other).Id);
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration otherValue))
                return false;

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}