var IsLogHelperLoaded = true;

var Loghelper = {
    HandleException: function (methodName, exception) {
        try {
            console.log(exception.message + "<hr />" + exception.stack);
            Loghelper.SendReport(methodName, exception);
        }
        catch (ex) {
            console.log(ex.message);
        }

    },
    TraceError: function (Exception) {

    },
    SendReport: function (methodName, exception) {
        try {
            //incase you need to send report to webservices
        }
        catch (ex) {

        }

    }
}