//// The authentication token of an authenticated user.
//var authenticationToken = null;

//// Prototype for the authentication model view.
//function AuthenticationModelView() {
//    var self = this;
//    var $self = $("#login");
//    var $serverMessage = $self.find(".login-error");
//    self.username = ko.observable();
//    self.password = ko.observable();

//    self.beginLogin = function () {
//        // If cookie is present, do not ask for user's credentials.
//        var authCookie = $.cookie("Authorization-Token");
//        if (authCookie) {
//            var token = JSON.parse(authCookie);
        
//            // Verify if token is expired. We don't want the user to have an expired token, ever.
//            // So consider a token that will expire in 15 minutes as already expired.
//            var expiresAt = moment(token.expiresAt);
//            if (moment() > expiresAt.subtract("minute", 15)) {
//                // Token is expired. Continue with authentication.
//                $.cookie("Authorization-Token", null);
//            } else {
//                // Token is still valid, skip authentication.
//                authenticationToken = token;
//                self.endLogin(token);
//                return;
//            }
//        }

//        // Show the login modal.
//        $self.modal({ backdrop: "static", keyboard: false });
//        $self.modal("show");
//        $self.focus();

//        // Blur the content.
//        $("#wrapper").addClass("blurred");
//    }

//    self.login = function () {
//        // Try to do a no-op on the rest server.
//        // If the user's credentials are wrong, this will throw.
//        restCall(buildUrl(apiUrl, "anonymous/noop"),
//            operations.get(), buildKerberosAuthHeader(self.username(), self.password()))
//            .done(function (data, textStatus, jqxhr) {
//            // Succeeded.
//            self.endLogin({
//                token: jqxhr.getResponseHeader("Authorization-Token"),
//                emittedFor: self.username(),
//                emittedAt: moment(jqxhr.getResponseHeader("Authorization-Token-Emitted-At"), moment().ISO_8601),
//                expiresAt: moment(jqxhr.getResponseHeader("Authorization-Token-Expires-At"), moment().ISO_8601)
//            });
//        }).fail(function (jqhxr, textStatus, ex) {
//            self.onLoginError(createErrorObject(jqhxr, textStatus, ex));
//        });
//    }

//    self.endLogin = function (token) {
//        // Set the static authentication token.
//        authenticationToken = token;
//        $.cookie('Authorization-Token', JSON.stringify(authenticationToken));

//        // Hide the login modal.
//        $self.modal("hide");

//        // Remove the content's blur.
//        $("#wrapper").removeClass("blurred");

//        // TODO find where to put those.
//        var enumerationsModelView = new EnumerationsModelView();
//        var profilAvanceModelView = new ProfilAvanceModelView();
//        var profilModelView = new ProfilModelView(profilAvanceModelView, enumerationsModelView);
//        ko.applyBindings(profilModelView, $('#profil')[0]);
//        ko.applyBindings(profilAvanceModelView, $('#profil-avance')[0]);
//    }

//    self.onLoginError = function (error) {
//        // Set an error message.
//        $serverMessage.text(error.exceptionMessage);

//        // Restart login.
//        self.beginLogin();
//    }

//    // Force authentication.
//    self.beginLogin();
//}

//// Prototype for the profil model view. It inherits the base model view.
//function ProfilModelView(profilAvanceModelView, enumerationsModelView) {
//    // Redefine this for closures.
//    var self = this;
//    var $self = $("#profil");

//    // Observable properties.
//    self.prenom = ko.observable();
//    self.nom = ko.observable();
//    self.concentrationId = ko.observable();
//    self.public = ko.observable();
//    self.editInProgress = ko.observable();
//    self.availableConcentrations = ko.observableArray(enumerationsModelView.concentrations);

//    // Get the profil's concentration.
//    self.concentration = ko.computed(function () {
//        var con = availableConcentrations[self.concentrationId()];
//        return sprintf('%s - %s', con.acronyme, con.description);
//    });

//    self.beginEdit = function () {
//        $self.find(".panel-primary").removeClass("panel-primary").addClass("panel-warning");
//        self.editInProgress(true);
//    }

//    self.edit = function () {
//        restCall(buildUrl(apiUrl, authenticationToken.emittedFor, "profil"), operations.update(), buildTokenAuthHeader(), {
//            nom: self.nom(),
//            prenom: self.prenom(),
//            concentrationId: self.concentrationId(),
//            'public': self.public(),
//            actif: true,
//            profilAvance: profilAvanceModelView.getProfilAvance()
//        }).done(function () {
//            $self.find(".panel-warning").removeClass("panel-warning").addClass("panel-primary");
//            self.editInProgress(false);
//        }).fail(function (jqhxr, textStatus, ex) {
//            self.onEditError(createErrorObject(jqhxr, textStatus, ex));
//        });
//    }

//    self.cancelEdit = function () {
//        $self.find(".panel-warning").removeClass("panel-warning").addClass("panel-primary");
//        self.editInProgress(false);
//    }

//    self.onEditError = function (error) {
//        alert(JSON.stringify(error));
//    }

//    // Load all available concentrations.
//    // self.restCall(self.buildUrl(self.apiUrl, "enumeration/concentrations"), self.httpMethods.get(), self.buildTokenAuthHeader())
//    // .done(function (data, textStatus, jqxhr) {
//    //     var availableConcentrations = [];
//    //     for (var iConcentration = 0; iConcentration < data.length; iConcentration++) {
//    //         var concentration = data[iConcentration];
//    //         availableConcentrations.push({
//    //             id: concentration.id,
//    //             acronyme: concentration.entity.acronyme,
//    //             description: concentration.entity.description
//    //         });
//    //     }
//    //     self.availableConcentrations(availableConcentrations);
//    // });

//    // Load the profil of current user.
//    restCall(buildUrl(apiUrl, authenticationToken.emittedFor, "profil"), operations.get(), buildTokenAuthHeader())
//    .done(function (data, textStatus, jqxhr) {
//        self.prenom(data.prenom);
//        self.nom(data.nom);
//        self.concentrationId(data.concentrationId);
//        self.public(data.isPublic);
//        self.editInProgress(false);

//        profilAvanceModelView.codePermanent(data.profilAvance.codePermanent);
//        profilAvanceModelView.dateNaissance(data.profilAvance.dateNaissance);
//        profilAvanceModelView.telephone(data.profilAvance.telephone);
//        profilAvanceModelView.courriel(data.profilAvance.courriel);
//        profilAvanceModelView.public(data.profilAvance.public);
//        profilAvanceModelView.editInProgress(false);
//    });
//}

//// Prototype for the profil avance model view. It inherits the base model view.
//function ProfilAvanceModelView() {
//    var self = this;
//    var $self = $("#profil-avance");
//    self.codePermanent = ko.observable();
//    self.dateNaissance = ko.observable();
//    self.telephone = ko.observable();
//    self.courriel = ko.observable();
//    self.public = ko.observable();
//    self.editInProgress = ko.observable();

//    self.getProfilAvance = function() {
//        return {
//            codePermanent: self.codePermanent(),
//            dateNaissance: self.dateNaissance(),
//            telephone: self.telephone(),
//            courriel: self.courriel(),
//            'public': self.public()
//        };
//    }

//    self.beginEdit = function() {
//        self.editInProgress(true);
//    }

//    self.edit = function () {

//    }

//    self.cancelEdit = function () {
//        self.editInProgress(false);
//    }

//    self.onEditError = function() {
        
//    }
//}

//// Prototype for all enumerations model view. It inherits the base model view.
//function EnumerationsModelView() {
//    var self = this;

//    self.asStore = function(data) {
//        var store = {};
//        for (var iDatum = 0; iDatum < data.length; iDatum++) {
//            var datum = data[iDatum];
//            store[datum.id] = datum.entity;
//        }

//        return store;
//    }

//    // Load all enumerations.
//    restCall(self.buildUrl(apiUrl, "enumeration/concentrations"), operations.get(), buildTokenAuthHeader())
//    .done(function (data) {
//        self.concentrations = self.asStore(data);
//    });

//    restCall(self.buildUrl(apiUrl, "enumeration/statuts-suivies"), operations.get(), buildTokenAuthHeader())
//    .done(function (data) {
//        self.statusSuivie = self.asStore(data);
//    });

//    restCall(self.buildUrl(apiUrl, "enumeration/types-contacts"), operations.get(), buildTokenAuthHeader())
//    .done(function (data) {
//        self.typesContacts = self.asStore(data);
//    });

//    restCall(buildUrl(self.apiUrl, "enumeration/unites"), operations.get(), buildTokenAuthHeader())
//    .done(function (data) {
//        self.unites = self.asStore(data);
//    });
//}

//// Core document.ready().
//jQuery(function($) {
//    // Apply all knockout bindings.
//    // ko.applyBindings(new LoginModelView(), $('#login')[0]);
//});