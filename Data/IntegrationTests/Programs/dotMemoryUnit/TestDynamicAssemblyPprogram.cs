using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;

namespace TestPrograms
{
  public class DynamicAssemblyPprogram
  {
    public static void Execute(Action getSnapshot)
    {
      AssemblyName asmName = new AssemblyName("MyAssembly");
      //Get AppDomain where the assembly needs to be created.
      AppDomain currentDomain = Thread.GetDomain();

      //AssemblyBuilder object can be defined from AppDomain needed to build a dynamic assembly. 
      //RunAndSave will let you run the application as well as save it to custom dll file
      AssemblyBuilder builder = currentDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);

      //Every assembly have modules which logically defines the Types. Mymodule is the name of the Module
      ModuleBuilder mbuilder = builder.DefineDynamicModule("MyModule");

      //Type Builder can generate a Type. Public class MyClass type is declared
      TypeBuilder tbuilder = mbuilder.DefineType("MyClass", TypeAttributes.Public);

      //Create the Type MyClass in the Assembly
      Type thistype = tbuilder.CreateType();

      object thisObj = Activator.CreateInstance(thistype);

      getSnapshot();

      GC.KeepAlive(thisObj);
    }
  }
}
