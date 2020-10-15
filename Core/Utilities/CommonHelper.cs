using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.IO;

namespace GovTown.Utilities
{

    public static partial class CommonHelper
    {

        /// <summary>
        /// Maps a virtual path to a physical disk path.
        /// </summary>
        /// <param name="path">The path to map. E.g. "~/bin"</param>
        /// <param name="findAppRoot">Specifies if the app root should be resolved when mapped directory does not exist</param>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        /// <remarks>
        /// This method is able to resolve the web application root
        /// even when it's called during design-time (e.g. from EF design-time tools).
        /// </remarks>
  //      public static string MapPath(string path, bool findAppRoot = true)
  //      {
  //          Guard.ArgumentNotNull(() => path);

  //          if (HostingEnvironment.IsHosted)
  //          {
  //              // hosted
  //              return HostingEnvironment.MapPath(path);
  //          }
  //          else
  //          {
  //              // not hosted. For example, running in unit tests or EF tooling
  //              string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
  //              path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');

  //              var testPath = Path.Combine(baseDirectory, path);

  //              if (findAppRoot /* && !Directory.Exists(testPath)*/)
  //              {
  //                  // most likely we're in unit tests or design-mode (EF migration scaffolding)...
  //                  // find solution root directory first
  //                  var dir = FindSolutionRoot(baseDirectory);

  //                  // concat the web root
  //                  if (dir != null)
  //                  {
  //                      baseDirectory = Path.Combine(dir.FullName, "GovTown");
  //                      testPath = Path.Combine(baseDirectory, path);
  //                  }
  //              }

  //              return testPath;
  //          }
  //      }
  //      /// <summary>
  //      /// Gets a setting from the application's <c>web.config</c> <c>appSettings</c> node
  //      /// </summary>
  //      /// <typeparam name="T">The type to convert the setting value to</typeparam>
  //      /// <param name="key">The key of the setting</param>
  //      /// <param name="defValue">The default value to return if the setting does not exist</param>
  //      /// <returns>The casted setting value</returns>
  //      public static T GetAppSetting<T>(string key, T defValue = default(T))
		//{
		//	Guard.ArgumentNotEmpty(() => key);

		//	var setting = ConfigurationManager.AppSettings[key];

		//	if (setting == null)
		//	{
		//		return defValue;
		//	}

		//	return setting.Convert<T>();
		//}


        private static DirectoryInfo FindSolutionRoot(string currentDir)
        {
            var dir = Directory.GetParent(currentDir);
            while (true)
            {
                if (dir == null || IsSolutionRoot(dir))
                    break;

                dir = dir.Parent;
            }

            return dir;
        }

        private static bool IsSolutionRoot(DirectoryInfo dir)
        {
            return File.Exists(Path.Combine(dir.FullName, "GovTownNET.sln"));
        }
    }
}
