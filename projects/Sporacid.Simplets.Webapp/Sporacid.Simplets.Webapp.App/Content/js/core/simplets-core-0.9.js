// Base url for the rest api.
var apiUrl = "http://localhost/services/api/v1/";

// Enumeration of all supported rest operations.
var operations = {
    "get": function () {
        return "GET";
    },
    "create": function () {
        return "POST";
    },
    "update": function () {
        return "PUT";
    },
    "delete": function () {
        return "DELETE";
    }
};

// Enumeration of all model view modes.
var viewmodes = {
    "view": function () {
        return "v";
    },
    "edition": function () {
        return "e";
    },
    "creation": function () {
        return "c";
    },
    "deletion": function () {
        return "d";
    }
};

// Utility function for rest api calls.
function restCall(uri, method, auth, data) {
    var request = {
        url: uri,
        type: method,
        cache: method === operations.get(),
        dataType: 'json',
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(data),
        beforeSend: function (xhr) {
            // Set the authorization header if the information exists.
            if (auth) {
                xhr.setRequestHeader("Authorization", auth);
            }
        },
        // Invokes the rest call request. To be called whenever callbacks have been set
        // and no race condition can happen.
        invoke: function () {
            // Invoke the request using jquery ajax.
            $.ajax(this);
        },
        // Adds a callback on request success.
        done: function (func) {
            // Get the old callback.
            var callback = this.success;

            // Recreate the real ajax callback on success with the new callback. 
            this.success = function (payload, textStatus, jqxhr) {
                if (callback) callback(payload, textStatus, jqxhr);
                func(payload, textStatus, jqxhr);
            };

            // Chain the request object.
            return this;
        },
        // Adds a callback on request failure.
        fail: function (func) {
            // Get the old callback.
            var callback = this.error;

            // Recreate the real ajax callback on error with the new callback. 
            this.error = function (jqhxr, textStatus, exception) {
                if (callback) callback(jqhxr, textStatus, exception);
                func(jqhxr, textStatus, exception);
            };

            // Chain the request object.
            return this;
        }
    };

    return request;
}

// Utility function to build the authorization header for kerberos authentication.
function buildKerberosAuthHeader(username, password) {
    return "Kerberos " + btoa(username + ":" + password);
}

// Utility function to build the authorization header for token authentication.
// If the token is in the cookie, this method can be called without parameter.
function buildTokenAuthHeader(token) {
    token = token ? token : authenticationToken.token;
    return "Token " + token;
}

// Utility function to throw a rest exception.
function throwRestException(jqhxr, textStatus, exception) {
    var errorObject = createErrorObject(jqhxr, textStatus, exception);
    throw errorObject;
}

// Utility function to create an error object from ajax' error parameters.
function createErrorObject(jqhxr, textStatus, exception) {
    var response = JSON.parse(jqhxr.responseText);
    return {
        httpStatus: jqhxr.status,
        httpStatusText: textStatus,
        response: response
    };
}

// Utility function to build a rest resource url. The method takes a variable number of arguments.
function buildUrl() {
    var url = "";
    var urlParts = arguments;

    // For each url part given in argument.
    for (var iUrlPart = 0; iUrlPart < urlParts.length; iUrlPart++) {
        var urlPart = urlParts[iUrlPart];

        // If url part starts with a slash, remove the slash.
        if (urlPart[0] === "/") {
            urlPart.substring(1);
        }

        // If url part does not end with a slash, add a slash.
        if (urlPart[urlPart.length - 1] !== "/" && iUrlPart < arguments.length - 1) {
            urlPart += "/";
        }

        // Concatenate sanitized url part to url.
        url += urlPart;
    }

    return url;
}

// Create a new method on array prototype to chain the as store method.
Array.prototype.asStore = function() {
    return asStore(this);
}

// Utility function to transform an array of objects to a data store.
function asStore(data) {
    var store = {};
    for (var iDatum = 0; iDatum < data.length; iDatum++) {
        var datum = data[iDatum];
        var datumId = datum.id;

        // Stored objects require an unique identifier named "id".
        if (!datumId) {
            continue;
        }

        // Store the object.
        store[datumId] = datum;
    }

    return store;
}

// Flag the jquery element as waiting for an async request.
jQuery.fn.waiting = function () {
    var zIndex = parseInt(this.css("z-index"));
    var $loadingIcon = $("<i></i>")
        .addClass("metro-icon metro-refresh-trans metro-icon-animate");
    var $loading = $("<div></div>")
        .addClass("loading")
        .css("z-index", zIndex ? zIndex + 1 : 1000)
        .append($loadingIcon);

    var position = this.css("position");
    if (position === "absolute") {
        // Handle css properties to hide an absolute dom element under a loading modal.
        $loading.css("top", this.css("top"))
            .css("left", this.css("left"));
    } else {
        // Handle css properties to hide any other dom element under a loading modal.
        if (position !== "relative") {
            // Parent needs to be relative.
            this.css("position", "relative");
        }

        $loading.css("top", 0)
            .css("left", 0)
            .height(this.height())
            .width(this.width());
    }
    
    // Prepend the loading screen.
    this.prepend($loading);

    // Adjust the icon position, now that the loading screen is rendered.
    $loadingIcon
        .css("top", (this.outerHeight() - $loadingIcon.height()) / 2)
        .css("left", (this.outerWidth() - $loadingIcon.width()) / 2);
    return this;
}

// Flag the jquery element as done with the async request and active again.
jQuery.fn.active = function () {
    this.find(".loading").remove();
    return this;
};

// Returns whether the jQuery element has any children matching the selector.
jQuery.fn.exists = function (selector) {
    return this.find(selector).length > 0;
};

// Returns whether the jQuery element has any element.
jQuery.fn.exists = function () {
    return this.length > 0;
};

// Get or set the id of the element.
jQuery.fn.id = function (id) {
    if (id == undefined) {
        return this.attr("id");
    } else {
        return this.attr("id", id);
    }
};

// Capture ENTER key in element and trigger a function (<div id="xxx" data-bind="enterkey: functionX">)
ko.bindingHandlers.enterkey = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var allBindings = allBindingsAccessor();

        $(element).on('keypress', 'input, textarea, select', function (e) {
            var keyCode = e.which || e.keyCode;
            if (keyCode !== 13) {
                return true;
            }

            var target = e.target;
            target.blur();

            allBindings.enterkey.call(viewModel, viewModel, target, element);

            return false;
        });
    }
};