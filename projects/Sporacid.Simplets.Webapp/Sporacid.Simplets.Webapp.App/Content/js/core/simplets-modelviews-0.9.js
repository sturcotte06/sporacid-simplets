// The authentication token of an authenticated user.
var authenticationToken = null;

// The preferences of the current authenticated user.
var currentUserPreferences = null;

// The current club context of the current authenticated user.
var currentClubContext = null;

// The current club context claims of the current authenticated user.
var currentClubContextClaims = null;

// The stores underlying data for immutable collections.
var storeData = {};

// The stores for immutable collections.
var stores = {};

jQuery(function($) {
    // Load concentrations as a data store.
    storeData.concentrations = ko.observableArray();
    api.enumerations.concentrations().done(function(concentrations) {
        $.each(concentrations, function(i, concentration) {
            concentration.toString = function() { return sprintf("%s - %s", concentration.acronyme, concentration.description); };
            storeData.concentrations().push(concentration);
        });

        stores.concentrations = asStore(concentrations);
    }).invoke();

    // Load contacts types as a data store.
    storeData.typesContacts = ko.observableArray();
    api.enumerations.typesContacts().done(function(typesContacts) {
        $.each(typesContacts, function(i, typeContact) {
            typeContact.toString = function() { return typeContact.nom; };
            storeData.typesContacts().push(typeContact);
        });

        stores.typesContacts = asStore(typesContacts);
    }).invoke();

    // Load statuts suivies as a data store.
    storeData.statutsSuivies = ko.observableArray();
    api.enumerations.statutsSuivies().done(function(statutsSuivies) {
        $.each(statutsSuivies, function(i, statutSuivie) {
            statutSuivie.toString = function() { return sprintf("%s - %s", statutSuivie.code, statutSuivie.description); };
            storeData.statutsSuivies().push(statutSuivie);
        });

        stores.statutsSuivies = asStore(statutsSuivies);
    }).invoke();

    // Load unites as a data store.
    storeData.unites = ko.observableArray();
    api.enumerations.unites().done(function(unites) {
        $.each(unites, function(i, unite) {
            unite.toString = function() { return sprintf("%s - %s", unite.systeme, unite.code); };
            storeData.statutsSuivies().push(unite);
        });

        stores.unites = asStore(unites);
    }).invoke();

    // Load commanditaire types as a data store.
    storeData.typesCommanditaires = ko.observableArray();
    api.enumerations.typesCommanditaires().done(function(typesCommanditaires) {
        $.each(typesCommanditaires, function(i, typeCommanditaire) {
            typeCommanditaire.toString = function() { return typeCommanditaire.nom; };
            storeData.typesCommanditaires().push(typeCommanditaire);
        });

        stores.typesCommanditaires = asStore(typesCommanditaires);
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
        api.userspace.profil.update(authenticationToken.emittedFor, self.profil()).done(function() {
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
        // Clear validation messages.
        validationModelView.clear();

        // Switch to view mode.
        self.viewmode(viewmodes.view());
        $panel.removeClass("panel-warning").addClass("panel-primary");

        // Reactivate the view.
        $panel.active();

        // Try to get the concentration of the profil.
        self.concentration(self.profil().concentrationId ? stores.concentrations[self.profil().concentrationId] : null);

        // Set either the user avatar or a placeholder everywhere it's required.
        var avatarImageSrc = profil.avatar ? sprintf("url(data:image/jpg;base64,%s)", profil.avatar) : "url(/app/Content/img/avatar-placeholder.png)";
        $self.find(".profil-avatar").each(function() {
            $(this).css("background-image", avatarImageSrc);
        });
    };

    // Manages an error that occured while managing the profil entity.
    self.error = function(error) {
        if (error.httpStatus === 400) {
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
        api.userspace.profil.get(authenticationToken.emittedFor).done(function(profil) {
            self.profil(profil);

            // Try to get the concentration of the user.
            self.concentration(profil.concentrationId ? stores.concentrations[profil.concentrationId] : null);

            // Set either the user avatar or a placeholder everywhere it's required.
            var avatarImageSrc = profil.avatar
                ? sprintf("url(%s)", buildImageDataUri(profil.avatar))
                : "url(/app/Content/img/avatar-placeholder.png)";
            $self.find(".profil-avatar").each(function() {
                $(this).css("background-image", avatarImageSrc);
            });

            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();
    }();
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
        api.userspace.profil.get(authenticationToken.emittedFor).done(function(profil) {
            profil.profilAvance = self.profilAvance();
            // Update the profil entity.
            api.userspace.profil.update(authenticationToken.emittedFor, profil).done(function() {
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

        if (error.httpStatus === 400) {
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
        api.userspace.profil.get(authenticationToken.emittedFor).done(function(profil) {
            // The separation between profil and profil avance is logical only.
            self.profilAvance(profil.profilAvance);
            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();
    }();
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
            if (validation.modelState.hasOwnProperty(key)) {
                var propertyStates = validation.modelState[key];
                for (var iState = 0; iState < propertyStates.length; iState++) {
                    states.push({ property: key, message: propertyStates[iState] });
                }
            }
        }

        // Refresh observables.
        self.message(validation.message);
        self.states(states);
    };

    // Clear all validation messages.
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
            api.utility.noop(buildTokenAuthHeader(token.token)).done(function() {
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
        api.utility.noop(buildKerberosAuthHeader(self.username(), self.password())).done(function(data, textStatus, jqxhr) {
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

        // Load the current user preferences
        api.userspace.preferences.getAll(authenticationToken.emittedFor).done(function(preferences) {
            currentUserPreferences = preferences;
        }).invoke();

        // Hide the login modal.
        $self.modal("hide");

        // Remove the content's blur.
        $("#page-wrapper").show();

        // Blur the content.
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

function FirstLoginModelView($self) {
    var self = this;

    self.concentrations = storeData.concentrations;
    self.profil = ko.observable();
    self.concentration = ko.observable();

    self.avatar = ko.computed(function() {
        return self.profil().avatar ? sprintf("data:image/jpg;base64,%s", self.profil().avatar) : "http://placehold.it/80";
    });

    self.loadImage = function() {
        var file = $self.find(".upload-ctrl").val();

        // Nesting prevention huehuehue 2015/04/25
        if (!file) {
            return;
        }

        var reader = new FileReader();

        reader.onload = function(e) {
            $(".avatar-image figure img")
                .attr("src", e.target.result)
                .width(200)
                .height(200);
        };

        reader.readAsDataURL(file);
    };

    self.load = function() {
        api.userspace.profil(authenticationToken.emittedFor).done(function(profil) {
            self.profil(profil);
            // self.avatar(profil.avatar ? sprintf("data:image/jpg;base64,%s", profil.avatar) : "http://placehold.it/80");
        }).invoke();
    };

    self.beforeLoad = function() {

    };
    self.save = function() {
        return true;
    };

    self.cancel = function() {
        return true;
    };
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
        api.clubs.membres.getAll(currentClubContext.nom).done(function(membres) {

            $.each(membres, function() {
                this.nom = sprintf("%s %s", this.profilPublic.prenom, this.profilPublic.nom);
                this.avatar = this.profilPublic.avatar ? sprintf("data:image/jpg;base64,%s", this.profilPublic.avatar) : "http://placehold.it/80";
            });

            self.membres(membres);

            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();
    }();
}

// Model view for the commanditaire object.
function CommanditaireListModelView($self, validationModelView) {
    // Define closure safe properties.
    var self = this;

    self.commanditairesList = ko.observableArray();

    // Loads the profil entity from the rest services.
    self.load = function() {
        // Deactivate the view.
        $panel.waiting();

        // Load the profilList entity.
        // ***GAGF url à déterminer, doit fournir le nom du club (dropdown)
        restCall(buildUrl(apiUrl, "nomDuClub", "commanditaire"), operations.get(), buildTokenAuthHeader()).done(function(commanditairesList) {
            self.commanditairesList(commanditairesList);

            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();
    };

    // Load the entity.
    self.load();

}

// Model view for the main menu (top menu).
function MainMenuModelView($self, $xsContainer, $mdContainer) {
    // Define closure safe properties.
    var self = this;

    // Define all jquery selectors.
    var $subscribedClubs = $self.find("#subscribed-clubs");

    // Define all observable properties.
    self.subscribedClubsModelView = ko.observable(new SubscribedClubsModelView($subscribedClubs));
    self.isXs = ko.observable($(".body > .visible-xs:visible").exists());
    
    // Bind a on resize event to switch between mobile container and medium device container.
    $(window).on("resize", function() {
        // Update is xs viewport flag.
        self.isXs($(".body > .visible-xs:visible").exists());

        // Append in the good container.
        if (self.isXs()) {
            $xsContainer.append($self);
        } else {
            $mdContainer.append($self);
        }
    }).trigger("resize");
}

// Model view for the commanditaire object.
function SubscribedClubsModelView($self) {
    // Define closure safe properties.
    var self = this;

    // Define all onservable properties.
    self.clubs = ko.observableArray();

    // Selects the current club.
    self.select = function(item) {
        currentClubContext = item;

        // Load the user's claims on the club.
        api.security.contexts.getClaims(item.nom).done(function(contextClaims) {
            currentClubContextClaims = contextClaims;
        }).invoke();
    };

    // Loads the club entities from the rest services.
    self.load = function() {
        api.clubs.getSubscribedClubs().done(function(clubs) {
            $.each(clubs, function (i, club) {
                if (club.logo)
                    club.logo = buildImageDataUri(club.logo);
            });

            self.clubs(clubs);
            self.select(clubs[0]);
        }).invoke();
    }();
}

// Model view for the club menu (left menu).
function ClubMenuModelView($self, entries) {
    // Define closure safe properties.
    var self = this;
    var originalEntries = entries;

    // Disposable function that cleans the menu entries of actions that would be unauthorized.
    var cleanEntries = function(ent, currentModule) {
        var indicesToRm = [];
        $.each(ent, function(i, entry) {
            // If module of the entry is defined, set current module.
            if (entry.module)
                currentModule = entry.module;

            // If claims of the entry is defined, check whether entries need to be removed.
            if (entry.claims) {
                // Get the module in the user's claim list on context.
                var contextModuleClaims;
                $.each(currentClubContextClaims, function(j, moduleClaims) {
                    if (moduleClaims.nom === currentModule) {
                        contextModuleClaims = moduleClaims.nom;
                        return;
                    }
                });

                // By default, remove entry if module does not exist.
                if (contextModuleClaims == null) {
                    indicesToRm.push(i);
                    return;
                }

                // Check if every required claims are present.
                var moduleClaimsArray = contextModuleClaims.claims.split(", ");
                var requiredClaimsArray = entry.claims.split(", ");
                $.each(requiredClaimsArray, function(j, requiredClaim) {
                    if (!$.inArray(requiredClaim, moduleClaimsArray)) {
                        // Claim is not present, remove the entry.
                        indicesToRm.push(i);
                        return;
                    }
                });
            }

            // Remove all indices to remove.
            $.each(indicesToRm, function(j, indexToRemove) {
                ent.remove(indexToRemove);
            });

            // If submenu of the entry is defined, recursivity.
            if (entry.entries)
                cleanEntries(entry.entries, currentModule);
        });
    };

    // Define all observable properties.
    self.entries = ko.observableArray(entries);
    self.isXs = ko.observable($("body > .visible-xs:visible").exists());
    
    // Refreshes the menu entries to match the current club.
    self.refresh = function() {
        // self.entries(cleanEntries(originalEntries));
    }();

    $(window).on("resize", function () {
        // Update is xs viewport flag.
        self.isXs($("body > .visible-xs:visible").exists());
    });
}