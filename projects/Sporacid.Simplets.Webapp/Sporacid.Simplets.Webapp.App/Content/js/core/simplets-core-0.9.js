// The authentication token of an authenticated user.
var authenticationToken = null;

// Base prototype for knockout model views.
function BaseModelView() {
    var self = this;

    // Base url for the rest api.
    self.apiUrl = "http://localhost/services/api/v1/";

    // Utility function for rest api calls.
    self.restCall = function (uri, method, auth, data) {
        var request = {
            url: uri,
            type: method,
            contentType: "application/json",
            accepts: "application/json",
            cache: false,
            dataType: 'json',
            data: JSON.stringify(data),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", auth);
            }
        };

        return $.ajax(request);
    }

    // Utility function to build the authorization header for kerberos authentication.
    self.buildKerberosAuthHeader = function (username, password) {
        return "Kerberos " + btoa(username + ":" + password);
    }

    // Utility function to build the authorization header for token authentication.
    // If the token is in the cookie, this method can be called without parameter.
    self.buildTokenAuthHeader = function (token) {
        token = token ? btoa(token) : authenticationToken.token;
        return "Token " + token;
    }

    // Utility function to throw a rest exception.
    self.throwRestException = function (jqhxr, textStatus, exception) {
        var errorObject = self.createErrorObject(jqhxr, textStatus, exception);
        throw errorObject;
        // throw "[" + errorObject.httpStatus + ": " + errorObject.httpStatusText + "] " +
        //         errorObject.exceptionType + ": " + errorObject.exceptionMessage;
    }

    // Utility function to create an error object from ajax' error parameters.
    self.createErrorObject = function (jqhxr, textStatus, exception) {
        var exceptionObject = JSON.parse(jqhxr.responseText);
        return {
            httpStatus: jqhxr.status,
            httpStatusText: textStatus,
            exceptionType: exceptionObject["ExceptionType"],
            exceptionMessage: exceptionObject["ExceptionMessage"]
        };
    }

    // Utility function to build a rest resource url.
    self.buildUrl = function () {
        var url = "";
        var urlParts = arguments;
        for (var iUrlPart = 0; iUrlPart < urlParts.length; iUrlPart++) {
            var urlPart = urlParts[iUrlPart];
            if (urlPart[0] === "/") {
                urlPart.substring(1);
            }

            if (urlPart[urlPart.length - 1] !== "/" && iUrlPart < arguments.length - 1) {
                urlPart += "/";
            }

            url += urlPart;
        }

        return url;
    }

    // Enumeration
    self.httpMethods = {
        "get": function() {
            return "GET";
        },
        "post": function () {
            return "POST";
        },
        "put": function () {
            return "PUT";
        },
        "delete": function () {
            return "DELETE";
        }
    };
}

// Prototype for the authentication model view. It inherits the base model view.
LoginModelView.prototype = new BaseModelView();
function LoginModelView() {
    var self = this;
    var $self = $("#login");
    var $serverMessage = $self.find(".login-error");
    self.username = ko.observable();
    self.password = ko.observable();

    self.beginLogin = function () {
        // // If cookie is present, do not ask for user's credentials.
        // var authCookie = $.cookie('Authorization-Token');
        // if (authCookie) {
        //     // Todo check token validity (emittedAt and validFor).
        //     var token = JSON.parse(authCookie);
        // 
        //     // Verify if token is expired.
        //     // var emittedAt = Date.parse(token.emittedAt);
        //     // var validFor = Date.parse(token.validFor);
        // 
        //     // authenticationToken = token;
        //     // 
        //     // ko.applyBindings(new ProfilModelView(), $('#profil')[0]);
        //     self.endLogin(token);
        //     return;
        // }

        // Show the login modal.
        $self.modal({ backdrop: "static", keyboard: false });
        $self.modal("show");
        $self.focus();

        // Blur the content.
        $("#wrapper").addClass("blurred");
    }

    self.login = function () {
        // Try to do a no-op on the rest server.
        // If the user's credentials are wrong, this will throw.
        self.restCall(self.buildUrl(self.apiUrl, "anonymous/noop"),
            self.httpMethods.get(), self.buildKerberosAuthHeader(self.username(), self.password()))
            .done(function (data, textStatus, jqxhr) {
            // Succeeded.
            self.endLogin({
                token: jqxhr.getResponseHeader("Authorization-Token"),
                emittedFor: self.username(),
                emittedAt: jqxhr.getResponseHeader("Authorization-Token-Emitted-At"),
                validFor: jqxhr.getResponseHeader("Authorization-Token-Valid-For")
            });
        }).fail(function (jqhxr, textStatus, ex) {
            self.onLoginError(self.createErrorObject(jqhxr, textStatus, ex));
        });
    }

    self.endLogin = function (token) {
        // Set the static authentication token.
        authenticationToken = token;
        $.cookie('Authorization-Token', JSON.stringify(authenticationToken));

        // Hide the login modal.
        $self.modal("hide");

        // Remove the content's blur.
        $("#wrapper").removeClass("blurred");

        // TODO find where to put those.
        var enumerationsModelView = new EnumerationsModelView();
        var profilAvanceModelView = new ProfilAvanceModelView();
        var profilModelView = new ProfilModelView(profilAvanceModelView, enumerationsModelView);
        ko.applyBindings(profilModelView, $('#profil')[0]);
        ko.applyBindings(profilAvanceModelView, $('#profil-avance')[0]);
    }

    self.onLoginError = function (error) {
        // Set an error message.
        $serverMessage.text(error.exceptionMessage);

        // Restart login.
        self.beginLogin();
    }

    // Force authentication.
    self.beginLogin();
}

// Prototype for the profil model view. It inherits the base model view.
ProfilModelView.prototype = new BaseModelView();
function ProfilModelView(profilAvanceModelView, enumerationsModelView) {
    // Redefine this for closures.
    var self = this;
    var $self = $("#profil");

    // Observable properties.
    self.prenom = ko.observable();
    self.nom = ko.observable();
    self.concentrationId = ko.observable();
    self.public = ko.observable();
    self.editInProgress = ko.observable();
    self.availableConcentrations = enumerationsModelView.concentrations;

    // Get the profil's concentration.
    self.concentration = function () {
        var concentration = enumerationsModelView.concentrations[self.concentrationId()];
        return sprintf('%s - %s', concentration.acronyme, concentration.description);
        // return enumerationsModelView.concentrations[self.concentrationId()];
    };

    self.beginEdit = function () {
        $self.find(".panel-primary").removeClass("panel-primary").addClass("panel-warning");
        self.editInProgress(true);
    }

    self.edit = function () {
        self.restCall(self.buildUrl(self.apiUrl, authenticationToken.emittedFor, "profil"), self.httpMethods.put(), self.buildTokenAuthHeader(), {
            nom: self.nom(),
            prenom: self.prenom(),
            concentrationId: self.concentrationId(),
            'public': self.public(),
            actif: true,
            profilAvance: profilAvanceModelView.getProfilAvance()
        }).done(function () {
            $self.find(".panel-warning").removeClass("panel-warning").addClass("panel-primary");
            self.editInProgress(false);
        }).fail(function (jqhxr, textStatus, ex) {
            self.onEditError(self.createErrorObject(jqhxr, textStatus, ex));
        });
    }

    self.cancelEdit = function () {
        $self.find(".panel-warning").removeClass("panel-warning").addClass("panel-primary");
        self.editInProgress(false);
    }

    self.onEditError = function (error) {
        alert(JSON.stringify(error));
    }

    // Load all available concentrations.
    // self.restCall(self.buildUrl(self.apiUrl, "enumeration/concentrations"), self.httpMethods.get(), self.buildTokenAuthHeader())
    // .done(function (data, textStatus, jqxhr) {
    //     var availableConcentrations = [];
    //     for (var iConcentration = 0; iConcentration < data.length; iConcentration++) {
    //         var concentration = data[iConcentration];
    //         availableConcentrations.push({
    //             id: concentration.id,
    //             acronyme: concentration.entity.acronyme,
    //             description: concentration.entity.description
    //         });
    //     }
    //     self.availableConcentrations(availableConcentrations);
    // });

    // Load the profil of current user.
    self.restCall(self.buildUrl(self.apiUrl, authenticationToken.emittedFor, "profil"), self.httpMethods.get(), self.buildTokenAuthHeader())
    .done(function (data, textStatus, jqxhr) {
        self.prenom(data.prenom);
        self.nom(data.nom);
        self.concentrationId(data.concentrationId);
        self.public(data.isPublic);
        self.editInProgress(false);

        profilAvanceModelView.codePermanent(data.profilAvance.codePermanent);
        profilAvanceModelView.dateNaissance(data.profilAvance.dateNaissance);
        profilAvanceModelView.telephone(data.profilAvance.telephone);
        profilAvanceModelView.courriel(data.profilAvance.courriel);
        profilAvanceModelView.public(data.profilAvance.public);
        profilAvanceModelView.editInProgress(false);
    });
}

// Prototype for the profil avance model view. It inherits the base model view.
ProfilAvanceModelView.prototype = new BaseModelView();
function ProfilAvanceModelView() {
    var self = this;
    var $self = $("#profil-avance");
    self.codePermanent = ko.observable();
    self.dateNaissance = ko.observable();
    self.telephone = ko.observable();
    self.courriel = ko.observable();
    self.public = ko.observable();
    self.editInProgress = ko.observable();

    self.getProfilAvance = function() {
        return {
            codePermanent: self.codePermanent(),
            dateNaissance: self.dateNaissance(),
            telephone: self.telephone(),
            courriel: self.courriel(),
            'public': self.public()
        };
    }

    self.beginEdit = function() {
        self.editInProgress(true);
    }

    self.edit = function () {

    }

    self.cancelEdit = function () {
        self.editInProgress(false);
    }

    self.onEditError = function() {
        
    }
}

// Prototype for all enumerations model view. It inherits the base model view.
EnumerationsModelView.prototype = new BaseModelView();
function EnumerationsModelView() {
    var self = this;

    self.asStore = function(data) {
        var store = {};
        for (var iDatum = 0; iDatum < data.length; iDatum++) {
            var datum = data[iDatum];
            store[datum.id] = datum.entity;
        }

        return store;
    }

    // Load all enumerations.
    self.restCall(self.buildUrl(self.apiUrl, "enumeration/concentrations"), self.httpMethods.get(), self.buildTokenAuthHeader())
    .done(function (data) {
        self.concentrations = self.asStore(data);
    });

    self.restCall(self.buildUrl(self.apiUrl, "enumeration/statuts-suivies"), self.httpMethods.get(), self.buildTokenAuthHeader())
    .done(function (data) {
        self.statusSuivie = self.asStore(data);
    });

    self.restCall(self.buildUrl(self.apiUrl, "enumeration/types-contacts"), self.httpMethods.get(), self.buildTokenAuthHeader())
    .done(function (data) {
        self.typesContacts = self.asStore(data);
    });

    self.restCall(self.buildUrl(self.apiUrl, "enumeration/unites"), self.httpMethods.get(), self.buildTokenAuthHeader())
    .done(function (data) {
        self.unites = self.asStore(data);
    });
}

// Core document.ready().
jQuery(function($) {
    // Apply all knockout bindings.
    ko.applyBindings(new LoginModelView(), $('#login')[0]);
});