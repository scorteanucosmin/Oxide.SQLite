using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Oxide.Core.Extensions;

namespace Oxide.Core.SQLite
{
    /// <summary>
    /// The extension class that represents this extension
    /// </summary>
    public class SQLiteExtension : Extension
    {
        internal static Assembly Assembly = Assembly.GetExecutingAssembly();
        internal static AssemblyName AssemblyName = Assembly.GetName();
        internal static VersionNumber AssemblyVersion = new VersionNumber(AssemblyName.Version.Major, AssemblyName.Version.Minor, AssemblyName.Version.Build);
        internal static string AssemblyAuthors = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly, typeof(AssemblyCompanyAttribute), false)).Company;

        /// <summary>
        /// Gets whether this extension is a core extension
        /// </summary>
        public override bool IsCoreExtension => true;

        /// <summary>
        /// Gets the name of this extension
        /// </summary>
        public override string Name => "SQLite";

        /// <summary>
        /// Gets the author of this extension
        /// </summary>
        public override string Author => AssemblyAuthors;

        /// <summary>
        /// Gets the version of this extension
        /// </summary>
        public override VersionNumber Version => AssemblyVersion;

        /// <summary>
        /// Initializes a new instance of the MySqlExtension class
        /// </summary>
        public SQLiteExtension(ExtensionManager manager) : base(manager)
        {
            NativeLibrary.SetDllImportResolver(typeof(SQLitePCL.SQLite3Provider_e_sqlite3).Assembly, ResolveNative);
            SQLitePCL.Batteries_V2.Init();
        }

        private static IntPtr ResolveNative(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName != "e_sqlite3")
            {
                return IntPtr.Zero;
            }

            string fileName = OperatingSystem.IsWindows() ? "e_sqlite3.dll" : OperatingSystem.IsMacOS() ? "libe_sqlite3.dylib" : "libe_sqlite3.so";
            string path = Path.Combine(Interface.Oxide.ExtensionDirectory, "x64", fileName);
            return NativeLibrary.TryLoad(path, out IntPtr handle) ? handle : IntPtr.Zero;
        }

        /// <summary>
        /// Loads this extension
        /// </summary>
        public override void Load() => Manager.RegisterLibrary("SQLite", new Libraries.SQLite());

        /// <summary>
        /// Loads plugin watchers used by this extension
        /// </summary>
        /// <param name="pluginDirectory"></param>
        public override void LoadPluginWatchers(string pluginDirectory)
        {
        }

        /// <summary>
        /// Called when all other extensions have been loaded
        /// </summary>
        public override void OnModLoad()
        {
        }
    }
}
