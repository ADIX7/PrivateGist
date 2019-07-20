import { Observable, Subject } from 'rxjs';

export default {
    initializeSignalRHandlers: function (signalRConnection) {

        let observables = {};
        let methods = {};

        //===GetGistsByUserAsync System.String userName===
        let gistsByUserSubject = new Subject();

        signalRConnection.on("GistsByUser", data => {
            gistsByUserSubject.next(data);
        });

        methods.getGistsByUser = function (userName) {
            signalRConnection.invoke("GetGistsByUserAsync", userName);
        }

        observables.gistsByUser = gistsByUserSubject;
        //===GetGistsByUserAsync END===


        return {
            observables: observables,
            methods: methods,
            getObservable: handlerName => observables[handlerName],
            signalRConnection: signalRConnection
        };
    }
}
