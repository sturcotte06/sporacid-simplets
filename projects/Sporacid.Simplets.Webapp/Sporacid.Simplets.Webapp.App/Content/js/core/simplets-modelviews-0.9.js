// Model view for the base profil object.
function ProfilBaseModelView($self, $error) {
    // Define closure safe properties.
    var self = this;

    // Define all properties.
    self.beforeEdit = null;
    self.errorModelView = new RestExceptionModelView($error);
    ko.applyBindings(self.errorModelView, $error[0]);

    // Define all observable properties.
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
    self.resizeAvatar = function () {
        $avatars.each(function () {
            var $avatar = $(this);
            $avatar.height($avatar.width());
        });
    };

    // Begin edition of base profil entity.
    self.beginEdit = function() {
        // Switch to edition mode.
        self.viewmode(app.enums.viewmodes.edition);
        $panel.removeClass("panel-primary").addClass("panel-warning");

        // Save the current version of the object.
        self.beforeEdit = jQuery.extend(true, {}, self.profil());
    };

    // Commits the edited properties of the base profil entity.
    self.edit = function() {
        // Deactivate the view.
        $panel.waiting();

        // Update the profil entity.
        api.userspace.profil.update(app.user.current.name, self.profil()).done(function() {
            self.endEdit();
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    };

    // Cancels the edition of profil entity.
    self.cancelEdit = function () {
        var profil = self.profil();
        jQuery.extend(profil, self.beforeEdit);
        self.endEdit();
    };

    // Ends the edition of profil entity.
    self.endEdit = function() {
        // Clear rest exception messages.
        self.errorModelView.clear();

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
    self.error = function(exception) {
        // Load the exception in the error model view.
        self.errorModelView.load(exception);
        // Reactivate the view.
        $panel.active();
    };

    // Loads the profil entity from the rest services.
    self.load = function() {
        // Deactivate the view.
        $panel.waiting();

        // Load the profil entity.
        api.userspace.profil.get(app.user.current.name).done(function(profil) {
            // Get the concentration of the user from the given concentration id.
            profil.concentration = profil.concentrationId ? app.data.enums.concentrations.store[profil.concentrationId] : null;

            // Set either the user avatar or a placeholder for the profil's avatar.
            profil.avatarDataUri = profil.avatar
                ? app.utility.image.buildDataUri(profil.avatar)
                : "/app/Content/img/avatar-placeholder.png";

            // Set the observable profil object.
            self.profil(profil);

            // Reactivate the view.
            $self.trigger("loaded");
            $panel.active();
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    }();
};

// Model view for the profil avance object.
function ProfilAvanceModelView($self, $error) {
    // Define closure safe properties.
    var self = this;

    // Define all properties.
    self.beforeEdit = null;
    self.errorModelView = new RestExceptionModelView($error);
    ko.applyBindings(self.errorModelView, $error[0]);

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

        // Save the current version of the object.
        self.beforeEdit = jQuery.extend(true, {}, self.profilAvance());
    };

    // Commits the edited properties of the profil entity.
    self.edit = function() {
        // Deactivate the view.
        $panel.waiting();

        // The separation between profil and profil avance is logical only.
        // This means the profil is the aggregation of both. Before posting the profil avance, reload
        // the base profil. If it has not changed, this will reload the cached value anyway.
        api.userspace.profil.get(app.user.current.name).done(function(profil) {
            profil.profilAvance = self.profilAvance();
            // Update the profil entity.
            api.userspace.profil.update(app.user.current.name, profil).done(function() {
                self.endEdit();
                self.profilAvance.valueHasMutated();
            }).fail(function(exception) {
                self.error(exception);
            }).invoke();
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    };

    // Cancels the edition of profil entity.
    self.cancelEdit = function() {
        self.profilAvance(self.beforeEdit);
        self.endEdit();
    };

    // Ends the edition of profil entity.
    self.endEdit = function() {
        // Clear rest exception messages.
        self.errorModelView.clear();

        // Switch to view mode.
        self.viewmode(app.enums.viewmodes.view);
        $panel.removeClass("panel-warning").addClass("panel-primary");

        // Reactivate the view.
        $panel.active();
    };

    // Manages an error that occured while managing the profil avance entity.
    self.error = function(exception) {
        // Load the exception in the error model view.
        self.errorModelView.load(exception);
        // Reactivate the view.
        $panel.active();
    };

    // Loads the profil entity from the rest services.
    self.load = function() {
        // Deactivate the view.
        $panel.waiting();

        // Load the profil entity.
        api.userspace.profil.get(app.user.current.name).done(function(profil) {
            // The separation between profil and profil avance is logical only.
            self.profilAvance(profil.profilAvance);

            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    }();
};

// Model view for the formation object list.
function FormationsModelView($self, $error) {
    // Define closure safe properties.
    var self = this;

    // Define all properties.
    self.errorModelView = new RestExceptionModelView($error);
    ko.applyBindings(self.errorModelView, $error[0]);

    // Define all observable properties.
    self.formations = ko.observableArray();

    // Define all jquery selectors.
    var $panel = $self.parents(".panel").first();

    // Adds a single formation entity.
    self.add = function(formation) {
        $panel.waiting();
        api.userspace.formations.create(app.user.current.name, formation).done(function(id) {
            formation.id = id;
            formation.viewmode(app.enums.viewmodes.view);

            self.errorModelView.clear();
            self.refresh();
            self.addBlank();
            $panel.active();
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    };

    // Adds a blank formation entity in creation mode.
    self.addBlank = function() {
        self.formations.push({
            viewmode: ko.observable(app.enums.viewmodes.creation),
            titre: null,
            description: null,
            "public": false
        });
    };

    // Deletes a single formation entity.
    self.delete = function(formation) {
        $panel.waiting();
        api.userspace.formations.delete(app.user.current.name, formation.id).done(function() {
            // Remove from the observable formation array.
            self.formations.remove(function(f) {
                return formation.id === f.id;
            });

            self.errorModelView.clear();
            $panel.active();
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    };

    // Begin edition of a single formation entity.
    self.beginEdit = function(formation) {
        // Save the current version of the object.
        formation.beforeEdit = jQuery.extend(true, {}, formation);

        // Switch view mode.
        formation.viewmode(app.enums.viewmodes.edition);
        self.refresh();
    };

    // Edit a single formation entity.
    self.edit = function(formation) {
        $panel.waiting();
        api.userspace.formations.update(app.user.current.name, formation.id, formation).done(function() {
            formation.viewmode(app.enums.viewmodes.view);

            self.errorModelView.clear();
            self.refresh();
            $panel.active();
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    };

    // Cancels the edition of profil entity.
    self.cancelEdit = function(formation) {
        $.extend(formation, formation.beforeEdit);
        formation.viewmode(app.enums.viewmodes.view);
        self.refreshTypeAntecedent(formation);
    };

    // Manages an error that occured while managing a formation entity.
    self.error = function(exception) {
        // Load the exception in the error model view.
        self.errorModelView.load(exception);
        // Reactivate the view.
        $panel.active();
    };

    // Refreshes the observable array of formations.
    self.refresh = function() {
        var formations = self.formations().slice(0);
        self.formations([]);
        self.formations(formations);
    };

    // Loads the formation list from the rest services.
    self.load = function() {
        $panel.waiting();
        api.userspace.formations.getAll(app.user.current.name).done(function(formations) {
            // Add a viewmode to each formation to keep track of view state.
            $.each(formations, function(i, formation) {
                formation.viewmode = ko.observable(app.enums.viewmodes.view);
            });

            self.formations(formations);
            self.addBlank();
            $panel.active();
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    }();
};

// Model view for the antecedent object list.
function AntecedentsModelView($self, $error) {
    // Define closure safe properties.
    var self = this;

    // Define all properties.
    self.errorModelView = new RestExceptionModelView($error);
    ko.applyBindings(self.errorModelView, $error[0]);

    // Define all observable properties.
    self.antecedents = ko.observableArray();
    self.typesAntecedents = app.data.enums.typesAntecedents.observable;

    // Define all jquery selectors.
    var $panel = $self.parents(".panel").first();

    // Updates the type antecedent object from the new type antecedent id.
    self.refreshTypeAntecedent = function(antecedent) {
        antecedent.typeAntecedent = antecedent.typeAntecedentId
            ? app.data.enums.typesAntecedents.store[antecedent.typeAntecedentId]
            : null;
    };

    // Adds a single antecedent entity.
    self.add = function(antecedent) {
        $panel.waiting();
        api.userspace.antecedents.create(app.user.current.name, antecedent).done(function(id) {
            antecedent.id = id;
            antecedent.viewmode(app.enums.viewmodes.view);
            self.refreshTypeAntecedent(antecedent);

            self.errorModelView.clear();
            self.refresh();
            self.addBlank();
            $panel.active();
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    };

    // Adds a blank antecedent entity in creation mode.
    self.addBlank = function() {
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
    self.delete = function(antecedent) {
        $panel.waiting();
        api.userspace.antecedents.delete(app.user.current.name, antecedent.id).done(function() {
            // Remove from the observable antecedent array.
            self.antecedents.remove(function(a) {
                return antecedent.id === a.id;
            });

            self.errorModelView.clear();
            $panel.active();
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    };

    // Begin edition of a single antecedent entity.
    self.beginEdit = function(antecedent) {
        // Save the current version of the object.
        antecedent.beforeEdit = jQuery.extend(true, {}, antecedent);

        // Switch view mode.
        antecedent.viewmode(app.enums.viewmodes.edition);
        self.refresh();
    };

    // Edit a single antecedent entity.
    self.edit = function(antecedent) {
        $panel.waiting();
        api.userspace.antecedents.update(app.user.current.name, antecedent.id, antecedent).done(function() {
            antecedent.viewmode(app.enums.viewmodes.view);
            self.refreshTypeAntecedent(antecedent);

            self.errorModelView.clear();
            self.refresh();
            $panel.active();
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    };

    // Cancels the edition of profil entity.
    self.cancelEdit = function(antecedent) {
        $.extend(antecedent, antecedent.beforeEdit);
        antecedent.viewmode(app.enums.viewmodes.view);
        self.refreshTypeAntecedent(antecedent);
    };

    // Manages an error that occured while managing an antecedent entity.
    self.error = function(exception) {
        // Load the exception in the error model view.
        self.errorModelView.load(exception);
        // Reactivate the view.
        $panel.active();
    };

    // Refreshes the observable array of antecedents.
    self.refresh = function() {
        var antecedents = self.antecedents().slice(0);
        self.antecedents([]);
        self.antecedents(antecedents);
    };

    // Loads the antecedent list from the rest services.
    self.load = function() {
        $panel.waiting();
        api.userspace.antecedents.getAll(app.user.current.name).done(function(antecedents) {
            // Add a viewmode to each formation to keep track of view state.
            $.each(antecedents, function(i, antecedent) {
                antecedent.viewmode = ko.observable(app.enums.viewmodes.view);
                self.refreshTypeAntecedent(antecedent);
            });

            self.antecedents(antecedents);
            self.addBlank();
            $panel.active();
        }).fail(function(exception) {
            self.error(exception);
        }).invoke();
    }();
};

// Model view for an exception coming from the rest services.
function RestExceptionModelView($self) {
    // Define closure safe properties.
    var self = this;

    // Define all onservable properties.
    self.exception = ko.observable();

    // Clears the exception from the model view.
    self.clear = function() {
        self.exception(null);
    };

    // Loads an exception into the model view.
    self.load = function(exception) {
        self.exception(exception);
    };
};

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

    // Begins the login process. If the user already has a valid token, the login process will be skipped.
    self.beginLogin = function() {
        // If cookie is present, do not ask for user's credentials.
        var usernameCookie = $.cookie(app.enums.cookies.username);
        var tokenCookie = $.cookie(app.enums.cookies.token);
        if (tokenCookie && tokenCookie !== "null" && tokenCookie !== "undefined" && usernameCookie && usernameCookie !== "null" && usernameCookie !== "undefined") {
            // Try to log in with the token.
            var token = JSON.parse(tokenCookie);
            api.utility.noop(app.utility.auth.token.header(token)).done(function() {
                // Worked, token is still valid.
                self.endLogin(usernameCookie, token);
            }).fail(function() {
                // Failed, force reauthentication.
                $.cookie(app.enums.cookies.username, null, { path: "/" });
                $.cookie(app.enums.cookies.token, null, { path: "/" });
                window.location.reload();
            }).invoke();
        } else {
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
    self.endLogin = function(username, token) {
        // Reactivate the view.
        if ($modal) $modal.active();

        // Setup the current user data.
        app.user.current.name = username;
        app.user.current.token = token;

        // Load the current user preferences
        api.userspace.preferences.getAll(app.user.current.name).done(function(preferences) {
            app.user.current.preferences = app.collection.store(preferences, function(p) { return p.name; });

            // Save the user's by token credentials in the cookie.
            $.cookie(app.enums.cookies.username, app.user.current.name, { path: "/" });
            $.cookie(app.enums.cookies.token, JSON.stringify(app.user.current.token), { path: "/" });

            // Hide the login modal.
            $self.modal("hide");

            // Trigger a new event to wake up model views waiting on this event.
            $self.trigger("logged-in");
        }).invoke();
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
};

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
};

// Model view for the base profil object.
function MembresModelView($self) {
    // Define closure safe properties.
    var self = this;
    var $panel = $self.parents(".panel").first();

    // Define all observable properties.
    self.membres = ko.observableArray();
    self.activeIndex = ko.observable();

    // Loads the profil entity from the rest services.
    self.load = function() {
        // Deactivate the view.
        $panel.waiting();

        // Load the profilList entity.
        api.clubs.membres.getAll(app.user.current.context.current.name).done(function(membres) {

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
};

// Model view for the fournisseur object list.
function FournisseursModelView($self, $error) {
    // Define closure safe properties.
    var self = this;

    // Define all properties.
    self.errorModelView = new RestExceptionModelView($error);
    ko.applyBindings(self.errorModelView, $error[0]);

    // Define all observable properties.
    self.fournisseurs = ko.observableArray();
    self.typesFournisseurs = app.data.enums.typesFournisseurs.observable;

    // Updates the type fournisseur object from the new type fournisseur id.
    self.refreshTypeFournisseur = function (fournisseur) {
        fournisseur.typeFournisseur = fournisseur.typeFournisseurId
            ? app.data.enums.typesFournisseurs.store[fournisseur.typeFournisseurId]
            : null;
    };

    // Manages an error that occured while managing a fournisseur entity.
    self.error = function (exception) {
        // Load the exception in the error model view.
        self.errorModelView.load(exception);

        // Reactivate the view.
        $self.active();
    };

    // Loads the fournisseur list from the rest services.
    self.load = function () {
        $self.waiting();
        api.clubs.fournisseurs.getAll(/*app.user.current.context.current.name*/"preci2015").done(function (fournisseurs) {
            // Add a viewmode to each fournisseur to keep track of view state.
            $.each(fournisseurs, function (i, fournisseur) {
                fournisseur.viewmode = ko.observable(app.enums.viewmodes.view);
                self.refreshTypeFournisseur(fournisseur);
            });

            self.fournisseurs(fournisseurs);
            $self.active();
        }).fail(function (exception) {
            self.error(exception);
        }).invoke();
    }();
};

// Model view for the main menu (top menu).
function MainMenuModelView($self, $xsContainer, $mdContainer, preferences) {
    // Define closure safe properties.
    var self = this;

    // Define all jquery selectors.
    var $subscribedClubs = $self.find("#subscribed-clubs");
    var $onOffPreferences = $self.find("#on-off-parameters");

    // Define all observable properties.
    self.subscribedClubsModelView = ko.observable(new SubscribedClubsModelView($subscribedClubs));
    self.onOffPreferencesModelView = ko.observable(new OnOffPreferencesModelView($onOffPreferences, preferences));
    self.isXs = ko.observable($("body > .visible-xs:visible").exists());

    // Logs the user out.
    self.logout = function() {
        // Set the current user saved credentials to null.
        $.cookie(app.enums.cookies.username, null, { path: "/" });
        $.cookie(app.enums.cookies.token, null, { path: "/" });

        // Redirect to default page.
        window.location = app.url;
    }; // Bind a on resize event to switch between mobile container and medium device container.
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
};

// Model view for the commanditaire object.
function SubscribedClubsModelView($self) {
    // Define closure safe properties.
    var self = this;

    // Define all onservable properties.
    self.clubs = ko.observableArray();
    self.selected = ko.observable();

    // Selects a club context to be the current context.
    self.select = function (club) {
        // Load the user's claims on the club.
        api.security.contexts.getClaims(club.nom).done(function (contextClaims) {
            self.selected(club);
            app.user.current.context.current = club;
            app.user.current.context.claims = contextClaims;
            $self.trigger("context-changed");
        }).invoke();
    };

    // Loads the club entities from the rest services.
    self.load = function() {
        api.clubs.getSubscribedClubs().done(function(clubs) {
            $.each(clubs, function(i, club) {
                club.logo = club.logo ? app.utility.image.buildDataUri(club.logo) : null;
            });
            self.clubs(clubs);
            $self.trigger("loaded");
        }).invoke();
    }();
};

function SelectedClubModelView($self) {
    // Define closure safe properties.
    var self = this;

    // Define all onservable properties.
    self.selectedClub = ko.observable();

    app.events.contextChanged(function($) {
        self.selectedClub(app.user.current.context.current);
    });
};

// Model view for the on/off parameters of the user.
function OnOffPreferencesModelView($self, preferences) {
    // Define closure safe properties.
    var self = this;

    // Define all onservable properties.
    self.preferences = ko.observableArray();

    // Event handler when a on/off parameter is toggled.
    self.toggled = function(preference) {
        var preferenceEntity = app.user.current.preferences[preference.name]; //_.find(app.user.current.preferences, function (p) { return p.name === preference.name; });
        var request;

        // Either create or update the preference.
        if (preferenceEntity) {
            preferenceEntity.value = preference.value ? preference.onValue : preference.offValue;
            request = api.userspace.preferences.update(app.user.current.name, preferenceEntity.id, preferenceEntity);
        } else {
            preferenceEntity = { name: preference.name, value: preference.value ? preference.onValue : preference.offValue };
            request = api.userspace.preferences.create(app.user.current.name, preferenceEntity);
        }

        request.done(function() {
            window.location.reload(true);
        }).invoke();
    };

    // Loads preferences into the model view.
    self.load = function(prefs) {
        $.each(prefs, function(i, pref) {
            var userVal = app.user.current.preferences[pref.name]; //_.find(app.user.current.preferences, function (p) { return p.name === pref.name; });
            pref.value = userVal ? userVal.value : pref.value;

            prefs[i].value = ko.observable(pref.value);
            prefs[i].value.subscribe(function(p) { self.toggled(p); });
        });

        self.preferences(prefs);
        $self.trigger("loaded");
    }(preferences);
};

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
    self.isClubSelected = ko.observable(app.user.current.context.current ? true : false);

    // Refreshes the menu entries to match the current club.
    self.refresh = function() {
        // self.entries(cleanEntries(originalEntries));
    }();

    // Refresh the isClubSelected flag whenever the app's context is changed.
    app.events.contextChanged(function($) {
        self.isClubSelected(app.user.current.context.current ? true : false);
    });

    // Refresh the is xs flag whenever the window is resized.
    $(window).on("resize", function() {
        // Update is xs viewport flag.
        self.isXs($("body > .visible-xs:visible").exists());
    });
};

// Model view for the api description help list.
function ApiDescriptionModelView($self) {
    // Define closure safe properties.
    var self = this;

    // Define all observable properties.
    self.modules = ko.observableArray();

    // Loads the api description from the rest services.
    self.load = function() {
        api.utility.apiDescription().done(function(modules) {
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
    self.load = function() {
        api.utility.entitiesDescription().done(function(entities) {
            self.entities(entities);
        }).invoke();
    }();
};

// Model view for the commanditaire object.
function CommanditairesModelView($self) {
    // Define closure safe properties.
    var self = this;
    var emptyCommanditaire;
    
    self.viewmode = ko.observable(app.enums.viewmodes.view);
    self.commanditaires = ko.observableArray();
    self.currentCommanditaire = ko.observable();
        //ko.observable({
        //    nom: "test",
        //    contact: "contact",
        //    typeCommanditaireId: 1,
        //    adresse: {
        //        NoCivique: 350,
        //        Rue: "des patates",
        //        Appartement: "10abc",
        //        Ville: "Stoke beach",
        //        CodePostal: "H0H 0H0"
        //    },
        //    contact: {
        //        TypeContactId: 1,
        //        Nom: "Dlascrap",
        //        Prenom: "Yvan",
        //        Telephone: "123-456-7890",
        //        Courriel: "bebelle@de.cul"
        //    },
        //    commentaire:"commentaire"
        //});


    self.typesCommanditaires = app.data.enums.typesCommanditaires.observable;
    self.typeCommanditaire = ko.observable();
    self.typesContacts = app.data.enums.typesContacts.observable;
    self.typeContact = ko.observable();
    
    // Define all jquery selectors.
    var $panel = $self.parents(".panel").first();
    var $modal = $self.find(".modal");

    // Begin adding a commanditaire.
    self.beginAdd = function () {
        
        if (emptyCommanditaire)
            self.currentCommanditaire(emptyCommanditaire);
        else {
            api.utility.empty("commanditaire").done(function (emptyCommanditaire) {
                this.emptyCommanditaire = emptyCommanditaire;
                self.currentCommanditaire(emptyCommanditaire);
            }).invoke();
        }

        self.viewmode(app.enums.viewmodes.creation);
        self.showModal();
    };

    // Begin edition of a commanditaire.
    self.beginEdit = function (commanditaire) {

        self.currentCommanditaire(commanditaire);
        self.viewmode(app.enums.viewmodes.edition);
        self.showModal();
    };

    // Deletion of a commanditaire.
    self.delete = function (commanditaire) {

        // GAGF TODO delete confirmation

        // Deactivate the view.
        $panel.waiting();

        // ***GAGF url � d�terminer, doit fournir le nom du club (va provenir d'un dropdown)
        api.clubs.commanditaires.delete(app.user.current.context.current.nom, commanditaire.id).done(function () {

            self.commanditaires.remove(commanditaire);

            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();

    };

    self.save = function () {

        if (self.viewmode() == app.enums.viewmodes.creation) {

            api.clubs.commanditaires.create(app.user.current.context.current.nom, self.currentCommanditaire).done(function (id) {
                self.currentCommanditaire.id = id;
                self.viewmode(app.enums.viewmodes.view);

            }).fail(function(exception) {
                self.error(exception);
            }).invoke();
        }
        else if (self.viewmode() == app.enums.viewmodes.edition) {

            api.clubs.commanditaires.update(app.user.current.context.current.nom, self.currentCommanditaire.id, self.currentCommanditaire).done(function () {
                self.viewmode(app.enums.viewmodes.view);

            }).fail(function (exception) {
                self.error(exception);
            }).invoke();
        }

        //self.closeModal;

    };

    self.cancel = function () {
        self.closeModal();
    };

    self.showModal = function () {

        $modal.modal({ backdrop: "static", keyboard: false });
        $modal.modal("show");
        $modal.focus();
    }

    self.closeModal = function () {
        //// Reactivate the view.
        if ($modal) $modal.active();

        $modal.modal("hide");
    };

    // sur retour de la modal pour refresh de la liste
    // Refreshes the observable array of antecedents.
    //self.refresh = function () {
    //    var antecedents = self.antecedents().slice(0);
    //    self.antecedents([]);
    //    self.antecedents(antecedents);
    //};

    // Loads the profil entity from the rest services.
    self.load = function () {
        // Deactivate the view.
        $panel.waiting();

        // Load the commanditairesList entity.
        api.clubs.commanditaires.getAll(app.user.current.context.current.nom).done(function (commanditaires) {
            /*$.each(commanditaires, function (i, commanditaire) {
                commanditaire.contact.toString = function ()
                {
                    return sprintf("%s %s (%s)",
                        commanditaire.contact.Prenom,
                        commanditaire.contact.Nom,
                        commanditaire.contact.Telephone);
                };

                commanditaire.adresse.toString = function () {
                    return sprintf("%s %s (%s)",
                        commanditaire.contact.Prenom,
                        commanditaire.contact.Nom,
                        commanditaire.contact.Telephone);
                };
            });*/

            self.commanditaires(commanditaires);

            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();

        //api.utility.empty("commanditaire").done(function (emptyCommanditaire) {
        //    alert(JSON.stringify(emptyCommanditaire));
        //    self.currentCommanditaire(emptyCommanditaire);
        //}).invoke();

    }();
}

// Model view for the commandite object.
function CommanditesModelView($self) {
    // Define closure safe properties.
    var self = this;

    self.viewmode = ko.observable(app.enums.viewmodes.view);
    self.commandites = ko.observableArray();

    // GAGF � ajouter dans le service
    self.typesCommandites = app.data.enums.typesCommandites.observable;
    self.typeCommandite = ko.observable();

    // Define all jquery selectors.
    var $panel = $self.parents(".panel").first();

    // Begin adding a commandite.
    self.beginAdd = function () {

        api.utility.empty("commandite").done(function (emptyCommandite) {
            // n�cessaire ou non ?
        }).invoke();        

        self.viewmode(app.enums.viewmodes.creation);
    };

    // Begin edition of a commandite.
    self.beginEdit = function (commandite) {

        commandite.viewmode(app.enums.viewmodes.edition);
    };

    // Deletion of a commandite.
    self.delete = function (commandite) {

        // Deactivate the view.
        $panel.waiting();

        api.clubs.commandites.delete(app.user.current.context.current.nom, commandite.id).done(function () {

            self.commandites.remove(commandite);

            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();

    };

    self.save = function (commandite) {

        if (self.viewmode() == app.enums.viewmodes.creation) {

            api.clubs.commandites.create(app.user.current.context.current.nom, commandite).done(function (id) {
                self.commandite.id = id;
                self.viewmode(app.enums.viewmodes.view);

            }).fail(function (exception) {
                self.error(exception);
            }).invoke();
        }
        else if (self.viewmode() == app.enums.viewmodes.edition) {

            api.clubs.commandites.update(app.user.current.context.current.nom, commandite.id, commandite).done(function () {
                self.viewmode(app.enums.viewmodes.view);

            }).fail(function (exception) {
                self.error(exception);
            }).invoke();
        }

        //self.closeModal;

    };

    self.cancel = function () {
        self.closeModal();
    };

    // Loads the profil entity from the rest services.
    self.load = function () {
        // Deactivate the view.
        $panel.waiting();

        // Load the commanditesList entity.
        api.clubs.commandites.getAll(app.user.current.context.current.nom, commanditaireId).done(function (commandites) {
            
            /*$.each(commandites, function (i, commandite) {
                typeCommandite.toString = function ()
                {
                    return sprintf("%s %s (%s)",
                        commandite.typeCommandite.Nom
                        commanditaire.contact.Nom,
                        commanditaire.contact.Telephone);
                };

                commanditaire.adresse.toString = function () {
                    return sprintf("%s %s (%s)",
                        commanditaire.contact.Prenom,
                        commanditaire.contact.Nom,
                        commanditaire.contact.Telephone);
                };
            });*/

            self.commandites(commandites);

            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();

        //api.utility.empty("commandite").done(function (emptyCommandite) {
        //    alert(JSON.stringify(emptyCommandite));
        //    self.currentCommandite(emptyCommandite);
        //}).invoke();

    }();
}

// Model view for the suivi object.
function SuiviesModelView($self) {
    // Define closure safe properties.
    var self = this;

    self.viewmode = ko.observable(app.enums.viewmodes.view);
    self.suivies = ko.observableArray();

    // GAGF � ajouter dans le service
    self.typesSuivies = app.data.enums.typesSuivies.observable;
    self.typeSuivie = ko.observable();

    // Define all jquery selectors.
    var $panel = $self.parents(".panel").first();

    // Begin adding a suivie.
    self.beginAdd = function () {

        api.utility.empty("suivie").done(function (emptySuivie) {
            // n�cessaire ou non ?
        }).invoke();

        self.viewmode(app.enums.viewmodes.creation);
    };

    // Begin edition of a suivie.
    self.beginEdit = function (suivie) {

        suivie.viewmode(app.enums.viewmodes.edition);
    };

    // Deletion of a suivie.
    self.delete = function (suivie) {

        // Deactivate the view.
        $panel.waiting();

        api.clubs.suivies.delete(app.user.current.context.current.nom, suivie.id).done(function () {

            self.suivies.remove(suivie);

            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();

    };

    self.save = function (suivie) {

        if (self.viewmode() == app.enums.viewmodes.creation) {

            api.clubs.suivies.create(app.user.current.context.current.nom, suivie).done(function (id) {
                self.suivie.id = id;
                self.viewmode(app.enums.viewmodes.view);

            }).fail(function (exception) {
                self.error(exception);
            }).invoke();
        }
        else if (self.viewmode() == app.enums.viewmodes.edition) {

            api.clubs.suivies.update(app.user.current.context.current.nom, suivie.id, suivie).done(function () {
                self.viewmode(app.enums.viewmodes.view);

            }).fail(function (exception) {
                self.error(exception);
            }).invoke();
        }

        //self.closeModal;

    };

    self.cancel = function () {
        self.closeModal();
    };

    // Loads the profil entity from the rest services.
    self.load = function () {
        // Deactivate the view.
        $panel.waiting();

        // Load the suiviesList entity.
        api.clubs.suivies.getAll(app.user.current.context.current.nom, commanditaireId, commanditeId).done(function (suivies) {
            
            self.suivies(suivies);

            // Reactivate the view.
            $panel.active();
            $self.trigger("loaded");
        }).invoke();

        //api.utility.empty("suivie").done(function (emptysuivie) {
        //    alert(JSON.stringify(emptysuivie));
        //    self.currentsuivie(emptysuivie);
        //}).invoke();

    }();
}