# .Net Data Frames
The .Net Data Frames package is for performing exploratory data analysis using any .Net language.
The Frame and Column classes inherit from generic lists so all LINQ methods work.
All data is stored internally typed.

# Create a new frame and populating with data in C#

```csharp
using Spearing.Utilities.Data.Frames;
using static Spearing.Utilities.Data.Frames.FrameExtensions;
```

```csharp 
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
 frame["HighScore"] = new double[] { 90.0, 92.0, 87.0}.ToColumn();

```

# Displaying data

```csharp
 // Print all columns
 frame.Print();
```


```csharp
   Names               StartDate   Ages   LowScore   HighScore
     Bob   10/1/2016 12:00:00 AM     41         78          90
    Mary    6/8/2016 12:00:00 AM     28         81          92
     Joe    9/2/2017 12:00:00 AM     35         85          87
```


```csharp
 // Print selected columns
 frame.Print("Names", "HighScore");
```


```csharp
   Names   HighScore
     Bob          90
    Mary          92
     Joe          87
```


```csharp
 // Print a column
 frame["Names"].Print();
```


```csharp
   Names
     Bob
    Mary
     Joe
```







# Retrieving data

```csharp
// The As extension method retrieves the data with typing
// Make sure to use the type that the column was created with
double averageAge = frame["Ages"].As<double>().Average();
double totalScore = frame["HighScore"].As<double>().Sum();

double[] ages = frame["Ages"].As<double>().ToArray();

// Columns inherit from List<T>
List<double> lowScores = frame["LowScore"].As<double>();
```




# Column level calculations

```csharp
 frame["ScoreDiff"] = frame["HighScore"].As<double>() - frame["LowScore"].As<double>();
 frame["HighPlus1"] = frame["HighScore"].As<double>() + 1.0;

 frame["Hours"] = new double[] { 25, 30, 38 };
 double[] hourlyRate = new double[] { 15, 20, 12 };

 frame["Pay"] = frame["Hours"].As<double>() * hourlyRate;
```




# Saving frame data

```csharp
frame.SaveCsv(@"c:\temp\Employees.csv");
```




# Generic lists / collections

```csharp
 public class Employee
 {
     public string Name { get; set; }
     public int Age { get; set; }
     public double HighScore { get; set; }
 }
```


```csharp
 // Use the ToFrame method to convert a strongly typed collection to a frame
 List<Employee> employees = new List<Employee>()
 {
     new Employee() {Name = "Bob", Age = 40, HighScore = 90.0 },
     new Employee() {Name = "Mary", Age = 28, HighScore = 92.0 },
     new Employee() {Name = "Joe", Age = 35, HighScore = 87.0 }
 };

 Frame employeesFrame = employees.ToFrame();

 employeesFrame.Print();
```


```csharp
   Name   Age   HighScore
    Bob    40          90
   Mary    28          92
    Joe    35          87
```


# Filtering

```csharp
 // Use Linq predicates to filter data
 DateTime startDate = new DateTime(2016, 9, 1);
 Frame newEmployees = frame
     .Where(row => row.Get<DateTime>("StartDate") >= startDate)
     .ToFrame(); 

 newEmployees.Print();
```

```csharp
   Names               StartDate   Ages   LowScore   HighScore   ScoreDiff   HighPlus1   Hours   Pay
     Bob   10/1/2016 12:00:00 AM     41         78          90          12          91      25   375
     Joe    9/2/2017 12:00:00 AM     35         85          87           2          88      38   456
```







# Grouping

```csharp
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
```


```csharp
   Year   AverageAge   Count
   2016         34.5       2
   2017           35       1
```







# Populating a frame from a file

```csharp
 // Local file
 Frame employeesLocal = Frame.ReadCSV<string, DateTime, double, double, double, double, double>(@"c:\temp\Employees.csv");

 // Web site
 Frame employeesWeb = Frame.ReadCSV<string, DateTime, double, double, double, double, double>(@"http://www.spearing.com/files/Employees.csv");

 // Git web site
 Frame employeesGit = Frame.ReadCSV<string, DateTime, double, double, double, double, double>(@"https://raw.githubusercontent.com/jackimburgia/Frames/master/Employees.csv");
```


# Head and Tail methods

```csharp
 // Get the top rows of the frame
 IEnumerable<Row> headEmployees = employeesGit.Head();

 headEmployees.Print();
```

```csharp
   Names               StartDate   Ages   HighScore   LowScore   ScoreDiff   HighPlus1
     Bob   10/1/2016 12:00:00 AM     41          90         78          12          91
    Mary    6/8/2016 12:00:00 AM     28          92         81          11          93
     Joe    9/2/2017 12:00:00 AM     35          87         85           2          88
```

```csharp
 // Get the last rows of the frame
 IEnumerable<Row> tailEmployees = employeesGit.Tail();

 tailEmployees.Print();
```

```csharp
   Names               StartDate   Ages   HighScore   LowScore   ScoreDiff   HighPlus1
     Bob   10/1/2016 12:00:00 AM     41          90         78          12          91
    Mary    6/8/2016 12:00:00 AM     28          92         81          11          93
     Joe    9/2/2017 12:00:00 AM     35          87         85           2          88
```


# SQL like Inner Join
```csharp
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
```

6 of the 7 rows from the books frame match an author.
```csharp
       Name                           Title   Other.Author    Surname   Nationality   Deceased
      Tukey       Exploratory Data Analysis                     Tukey            US       True
   Venables   Modern Applied Statistics ...         Ripley   Venables     Australia      False
    Tierney                       LISP-STAT                   Tierney            US      False
     Ripley              Spatial Statistics                    Ripley            UK      False
     Ripley           Stochastic Simulation                    Ripley            UK      False
     McNeil       Interactive Data Analysis                    McNeil     Australia      False
```


# SQL like Outer Join

```csharp
// Perform a left "outer" style join
// Display all rows from left table and only the values from the right
//      table where the keys match
Frame outerJoin = books.OuterJoin(authors,
    row => row.Get<string>("Name"),
    row => row.Get<string>("Surname")
    );

outerJoin.Print();
```

All of the rows from the books frame are displayed.  "R Core" does not match but is still displayed.  Default values are displayed on that row on the authors side.
```csharp
       Name                           Title       Other.Author    Surname   Nationality   Deceased
      Tukey       Exploratory Data Analysis                         Tukey            US       True
   Venables   Modern Applied Statistics ...             Ripley   Venables     Australia      False
    Tierney                       LISP-STAT                       Tierney            US      False
     Ripley              Spatial Statistics                        Ripley            UK      False
     Ripley           Stochastic Simulation                        Ripley            UK      False
     McNeil       Interactive Data Analysis                        McNeil     Australia      False
     R Core            An Introduction to R   Venables & Smith                               False
```


# Joining on multiple keys
```csharp
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
```

```csharp
   State   Site   Latitude   Longitude   Region   Monitor   Parameter   Duration
      IL      1   42.46757   -87.81005       IL         1       ozone         1h
      IL      2   42.04915   -88.27303       IL         2       ozone         8h
      IL      2   42.04915   -88.27303       IL         2         no2         1h
      IN      1    41.6814   -87.49473       IN         1         so2         1h
```
              
# Melting frame data              
```csharp
Frame meltData = new Frame();
meltData["FactorA"] = c("Low", "Medium", "High", "Low", "Medium", "High", "Low", "Medium", "High");
meltData["FactorB"] = c("Low", "Low", "Low", "Medium", "Medium", "Medium", "High", "High", "High");
meltData["Group1"] = c(-1.1616334, -0.5991478, 0.8420797, 1.6225569, -0.3450745, 1.6025044, -1.2991011, -0.49064, 0.3897769);
meltData["Group2"] = c(-0.5228371, -1.0461138, -1.5413266, -1.2706469, -1.3377985, 0.7631882, -0.2223622, -1.1802192, -0.3832142);
meltData["Group3"] = c(-0.6587093, -0.1942979, 0.6318852, -0.8026467, 1.4988363, -0.5375833, -0.6321478, 0.1235253, 0.6671101);
meltData["Group4"] = c(0.45064563, 2.47985577, -0.98948125, -0.32332181, 0.36541918, 0.85028148, -1.57284216, 0.09891793, 0.23407257);

meltData.Print();
```

```csharp
   FactorA   FactorB       Group1       Group2       Group3        Group4
       Low       Low   -1.1616334   -0.5228371   -0.6587093    0.45064563
    Medium       Low   -0.5991478   -1.0461138   -0.1942979    2.47985577
      High       Low    0.8420797   -1.5413266    0.6318852   -0.98948125
       Low    Medium    1.6225569   -1.2706469   -0.8026467   -0.32332181
    Medium    Medium   -0.3450745   -1.3377985    1.4988363    0.36541918
      High    Medium    1.6025044    0.7631882   -0.5375833    0.85028148
       Low      High   -1.2991011   -0.2223622   -0.6321478   -1.57284216
    Medium      High     -0.49064   -1.1802192    0.1235253    0.09891793
      High      High    0.3897769   -0.3832142    0.6671101    0.23407257
```


```csharp
Frame melted = meltData.Melt(
    c("FactorA", "FactorB"), 
    c("Group1", "Group2")
);

melted.Print();
```

```csharp
   FactorA   FactorB   Variable        Value
       Low       Low     Group1   -1.1616334
    Medium       Low     Group1   -0.5991478
      High       Low     Group1    0.8420797
       Low    Medium     Group1    1.6225569
    Medium    Medium     Group1   -0.3450745
      High    Medium     Group1    1.6025044
       Low      High     Group1   -1.2991011
    Medium      High     Group1     -0.49064
      High      High     Group1    0.3897769
       Low       Low     Group2   -0.5228371
    Medium       Low     Group2   -1.0461138
      High       Low     Group2   -1.5413266
       Low    Medium     Group2   -1.2706469
    Medium    Medium     Group2   -1.3377985
      High    Medium     Group2    0.7631882
       Low      High     Group2   -0.2223622
    Medium      High     Group2   -1.1802192
      High      High     Group2   -0.3832142
```
