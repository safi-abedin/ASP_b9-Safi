
using Ado.Net;

var name = "";
var fees = 0;
var date = default(DateTime);

var ParameterMap = new Dictionary<string, object>();

ParameterMap.Add("name", name);
ParameterMap.Add("Fees", fees);
ParameterMap.Add("Date", date);


var connectionString = "";
DataUtility dataUtility = new DataUtility(connectionString);

var commandText = "";
dataUtility.WriteOperation(commandText,ParameterMap);
