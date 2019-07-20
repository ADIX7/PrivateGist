#!/usr/bin/env dotnet-script

#r "../../bin/Debug/netcoreapp3.0/PrivateGist.dll"

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using PrivateGist.Hubs;

public static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);
public static string GetScriptFile([CallerFilePath] string path = null) => Path.GetFileName(path);


var folderName = GetScriptFolder();
var fileName = GetScriptFile();

var targetFileName = Path.Combine(folderName, fileName.Replace(".csx", ".js"));
var Output = new StreamWriter(targetFileName);
Output.WriteLine(@"import { Observable, Subject } from 'rxjs';

export default {
    initializeSignalRHandlers: function (signalRConnection) {

        let observables = {};
        let methods = {};");
var serverType = typeof(IApiHubServerFunctions);

foreach (var method in serverType.GetMethods())
{
    var methodName = method.Name.Substring(3);
    methodName = methodName.Substring(0, methodName.Length - 5);
    var methodNameCamel = methodName[0].ToString().ToLower() + methodName.Substring(1);

    var parameters = string.Join(", ", method.GetParameters().Select(p => p.Name));

    Output.WriteLine($@"
        //==={method.Name} {string.Join(" ", method.GetParameters().Select(p => p.ParameterType.ToString() + " " + p.Name))}===
        let {methodNameCamel}Subject = new Subject();

        signalRConnection.on(""{methodName}"", data => {{
            {methodNameCamel}Subject.next(data);
        }});

        methods.get{methodName} = function ({parameters}) {{
            signalRConnection.invoke(""Get{methodName}Async"", {parameters});
        }}

        observables.{methodNameCamel} = {methodNameCamel}Subject;
        //==={method.Name} END===
");
}

Output.WriteLine(@"
        return {
            observables: observables,
            methods: methods,
            getObservable: handlerName => observables[handlerName],
            signalRConnection: signalRConnection
        };
    }
}");
Output.Close();