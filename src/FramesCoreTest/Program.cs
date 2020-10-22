using System;
using System.Collections.Generic;
using System.Linq;
using Spearing.Utilities.Data.FramesCore;
using static Spearing.Utilities.Data.FramesCore.FrameExtensions;

namespace FramesCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new frame
            Frame frame = new Frame();

            // Generic lists and typed arrays are stored internally typed
            frame["Names"] = new string[] { "Bob", "Mary", "Joe" };
            frame["StartDate"] = new DateTime[] {
                new DateTime(2016, 10, 1),
                new DateTime(2016, 6, 8),
                new DateTime(2017, 9, 2)
             };

            // The static col method allows you quickly create a Column
            frame["Ages"] = col(41.0, 28.0, 35.0);
            frame["LowScore"] = col(78.0, 81.0, 85.0);

            // Use the ToColumn extension method to convert an IEnumerable<T> to a Column (also works on arrays and lists)
            frame["HighScore"] = new double[] { 90.0, 92.0, 87.0 }.ToColumn();

            // Print all columns
            frame.Print();

            // Print selected columns
            frame.Print("Names", "HighScore");

            // Print a column
            frame["Names"].Print();


            // The As extension method retrieves the data with typing
            // Make sure to use the type that the column was created with
            double averageAge = frame["Ages"].As<double>().Average();
            double totalScore = frame["HighScore"].As<double>().Sum();

            double[] ages = frame["Ages"].As<double>().ToArray();

            // Columns inherit from List<T>
            List<double> lowScores = frame["LowScore"].As<double>();

            frame["ScoreDiff"] = frame["HighScore"].As<double>() - frame["LowScore"].As<double>();
            frame["HighPlus1"] = frame["HighScore"].As<double>() + 1.0;

            frame["Hours"] = new double[] { 25, 30, 38 };
            double[] hourlyRate = new double[] { 15, 20, 12 };

            frame["Pay"] = frame["Hours"].As<double>() * hourlyRate;

            frame.SaveCsv(@"c:\temp\Employees.csv");


            // Use the ToFrame method to convert a strongly typed collection to a frame
            List<Employee> employees = new List<Employee>()
             {
                 new Employee() {Name = "Bob", Age = 40, HighScore = 90.0 },
                 new Employee() {Name = "Mary", Age = 28, HighScore = 92.0 },
                 new Employee() {Name = "Joe", Age = 35, HighScore = 87.0 }
             };

            Frame employeesFrame = employees.ToFrame();

            employeesFrame.Print();

            // Use Linq predicates to filter data
            DateTime startDate = new DateTime(2016, 9, 1);
            Frame newEmployees = frame
                .Where(row => row.Get<DateTime>("StartDate") >= startDate)
                .ToFrame();

            newEmployees.Print();

            // Group data and use anonymous types to create a new frame
            Frame empYearSummary = frame
                .GroupBy(row => row.Get<DateTime>("StartDate").Year)
                .Select(grp => new
                {
                    Year = grp.Key,
                    AverageAge = grp.Average(row => row.Get<double>("Ages")),
                    Count = grp.Count()
                })
                .ToFrame();

            empYearSummary.Print();

            // Local file
            Frame employeesLocal = Frame.ReadCSV<string, DateTime, double, double, double, double, double>(@"c:\temp\Employees.csv");

            // Web site
            Frame employeesWeb = Frame.ReadCSV<string, DateTime, double, double, double, double, double>(@"http://www.spearing.com/files/Employees.csv");

            // Git web site
            Frame employeesGit = Frame.ReadCSV<string, DateTime, double, double, double, double, double>(@"https://raw.githubusercontent.com/jackimburgia/Frames/master/Employees.csv");

            // Get the top rows of the frame
            IEnumerable<Row> headEmployees = employeesGit.Head();

            headEmployees.Print();

            // Get the last rows of the frame
            IEnumerable<Row> tailEmployees = employeesGit.Tail();

            tailEmployees.Print();

            // Create the frame that will be the left side
            // Use the static c method to create a typed array
            Frame books = new Frame();
            books["Name"] = c("Tukey", "Venables", "Tierney", "Ripley", "Ripley", "McNeil", "R Core");
            books["Title"] = c("Exploratory Data Analysis",
                            "Modern Applied Statistics ...",
                            "LISP-STAT",
                            "Spatial Statistics", "Stochastic Simulation",
                            "Interactive Data Analysis",
                            "An Introduction to R");
            books["Other.Author"] = c(null, "Ripley", null, null, null, null, "Venables & Smith");

            // Create the frame that will be the right side
            Frame authors = new Frame();
            authors["Surname"] = c("Tukey", "Venables", "Tierney", "Ripley", "McNeil");
            authors["Nationality"] = c("US", "Australia", "US", "UK", "Australia");
            authors["Deceased"] = c(true, false, false, false, false);

            // Perform an "inner" style join; only display rows where keys match
            Frame joined = books.Join(authors,
                row => row.Get<string>("Name"),
                row => row.Get<string>("Surname")
                );

            joined.Print();

            // Perform a left "outer" style join
            // Display all rows from left table and only the values from the right
            //      table where the keys match
            Frame outerJoin = books.OuterJoin(authors,
                row => row.Get<string>("Name"),
                row => row.Get<string>("Surname")
                );

            outerJoin.Print();

            Frame sites = new Frame();
            sites["State"] = c("IL", "IL", "IN");
            sites["Site"] = c(1, 2, 1);
            sites["Latitude"] = c(42.46757, 42.04915, 41.6814);
            sites["Longitude"] = c(-87.81005, -88.27303, -87.49473);


            Frame parameters = new Frame();
            parameters["Region"] = c("IL", "IN", "IL", "IL");
            parameters["Monitor"] = c(1, 1, 2, 2);
            parameters["Parameter"] = c("ozone", "so2", "ozone", "no2");
            parameters["Duration"] = c("1h", "1h", "8h", "1h");

            // Create anonymous types that will act as keys to join on each frame
            var multipleKey = sites
                .Join(parameters,
                row => new { State = row.Get<string>("State"), Site = row.Get<int>("Site") },
                row => new { State = row.Get<string>("Region"), Site = row.Get<int>("Monitor") }
                );

            multipleKey.Print();

            Frame meltData = new Frame();
            meltData["FactorA"] = c("Low", "Medium", "High", "Low", "Medium", "High", "Low", "Medium", "High");
            meltData["FactorB"] = c("Low", "Low", "Low", "Medium", "Medium", "Medium", "High", "High", "High");
            meltData["Group1"] = c(-1.1616334, -0.5991478, 0.8420797, 1.6225569, -0.3450745, 1.6025044, -1.2991011, -0.49064, 0.3897769);
            meltData["Group2"] = c(-0.5228371, -1.0461138, -1.5413266, -1.2706469, -1.3377985, 0.7631882, -0.2223622, -1.1802192, -0.3832142);
            meltData["Group3"] = c(-0.6587093, -0.1942979, 0.6318852, -0.8026467, 1.4988363, -0.5375833, -0.6321478, 0.1235253, 0.6671101);
            meltData["Group4"] = c(0.45064563, 2.47985577, -0.98948125, -0.32332181, 0.36541918, 0.85028148, -1.57284216, 0.09891793, 0.23407257);

            meltData.Print();

            Frame melted = meltData.Melt(
                c("FactorA", "FactorB"),
                c("Group1", "Group2")
            );

            melted.Print();
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double HighScore { get; set; }
    }
}
