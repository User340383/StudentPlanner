using StudentPlanner.Core;
using StudentPlanner.Data;

namespace StudentPlanner.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

			// Create DB file and later create tables
			DatabaseInitializer.Initialize();

			ICourseRepository courseRepo = new CourseRepository();
			Application.Run(new Form1());
        }
    }
}
