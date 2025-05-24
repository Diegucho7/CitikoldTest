using System.Reflection;

namespace RetailCitikold.Infrastructure;

public static  class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
