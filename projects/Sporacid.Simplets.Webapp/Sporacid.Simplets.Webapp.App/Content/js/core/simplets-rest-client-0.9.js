// Default config object. Should be good in 99.99% of cases.
var defaultRestClientConfig = {
    // Base url for the rest api.
    apiUrl: "http://localhost/services/api/v1/"
};

// Config object for the simplets client.
SimpletsRestClient.prototype.restClientConfig = null;
// Authorization token to use when the user is logged in. 
// This field is null until login() is successfully called.
SimpletsRestClient.prototype.authorizationToken = null;
// Current club context.
SimpletsRestClient.prototype.currentContext = null;
// List of all Stores.
SimpletsRestClient.prototype.stores = {};

/// <summary>
/// Constructor for the SimpletsRestClient prototype.
/// Instances of this prototype will be able to access data of simplets through async rest calls.
/// </summary>
function SimpletsRestClient(restClientConfig) {
    if (!restClientConfig) {
        restClientConfig = defaultRestClientConfig;
    }

    this.restClientConfig = restClientConfig;
    this.authorizationToken = null;
    this.currentUser = null;
    this.currentClub = null;

    // Stores initialization.
    this.stores["enumeration"] = new EnumerationStore(this);
    this.stores["profil"] = new ProfilStore(this);
}

/// <summary>
/// Changes the current context of this client. 
/// </summary>
SimpletsRestClient.prototype.changeContext = function(context) {
    this.currentContext = context;
};

/// <summary>
/// Validates the credentials. 
/// This operation is simply to test whether the user is in the system or not.
/// Technically, the rest services are stateless so login is not required, however, it is
/// convenient to know beforehand if the user has rights in the system.
/// </summary>
/// <param name="username">Username of the user.</param>
/// <param name="password">Password of the user.</param>
SimpletsRestClient.prototype.login = function(username, password) {
    if (!username) {
        throw "Username is required for login.";
    }

    if (!password) {
        throw "Password is required for login.";
    }
    
    // A no-op operation on the rest services will throw if the user is not authenticated.
    // We can thus use the no-op action to validate the user's credentials
    var noopUrl = buildUrl(this.restClientConfig.apiUrl, "anonymous/noop");

    // Call the action synchronously, because we need the authentication token.
    var authorizationToken;
    $.ajax({
        async: false,
        url: noopUrl,
        method: httpMethods.get(),
        headers: {
            "Authorization": "Kerberos " + btoa(username + ":" + password)
        },
        success: function(data, textStatus, jqhxr) {
            // Create the authorization token and store it for accessing rest calls.
            authorizationToken = {
                user: username,
                token: jqhxr.getResponseHeader("Authorization-Token"),
                emittedAt: jqhxr.getResponseHeader("Authorization-Token-Emitted-At"),
                validFor: jqhxr.getResponseHeader("Authorization-Token-Valid-For")
            };
        },
        error: function (jqhxr, textStatus, exception) {
            var errorObject = createErrorObject(jqhxr, textStatus, exception);
            throwRestException(errorObject);
        }
    });

    this.authorizationToken = authorizationToken;
};

SimpletsRestClient.prototype.get = function (actionUrl, id) {
    if (!actionUrl) {
        throw "Get requests must have an actionUrl.";
    }
    
    return new RestCallBuilder({
        method: httpMethods.get(),
        authorizationToken: this.authorizationToken.token,
        url: id ? buildUrl(this.restClientConfig.apiUrl, actionUrl, id) : buildUrl(this.restClientConfig.apiUrl, actionUrl)
    }).onError(function (restClient, errorObject) {
        throwRestException(errorObject);
    });
};

SimpletsRestClient.prototype.update = function(actionUrl, id, entity) {
    if (!actionUrl) {
        throw "Update requests must have an actionUrl.";
    }

    if (!id) {
        throw "Update requests must have an id.";
    }

    if (!entity) {
        throw "Update requests must have an entity.";
    }
    
    return new RestCallBuilder({
        method: httpMethods.put(),
        authorizationToken: this.authorizationToken.token,
        url: buildUrl(this.restClientConfig.apiUrl, actionUrl, id),
        entity: entity
    }).onError(function (restClient, errorObject) {
        throwRestException(errorObject);
    });
};

SimpletsRestClient.prototype.create = function (actionUrl, entity) {
    if (!actionUrl) {
        throw "Create requests must have an actionUrl.";
    }

    if (!entity) {
        throw "Create requests must have an entity.";
    }

    return new RestCallBuilder({
        method: httpMethods.post(),
        authorizationToken: this.authorizationToken.token,
        url: buildUrl(this.restClientConfig.apiUrl, actionUrl, id),
        entity: entity
    }).onError(function (restClient, errorObject) {
        throwRestException(errorObject);
    });
};

SimpletsRestClient.prototype.delete = function (actionUrl, id) {
    if (!actionUrl) {
        throw "Create requests must have an actionUrl.";
    }

    if (!id) {
        throw "Create requests must have an id.";
    }

    return new RestCallBuilder({
        method: httpMethods.delete(),
        authorizationToken: this.authorizationToken.token,
        url: buildUrl(this.restClientConfig.apiUrl, actionUrl, id)
    }).onError(function (restClient, errorObject) {
        throwRestException(errorObject);
    });
};

function createErrorObject(jqhxr, textStatus, exception) {
    var exceptionObject = JSON.parse(jqhxr.responseText);
    return {
        httpStatus: jqhxr.status,
        httpStatusText: textStatus,
        exceptionType: exceptionObject["ExceptionType"],
        exceptionMessage: exceptionObject["ExceptionMessage"]
    };
}

/// <summary>
/// Throws an exception which text represent the server-side error.
/// </summary>
/// <param name="errorObject">Rest error object.</param>
function throwRestException(errorObject) {
    throw "[" + errorObject.httpStatus + ": " + errorObject.httpStatusText + "] " +
            errorObject.exceptionType + ": " + errorObject.exceptionMessage;
}

/// <summary>
/// Build an url from parts of one.
/// If id is null, it will not be concatenated to the url.
/// </summary>
/// <param name="apiUrl">Base url for the api.</param>
/// <param name="actionUrl">Relative url of the action.</param>
/// <param name="id">Optional parameter to append at the end of the url.</param>
function buildUrl(apiUrl, actionUrl, id) {
    return apiUrl + actionUrl + (id ? "/" + id : "");
}

/// <summary>
/// Does a rest call to an url.
/// </summary>
/// <param name="restCallParams">All required parameter for the rest call.</param>
function restCall(restCallParams) {
    // Mandatory parameter.
    if (!restCallParams) {
        throw "Rest calls must have rest call parameters.";
    }

    // Mandatory parameter.
    if (!restCallParams.method) {
        throw "Rest calls must have rest call parameter called method.";
    }

    // Mandatory parameter.
    if (!restCallParams.url) {
        throw "Rest calls must have rest call parameter called url.";
    }

    // Mandatory state of the rest client.
    if (!restCallParams.authorizationToken) {
        throw "Rest calls requires an authorization token. Call the login() method before reattempting.";
    }

    if (restCallParams.entity === undefined) {
        restCallParams.entity = null;
    }
        
    // Do the call.
    $.ajax({
        url: restCallParams.url,
        method: restCallParams.method,
        data: restCallParams.entity,
        dataType: "json",
        crossDomain: true,
        headers: {
            "Authorization": "Token " + restCallParams.authorizationToken,
            "Accept-Lang": "fr, en"
        },
        success: function (data, textStatus, jqhxr) {
            // Invoke each handlers.
            var handlers = restCallParams.successHandlers;
            for (var iHandler = 0; iHandler < handlers.length; ++iHandler) {
                handlers[iHandler](this, data);
            }
        },
        error: function (jqhxr, textStatus, exception) {
            var errorObject = createErrorObject(jqhxr, textStatus, exception);
            var handlers = restCallParams.errorHandlers;
            for (var iHandler = 0; iHandler < handlers.length; ++iHandler) {
                handlers[iHandler](this, errorObject);
            }
        }
    });
}

/// <summary>
/// Enumeration of all supported http methods.
/// </summary>
var httpMethods = {
    get: function() {
        return "GET";
    },
    post: function() {
        return "POST";
    },
    put: function() {
        return "PUT";
    },
    "delete": function() {
        return "DELETE";
    }
};

RestCallBuilder.prototype.restCallParams = null;

/// <summary>
/// Rest call builder object that can be used to append handlers on success and error at different levels.
/// </summary>
function RestCallBuilder (restCallParams) {
    this.restCallParams = restCallParams;

    // Initialize handlers.
    this.restCallParams["successHandlers"] = [];
    this.restCallParams["errorHandlers"] = [];
};

/// <summary>
/// Adds a on success handler to the rest call.
/// </summary>
/// <param name="doneHandler">
/// A function to handle on success. The first parameter will be the rest client, the second, the data.
/// </param>
RestCallBuilder.prototype.whenDone = function(doneHandler) {
    if (!doneHandler || typeof (doneHandler) != "function") {
        throw "Done handler parameter must be a function type.";
    }
            
    this.restCallParams.successHandlers.push(doneHandler);
    return this;
}

/// <summary>
/// Adds a on error handler to the rest call.
/// </summary>
/// <param name="doneHandler">
/// A function to handle on error. The first parameter will be the rest client, the second, the exception.
/// </param>
RestCallBuilder.prototype.onError = function(errorHandler) {
    if (!errorHandler || typeof (errorHandler) != "function") {
        throw "Error handler parameter must be a function type.";
    }

    this.restCallParams.errorHandlers.push(errorHandler);
    return this;
};

/// <summary>
/// Does the rest call. 
/// The builder will be reset and should be considered disposed.
/// </summary>
RestCallBuilder.prototype.do = function() {
    restCall(this.restCallParams);
    this.restCallParams = null;
}


/// <summary>
/// Rest client of this enumeration store.
/// </summary>
EnumerationStore.restClient = null;

/// <summary>
/// Constructor for the EnumerationStore prototype.
/// Instances of this prototype will be able to access enumerations data of simplets through async rest calls.
/// </summary>
function EnumerationStore(restClient) {
    if (!restClient || !(restClient instanceof SimpletsRestClient)) {
        throw "Rest client is required to instantiate enumeration store.";
    }

    this.restClient = restClient;
}

EnumerationStore.prototype.getAllTypesContacts = function () {
    return this.restClient.get("enumeration/types-contacts");
};

EnumerationStore.prototype.getAllStatutsSuivie = function () {
    return this.restClient.get("enumeration/statuts-suivies");
};

EnumerationStore.prototype.getAllConcentrations = function () {
    return this.restClient.get("enumeration/concentrations");
};

EnumerationStore.prototype.getAllUnites = function () {
    return this.restClient.get("enumeration/unites");
};

/// <summary>
/// Rest client of this profil store.
/// </summary>
ProfilStore.restClient = null;

/// <summary>
/// Constructor for the ProfilStore prototype.
/// Instances of this prototype will be able to access profil data of simplets through async rest calls.
/// </summary>
function ProfilStore(restClient) {
    if (!restClient || !(restClient instanceof SimpletsRestClient)) {
        throw "Rest client is required to instantiate enumeration store.";
    }

    this.restClient = restClient;
}

ProfilStore.prototype.getProfil = function () {
    var user = this.restClient.authorizationToken.user;
    return this.restClient.get(user + "/profil");
}

ProfilStore.prototype.getAvailableContexts = function () {
    var user = this.restClient.authorizationToken.user;
    return this.restClient.get(user + "/profil/clubs");
}