// Model view for the base profil object.
function ProfilBaseModelView($self, validationModelView) {
    // Define closure safe properties.
    var self = this;

    // Define all onservable properties.
    self.viewmode = ko.observable(app.enums.viewmodes.view);
    self.concentrations = app.data.enums.concentrations.observable;
    self.profil = ko.observable();

    // Define all jquery selectors.
    var $panel = $self.parents(".panel").first();
    var $avatars = $self.find(".profil-avatar");

    $(window).on("resize", function() {
        self.resizeAvatar();
    });

    // Resize the avatar elements so their height matches their width.
    self.resizeAvatar = function() {
        $avatars.each(function() {
            var $avatar = $(this);
            app.loggers.mv.debug("Avatar width: ", $avatar.width(), "px.");
            $avatar.height($avatar.width());
        });
    };

    // Begin edition of base profil entity.
    self.beginEdit = function() {
        // Switch to edition mode.
        self.viewmode(app.enums.viewmodes.edition);
        $panel.removeClass("panel-primary").addClass("panel-warning");
    };

    // Commits the edited properties of the base profil entity.
    self.edit = function() {
        // Deactivate the view.
        $panel.waiting();

        // Update the profil entity.
        api.userspace.profil.update(app.user.current.name, self.profil()).done(function () {
            self.endEdit();
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    };

    // Cancels the edition of profil entity.
    self.cancelEdit = function () {
        self.endEdit();
    };

    // Ends the edition of profil entity.
    self.endEdit = function() {
        // Clear validation messages.
        validationModelView.clear();

        // Switch to view mode.
        self.viewmode(app.enums.viewmodes.view);
        $panel.removeClass("panel-warning").addClass("panel-primary");

        // Reactivate the view.
        $panel.active();

        // Try to get the concentration of the profil.
        self.profil().concentration = self.profil().concentrationId ? app.data.enums.concentrations.store[self.profil().concentrationId] : null;

        // Set either the user avatar or a placeholder for the profil's avatar.
        self.profil().avatarDataUri = self.profil().avatar
            ? app.utility.image.buildDataUri(self.profil().avatar)
            : "/app/Content/img/avatar-placeholder.png";

        self.profil.valueHasMutated();
    };

    // Manages an error that occured while managing the profil entity.
    self.error = function(error) {
        if (error.httpStatus === 400) {
            // Bad request: validation error.
            validationModelView.load(error.response);
        }

        // Log the error.
        app.loggers.mv.error("Exception in ProfilBaseModelView(): ", error);

        // Reactivate the view.
        $panel.active();
    };

    // Loads the profil entity from the rest services.
    self.load = function() {
        // Deactivate the view.
        $panel.waiting();

        // Load the profil entity.
        api.userspace.profil.get(app.user.current.name).done(function (profil) {
            // Get the concentration of the user from the given concentration id.
            profil.concentration = profil.concentrationId ? app.data.enums.concentrations.store[profil.concentrationId] : null;

            // Set either the user avatar or a placeholder for the profil's avatar.
            profil.avatarDataUri = profil.avatar
                ? app.utility.image.buildDataUri(profil.avatar)
                : "/app/Content/img/avatar-placeholder.png";

            // Set the observable profil object.
            self.profil(profil);

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
    self.viewmode = ko.observable(app.enums.viewmodes.view);
    self.profilAvance = ko.observable();

    // Define all jquery selectors.
    var $panel = $self.parents(".panel").first();

    // Begin edition of profil entity.
    self.beginEdit = function() {
        // Switch to edition mode.
        self.viewmode(app.enums.viewmodes.edition);
        $panel.removeClass("panel-primary").addClass("panel-warning");
    };

    // Commits the edited properties of the profil entity.
    self.edit = function() {
        // Deactivate the view.
        $panel.waiting();

        // The separation between profil and profil avance is logical only.
        // This means the profil is the aggregation of both. Before posting the profil avance, reload
        // the base profil. If it has not changed, this will reload the cached value anyway.
        api.userspace.profil.get(app.user.current.name).done(function (profil) {
            profil.profilAvance = self.profilAvance();
            // Update the profil entity.
            api.userspace.profil.update(app.user.current.name, profil).done(function () {
                self.endEdit();
                self.profilAvance.valueHasMutated();
            }).fail(function (exception) {
                self.error(exception);
            }).invoke();
        }).fail(function(exception) {
            self.error(exception);
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
        self.viewmode(app.enums.viewmodes.view);
        $panel.removeClass("panel-warning").addClass("panel-primary");

        // Reactivate the view.
        $panel.active();
    };

    // Manages an error that occured while managing the profil entity.
    self.error = function(error) {
        if (error.httpStatus === 400) {
            // Bad request: validation error.
            validationModelView.load(error.response);
        }

        // Log the error.
        app.loggers.mv.error("Exception in ProfilAvanceModelView(): ", error);

        // Reactivate the view.
        $panel.active();
    };

    // Loads the profil entity from the rest services.
    self.load = function() {
        // Deactivate the view.
        $panel.waiting();

        // Load the profil entity.
        api.userspace.profil.get(app.user.current.name).done(function (profil) {
            // The separation between profil and profil avance is logical only.
            self.profilAvance(profil.profilAvance);
            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();
    }();
}

// Model view for the formation object list.
function FormationsModelView($self) {
    // Define closure safe properties.
    var self = this;

    // Define all onservable properties.
    self.formations = ko.observableArray();

    // Adds a single formation entity.
    self.add = function(formation) {
        $self.waiting();
        api.userspace.formations.create(app.user.current.name, formation).done(function(id) {
            formation.id = id;
            formation.viewmode(app.enums.viewmodes.view);
            self.refresh();

            self.addBlank();
            $self.active();
        }).invoke();
    };

    // Adds a blank formation entity in creation mode.
    self.addBlank = function () {
        self.formations.push({
            viewmode: ko.observable(app.enums.viewmodes.creation),
            titre: null,
            description: null,
            "public": false
        });
    };

    // Deletes a single formation entity.
    self.delete = function(formation) {
        $self.waiting();
        api.userspace.formations.delete(app.user.current.name, formation.id).done(function () {
            // Remove from the observable formation array.
            self.formations.remove(function(f) {
                return formation.id === f.id;
            });
            $self.active();
        }).invoke();
    };

    // Begin edition of a single formation entity.
    self.beginEdit = function(formation) {
        // Switch view mode.
        formation.viewmode(app.enums.viewmodes.edition);
        self.refresh();
    };

    // Edit a single formation entity.
    self.edit = function(formation) {
        $self.waiting();
        api.userspace.formations.update(app.user.current.name, formation.id, formation).done(function () {
            formation.viewmode(app.enums.viewmodes.view);
            self.refresh();
            $self.active();
        }).invoke();
    };

    // Refreshes the observable array of formations.
    self.refresh = function() {
        var formations = self.formations().slice(0);
        self.formations([]);
        self.formations(formations);
    };

    // Loads the formation list from the rest services.
    self.load = function() {
        $self.waiting();
        api.userspace.formations.getAll(app.user.current.name).done(function (formations) {
            // Add a viewmode to each formation to keep track of view state.
            $.each(formations, function(i, formation) {
                formation.viewmode = ko.observable(app.enums.viewmodes.view);
            });

            self.formations(formations);
            self.addBlank();
            $self.active();
        }).invoke();
    }();
}

// Model view for the antecedent object list.
function AntecedentsModelView($self) {
    // Define closure safe properties.
    var self = this;

    // Define all onservable properties.
    self.antecedents = ko.observableArray();
    self.typesAntecedents = app.data.enums.typesAntecedents.observable;

    // Updates the type antecedent object from the new type antecedent id.
    self.refreshTypeAntecedent = function(antecedent) {
        antecedent.typeAntecedent = antecedent.typeAntecedentId
            ? app.data.enums.typesAntecedents.store[antecedent.typeAntecedentId]
            : null;
    };

    // Adds a single antecedent entity.
    self.add = function (antecedent) {
        $self.waiting();
        api.userspace.antecedents.create(app.user.current.name, antecedent).done(function (id) {
            antecedent.id = id;
            antecedent.viewmode(app.enums.viewmodes.view);
            self.refreshTypeAntecedent(antecedent);

            self.refresh();
            self.addBlank();
            $self.active();
        }).invoke();
    };

    // Adds a blank antecedent entity in creation mode.
    self.addBlank = function () {
        self.antecedents.push({
            viewmode: ko.observable(app.enums.viewmodes.creation),
            nom: null,
            description: null,
            typeAntecedentId: 0,
            typeAntecedent: null,
            "public": false
        });
    };

    // Deletes a single antecedent entity.
    self.delete = function (antecedent) {
        $self.waiting();
        api.userspace.antecedents.delete(app.user.current.name, antecedent.id).done(function () {
            // Remove from the observable antecedent array.
            self.antecedents.remove(function (a) {
                return antecedent.id === a.id;
            });
            $self.active();
        }).invoke();
    };

    // Begin edition of a single antecedent entity.
    self.beginEdit = function (antecedent) {
        // Switch view mode.
        antecedent.viewmode(app.enums.viewmodes.edition);
        self.refresh();
    };

    // Edit a single antecedent entity.
    self.edit = function (antecedent) {
        $self.waiting();
        api.userspace.antecedents.update(app.user.current.name, antecedent.id, antecedent).done(function () {
            antecedent.viewmode(app.enums.viewmodes.view);
            self.refreshTypeAntecedent(antecedent);

            self.refresh();
            $self.active();
        }).invoke();
    };

    // Refreshes the observable array of antecedents.
    self.refresh = function () {
        var antecedents = self.antecedents().slice(0);
        self.antecedents([]);
        self.antecedents(antecedents);
    };

    // Loads the antecedent list from the rest services.
    self.load = function () {
        $self.waiting();
        api.userspace.antecedents.getAll(app.user.current.name).done(function (antecedents) {
            // Add a viewmode to each formation to keep track of view state.
            $.each(antecedents, function (i, antecedent) {
                antecedent.viewmode = ko.observable(app.enums.viewmodes.view);
                self.refreshTypeAntecedent(antecedent);
            });

            self.antecedents(antecedents);
            self.addBlank();
            $self.active();
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
    var usernameCookieKey = "app.user.current.name";
    var tokenCookieKey = "app.user.current.token";

    // Define all onservable properties.
    self.username = ko.observable();
    self.password = ko.observable();

    // Define all jquery selectors.
    var $loginError = $self.find(".login-error");
    var $modal;

    // Begins the login process. If the user already has a valid token, the login process will be skipped.
    self.beginLogin = function() {
        // Hide the main content wrapper while authenticating the user.
        $("#page-wrapper").hide();

        // If cookie is present, do not ask for user's credentials.
        var usernameCookie = $.cookie(usernameCookieKey);
        var tokenCookie = $.cookie(tokenCookieKey);

        if (tokenCookie && tokenCookie !== "null" && tokenCookie !== "undefined" &&
            usernameCookie && usernameCookie !== "null" && usernameCookie !== "undefined") {
            // Try to log in with the token.
            var token = JSON.parse(tokenCookie);

            api.utility.noop(app.utility.auth.token.header(token)).done(function () {
                // Worked, token is still valid.
                self.endLogin(usernameCookie, token);
            }).fail(function() {
                // Failed, force reauthentication.
                $.cookie(usernameCookieKey, null);
                $.cookie(tokenCookieKey, null);

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
    }();

    // Logs the user in. A rest call will be tried to assert the user's credentials validity.
    self.login = function() {
        // Deactivate the view.
        $modal.waiting();

        // Try to do a no-op on the rest server.
        // If the user's credentials are wrong, this will throw.
        api.utility.noop(app.utility.auth.kerberos.header(self.username(), self.password())).done(function(data, textStatus, jqxhr) {
            // Succeeded. Extract the authentication token for further calls to the api and end the login procedure.
            self.endLogin(self.username(), app.utility.auth.token.from(data, textStatus, jqxhr));
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    };

    // End the login process.
    self.endLogin = function (username, token) {
        // Reactivate the view.
        if ($modal) $modal.active();

        // Setup the current user data.
        app.user.current.name = username;
        app.user.current.token = token;

        // Load the current user preferences
        api.userspace.preferences.getAll(app.user.current.name).done(function (preferences) {
            app.user.current.preferences = preferences;
        }).invoke();

        // Save the user's by token credentials in the cookie.
        $.cookie(usernameCookieKey, app.user.current.name);
        $.cookie(tokenCookieKey, JSON.stringify(app.user.current.token));

        // Hide the login modal.
        $self.modal("hide");

        // Remove the content's blur.
        $("#wrapper").removeClass("blurred");
        $("#page-wrapper").show();

        // Trigger a new event to wake up model views waiting on this event.
        $self.trigger("logged-in");
    };

    // Manages an error that occured while logging the user in.
    self.error = function(error) {
        // Reactivate the view.
        if ($modal) $modal.active();

        // Set an error message.
        $loginError.text(error.response.exceptionMessage);

        // Restart login.
        self.beginLogin();
    };
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
    self.isXs = ko.observable($("body > .visible-xs:visible").exists());

    // Bind a on resize event to switch between mobile container and medium device container.
    $(window).on("resize", function() {
        // Update is xs viewport flag.
        self.isXs($("body > .visible-xs:visible").exists());

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

    // Selects a club context to be the current context.
    self.select = function(item) {
        app.user.current.context.current = item;

        // Load the user's claims on the club.
        api.security.contexts.getClaims(item.nom).done(function(contextClaims) {
            app.user.current.context.claims = contextClaims;
        }).invoke();
    };

    // Loads the club entities from the rest services.
    self.load = function() {
        api.clubs.getSubscribedClubs().done(function(clubs) {
            $.each(clubs, function (i, club) {
                club.logo = club.logo ? app.utility.image.buildDataUri(club.logo) : null;
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
            if (entry.requiredClaims) {
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
                var requiredClaimsArray = entry.requiredClaims.split(", ");
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

    // Refresh the is xs flag whenever the window is resized.
    $(window).on("resize", function() {
        // Update is xs viewport flag.
        self.isXs($("body > .visible-xs:visible").exists());
    });
}

// Model view for the api description help list.
function ApiDescriptionModelView($self) {
    // Define closure safe properties.
    var self = this;

    // Define all observable properties.
    self.modules = ko.observableArray();

    // Loads the api description from the rest services.
    self.load = function() {
        api.utility.apiDescription().done(function (modules) {
            self.modules(modules);
        }).invoke();
    }();
};

// Model view for the entities description help list.
function EntitiesDescriptionModelView($self) {
    // Define closure safe properties.
    var self = this;

    // Define all observable properties.
    self.entities = ko.observableArray();

    // Loads the entities description from the rest services.
    self.load = function () {
        api.utility.entitiesDescription().done(function (entities) {
            self.entities(entities);
        }).invoke();
    }();
};