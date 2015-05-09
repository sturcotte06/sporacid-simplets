// Namespace that represents the running application.
var app = {
    url: "/app",
    events: {
        // Event triggered when the application is ready for content to be loaded.
        ready: function(func) {
            if (!func) {
                loggers.core.warn("app.events.ready() callback func cannot be null.");
                return;
            }

            // Application is considered ready when the document is ready and the user is logged in.
            jQuery(function($) {
                if (!app.user.current.token) {
                    $("#login").on("logged-in", function() { func($); });
                } else {
                    func($);
                }
            });
        },
        // Event triggered whenever the application's context changes.
        contextChanged: function(func) {
            if (!func) {
                loggers.core.warn("app.ready() callback func cannot be null.");
                return;
            }
        }
    },
    // Namespace of all utility functions of the application.
    utility: {
        // Namespace for all ajax related utility functions.
        ajax: {
            // Utility function to load an url into the main content.
            loadContent: function(url) {
                var $content = $("#page-wrapper");

                $content.hide();
                $content.load(url, null, function() {
                    $content.show();
                });
            }
        },
        // Namespace for all rest services utility functions.
        rest: {
            // Utility function for rest api calls.
            call: function(uri, operation, auth, data) {
                var request = {
                    url: uri,
                    type: operation,
                    cache: operation === app.enums.operations.get,
                    dataType: "json",
                    accepts: "application/json",
                    contentType: "application/json",
                    data: JSON.stringify(data),
                    beforeSend: function (jqxhr) {
                        // Set the authorization header if the information exists.
                        if (auth) {
                            jqxhr.setRequestHeader("Authorization", auth);
                        }

                        app.loggers.core.debug(
                            "Sending", operation,
                            "request at", uri,
                            "with data", JSON.stringify(data));
                    },
                    // Invokes the rest call request. To be called whenever callbacks have been set
                    // and no race condition can happen.
                    invoke: function() {
                        // Invoke the request using jquery ajax.
                        $.ajax(this);
                    },
                    // Adds a callback on request success.
                    done: function(func) {
                        // Get the old callback.
                        var callback = this.success;

                        // Recreate the real ajax callback on success with the new callback. 
                        this.success = function(payload, textStatus, jqxhr) {
                            if (callback) callback(payload, textStatus, jqxhr);
                            func(payload, textStatus, jqxhr);
                        };

                        // Chain the request object.
                        return this;
                    },
                    // Adds a callback on request failure.
                    fail: function(func) {
                        // Get the old callback.
                        var previousFunc = this.error;

                        // Recreate the real ajax callback on error with the new callback. 
                        this.error = function(jqhxr) {
                            var exception = app.utility.rest.exception(jqhxr);
                            if (previousFunc) previousFunc(jqhxr);
                            func(exception);
                        };

                        // Chain the request object.
                        return this;
                    }
                };
                //// Add a default error handler that logs the exception.
                //.fail(function(exception) {
                //    app.loggers.core.error("Error for", operation, "request at", uri, ": ", JSON.stringify(exception));
                //});

                return request;
            },
            // Utility function to return an exception object from a rest call error.
            exception: function(jqhxr) {
                var data = JSON.parse(jqhxr.responseText);
                return {
                    data: data,
                    status: jqhxr.status
                };
            }
        },
        // Namespace for all authentication utility functions.
        auth: {
            kerberos: {
                // Utility function to build the authorization header for kerberos authentication.
                header: function(username, password) {
                    return "Kerberos " + btoa(username + ":" + password);
                }
            },
            token: {
                // Utility function to build the authorization header for token authentication.
                // If the supplied token is null, the current user token will be retrieved.
                header: function(token) {
                    var tokenValue = token ? token.value : app.user.current.token.value;
                    return "Token " + tokenValue;
                },
                // Utility function to build the authorization token object from the server http response.
                from: function(data, textStatus, jqxhr) {
                    return {
                        value: jqxhr.getResponseHeader("Authorization-Token"),
                        emittedAt: moment(jqxhr.getResponseHeader("Authorization-Token-Emitted-At"), moment().ISO_8601),
                        expiresAt: moment(jqxhr.getResponseHeader("Authorization-Token-Expires-At"), moment().ISO_8601)
                    };
                }
            }
        },
        // Namespace for all url utility functions.
        url: {
            // Utility function to build a rest resource url. The method takes a variable number of arguments.
            build: function() {
                var url = "";
                var urlParts = arguments;

                // For each url part given in argument.
                $.each(urlParts, function (i, urlPart) {
                    if (!urlPart) {
                        app.loggers.core.warn("app.utility.url.build(): url part at index", i, "was undefined and will be skipped.");
                        return;
                    }

                    // If url part starts with a slash, remove the slash.
                    if (urlPart[0] === "/") {
                        urlPart.substring(1);
                    }

                    // If url part does not end with a slash, add a slash.
                    if (urlPart[urlPart.length - 1] !== "/" && i < urlParts.length - 1) {
                        urlPart += "/";
                    }

                    // Concatenate sanitized url part to url.
                    url += urlPart;
                });

                return url;
            },
            // Namespace for all url's query string utility functions.
            qs: {
                // Utility function to build the skip and take query string from nullable skip and take arguments.
                buildSkipTake: function(skip, take) {
                    return skip && take ? sprintf("skip=%d&take=%d", skip, take) : "";
                }
            }
        },
        // Namespace for all images utility functions.
        image: {
            // Utility function that returns a data uri for a base64 encoded image.
            buildDataUri: function(base64Image) {
                return sprintf("data:%s;base64,%s", app.utility.image.detectMimeType(base64Image), base64Image);
            },
            // Utility function to detect the image type of a base64 encoded image.
            detectMimeType: function(base64Image) {
                // 1. Transform first 4 bytes of base64 image.
                var bytes = window.atob(base64Image);
                var array = new Uint8Array(new ArrayBuffer(4));
                for (var i = 0; i < 4 && i < bytes.length; i++) {
                    array[i] = bytes.charCodeAt(i);
                }

                // 2. First few bytes (maximum 4) of data stream is the image type signature. Naive check.
                var jpeg = new Uint8Array([255, 216, 255, 224]);
                var jpeg2 = new Uint8Array([255, 216, 255, 225]);
                if (app.utility.array.matches(jpeg, array) || app.utility.array.matches(jpeg2, array))
                    return "image/jpeg";

                var png = new Uint8Array([137, 80, 78, 71]);
                if (app.utility.array.matches(png, array))
                    return "image/png";

                var bmp = new Uint8Array([66, 77]);
                if (app.utility.array.matches(bmp, array))
                    return "image/bmp";

                var gif = new Uint8Array([71, 73, 70]);
                if (app.utility.array.matches(gif, array))
                    return "image/gif";

                var tiff = new Uint8Array([73, 73, 42]);
                var tiff2 = new Uint8Array([77, 77, 42]);
                if (app.utility.array.matches(tiff, array) || app.utility.array.matches(tiff2, array))
                    return "image/tiff";

                return null;
            }
        },
        array: {
            // Utility function to test if a sequence array matches a given array.
            matches: function(sequence, array) {
                for (var i = 0; i < sequence.length; i++)
                    if (sequence[i] !== array[i]) return false;
                return true;
            }
        }
    },
    // Namespace for all collection constructors.
    collection: {
        // Dictionary collection. The key is the id property of every datum.
        store: function(data, keyFunc) {
            if (!keyFunc) keyFunc = function(d) { return d.id };
            var store = {};

            $.each(data, function(i, datum) {
                var key = keyFunc(datum);
                if (!key) return;
                store[key] = datum;
            });
            
            return store;
        }
    },
    // Namespace for all user related functions and objects.
    user: {
        // Object for the currently authenticated user.
        current: {
            name: null,
            token: null,
            preferences: null,
            context: {
                current: null,
                claims: null
            }
        }
    },
    // Namespace for all enumeration functions and objects.
    enums: {
        // Enumeration of all supported user preferences.
        preferences: {
            hasLoggedOn: "app.auth.hasLoggedOn",
            locale: "app.locale",
            defaultClub: "app.context.defaultClub"
        },
        // Enumeration of all supported rest operations.
        operations: {
            get: "GET",
            create: "POST",
            update: "PUT",
            "delete": "DELETE"
        },
        // Enumeration of all model view modes.
        viewmodes: {
            view: "view",
            edition: "edition",
            creation: "creation",
            deletion: "deletion"
        },
        // Enumeration of all of the application's modules.
        modules: {
            commanditaires: "Commanditaires",
            membres: "Membres",
            fournisseurs: "Fournisseurs",
            evenements: "Evenements"
        },
        // Enumeration of all of the application's claims.
        claims: {
            read: "Read",
            readAll: "Read, ReadAll",
            create: "Create",
            createAll: "Create, CreateAll",
            update: "Update",
            updateAll: "Update, UpdateAll",
            "delete": "Delete",
            deleteAll: "Delete, DeleteAll",
            admin: "Admin",
            all: "Read, ReadAll, Create, CreateAll, Update, UpdateAll, Delete, DeleteAll, Admin"
        },
        // Enumeration of all of the application's cookies.
        cookies: {
            username: "app_user_current_name",
            token: "app_user_current_token"
        }
    },
    // Namespace that represents all loaded and cached data of the application.
    data: {
        // Dictionary of all enumerations data.
        enums: {
            concentrations: {
                observable: ko.observableArray(),
                store: null,
                // Loads the concentration enumeration as both an observable array and a data store.
                load: function() {
                    api.enumerations.concentrations().done(function(concentrations) {
                        $.each(concentrations, function(i, concentration) {
                            concentration.toString = function() { return sprintf("%s - %s", concentration.acronyme, concentration.description); };
                        });

                        app.data.enums.concentrations.observable(concentrations);
                        app.data.enums.concentrations.store = app.collection.store(concentrations);
                    }).invoke();
                }
            },
            typesContacts: {
                observable: ko.observableArray(),
                store: null,
                // Loads the types contacts enumeration as both an observable array and a data store.
                load: function() {
                    // Load contacts types as a data store.
                    api.enumerations.typesContacts().done(function(typesContacts) {
                        $.each(typesContacts, function(i, typeContact) {
                            typeContact.toString = function() { return typeContact.nom; };
                        });

                        app.data.enums.typesContacts.observable(typesContacts);
                        app.data.enums.typesContacts.store = app.collection.store(typesContacts);
                    }).invoke();
                }
            },
            statutsSuivies: {
                observable: ko.observableArray(),
                store: null,
                // Loads the statuts suivies enumeration as both an observable array and a data store.
                load: function() {
                    api.enumerations.statutsSuivies().done(function(statutsSuivies) {
                        $.each(statutsSuivies, function(i, statutSuivie) {
                            statutSuivie.toString = function() { return sprintf("%s - %s", statutSuivie.code, statutSuivie.description); };
                        });

                        app.data.enums.statutsSuivies.observable(statutsSuivies);
                        app.data.enums.statutsSuivies.store = app.collection.store(statutsSuivies);
                    }).invoke();
                }
            },
            unites: {
                observable: ko.observableArray(),
                store: null,
                // Loads the unites enumeration as both an observable array and a data store.
                load: function() {
                    api.enumerations.unites().done(function(unites) {
                        $.each(unites, function(i, unite) {
                            unite.toString = function() { return sprintf("%s - %s", unite.systeme, unite.code); };
                        });

                        app.data.enums.unites.observable(unites);
                        app.data.enums.unites.store = app.collection.store(unites);
                    }).invoke();
                }
            },
            typesCommanditaires: {
                observable: ko.observableArray(),
                store: null,
                // Loads the types commanditaires enumeration as both an observable array and a data store.
                load: function() {
                    api.enumerations.typesCommanditaires().done(function(typesCommanditaires) {
                        $.each(typesCommanditaires, function(i, typeCommanditaire) {
                            typeCommanditaire.toString = function() { return typeCommanditaire.nom; };
                        });

                        app.data.enums.typesCommanditaires.observable(typesCommanditaires);
                        app.data.enums.typesCommanditaires.store = app.collection.store(typesCommanditaires);
                    }).invoke();
                }
            },
            typesAntecedents: {
                observable: ko.observableArray(),
                store: null,
                // Loads the types antecedents enumeration as both an observable array and a data store.
                load: function () {
                    api.enumerations.typesAntecedents().done(function(typesAntecedents) {
                        $.each(typesAntecedents, function(i, typeAntecedent) {
                            typeAntecedent.toString = function() { return typeAntecedent.nom; };
                        });

                        app.data.enums.typesAntecedents.observable(typesAntecedents);
                        app.data.enums.typesAntecedents.store = app.collection.store(typesAntecedents);
                    }).invoke();
                }
            },
            // Loads all enumerations.
            loadAll: function() {
                for (var e in app.data.enums) {
                    if (app.data.enums.hasOwnProperty(e) && app.data.enums[e].load) {
                        app.data.enums[e].load();
                    }
                }
            }
        }
    },
    // Dictionary of all loggers.
    loggers: {
        // Logger for the core section of the application.
        core: log4javascript.getLogger("app.loggers.core"),
        // Logger for the modelview section of the application.
        mv: log4javascript.getLogger("app.loggers.modelviews"),
        // Setups the logging. This method invokes itself, it is therefore not required to call it.
        setup: function() {
            // Setup default console appender.
            var appender = new log4javascript.BrowserConsoleAppender();
            var appenderLayout = new log4javascript.PatternLayout("%d{HH:mm:ss} %-5p - %m%n");
            appender.setLayout(appenderLayout);

            // Setup all loggers with the default appender.
            app.loggers.core.setLevel(log4javascript.Level.DEBUG);
            app.loggers.core.addAppender(appender);
            app.loggers.mv.setLevel(log4javascript.Level.DEBUG);
            app.loggers.mv.addAppender(appender);
        }
    }
};