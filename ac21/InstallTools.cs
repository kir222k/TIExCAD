using Microsoft.Win32;
using System.Reflection;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

[assembly: CommandClass(typeof(TIExCAD.InstallTools))]



namespace TIExCAD
{

[CommandMethod("TIExCADreg")]
public void InstallTools()
{
  // Get the AutoCAD Applications key
  string sProdKey = HostApplicationServices.Current.RegistryProductRootKey;
  string sAppName = "TiExCAD";

  RegistryKey regAcadProdKey = Registry.CurrentUser.OpenSubKey(sProdKey);
  RegistryKey regAcadAppKey = regAcadProdKey.OpenSubKey("Applications", true);

  // Check to see if the "MyApp" key exists
  string[] subKeys = regAcadAppKey.GetSubKeyNames();
  foreach (string subKey in subKeys)
  {
      // If the application is already registered, exit
      if (subKey.Equals(sAppName))
      {
          regAcadAppKey.Close();
          return;
      }
  }

  // Get the location of this module
  string sAssemblyPath = Assembly.GetExecutingAssembly().Location;

  // Register the application
  RegistryKey regAppAddInKey = regAcadAppKey.CreateSubKey(sAppName);
  regAppAddInKey.SetValue("DESCRIPTION", sAppName, RegistryValueKind.String);
  regAppAddInKey.SetValue("LOADCTRLS", 14, RegistryValueKind.DWord);
  regAppAddInKey.SetValue("LOADER", sAssemblyPath, RegistryValueKind.String);
  regAppAddInKey.SetValue("MANAGED", 1, RegistryValueKind.DWord);

  regAcadAppKey.Close();
}

[CommandMethod("TIExCADunreg")]
public void UnregisterMyApp()
{
  // Get the AutoCAD Applications key
  string sProdKey = HostApplicationServices.Current.RegistryProductRootKey;
  string sAppName = "TiExCAD";

  RegistryKey regAcadProdKey = Registry.CurrentUser.OpenSubKey(sProdKey);
  RegistryKey regAcadAppKey = regAcadProdKey.OpenSubKey("Applications", true);

  // Delete the key for the application
  regAcadAppKey.DeleteSubKeyTree(sAppName);
  regAcadAppKey.Close();
}

}