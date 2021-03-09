# Call Rest Service Demonstration

This project was initially built to demonstrate how to call a REST service from C#, but has been built out more to serve a demonstration of a completed simple Code Louisville project.  As such, this project has been built out to provide the following functionality:

* Report the current temperature for an area
* Convert between Fahrenheit, Celsius, and kelvin

### Instructions

In order to run this application, you will need to perform the following steps:

1. Close Visual Studio if opened
2. Create a free account at https://home.openweathermap.org/users/sign_up
3. Confirm the account from the email sent from OpenWeather
4. Go to https://home.openweathermap.org/api_keys, and copy the OpenWeather API key
5. Create an environment variable named `OpenWeatherAPI` using the copied OpenWeather API as the value
6. Open Visual Studio and load the CallRestService solution
7. Run the application

Weather locations can be a ZIP code or a city.  When specifying a city and state, you must also specify the country.  For example, specify `Louisville, KY, US` instead of `Louisville, KY`.  But note that some cities (such as Louisville) works correctly if you simply enter `Louisville`.  This is API dependent.

Any errors (such as weather location not found) will be recorded within the `bin` folder of the application in the `Error.log` file.

### Code Louisville Feature Matrix

- [x] Implement a “master loop” console application where the user can repeatedly enter commands/perform actions, including choosing to exit the program
- [ ] Create an additional class which inherits one or more properties from its parent
- [ ] Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program
- [x] Implement a log that records errors, invalid inputs, or other important events and writes them to a text file
- [ ] Read data from an external file, such as text, JSON, CSV, etc and use that data in your application
- [ ] Implement a regular expression (regex) to ensure a field either a phone number or an email address is always stored and displayed in the same format
- [x] Connect to an external/3rd party API and read data into your app
- [x] Use a LINQ query to retrieve information from a data structure (such as a list or array) or file
- [ ] Create 3 or more unit tests for your application
- [x] Build a conversion tool that converts user input to another type and displays it (ex: converts cups to grams)
- [ ] Calculate and display data based on an external factor (ex: get the current date, and display how many days remaining until some event)
- [ ] Analyze text and display information about it (ex: how many words in a paragraph)
- [ ] Visualize data in a graph, chart, or other visual representation of data
