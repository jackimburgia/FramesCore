# FramesCore

```
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

 // Use the ToColumn extension method to convert an IEnumerable to a Column (also works on arrays and lists)
 frame["HighScore"] = new double[] { 90.0, 92.0, 87.0}.ToColumn();
```
