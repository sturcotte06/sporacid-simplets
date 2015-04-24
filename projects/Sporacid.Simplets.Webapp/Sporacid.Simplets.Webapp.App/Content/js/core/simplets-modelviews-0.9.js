// The authentication token of an authenticated user.
var authenticationToken = null;

// The rights of the user per module.
var principalRights = null;

// The stores underlying data for immutable collections.
var storeData = {};

// The stores for immutable collections.
var stores = {};

jQuery(function($) {
    // Load concentrations as a data store.
    storeData.concentrations = ko.observableArray();
    restCall(buildUrl(apiUrl, "enumeration/concentrations"), operations.get()).done(function(concentrations) {
        $.each(concentrations, function(i, concentration) {
            concentration.toString = function() { return sprintf("%s - %s", concentration.acronyme, concentration.description); };
            storeData.concentrations().push(concentration);
        });

        stores.concentrations = concentrations.asStore();
    }).invoke();

    // Load contacts types as a data store.
    storeData.typesContacts = ko.observableArray();
    restCall(buildUrl(apiUrl, "enumeration/types-contacts"), operations.get()).done(function(typesContacts) {
        $.each(typesContacts, function(typeContact, i) {
            typeContact.toString = function() { return typeContact.nom; };
            storeData.typesContacts().push(typeContact);
        });

        stores.typesContacts = typesContacts.asStore();
    }).invoke();

    // Load statuts suivies as a data store.
    storeData.statutsSuivies = ko.observableArray();
    restCall(buildUrl(apiUrl, "enumeration/statuts-suivies"), operations.get()).done(function(statutsSuivies) {
        $.each(statutsSuivies, function(statutSuivie) {
            statutSuivie.toString = function() { return sprintf("%s - %s", statutSuivie.code, statutSuivie.description); };
            storeData.statutsSuivies().push(statutSuivie);
        });

        stores.statutsSuivies = statutsSuivies.asStore();
    }).invoke();

    // Load unites as a data store.
    storeData.unites = ko.observableArray();
    restCall(buildUrl(apiUrl, "enumeration/unites"), operations.get()).done(function(unites) {
        $.each(unites, function(unite) {
            unite.toString = function() { return sprintf("%s - %s", unite.systeme, unite.code); };
            storeData.statutsSuivies().push(unite);
        });

        stores.unites = unites.asStore();
    }).invoke();

    // Load commanditaire types as a data store.
    storeData.typesCommanditaires = ko.observableArray();
    restCall(buildUrl(apiUrl, "enumeration/types-commanditaires"), operations.get()).done(function (typesCommanditaires) {
        $.each(typesCommanditaires, function (typeCommanditaire, i) {
            typeCommanditaire.toString = function () { return typeCommanditaire.nom; };
            storeData.typesCommanditaires().push(typeCommanditaire);
        });

        stores.typesCommanditaires = typesCommanditaires.asStore();
    }).invoke();
});

// Model view for the base profil object.
function ProfilBaseModelView($self, validationModelView) {
    // Define closure safe properties.
    var self = this;

    // Define all onservable properties.
    self.viewmode = ko.observable(viewmodes.view());
    self.concentrations = storeData.concentrations;
    self.profil = ko.observable();
    self.concentration = ko.observable();

    // Define all jquery selectors.
    var $panel = $self.parents(".panel").first();

    // Begin edition of base profil entity.
    self.beginEdit = function() {
        // Switch to edition mode.
        self.viewmode(viewmodes.edition());
        $panel.removeClass("panel-primary").addClass("panel-warning");
    };

    // Commits the edited properties of the base profil entity.
    self.edit = function() {
        // Deactivate the view.
        $panel.waiting();

        // Update the profil entity.
        restCall(buildUrl(apiUrl, authenticationToken.emittedFor, "profil"), operations.update(), buildTokenAuthHeader(), self.profil()).done(function() {
            self.profil.valueHasMutated();
            self.endEdit();
        }).fail(function(jqhxr, textStatus, ex) {
            self.error(createErrorObject(jqhxr, textStatus, ex));
        }).invoke();
    };

    // Cancels the edition of profil entity.
    self.cancelEdit = function() {
        self.endEdit();
    };

    // Ends the edition of profil entity.
    self.endEdit = function() {
        // Clear valdiation messages.
        validationModelView.clear();

        // Switch to view mode.
        self.viewmode(viewmodes.view());
        $panel.removeClass("panel-warning").addClass("panel-primary");

        // Reactivate the view.
        $panel.active();

        // Update commandite manually.
        if (self.profil().concentrationId) {
            self.concentration(stores.concentrations[self.profil().concentrationId]);
        }

    };

    // Manages an error that occured while managing the profil entity.
    self.error = function(error) {
        if (error.httpStatus == 400) {
            // Bad request: validation error.
            validationModelView.load(error.response);
        }

        // Reactivate the view.
        $panel.active();
    };

    // Loads the profil entity from the rest services.
    self.load = function() {
        // Deactivate the view.
        $panel.waiting();

        // Load the profil entity.
        restCall(buildUrl(apiUrl, authenticationToken.emittedFor, "profil"), operations.get(), buildTokenAuthHeader()).done(function(profil) {
            self.profil(profil);
            if (profil.concentrationId) {
                self.concentration(stores.concentrations[profil.concentrationId]);
            }


            if (profil.avatar) {
                $self.find(".profil-avatar").each(function() {
                    $(this).css("background-image", "url(data:image/jpg;base64," + profil.avatar + ")");
                });
            }


            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();
    };

    // Load the entity.
    self.load();
}

// Model view for the profil avance object.
function ProfilAvanceModelView($self, validationModelView) {
    // Define closure safe properties.
    var self = this;

    // Define all onservable properties.
    self.viewmode = ko.observable(viewmodes.view());
    self.profilAvance = ko.observable();

    // Define all jquery selectors.
    var $panel = $self.parents(".panel").first();

    // Begin edition of profil entity.
    self.beginEdit = function() {
        // Switch to edition mode.
        self.viewmode(viewmodes.edition());
        $panel.removeClass("panel-primary").addClass("panel-warning");
    };

    // Commits the edited properties of the profil entity.
    self.edit = function() {
        // Deactivate the view.
        $panel.waiting();

        // The separation between profil and profil avance is logical only.
        // This means the profil is the aggregation of both. Before posting the profil avance, reload
        // the base profil. If it has not changed, this will reload the cached value anyway.
        restCall(buildUrl(apiUrl, authenticationToken.emittedFor, "profil"), operations.get(), buildTokenAuthHeader()).done(function(profil) {
            profil.profilAvance = self.profilAvance();

            // Update the profil entity.
            restCall(buildUrl(apiUrl, authenticationToken.emittedFor, "profil"), operations.update(), buildTokenAuthHeader(), profil).done(function() {
                self.endEdit();
                self.profilAvance.valueHasMutated();
            }).fail(function(jqhxr, textStatus, ex) {
                self.error(createErrorObject(jqhxr, textStatus, ex));
            }).invoke();
        }).fail(function(jqhxr, textStatus, ex) {
            self.error(createErrorObject(jqhxr, textStatus, ex));
        }).invoke();
    };

    // Cancels the edition of profil entity.
    self.cancelEdit = function() {
        self.endEdit();
    };

    // Ends the edition of profil entity.
    self.endEdit = function() {
        // Clear valdiation messages.
        validationModelView.clear();

        // Switch to view mode.
        self.viewmode(viewmodes.view());
        $panel.removeClass("panel-warning").addClass("panel-primary");

        // Reactivate the view.
        $panel.active();
    };

    // Manages an error that occured while managing the profil entity.
    self.error = function(error) {
        // Reactivate the view.
        $panel.active();

        if (error.httpStatus == 400) {
            // Bad request == validation error.
            validationModelView.load(error.response);
        } else {
            // Other error, cannot handle.
            throw error;
        }
    };

    // Loads the profil entity from the rest services.
    self.load = function() {
        // Deactivate the view.
        $panel.waiting();

        // Load the profil entity.
        restCall(buildUrl(apiUrl, authenticationToken.emittedFor, "profil"), operations.get(), buildTokenAuthHeader()).done(function(profil) {
            // The separation between profil and profil avance is logical only.
            self.profilAvance(profil.profilAvance);
            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();
    };

    // Load the entity.
    self.load();
}

// Prototype for a validation exception model view.
function ValidationExceptionModelView($self) {
    // Define closure safe properties.
    var self = this;

    // Define all onservable properties.
    self.message = ko.observable();
    self.states = ko.observableArray();

    // Load all validation messages.
    self.load = function(validation) {
        var states = [];
        for (var key in validation.modelState) {
            var propertyStates = validation.modelState[key];
            for (var iState = 0; iState < propertyStates.length; iState++) {
                states.push({ property: key, message: propertyStates[iState] });
            }
        }

        // Refresh observables.
        self.message(validation.message);
        self.states(states);
    };
    self.clear = function() {
        self.message = ko.observable("");
        self.states.removeAll();
    };
}

// Prototype for the authentication model view.
function LoginModelView($self) {
    // Define closure safe properties.
    var self = this;

    // Define all onservable properties.
    self.username = ko.observable();
    self.password = ko.observable();

    // Define all jquery selectors.
    var $loginError = $self.find(".login-error");
    var $modal;

    self.beginLogin = function() {
        // Hide the main content wrapper while authenticating the user.
        $("#page-wrapper").hide();

        // If cookie is present, do not ask for user's credentials.
        var authCookie = $.cookie("Authorization-Token");
        if (authCookie && authCookie !== "null" && authCookie !== "undefined") {
            // Try to log in with the token.
            var token = JSON.parse(authCookie);
            restCall(buildUrl(apiUrl, "no-op"), operations.get(), buildTokenAuthHeader(token.token)).done(function() {
                // Worked, token is still valid.
                self.endLogin(token);
            }).fail(function() {
                // Failed, force reauthentication.
                $.cookie("Authorization-Token", null, { path: "/" });
                self.beginLogin();
            }).invoke();
        } else {
            // Blur the content.
            $("#wrapper").addClass("blurred");

            // Show the login modal.
            $self.modal({ backdrop: "static", keyboard: false });
            $self.modal("show");
            $modal = $self.find(".modal-dialog");
            $self.focus();
        }
    };

    self.login = function() {
        // Deactivate the view.
        $modal.waiting();

        // Try to do a no-op on the rest server.
        // If the user's credentials are wrong, this will throw.
        restCall(buildUrl(apiUrl, "no-op"), operations.get(), buildKerberosAuthHeader(self.username(), self.password())).done(function(data, textStatus, jqxhr) {
            // Succeeded. Extract the authentication token for further calls to the api and end the login procedure.
            self.endLogin({
                token: jqxhr.getResponseHeader("Authorization-Token"),
                emittedFor: self.username(),
                emittedAt: moment(jqxhr.getResponseHeader("Authorization-Token-Emitted-At"), moment().ISO_8601),
                expiresAt: moment(jqxhr.getResponseHeader("Authorization-Token-Expires-At"), moment().ISO_8601)
            });
        }).fail(function(jqhxr, textStatus, ex) {
            self.error(createErrorObject(jqhxr, textStatus, ex));
        }).invoke();
    };

    self.endLogin = function(token) {
        // Reactivate the view.
        if ($modal) $modal.active();

        // Set the static authentication token.
        authenticationToken = token;
        $.cookie("Authorization-Token", JSON.stringify(authenticationToken), { path: "/" });

        // Get the principal's rights.
        restCall(buildUrl(apiUrl, authenticationToken.emittedFor, "claims"), operations.get(), buildTokenAuthHeader()).done(function(rights) {
            principalRights = rights;
        });

        // Hide the login modal.
        $self.modal("hide");

        // Remove the content's blur.
        $("#page-wrapper").show();
        $("#wrapper").removeClass("blurred");

        // Trigger a new event to wake up model views waiting on this event.
        $self.trigger("logged-in");
    };

    self.error = function(error) {
        // Reactivate the view.
        if ($modal) $modal.active();

        // Set an error message.
        $loginError.text(error.response.exceptionMessage);

        // Restart login.
        self.beginLogin();
    };

    // Force authentication.
    self.beginLogin();
}

// Model view for the base profil object.
function MembersModelView($self) {
    // Define closure safe properties.
    var self = this;
    var $panel = $self.parents(".panel").first();

    self.membres = ko.observableArray();
    self.activeIndex = ko.observableArray();

    // Loads the profil entity from the rest services.
    self.load = function() {
        // Deactivate the view.
        $panel.waiting();

        // Load the profilList entity.
        restCall(buildUrl(apiUrl, "demandespecial/membre"), operations.get(), buildTokenAuthHeader()).done(function(membres) {

            $.each(membres, function() {
                this.nom = sprintf("%s %s", this.profilPublic.prenom, this.profilPublic.nom);
                this.avatar = this.profilPublic.avatar ? sprintf("data:image/jpg;base64,%s", this.profilPublic.avatar) : "http://placehold.it/80";
            });

            self.membres(membres);

            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();
    };


// Load the entity.
    self.load();

}

// Model view for the commanditaire object.
function CommanditaireListModelView($self, validationModelView) {
    // Define closure safe properties.
    var self = this;

    self.commanditairesList = ko.observableArray();

    // Loads the profil entity from the rest services.
    self.load = function () {
        // Deactivate the view.
        $panel.waiting();

        // Load the profilList entity.
        // ***GAGF url à déterminer, doit fournir le nom du club (dropdown)
        restCall(buildUrl(apiUrl, "nomDuClub", "commanditaire"), operations.get(), buildTokenAuthHeader()).done(function (commanditairesList) {
            self.commanditairesList(commanditairesList);

            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();
    };

    // Load the entity.
    self.load();

}